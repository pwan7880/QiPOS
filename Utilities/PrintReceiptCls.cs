using System;
using System.Data; 
using System.Text; 

namespace QiPOS
{
    public sealed class PrintReceiptCls
    {
        private readonly string printerNameStr;
        private readonly string saleId;

        public PrintReceiptCls(string in_saleId, string in_printerNameStr)
        {
            saleId = in_saleId;
            printerNameStr = in_printerNameStr;
        }                                                                                                                                           

        private string CurrencyFormat(string inStr)
        {
            if (decimal.TryParse(inStr, out decimal value))
                return value.ToString("C");
            return "$0.00";
        }

        private CompanyData MyDetail()
        {
            return new ConfigurationReader().CompanyInfo();
        }

        public void PrintReceiptDirect()
        {
            bool eftpos = false;
            CompanyData companyData = MyDetail();
            var sb = new StringBuilder();

            // Header
            sb.Append("\x001B!\x0002\x001D!\0\x001B \x0001\x001B3D\x001BE\x0001 ");
            sb.AppendLine(companyData.CompanyName);
            sb.AppendLine("\x001BE\0 " + companyData.AddressLine1);
            sb.AppendLine(" " + companyData.AddressCity);
            sb.AppendLine(" " + companyData.CompanyABN);
            sb.AppendLine(" " + companyData.Telephone);
            sb.AppendLine(" --------------------------------------");
            sb.AppendLine("\x001BE\x0001\x001Ba\x0001TAX INVOICE");
            sb.AppendLine("\x001BE\0\x001Ba\0 ITEM                     QTY    AMOUNT");
            sb.AppendLine(" --------------------------------------");

            DataTable dataTable = SaleDetails(saleId);
            DataTable salesData = SalesInfo(saleId);

            if (salesData.Rows.Count > 0 && salesData.Rows[0]["cashsale"].ToString() == "0")
                eftpos = true;

            int count = dataTable.Rows.Count;
            if (count < 5)
                throw new InvalidOperationException("Insufficient sale detail rows.");

            int index1 = count - 4;
            if (dataTable.Rows[count - 2][0].ToString() == "Change")
                --index1;

            const int itemFieldLength = 27;
            for (int i = 0; i < index1; ++i)
            {
                string item = " " + dataTable.Rows[i]["Item"].ToString().Trim();
                if (item.Contains("paid to"))
                {
                    int idx = item.IndexOf("paid to");
                    string firstStr = item.Substring(0, idx - 1);
                    string second = item.Substring(idx);

                    firstStr = firstStr.Length <= itemFieldLength ? firstStr.PadRight(itemFieldLength) : firstStr.Substring(0, itemFieldLength);
                    string amount = CurrencyFormat(dataTable.Rows[i]["Amount"].ToString()).PadRight(6);

                    sb.AppendLine(firstStr + amount);
                    sb.AppendLine(" " + second);
                }
                else
                {
                    string itemStr = item.Length <= itemFieldLength ? item.PadRight(itemFieldLength) : item.Substring(0, itemFieldLength);
                    string qtyStr = dataTable.Rows[i]["Qty"].ToString().Trim();
                    string amtStr = CurrencyFormat(dataTable.Rows[i]["Amount"].ToString()).PadLeft(11);

                    sb.AppendLine(itemStr + qtyStr + amtStr);
                }
            }

            sb.AppendLine(" --------------------------------------");
            string total = CurrencyFormat(dataTable.Rows[index1]["Amount"].ToString()).PadLeft(33);
            sb.AppendLine(" TOTAL" + total);

            string gst = CurrencyFormat(dataTable.Rows[index1 + 1]["Amount"].ToString()).PadLeft(20);
            sb.AppendLine(" TOTAL Includes GST" + gst);
            sb.AppendLine(" * GST FREE ITEMS");

            if (dataTable.Rows[index1 + 2]["Amount"].ToString() != "0.00" && !eftpos)
            {
                string received = CurrencyFormat(dataTable.Rows[index1 + 2]["Amount"].ToString()).PadLeft(30);
                sb.AppendLine(" Received" + received);
            }
            if (eftpos)
            {
                string eftposStr = CurrencyFormat(dataTable.Rows[index1]["Amount"].ToString()).PadLeft(30);
                sb.AppendLine(" EFTPOS  " + eftposStr);
            }
            if (index1 == count - 5 && !eftpos)
            {
                string change = CurrencyFormat(dataTable.Rows[index1 + 3]["Amount"].ToString()).PadLeft(32);
                sb.AppendLine(" Change" + change);
            }

            sb.AppendLine(" --------------------------------------");
            sb.AppendLine($" Inv No. {saleId}");
            sb.AppendLine(" " + dataTable.Rows[count - 1]["Item"].ToString().Trim() + "\x001Bd\x0006\x001DV\x0001");

            RawPrinterHelper.SendStringToPrinter(printerNameStr, sb.ToString());
        }

        private DataTable SalesInfo(string salesID)
        {
            Connect connect = new Connect();
            string s = "SELECT * FROM pos_sale WHERE sale_trans_id =" + salesID.ToString();
            connect.QueryTable(s);
            DataTable dataTable1 = connect.aTable;
            return dataTable1;
        }

        private DataTable SaleDetails(string salesID)
        {
            Connect connect = new Connect();
            string s = "SELECT CASE WHEN GST_collect=0  then descr + '*' ELSE descr END AS Item,  number_items AS Qty, sales AS Amount  FROM pos_sale_detail WHERE sale_trans_id=" + salesID.ToString();
            connect.QueryTable(s);
            System.Data.DataTable dataTable1 = connect.aTable;
            s = "SELECT sale_date, sale_time, sales_amount, GST_collect, received,change FROM pos_sale WHERE sale_trans_id=" + salesID.ToString();
            connect.QueryTable(s);
            System.Data.DataRow dataRow = dataTable1.NewRow();
            dataRow["Item"] = "TOTAL";
            dataRow["Amount"] = connect.aTable.Rows[0]["sales_amount"];
            dataTable1.Rows.Add(dataRow);
            dataRow = dataTable1.NewRow();
            dataRow["Item"] = "Total Includes GST";
            dataRow["Amount"] = connect.aTable.Rows[0]["GST_collect"];
            dataTable1.Rows.Add(dataRow);
            dataRow = dataTable1.NewRow();
            dataRow["Item"] = "Received";
            dataRow["Amount"] = connect.aTable.Rows[0]["received"];
            dataTable1.Rows.Add(dataRow);
            bool flag = (decimal)connect.aTable.Rows[0]["change"] == (0M);
            if (!flag)
            {
                dataRow = dataTable1.NewRow();
                dataRow["Item"] = "Change";
                dataRow["Amount"] = connect.aTable.Rows[0]["change"];
                dataTable1.Rows.Add(dataRow);
            }
            dataRow = dataTable1.NewRow();
            System.DateTime dateTime1 = (System.DateTime)connect.aTable.Rows[0]["sale_date"];
            System.DateTime dateTime2 = (System.DateTime)connect.aTable.Rows[0]["sale_time"];
            dataRow["Item"] = "Date:" + dateTime1.ToString("dd MMM yyyy") + " " + dateTime2.ToString("T");
            dataTable1.Rows.Add(dataRow);
            return dataTable1;
        }
    }
}

