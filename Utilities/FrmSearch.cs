using System;
using System.Data;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmSearch : Form
    {
        private DataGridView dgSearchResult;
        private Label lblCat;
        private Label lblSupplier;
        private Label lblDesc;
        private ComboBox cbxCat;
        private ComboBox cbxSupplier;
        private TextBox TxtDescr;
        public string funIdentifier;
        private DataTable accountDB;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton BtnSave;
        private string queryStr;
        public SearchItem currentItem;

        public FrmSearch()
        {
            this.InitializeComponent();
        }

        #region hide 

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgSearchResult = new System.Windows.Forms.DataGridView();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.cbxCat = new System.Windows.Forms.ComboBox();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.TxtDescr = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSave = new QiPOS.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearchResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSearchResult
            // 
            this.dgSearchResult.AllowUserToAddRows = false;
            this.dgSearchResult.AllowUserToDeleteRows = false;
            this.dgSearchResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dgSearchResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSearchResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSearchResult.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearchResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSearchResult.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSearchResult.Location = new System.Drawing.Point(3, 191);
            this.dgSearchResult.Name = "dgSearchResult";
            this.dgSearchResult.ReadOnly = true;
            this.dgSearchResult.RowHeadersVisible = false;
            this.dgSearchResult.RowTemplate.Height = 36;
            this.dgSearchResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSearchResult.Size = new System.Drawing.Size(908, 368);
            this.dgSearchResult.TabIndex = 100;
            this.dgSearchResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSearchResult_CellDoubleClick);
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCat.ForeColor = System.Drawing.Color.Blue;
            this.lblCat.Location = new System.Drawing.Point(431, 16);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(94, 25);
            this.lblCat.TabIndex = 102;
            this.lblCat.Text = "Category";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.ForeColor = System.Drawing.Color.Blue;
            this.lblSupplier.Location = new System.Drawing.Point(577, 16);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(87, 25);
            this.lblSupplier.TabIndex = 103;
            this.lblSupplier.Text = "Supplier";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.Blue;
            this.lblDesc.Location = new System.Drawing.Point(127, 16);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(114, 25);
            this.lblDesc.TabIndex = 104;
            this.lblDesc.Text = "Description";
            // 
            // cbxCat
            // 
            this.cbxCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCat.FormattingEnabled = true;
            this.cbxCat.Location = new System.Drawing.Point(436, 52);
            this.cbxCat.Name = "cbxCat";
            this.cbxCat.Size = new System.Drawing.Size(140, 33);
            this.cbxCat.TabIndex = 1;
            this.cbxCat.SelectedIndexChanged += new System.EventHandler(this.CbxCat_SelectedIndexChanged);
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupplier.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(582, 52);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(140, 33);
            this.cbxSupplier.TabIndex = 2;
            // 
            // TxtDescr
            // 
            this.TxtDescr.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescr.Location = new System.Drawing.Point(127, 52);
            this.TxtDescr.Name = "TxtDescr";
            this.TxtDescr.Size = new System.Drawing.Size(303, 33);
            this.TxtDescr.TabIndex = 0;
            this.TxtDescr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescr_KeyDown);
            this.TxtDescr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDescr_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.lblCat);
            this.panel1.Controls.Add(this.lblSupplier);
            this.panel1.Controls.Add(this.TxtDescr);
            this.panel1.Controls.Add(this.lblDesc);
            this.panel1.Controls.Add(this.cbxSupplier);
            this.panel1.Controls.Add(this.cbxCat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 182);
            this.panel1.TabIndex = 107;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.BtnSave.CornerRadius = 40;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.Blue;
            this.BtnSave.Location = new System.Drawing.Point(360, 107);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(140, 40);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgSearchResult, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(914, 562);
            this.tableLayoutPanel1.TabIndex = 108;
            // 
            // FrmSearch
            // 
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(0, 30);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmSearch";
            this.Text = "Search Stock Item ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSearch_FormClosing);
            this.Load += new System.EventHandler(this.FormSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSearchResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void FormSearch_Load(object sender, EventArgs e)
        {
            this.Initiate();
            this.cbxCat.Text = currentItem.category;
            this.cbxSupplier.Text = currentItem.supplier;
            this.TxtDescr.Text = currentItem.result;

            if (currentItem.category == "" || currentItem.category == null)
            {
                this.cbxCat.SelectedIndex = 0;
            }
            this.ActiveControl = TxtDescr;
        }

        private void Initiate()
        {
            Connect connect = new Connect();
            this.queryStr = "SELECT acc_name, acc_id, acc_number FROM account_list WHERE acc_type_id=5 ORDER BY acc_name";
            connect.QueryTable(this.queryStr);
            DataRow row = connect.aTable.NewRow();
            row[0] = "ALL";
            row[1] = 0;
            row[2] = 0;
            connect.aTable.Rows.InsertAt(row, 0);
            this.cbxCat.DataSource = connect.aTable;
            this.cbxCat.DisplayMember = "acc_name";
            this.cbxCat.ValueMember = "acc_id";
            this.accountDB = connect.aTable;
            if (this.funIdentifier == "DefineShortCut")
            {
                this.dgSearchResult.MultiSelect = false; 
                this.BtnSave.Text = "Accept"; 
            }
            else
            {
                if (!(this.funIdentifier == "Search"))
                {
                    return;
                }
                this.dgSearchResult.MultiSelect = false; 
                this.BtnSave.Text = "Accept"; 
            }
        }

        private void CbxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            if (cbxCat.SelectedValue == null)
                return;
            this.queryStr = this.cbxCat.SelectedValue.ToString();
            if (!char.IsDigit(this.queryStr, 0))
                this.queryStr = "0";
            this.queryStr = "SELECT card_id, name FROM account_cards WHERE acc_id=" + this.queryStr + " ORDER BY name";
            connect.QueryTable(this.queryStr);
            DataRow row = connect.aTable.NewRow();
            row[1] = "  ALL  ";
            row[0] = 0;
            connect.aTable.Rows.InsertAt(row, 0);
            this.cbxSupplier.DataSource = connect.aTable;
            this.cbxSupplier.DisplayMember = "name";
            this.cbxSupplier.ValueMember = "card_id";
        }
         

        private void TxtStart_TextChanged(object sender, EventArgs e)
        {
            this.StockSearch(true);
        }

        private void StockSearch(bool startWith)
        { 
            Connect connect = new Connect(); 
            this.queryStr = "SELECT TOP 120 stock_id AS ID, barcode AS Barcode, descr Description, acc_name AS Category , RRP AS Price, name AS Supplier FROM pos_stock LEFT JOIN account_list ON ( pos_stock.acc_number = account_list.acc_number ) LEFT JOIN account_cards ON ( pos_stock.card_id = account_cards.card_id ) WHERE account_list.acc_type_id =5 ";
             
             
                int num1 = 0;
                foreach (DataRow dataRow in this.accountDB.Rows)
                {
                    if (dataRow["acc_id"].ToString().Equals(this.cbxCat.SelectedValue.ToString()))
                    {
                        num1 = (int)dataRow["acc_number"];
                        break;
                    }
                }
                int supplierNo = (int)this.cbxSupplier.SelectedValue;
                string searchTxt = this.TxtDescr.Text.Trim();
                if (searchTxt != "" && !startWith)
                {
                    string[] strArray = connect.AddBackslash(searchTxt).Split(new char[1]
          {
            ' '
          });
                    foreach (string word in strArray)
                    {
                        if (word.Trim() != "")
                        {
                            this.queryStr += (" AND ( descr like'%" + word.Trim() + "%')");
                        }
                    }
                } 
                if (num1 != 0)
                {
                    this.queryStr += (" AND pos_stock.acc_number=" + num1.ToString());
                }
                if (supplierNo != 0)
                {
                    this.queryStr += (" AND pos_stock.card_id=" + supplierNo.ToString());
                }
                this.queryStr += " ORDER BY descr ";
            
            connect.QueryTable(this.queryStr);
            this.dgSearchResult.DataSource = connect.aTable;
            this.dgSearchResult.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["Barcode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["ID"].Visible = false;
            this.dgSearchResult.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgSearchResult.Columns["Price"].DefaultCellStyle.Format = "C"; 
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!(this.funIdentifier == "DefineShortCut") && !(this.funIdentifier == "Search"))
                return;
            if (this.dgSearchResult.SelectedRows.Count > 0)
            {
                this.Owner.Tag = this.dgSearchResult.SelectedRows[0].Cells["ID"].Value.ToString();
                this.DialogResult = DialogResult.Yes;
            }
            else
                this.DialogResult = DialogResult.No;
        }         

        private void DgSearchResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = Convert.ToInt32(this.dgSearchResult[0, e.RowIndex].Value.ToString());
            FrmNewItem frmNewItem = new FrmNewItem
            {
                stock_id = num
            };
            this.AddOwnedForm(frmNewItem);
            if (frmNewItem.ShowDialog(this) != DialogResult.Yes)
            {
                return;
            }
            Connect connect = new Connect();
            connect.QueryTable(this.queryStr);
            this.dgSearchResult.DataSource = connect.aTable;
            this.dgSearchResult.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["Barcode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgSearchResult.Columns["ID"].Visible = false; 
        }

        private void FrmSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentItem = new SearchItem
            {
                category = cbxCat.Text,
                supplier = cbxSupplier.Text,
                result = TxtDescr.Text
            };
        }
         
        private void TxtDescr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.StockSearch(false);
            } 
        }

        private void TxtDescr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            { this.Close(); }
        }
    }

    public struct SearchItem
    {
        public string category;
        public string supplier;
        public string result;
    }
}

