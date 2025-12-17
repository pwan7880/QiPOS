using System;
using System.Data;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmDefineShortcut : Form
    {
        #region defines
        private readonly string whichKey;
        private readonly string whichMenu;
        private readonly string whichButton;
        private readonly int multi_select;
        private string queryStr;
        private Connect connDB;
        private Connect connWeekly;
        private int stockId;
        private Label lbl1;
        private Label lblKey;
        private TabPage tpWeekly;
        private DataGridView dgDefineList;
        private TabPage tpStock;
        private Label lblItem;
        private TabPage tpCat;
        private DataGridView dgCat;
        private TabControl tbcdefine;
        private TextBox TxtStockId;
        private TextBox TxtAcc_number;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton BtnNext;
        private CustomButton customButton3;
        private SearchItem currentItem;

        #endregion



        public FrmDefineShortcut(string in_whichKey)
        {
            this.InitializeComponent();
            this.whichKey = in_whichKey;
            this.whichMenu = "";
            this.whichButton = "";
            this.multi_select = 0;
        }

        public FrmDefineShortcut(string in_whichMenu, string in_whichButton)
        {
            this.InitializeComponent();
            this.whichKey = "";
            this.whichMenu = in_whichMenu;
            this.whichButton = in_whichButton;
            this.multi_select = 0;
        }

        public FrmDefineShortcut(string in_whichMenu, string in_whichButton, int in_multi_select)
        {
            this.InitializeComponent();
            this.whichKey = "";
            this.whichMenu = in_whichMenu;
            this.whichButton = in_whichButton;
            this.multi_select = in_multi_select;
        }

        private void FrmDefineShortcut_Load(object sender, EventArgs e)
        {
            this.Initiate();
        }

        private void Initiate()
        {
            string str;
            if (this.whichKey == "")
                str = "menu_Btn='" + this.whichMenu + "' AND short_Btn='" + this.whichButton + "'";
            else
                str = "key_value1='" + this.whichKey + "'";
            this.lblKey.Text = this.whichKey;
            this.connDB = new Connect();
            this.connWeekly = new Connect();
            this.queryStr = "SELECT acc_name, acc_number FROM account_list WHERE acc_type_id =4 ORDER BY acc_name";
            this.connDB.QueryTable(this.queryStr);
            this.dgCat.DataSource = this.connDB.aTable;
            this.dgCat.Columns[1].Visible = false;
            this.queryStr = "SELECT descr AS Description, RRP AS Price, dayofweek, pos_look_up.stock_id, pos_look_up.acc_number FROM pos_look_up LEFT JOIN pos_stock ON ( pos_look_up.stock_id = pos_stock.stock_id ) WHERE " + str + " ORDER BY dayofweek";
           
            // Only query main data from pos_look_up + pos_stock
            this.connWeekly.QueryTable(this.queryStr);

            // Manually create the second table: the old "pos_dayofweek"
            DataTable weekTable = WeekHelper.GetDayOfWeekTable();

            // Manually add it to the same dataset (to keep same structure)
            this.connWeekly.aDataSet.Tables.Add(weekTable);
            weekTable.TableName = "returntable2";

            // Recreate the relation just like before
            this.connWeekly.aDataSet.Relations.Add(
                weekTable.Columns["dayofweek"],
                this.connWeekly.aDataSet.Tables["returntable1"].Columns["dayofweek"]);             

            this.dgDefineList.DataSource = this.connWeekly.aDataSet.Tables["returntable1"];

            this.dgDefineList.Columns.Remove("dayofweek");
            DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Weeks",
                DataSource = weekTable,
                DisplayMember = "week_short",
                ValueMember = "dayofweek",
                DataPropertyName = "dayofweek"
            };
            this.dgDefineList.Columns.Insert(2, (DataGridViewColumn)viewComboBoxColumn);
            this.dgDefineList.Columns["stock_id"].Visible = false;
            this.dgDefineList.Columns["acc_number"].Visible = false;
            this.dgDefineList.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgDefineList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgDefineList.Columns["Price"].DefaultCellStyle.Format = "C";
            this.queryStr = "SELECT * FROM pos_look_up WHERE " + str;
            this.connDB.QueryTable(this.queryStr);
            DataTable dataTable = this.connDB.aTable;
            if (dataTable.Rows.Count <= 0)
                return;
            this.stockId = (int)dataTable.Rows[0]["stock_id"];
            int num1 = (int)dataTable.Rows[0]["dayofweek"];
            if (this.stockId == 0 && num1 == 0)
            {
                for (int index = 0; index < this.dgCat.Rows.Count; ++index)
                {
                    if (dataTable.Rows[0]["acc_number"].ToString() == this.dgCat[1, index].Value.ToString())
                        this.dgCat.Rows[index].Selected = true;
                }
            }
            else if (this.stockId != 0 && num1 == 0)
            {
                this.tbcdefine.SelectedTab = this.tpStock;
                this.queryStr = "SELECT * FROM pos_stock WHERE stock_id=" + this.stockId;
                this.connDB.QueryTable(this.queryStr);
                this.lblItem.Text = "   " + this.connDB.aTable.Rows[0]["descr"].ToString() + ",     " + string.Format("{0:C}", this.connDB.aTable.Rows[0]["RRP"]) + "    ";
                this.TxtStockId.Text = this.connDB.aTable.Rows[0]["stock_id"].ToString();
                this.TxtAcc_number.Text = this.connDB.aTable.Rows[0]["acc_number"].ToString();
            }
            else if (this.stockId != 0 && num1 != 0)
                this.tbcdefine.SelectedTab = this.tpWeekly;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            FrmSearch frmSearch = new FrmSearch
            {
                currentItem = this.currentItem,
                funIdentifier = "DefineShortCut"
            };
            this.AddOwnedForm(frmSearch);
            if (frmSearch.ShowDialog(this) != DialogResult.Yes)
                return;

            this.currentItem = frmSearch.currentItem;
            this.stockId = Convert.ToInt32(this.Tag.ToString());
            this.queryStr = "SELECT * FROM pos_stock WHERE stock_id=" + this.stockId;
            this.connDB.QueryTable(this.queryStr);
            DataRow firstRow = this.connDB.aTable.Rows[0];
            string newspaperName = firstRow["descr"].ToString();
            int index = newspaperName.LastIndexOf(":");
            newspaperName = newspaperName.Substring(0, index);
            this.queryStr = "SELECT * FROM pos_stock WHERE descr like '%" + newspaperName + "%'";
            this.connDB.QueryTable(this.queryStr);

            if ((this.tbcdefine.SelectedTab.Name).ToString() == "tpWeekly")
            {
                foreach (DataRow dataRow in connDB.aTable.Rows)
                {
                    DataRow row = this.connWeekly.aTable.NewRow();
                    row["Description"] = dataRow["descr"];
                    row["Price"] = dataRow["RRP"];
                    row["stock_id"] = dataRow["stock_id"];
                    row["acc_number"] = dataRow["acc_number"];
                    this.connWeekly.aDataSet.Tables["returntable1"].Rows.Add(row);
                }
                this.dgDefineList.Refresh();
            }
            else if ((this.tbcdefine.SelectedTab.Name).ToString() == "tpStock")
            {
                this.lblItem.Text = "   " + firstRow["descr"].ToString() + ",     " + string.Format("{0:C}", firstRow["RRP"]) + "    ";
                this.TxtStockId.Text = this.stockId.ToString();
                this.TxtAcc_number.Text = firstRow["acc_number"].ToString();
            }
        }

        private void TpStock_Enter(object sender, EventArgs e)
        {
            this.BtnNext.Visible = true;
        }

        private void TpCat_Enter(object sender, EventArgs e)
        {
            this.BtnNext.Visible = false;
        }

        private void TpWeekly_Enter(object sender, EventArgs e)
        {
            this.BtnNext.Visible = true;
        }

        private void BtnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            string str;
            if (this.whichKey == "")
                str = "menu_Btn='" + this.whichMenu + "' AND short_Btn='" + this.whichButton + "'";
            else
                str = "key_value='" + this.whichKey + "' OR key_value1='" + this.whichKey + "'";
            this.queryStr = "DELETE FROM pos_look_up WHERE " + str;
            this.connDB.NoReturnQuery(this.queryStr);
            if ((this.tbcdefine.SelectedTab.Name).ToString() == "tpCat")
            {
                if (this.whichKey == "")
                    this.queryStr = "INSERT  pos_look_up  (acc_number , stock_id, key_code, key_code1, key_value, key_value1, menu_Btn, short_Btn, dayofweek, multi_select, commnets)  VALUES (" + this.dgCat.SelectedRows[0].Cells["acc_number"].Value.ToString() + ", 0, 0, 0, '', '', '" + this.whichMenu + "', '" + this.whichButton + "', 0," + this.multi_select + ", '" + this.dgCat.SelectedRows[0].Cells["acc_name"].Value.ToString() + "')";
                else
                    this.queryStr = "INSERT  pos_look_up  (acc_number , stock_id, key_code, key_code1, key_value, key_value1, dayofweek, multi_select, commnets)  VALUES (" + this.dgCat.SelectedRows[0].Cells["acc_number"].Value.ToString() + ", 0, 0, 0, '" + this.whichKey.ToLower() + "','" + this.whichKey.ToLower() + "', 0, " + this.multi_select + ", '" + this.dgCat.SelectedRows[0].Cells["acc_name"].Value.ToString() + "')";
                this.connDB.NoReturnQuery(this.queryStr);
            }
            else if ((this.tbcdefine.SelectedTab.Name).ToString() == "tpStock" && this.lblItem.Text != "New Stock Item")
            {
                this.queryStr = this.lblItem.Text;
                this.queryStr = this.connDB.AddBackslash(this.queryStr);
                if (this.whichKey == "")
                    this.queryStr = "INSERT INTO pos_look_up (acc_number, stock_id, key_code, key_code1, key_value, key_value1, menu_Btn, short_Btn, dayofweek, commnets, multi_select ) VALUES ( " + this.TxtAcc_number.Text + ", " + this.TxtStockId.Text + ", 0, 0, '', '', '" + this.whichMenu + "', '" + this.whichButton + "', 0, '" + this.queryStr + "', " + this.multi_select + ")";
                else
                    this.queryStr = "INSERT INTO pos_look_up (acc_number, stock_id, key_code, key_code1, key_value, key_value1, dayofweek, commnets, multi_select) VALUES (" + this.TxtAcc_number.Text + ", " + this.TxtStockId.Text + ", 0, 0, '" + this.whichKey.ToLower() + "', '" + this.whichKey + "', 0, '" + this.queryStr + "', " + this.multi_select + ")";
                this.connDB.NoReturnQuery(this.queryStr);
            }

            else if ((this.tbcdefine.SelectedTab.Name).ToString() == "tpWeekly")
            {
                for (int index = 0; index < this.connWeekly.aDataSet.Tables["returntable1"].Rows.Count; ++index)
                {
                    DataRow dataRow = this.connWeekly.aDataSet.Tables["returntable1"].Rows[index];
                    if (dataRow.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    this.queryStr = dataRow["Description"].ToString();
                    int dayIndex = 0;
                    if (queryStr.ToUpper().Contains("MONDAY"))
                    {
                        dayIndex = 1;
                    }
                    else if (queryStr.ToUpper().Contains("TUESDAY"))
                    {
                        dayIndex = 2;
                    }
                    if (queryStr.ToUpper().Contains("WEDNESDAY"))
                    {
                        dayIndex = 3;
                    }
                    if (queryStr.ToUpper().Contains("THURSDAY"))
                    {
                        dayIndex = 4;
                    }
                    if (queryStr.ToUpper().Contains("FRIDAY"))
                    {
                        dayIndex = 5;
                    }
                    if (queryStr.ToUpper().Contains("SATURDAY"))
                    {
                        dayIndex = 6;
                    }
                    this.queryStr = this.connDB.AddBackslash(this.queryStr);
                    if (this.whichKey == "")
                    {
                        this.queryStr = "INSERT INTO pos_look_up ( acc_number, stock_id, key_code, key_code1, key_value, key_value1, menu_Btn, short_Btn, dayofweek, commnets, multi_select ) VALUES ( " + dataRow["acc_number"].ToString() + ", " + dataRow["stock_id"].ToString() + ", 0, 0, '', '', '" + this.whichMenu + "', '" + this.whichButton + "'," + dayIndex + ", '" + this.queryStr + "', " + this.multi_select + ")";
                    }
                    else
                    {
                        this.queryStr = "INSERT INTO pos_look_up ( acc_number, stock_id, key_code, key_code1, key_value, key_value1, dayofweek, commnets, multi_select ) VALUES ( " + dataRow["acc_number"].ToString() + ", " + dataRow["stock_id"].ToString() + ", 0, 0, '" + this.whichKey.ToLower() + "', '" + this.whichKey + "', " + dayIndex + ", '" + this.queryStr + "', " + this.multi_select + ")";
                    }
                    this.connDB.NoReturnQuery(this.queryStr);
                }
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
            base.Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string str;
            if (this.whichKey == "")
                str = "menu_Btn='" + this.whichMenu + "' AND short_Btn='" + this.whichButton + "'";
            else
                str = "key_value='" + this.whichKey + "' OR key_value1='" + this.whichKey + "'";
            this.queryStr = "DELETE FROM pos_look_up WHERE " + str;
            this.connDB.NoReturnQuery(this.queryStr);
            this.DialogResult = DialogResult.Yes;
            this.Close();
            base.Dispose();
        }

        #region init

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.tpWeekly = new System.Windows.Forms.TabPage();
            this.dgDefineList = new System.Windows.Forms.DataGridView();
            this.tpStock = new System.Windows.Forms.TabPage();
            this.TxtAcc_number = new System.Windows.Forms.TextBox();
            this.TxtStockId = new System.Windows.Forms.TextBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.tpCat = new System.Windows.Forms.TabPage();
            this.dgCat = new System.Windows.Forms.DataGridView();
            this.tbcdefine = new System.Windows.Forms.TabControl();
            this.customButton1 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.customButton3 = new QiPOS.CustomButton();
            this.BtnNext = new QiPOS.CustomButton();
            this.tpWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDefineList)).BeginInit();
            this.tpStock.SuspendLayout();
            this.tpCat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCat)).BeginInit();
            this.tbcdefine.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(406, 26);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(158, 25);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Short Cut Key";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKey.ForeColor = System.Drawing.Color.Crimson;
            this.lblKey.Location = new System.Drawing.Point(570, 20);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(33, 31);
            this.lblKey.TabIndex = 2;
            this.lblKey.Text = "A";
            // 
            // tpWeekly
            // 
            this.tpWeekly.Controls.Add(this.dgDefineList);
            this.tpWeekly.Location = new System.Drawing.Point(4, 22);
            this.tpWeekly.Name = "tpWeekly";
            this.tpWeekly.Size = new System.Drawing.Size(845, 481);
            this.tpWeekly.TabIndex = 2;
            this.tpWeekly.Text = "Weekly Item";
            this.tpWeekly.UseVisualStyleBackColor = true;
            this.tpWeekly.Enter += new System.EventHandler(this.TpWeekly_Enter);
            // 
            // dgDefineList
            // 
            this.dgDefineList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgDefineList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDefineList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDefineList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgDefineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDefineList.Location = new System.Drawing.Point(0, 3);
            this.dgDefineList.Name = "dgDefineList";
            this.dgDefineList.RowTemplate.Height = 36;
            this.dgDefineList.Size = new System.Drawing.Size(845, 475);
            this.dgDefineList.TabIndex = 0;
            // 
            // tpStock
            // 
            this.tpStock.BackColor = System.Drawing.Color.Transparent;
            this.tpStock.Controls.Add(this.TxtAcc_number);
            this.tpStock.Controls.Add(this.TxtStockId);
            this.tpStock.Controls.Add(this.lblItem);
            this.tpStock.Location = new System.Drawing.Point(4, 22);
            this.tpStock.Name = "tpStock";
            this.tpStock.Padding = new System.Windows.Forms.Padding(3);
            this.tpStock.Size = new System.Drawing.Size(845, 481);
            this.tpStock.TabIndex = 1;
            this.tpStock.Text = "Stock Item";
            this.tpStock.UseVisualStyleBackColor = true;
            this.tpStock.Enter += new System.EventHandler(this.TpStock_Enter);
            // 
            // TxtAcc_number
            // 
            this.TxtAcc_number.Location = new System.Drawing.Point(45, 203);
            this.TxtAcc_number.Name = "TxtAcc_number";
            this.TxtAcc_number.Size = new System.Drawing.Size(100, 29);
            this.TxtAcc_number.TabIndex = 2;
            this.TxtAcc_number.Visible = false;
            // 
            // TxtStockId
            // 
            this.TxtStockId.Location = new System.Drawing.Point(45, 134);
            this.TxtStockId.Name = "TxtStockId";
            this.TxtStockId.Size = new System.Drawing.Size(100, 29);
            this.TxtStockId.TabIndex = 1;
            this.TxtStockId.Visible = false;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.BackColor = System.Drawing.Color.LightCyan;
            this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.Location = new System.Drawing.Point(40, 62);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(181, 29);
            this.lblItem.TabIndex = 0;
            this.lblItem.Text = "New Stock Item";
            // 
            // tpCat
            // 
            this.tpCat.Controls.Add(this.dgCat);
            this.tpCat.Location = new System.Drawing.Point(4, 33);
            this.tpCat.Name = "tpCat";
            this.tpCat.Padding = new System.Windows.Forms.Padding(3);
            this.tpCat.Size = new System.Drawing.Size(845, 470);
            this.tpCat.TabIndex = 0;
            this.tpCat.Text = "Category";
            this.tpCat.UseVisualStyleBackColor = true;
            this.tpCat.Enter += new System.EventHandler(this.TpCat_Enter);
            // 
            // dgCat
            // 
            this.dgCat.AllowUserToAddRows = false;
            this.dgCat.AllowUserToDeleteRows = false;
            this.dgCat.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.dgCat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgCat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCat.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCat.ColumnHeadersVisible = false;
            this.dgCat.Location = new System.Drawing.Point(0, 0);
            this.dgCat.MultiSelect = false;
            this.dgCat.Name = "dgCat";
            this.dgCat.ReadOnly = true;
            this.dgCat.RowHeadersVisible = false;
            this.dgCat.RowTemplate.Height = 40;
            this.dgCat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCat.Size = new System.Drawing.Size(845, 469);
            this.dgCat.TabIndex = 0;
            // 
            // tbcdefine
            // 
            this.tbcdefine.Controls.Add(this.tpCat);
            this.tbcdefine.Controls.Add(this.tpStock);
            this.tbcdefine.Controls.Add(this.tpWeekly);
            this.tbcdefine.Location = new System.Drawing.Point(21, 41);
            this.tbcdefine.Name = "tbcdefine";
            this.tbcdefine.SelectedIndex = 0;
            this.tbcdefine.Size = new System.Drawing.Size(853, 507);
            this.tbcdefine.TabIndex = 0;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Black;
            this.customButton1.Location = new System.Drawing.Point(681, 559);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(165, 40);
            this.customButton1.TabIndex = 142;
            this.customButton1.Text = "Abort";
            this.customButton1.Click += new System.EventHandler(this.BtnAbort_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.Control;
            this.customButton2.CornerRadius = 40;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Black;
            this.customButton2.Location = new System.Drawing.Point(484, 559);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(165, 40);
            this.customButton2.TabIndex = 142;
            this.customButton2.Text = "Accept";
            this.customButton2.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // customButton3
            // 
            this.customButton3.BackColor = System.Drawing.SystemColors.Control;
            this.customButton3.CornerRadius = 40;
            this.customButton3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton3.ForeColor = System.Drawing.Color.Black;
            this.customButton3.Location = new System.Drawing.Point(286, 559);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton3.Size = new System.Drawing.Size(165, 40);
            this.customButton3.TabIndex = 142;
            this.customButton3.Text = "Delete";
            this.customButton3.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.BackColor = System.Drawing.SystemColors.Control;
            this.BtnNext.CornerRadius = 40;
            this.BtnNext.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNext.ForeColor = System.Drawing.Color.Black;
            this.BtnNext.Location = new System.Drawing.Point(88, 559);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnNext.Size = new System.Drawing.Size(165, 40);
            this.BtnNext.TabIndex = 142;
            this.BtnNext.Text = "Next Item";
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // FrmDefineShortcut
            // 
            this.ClientSize = new System.Drawing.Size(894, 611);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.tbcdefine);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(0, 30);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmDefineShortcut";
            this.Text = "Define A Shortcut  Key";
            this.Load += new System.EventHandler(this.FrmDefineShortcut_Load);
            this.tpWeekly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDefineList)).EndInit();
            this.tpStock.ResumeLayout(false);
            this.tpStock.PerformLayout();
            this.tpCat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCat)).EndInit();
            this.tbcdefine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}

