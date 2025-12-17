using System;

namespace QiPOS
{
    internal class MagRecord
    {
        private string row_title;
        private Decimal row_price;
        private string col_1_supply;
        private string col_1_return;
        private string col_2_supply;
        private string col_2_return;
        private string col_3_supply;
        private string col_3_return;
        private string col_4_supply;
        private string col_4_return;
        private string col_5_supply;
        private string col_5_return;
        private string descr;
        private Decimal rrp;
        private DateTime enter_date;
        private Decimal c_rate;
        private Decimal commision;
        private int supply;
        private int rtn;

        public string Row_title
        {
            get
            {
                return this.row_title;
            }
        }

        public Decimal Row_price
        {
            get
            {
                return this.row_price;
            }
        }

        public string Col_1_supply
        {
            get
            {
                return this.col_1_supply;
            }
        }

        public string Col_1_return
        {
            get
            {
                return this.col_1_return;
            }
        }

        public string Col_2_supply
        {
            get
            {
                return this.col_2_supply;
            }
        }

        public string Col_2_return
        {
            get
            {
                return this.col_2_return;
            }
        }

        public string Col_3_supply
        {
            get
            {
                return this.col_3_supply;
            }
        }

        public string Col_3_return
        {
            get
            {
                return this.col_3_return;
            }
        }

        public string Col_4_supply
        {
            get
            {
                return this.col_4_supply;
            }
        }

        public string Col_4_return
        {
            get
            {
                return this.col_4_return;
            }
        }

        public string Col_5_supply
        {
            get
            {
                return this.col_5_supply;
            }
        }

        public string Col_5_return
        {
            get
            {
                return this.col_5_return;
            }
        }

        public string Descr
        {
            get
            {
                return this.descr;
            }
        }

        public Decimal Rrp
        {
            get
            {
                return this.rrp;
            }
        }

        public DateTime Enter_date
        {
            get
            {
                return this.enter_date;
            }
        }

        public Decimal C_rate
        {
            get
            {
                return this.c_rate;
            }
        }

        public Decimal Commision
        {
            get
            {
                return this.commision;
            }
        }

        public int Supply
        {
            get
            {
                return this.supply;
            }
        }

        public int Rtn
        {
            get
            {
                return this.rtn;
            }
        }

        public MagRecord(string in_row_title, Decimal in_row_price, string in_col_1_supply, string in_col_1_return, string in_col_2_supply, string in_col_2_return, string in_col_3_supply, string in_col_3_return, string in_col_4_supply, string in_col_4_return, string in_col_5_supply, string in_col_5_return, string in_descr, Decimal in_rrp, DateTime in_enter_date, Decimal in_c_rate, Decimal in_commision, int in_supply, int in_rtn)
        {
            this.row_title = in_row_title;
            this.row_price = in_row_price;
            this.col_1_supply = in_col_1_supply;
            this.col_1_return = in_col_1_return;
            this.col_2_supply = in_col_2_supply;
            this.col_2_return = in_col_2_return;
            this.col_3_supply = in_col_3_supply;
            this.col_3_return = in_col_3_return;
            this.col_4_supply = in_col_4_supply;
            this.col_4_return = in_col_4_return;
            this.col_5_supply = in_col_5_supply;
            this.col_5_return = in_col_5_return;
            this.descr = in_descr;
            this.rrp = in_rrp;
            this.enter_date = in_enter_date;
            this.c_rate = in_c_rate;
            this.commision = in_commision;
            this.supply = in_supply;
            this.rtn = in_rtn;
        }
    }
}

