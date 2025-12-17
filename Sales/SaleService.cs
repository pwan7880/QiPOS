using System;
using System.Data;
using System.Data.SqlClient;

namespace QiPOS
{
    public sealed class SaleService
    {
        private readonly Connect _conn;

        public SaleService(Connect conn)
        {
            _conn = conn;
        }

        public int FinalizeSale(DataTable saleTable, bool isCash, decimal receivedAmount, out decimal change)
        {
            if (saleTable == null || saleTable.Rows.Count == 0)
                throw new ArgumentException("No sale data to finalize.");

            decimal total = 0m, gstCollected = 0m, gstPaid = 0m;

            foreach (DataRow row in saleTable.Rows)
            {
                total += CurrencyUtil.SafeToDecimal(row["sales"]);
                gstCollected += CurrencyUtil.SafeToDecimal(row["GST_collect"]);
                gstPaid += CurrencyUtil.SafeToDecimal(row["GST_paid"]);
            }
            if (receivedAmount == 0)
                receivedAmount = total;
            change = receivedAmount - total;
            int cashSaleFlag = isCash ? 1 : 0;
            if (total < 0) cashSaleFlag = 1; // Refunds treated as cash

            int saleId = InsertSaleHeader(total, gstCollected, gstPaid, receivedAmount, change, cashSaleFlag);
            SaveSaleDetails(saleTable, saleId);
            UpdateStockLevels(saleTable);

            return saleId;
        }

        /// <summary>
        /// Inserts a new sale header into the database and returns the generated SaleTransId.
        /// </summary>
        /// <param name="total"></param>
        /// <param name="gstCollected"></param>
        /// <param name="gstPaid"></param>
        /// <param name="received"></param>
        /// <param name="change"></param>
        /// <param name="isCash"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="DatabaseUnavailableException"></exception>
        private int InsertSaleHeader(decimal total, decimal gstCollected, decimal gstPaid, decimal received, decimal change, int isCash)
        {
            try
            {
                _conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("InsertSaleHeader", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@TermiId", 0);
                    cmd.Parameters.AddWithValue("@SaleDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@SaleTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SalesAmount", total);
                    cmd.Parameters.AddWithValue("@GSTCollect", gstCollected);
                    cmd.Parameters.AddWithValue("@GSTPaid", gstPaid);
                    cmd.Parameters.AddWithValue("@Received", received);
                    cmd.Parameters.AddWithValue("@Change", change);
                    cmd.Parameters.AddWithValue("@CashSale", isCash);

                    // Define output parameter for SaleTransId
                    var saleTransIdParam = new SqlParameter("@SaleTransId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(saleTransIdParam);

                    cmd.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    return saleTransIdParam.Value != DBNull.Value ? Convert.ToInt32(saleTransIdParam.Value) : throw new InvalidOperationException("Failed to retrieve sale_trans_id.");
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error executing InsertSaleHeader");
                throw new DatabaseUnavailableException($"Failed to insert sale header: {ex.Message}", ex);
            }
            finally
            {
                _conn.Close();
            }
        }

        private void SaveSaleDetails(DataTable saleTable, int saleId)
        {
            try
            {
                _conn.ConnectBD(); // Ensure connection is open

                // Update sale_trans_id in the DataTable
                foreach (DataRow row in saleTable.Rows)
                    row["sale_trans_id"] = saleId;

                // Apply changes to pos_sale_cache
                DataTable changes = saleTable.GetChanges();
                if (changes != null)
                {
                    _conn.QueryTable("SELECT * FROM pos_sale_cache WHERE user_id = -1");
                    _conn.UpdateTable(changes);
                }

                // Insert sale details from cache to pos_sale_detail
                using (var cmd = new SqlCommand("InsertSaleDetailsFromCache", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleTransId", saleId);
                    cmd.ExecuteNonQuery();
                }

                // Delete pos_sale_cache records for user_id = 0
                using (var cmd = new SqlCommand("DeleteSaleCacheByUserId", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, $"Error executing SaveSaleDetails for sale_trans_id {saleId}");
                throw new DatabaseUnavailableException($"Failed to save sale details: {ex.Message}", ex);
            }
            finally
            {
                _conn.Close();
            }
        }

        /// <summary>
        /// Update the stock levels based on the sale details.
        /// </summary>
        /// <param name="saleTable"></param>
        /// <exception cref="DatabaseUnavailableException"></exception>
        private void UpdateStockLevels(DataTable saleTable)
        {
            try
            {
                _conn.ConnectBD(); // Ensure connection is open
                foreach (DataRow row in saleTable.Rows)
                {
                    int stockId = Convert.ToInt32(row["stock_id"]);
                    if (stockId == 0) continue;

                    int qty = Convert.ToInt32(row["number_items"]);
                    using (var cmd = new SqlCommand("UpdateStockQuantity", Connect.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StockId", stockId);
                        cmd.Parameters.AddWithValue("@DeltaQty", -qty); // Subtract quantity
                        cmd.Parameters.AddWithValue("@LastSoldDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error executing UpdateStockQuantity in UpdateStockLevels");
                throw new DatabaseUnavailableException($"Failed to update stock levels: {ex.Message}", ex);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
