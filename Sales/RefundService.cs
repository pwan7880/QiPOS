using System;
using System.Data;

namespace QiPOS
{
    public sealed class RefundService
    {
        // This class handles the refund process for sales transactions.
        // Currently, the shop doesn't do refunds, so this is a placeholder.
        // Actual refund logic will be implemented later.
        // In the shop refunds are done manually by the cashier
        private readonly Connect _conn;

        public RefundService(Connect conn)
        {
            _conn = conn;
        }
        /// <summary>
        /// Using this method, we can load the sale details for a specific sale ID.
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public DataTable LoadRefundableSale(string saleId)
        {
            string query = $@"
                SELECT * FROM pos_sale_detail 
                WHERE sale_trans_id = {saleId};";

            _conn.QueryTable(query);
            return _conn.aTable.Copy();
        }

        /// <summary>
        /// When a sale is refunded, we need to process the refund.
        /// </summary>
        /// <param name="originalItems"></param>
        /// <param name="refundAmount"></param>
        /// <param name="refundChange"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int ProcessRefund(DataTable originalItems, decimal refundAmount, out decimal refundChange)
        {
            if (originalItems == null || originalItems.Rows.Count == 0)
                throw new ArgumentException("No sale data found to refund.");

            refundChange = -refundAmount;

            decimal total = 0m, gstCollected = 0m, gstPaid = 0m;
            foreach (DataRow row in originalItems.Rows)
            {
                total += CurrencyUtil.SafeToDecimal(row["sales"]);
                gstCollected += CurrencyUtil.SafeToDecimal(row["GST_collect"]);
                gstPaid += CurrencyUtil.SafeToDecimal(row["GST_paid"]);
            }

            total = -total;
            gstCollected = -gstCollected;
            gstPaid = -gstPaid;

            string nowDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string insertQuery = $@"
                INSERT INTO pos_sale (user_id, termi_id, sale_date, sale_time, sales_amount, GST_collect, GST_paid, received, change, cashsale)
                VALUES (0, 0, '{nowDate}', GETDATE(), {total}, {gstCollected}, {gstPaid}, 0, {refundChange}, 1);";

            _conn.NoReturnQuery(insertQuery);
            int refundId = _conn.GetInt32("SELECT MAX(sale_trans_id) FROM pos_sale WHERE user_id = 0 AND termi_id = 0");

            SaveRefundDetails(refundId, originalItems);

            return refundId;
        }

        /// <summary>
        /// once refund is processed, we save the details of the refund to the database.
        /// </summary>
        /// <param name="refundId"></param>
        /// <param name="originalItems"></param>
        private void SaveRefundDetails(int refundId, DataTable originalItems)
        {
            foreach (DataRow row in originalItems.Rows)
                row["sale_trans_id"] = refundId;

            _conn.QueryTable("SELECT * FROM pos_sale_detail WHERE 1=0"); // schema only
            _conn.UpdateTable(originalItems);
        }
    }
}
