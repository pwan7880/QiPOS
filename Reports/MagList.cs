using System;
using System.Collections.Generic;
using System.Data;

namespace QiPOS
{
    internal class MagList
    {
        private List<MagRecord> records;

        public MagList(string strQuery, string c_rateStr, string connectionStr)
        {
            this.SetMagList(strQuery, c_rateStr, connectionStr);
        }

        public void SetMagList(string invNo, string c_rateStr, string connectionStr)
        {
            this.records = new List<MagRecord>();
            Connect connect = new Connect();
            Decimal num = Convert.ToDecimal(c_rateStr);
            DateTime[] dateTimeArray = new DateTime[5];
            string queryStr1 = "SELECT * FROM sub_inv_paper_details WHERE sub_inv_id=" + invNo;
            connect.QueryTable(queryStr1);
            DataTable dataTable1 = connect.aTable;
            DateTime in_enter_date = new DateTime(1900, 1, 1);
            if (dataTable1.Rows.Count > 0)
            {
                for (int index = 0; index < 5; ++index)
                    dateTimeArray[index] = (DateTime)dataTable1.Rows[0][index * 4 + 6];
                this.records.Add(new MagRecord("", new Decimal(-11), dateTimeArray[0].ToString("ddd"), "", dateTimeArray[1].ToString("ddd"), "", dateTimeArray[2].ToString("ddd"), "", dateTimeArray[3].ToString("ddd"), "", dateTimeArray[4].ToString("ddd"), "", "", new Decimal(0), in_enter_date, new Decimal(0), new Decimal(0), 0, 0));
                this.records.Add(new MagRecord("", new Decimal(-2), dateTimeArray[0].ToString("dd/MM"), "RT", dateTimeArray[1].ToString("dd/MM"), "RT", dateTimeArray[2].ToString("dd/MM"), "RT", dateTimeArray[3].ToString("dd/MM"), "RT", dateTimeArray[4].ToString("dd/MM"), "RT", "", new Decimal(0), in_enter_date, new Decimal(0), new Decimal(0), 0, 0));
            }
            for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
            {
                bool flag = false;
                for (int index2 = 0; index2 < 5; ++index2)
                {
                    if (dateTimeArray[index2].CompareTo((DateTime)dataTable1.Rows[index1][index2 * 4 + 6]) != 0)
                        flag = true;
                    dateTimeArray[index2] = (DateTime)dataTable1.Rows[index1][index2 * 4 + 6];
                }
                if (flag)
                {
                    this.records.Add(new MagRecord("", new Decimal(-1), dateTimeArray[0].ToString("ddd"), "", dateTimeArray[1].ToString("ddd"), "", dateTimeArray[2].ToString("ddd"), "", dateTimeArray[3].ToString("ddd"), "", dateTimeArray[4].ToString("ddd"), "", "", new Decimal(0), in_enter_date, new Decimal(0), new Decimal(0), 0, 0));
                    this.records.Add(new MagRecord("", new Decimal(-2), dateTimeArray[0].ToString("dd/MM"), "RT", dateTimeArray[1].ToString("dd/MM"), "RT", dateTimeArray[2].ToString("dd/MM"), "RT", dateTimeArray[3].ToString("dd/MM"), "RT", dateTimeArray[4].ToString("dd/MM"), "RT", "", new Decimal(0), in_enter_date, new Decimal(0), new Decimal(0), 0, 0));
                }
                int in_supply = (int)dataTable1.Rows[index1]["col_1_supply"] - (int)dataTable1.Rows[index1]["col_1_return"] + (int)dataTable1.Rows[index1]["col_2_supply"] - (int)dataTable1.Rows[index1]["col_2_return"] + (int)dataTable1.Rows[index1]["col_3_supply"] - (int)dataTable1.Rows[index1]["col_3_return"] + (int)dataTable1.Rows[index1]["col_4_supply"] - (int)dataTable1.Rows[index1]["col_4_return"] + (int)dataTable1.Rows[index1]["col_5_supply"] - (int)dataTable1.Rows[index1]["col_5_return"];
                this.records.Add(new MagRecord(dataTable1.Rows[index1]["row_title"].ToString(), (Decimal)dataTable1.Rows[index1]["row_price"], dataTable1.Rows[index1]["col_1_supply"].ToString(), dataTable1.Rows[index1]["col_1_return"].ToString(), dataTable1.Rows[index1]["col_2_supply"].ToString(), dataTable1.Rows[index1]["col_2_return"].ToString(), dataTable1.Rows[index1]["col_3_supply"].ToString(), dataTable1.Rows[index1]["col_3_return"].ToString(), dataTable1.Rows[index1]["col_4_supply"].ToString(), dataTable1.Rows[index1]["col_4_return"].ToString(), dataTable1.Rows[index1]["col_5_supply"].ToString(), dataTable1.Rows[index1]["col_5_return"].ToString(), "", new Decimal(0), in_enter_date, (Decimal)(index1 + 1), new Decimal(0), in_supply, 0));
            }
            string queryStr2 = "SELECT * FROM sub_inv_mag WHERE sub_inv_id=" + invNo + " ORDER BY enter_date, descr";
            connect.QueryTable(queryStr2);
            DataTable dataTable2 = connect.aTable;
            DateTime dateTime = DateTime.Now;
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
                MagRecord magRecord;
                if (dateTime.Equals((DateTime)dataTable2.Rows[index]["enter_date"]) && num == (Decimal)dataTable2.Rows[index]["c_rate"])
                {
                    magRecord = new MagRecord("r", (Decimal)index, "", "", "", "", "", "", "", "", "", "", dataTable2.Rows[index]["descr"].ToString(), (Decimal)dataTable2.Rows[index]["rrp"], (DateTime)dataTable2.Rows[index]["enter_date"], (Decimal)dataTable2.Rows[index]["c_rate"], (Decimal)dataTable2.Rows[index]["commision"], (int)dataTable2.Rows[index]["supply"], (int)dataTable2.Rows[index]["rtn"]);
                }
                else
                {
                    dateTime = (DateTime)dataTable2.Rows[index]["enter_date"];
                    magRecord = new MagRecord("", (Decimal)index, "", "", "", "", "", "", "", "", "", "", dataTable2.Rows[index]["descr"].ToString(), (Decimal)dataTable2.Rows[index]["rrp"], (DateTime)dataTable2.Rows[index]["enter_date"], (Decimal)dataTable2.Rows[index]["c_rate"], (Decimal)dataTable2.Rows[index]["commision"], (int)dataTable2.Rows[index]["supply"], (int)dataTable2.Rows[index]["rtn"]);
                }
                this.records.Add(magRecord);
            }
            connect.Close();
        }

        public List<MagRecord> GetMagList()
        {
            return this.records;
        }
    }
}

