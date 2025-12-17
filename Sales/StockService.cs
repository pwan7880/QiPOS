using System;
using System.Data;
using System.Data.SqlClient;

namespace QiPOS
{
    public sealed class StockService
    {
        private readonly Connect _conn;

        public StockService(Connect conn)
        {
            _conn = conn;
        }

        /// <summary>
        /// Get the stock details for a given stock ID.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public DataRow GetStockByBarcode(string barcode)
        {
            try
            {
                _conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("GetStockByBarcode", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Barcode", barcode);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable stockTable = new DataTable();
                        adapter.Fill(stockTable);
                        return stockTable.Rows.Count > 0 ? stockTable.Rows[0] : null;
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, $"Error executing GetStockByBarcode for barcode {barcode}");
                throw new DatabaseUnavailableException($"Failed to retrieve stock for barcode {barcode}: {ex.Message}", ex);
            }
            finally
            {
                _conn.Close();
            }
        }
        /// <summary>
        /// Updates the stock quantity for a given stock ID.
        /// </summary>
        /// <param name="stockId">The ID of the stock to update.</param>
        /// <param name="deltaQty">The change in quantity (positive or negative).</param>
        public void UpdateStockQuantity(int stockId, int deltaQty)
        {
            try
            {
                _conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("UpdateStockQuantity", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StockId", stockId);
                    cmd.Parameters.AddWithValue("@DeltaQty", deltaQty);
                    cmd.Parameters.AddWithValue("@LastSoldDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, $"Error executing UpdateStockQuantity for stock_id {stockId} with deltaQty {deltaQty}");
                throw new DatabaseUnavailableException($"Failed to update stock quantity for stock_id {stockId}: {ex.Message}", ex);
            }
            finally
            {
                _conn.Close();
            }
        }
        public bool ItemExists(string barcode)
        {
            return GetStockByBarcode(barcode) != null;
        }
    }
}