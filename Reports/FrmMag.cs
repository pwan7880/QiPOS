using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmMag : Form
    { 
        private Connect connDB;
        private Connect connMagDB;
        private DataTable magTB;
        private readonly int invNo;
        private int currRowindex;
        private readonly Decimal c_rate;
        private bool cellEditHandler;
        private int cellInputLenth;
        private IContainer components;
        private DataGridView dgMag;
        private Panel pnlControl;
        private Label lblTitle;
        private TextBox txtInput;
        private Timer tmrLoad;
        private DataGridView dgSum;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButton2;
        private CustomButton BtnPlast;
        private CustomButton BtnPall;
        private CustomButton customButton4;
        private CustomButton customButton5;
        private CustomButton BtnSup;
        private DateTimePicker dptStart;
        private string shopName = "";


        public FrmMag(int in_invNo, Decimal in_rate)
        {
            this.cellEditHandler = true;
            this.components = (IContainer)null;
            this.InitializeComponent();
            this.invNo = in_invNo;
            this.c_rate = in_rate;
            this.Initiate();
        }

        public DataGridView GetGrid()
        {
            return this.dgMag;
        }
        private void Initiate()
        {
            try
            {
                // Get shop name for title
                string name = string.Empty;
                Connect connect = new Connect();
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    connection.Open();

                    // Call GetMagazineInvoiceDetails
                    using (var command = new SqlCommand("GetMagazineInvoiceDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubInvId", invNo);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader["name"].ToString();
                            }
                        }
                    }

                    // Set title and shopName
                    lblTitle.Text = $"Magazine List for {name}";
                    shopName = name;

                    // Populate dgMag (using refactored FilldgMag)
                    FilldgMag();
                    dgMag.Columns["sub_inv_mag_id"].Visible = false;
                    dgMag.Columns["sub_inv_id"].Visible = false;
                    dgMag.Columns["stock_id"].Visible = false;
                    dgMag.Columns["commision"].Visible = false;
                    dgMag.Columns["ITEM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgMag.Columns["ITEM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgMag.Columns["PRICE"].DefaultCellStyle.Format = "C";
                    dgMag.Columns["DATE"].DefaultCellStyle.Format = "dd MMM";

                    // Call GetMagazineSummary
                    using (var command = new SqlCommand("GetMagazineSummary", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubInvId", invNo);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgSum.DataSource = dataTable;
                            dgSum.Columns["DATE"].DefaultCellStyle.Format = "dd MMM";
                        }
                    }
                }

                txtInput.Focus();
                currRowindex = -1;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing magazine list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FilldgMag()
        {
            try
            {
                Connect connect = new Connect();
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetMagazineItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubInvId", invNo);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            magTB = new DataTable();
                            adapter.Fill(magTB);
                        }
                    }
                }

                dgMag.DataSource = magTB;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading magazine data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void BtnSup_Click(object sender, EventArgs e)
        {
            if (this.lblTitle.Text == "Special Items List")
            {
                this.lblTitle.Text = "Magazine List";
                this.dgMag.Visible = true;
                this.BackColor = Color.Azure;
            }
            else if (this.BtnSup.Text == "Mag Supply")
            {
                this.BtnSup.Text = "Mag Return";
                this.BtnSup.ForeColor = Color.Red;
                this.BackColor = Color.LightPink;
            }
            else
            {
                this.BtnSup.Text = "Mag Supply";
                this.BtnSup.ForeColor = Color.Blue;
                this.BackColor = Color.Azure;
            }
            this.txtInput.Focus();
        }

        private void TmrLoad_Tick(object sender, EventArgs e)
        {
            this.txtInput.Focus();
            this.tmrLoad.Enabled = false;
        }

        private void TxtInput_KeyUp(object sender, KeyEventArgs e)
        {
            this.BtnPall.Enabled = false;
            this.BtnPlast.Enabled = false;
            string barcode = this.txtInput.Text.Trim();
            if (barcode.Length == 15)
            {
                barcode = barcode.Substring(0, 13);
            }
            try
            {
                if (e.KeyCode == Keys.Return && this.txtInput.Text.Length > 3)
                {
                    this.GetStockItem(barcode);
                    this.dgMag.Visible = true;
                    if (!(this.BtnSup.Text == "Mag Return") || this.currRowindex < 0)
                    {
                        return;
                    }
                    this.dgMag.Rows[this.currRowindex].Cells[3].Selected = true;
                }

                else if ((e.KeyValue == 107 || e.KeyValue == 192) && this.txtInput.Text.Length < 4 && this.currRowindex >= 0)
                {
                    if (this.magTB.Rows.Count <= 0)
                    {
                        return;
                    }
                    string str = (barcode).ToString();
                    int num = Convert.ToInt32(str.Substring(0, str.Length - 1));
                    if (this.BtnSup.Text == "Mag Supply")
                    {
                        this.magTB.Rows[this.currRowindex]["QTY"] = num;
                    }
                    else
                    {
                        this.magTB.Rows[this.currRowindex]["RT"] = num;
                    }
                    this.txtInput.Text = "";
                    this.currRowindex = -1;
                }
                else
                {
                    if (e.KeyValue >= 48 && e.KeyValue <= 57 || e.KeyValue >= 96 && e.KeyValue <= 105)
                    {
                        return;
                    }
                    this.txtInput.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cat Input Exception: " + (ex.Message).ToString());
                this.txtInput.Focus();
            }
        }

        private void GetStockItem(string newBarCode)
        {
            try
            {
                int dayOfYear1 = DateTime.Now.DayOfYear;
                using (SqlConnection conn = new SqlConnection(new Connect().ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetStockByBarcode", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Barcode", newBarCode);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            // Existing logic to process dataTable
                            if (dataTable.Rows.Count > 0)
                            {
                                bool flag = true;
                                int index1 = 0;
                                for (int index2 = this.magTB.Rows.Count - 1; index2 >= 0; --index2)
                                {
                                    if (this.magTB.Rows[index2].RowState != DataRowState.Deleted)
                                    {
                                        if (this.BtnSup.Text == "Mag Supply")
                                        {
                                            int dayOfYear2 = ((DateTime)this.magTB.Rows[index2]["DATE"]).DayOfYear;
                                            if (dataTable.Rows[0]["stock_id"].Equals(this.magTB.Rows[index2]["stock_id"]) && dayOfYear1 == dayOfYear2)
                                            {
                                                flag = false;
                                                index1 = index2;
                                                break;
                                            }
                                        }
                                        else if (dataTable.Rows[0]["stock_id"].Equals(this.magTB.Rows[index2]["stock_id"]))
                                        {
                                            flag = false;
                                            index1 = index2;
                                            break;
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    DateTime now = DateTime.Now;
                                    DataRow row = this.magTB.NewRow();
                                    row["sub_inv_id"] = this.invNo;
                                    row["stock_id"] = dataTable.Rows[0]["stock_id"];
                                    row["ITEM"] = dataTable.Rows[0]["descr"];
                                    row["PRICE"] = dataTable.Rows[0]["RRP"];
                                    row["DATE"] = now.ToString("yyyy-MM-dd");
                                    row["Rate"] = this.c_rate;
                                    row["commision"] = 0;
                                    if (this.BtnSup.Text == "Mag Supply")
                                    {
                                        row["QTY"] = 1;
                                        row["RT"] = 0;
                                    }
                                    else
                                    {
                                        row["QTY"] = 0;
                                        row["RT"] = 1;
                                    }
                                    this.magTB.Rows.InsertAt(row, 0);
                                    this.currRowindex = 0;
                                }
                                else
                                {
                                    if (this.BtnSup.Text == "Mag Supply")
                                    {
                                        int num = (int)this.magTB.Rows[index1]["QTY"] + 1;
                                        this.magTB.Rows[index1]["QTY"] = num;
                                    }
                                    else
                                    {
                                        int num = (int)this.magTB.Rows[index1]["RT"] + 1;
                                        this.magTB.Rows[index1]["RT"] = num;
                                    }
                                    this.currRowindex = index1;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Not a Stock Item ");
                            }
                            this.txtInput.Text = "";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving stock item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.BtnPall.Enabled = true;
                this.BtnPlast.Enabled = true;
                DateTime now = DateTime.Now;
                for (int index = 0; index < this.magTB.Rows.Count; ++index)
                {
                    if (this.magTB.Rows[index].RowState != DataRowState.Deleted && this.magTB.Rows[index]["sub_inv_id"].ToString() == "")
                    {
                        this.magTB.Rows[index]["sub_inv_id"] = this.invNo;
                        this.magTB.Rows[index]["stock_id"] = 0;
                        this.magTB.Rows[index]["Date"] = now.ToString("yyyy-MM-dd");
                        this.magTB.Rows[index]["commision"] = 0;
                    }
                }
                this.connMagDB.Close();
                this.connMagDB.UpdateTable(this.magTB.GetChanges());

                using (SqlConnection conn = new SqlConnection(new Connect().ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetMagazineSummaryWithTotals", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubInvId", this.invNo);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            this.dgSum.DataSource = dt;
                        }
                    }
                }

                this.FilldgMag(); // Already uses GetMagazineItems proc
                this.dgMag.Visible = false;
                this.txtInput.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving magazine data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmMag_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtInput.Text = "";
            this.txtInput.Focus();
        }

        private void DgMag_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtInput.Text = "";
        }

        private void DgMag_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.cellInputLenth = 0;
            if (!this.cellEditHandler)
                return;

            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            this.cellEditHandler = false;
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int num = (int)e.KeyChar;
                if ((num < 48 || num > 57) && num != 46 && this.dgMag.CurrentCell.ColumnIndex > 3)
                    e.Handled = true;
                else
                    ++this.cellInputLenth;
                if (this.cellInputLenth <= 6 || this.dgMag.CurrentCell.ColumnIndex <= 3)
                    return;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit Datagridview Cell Exception: " + (ex.Message).ToString());
            }
        }

        private void BtnPall_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new Connect().ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetMagazineItemsBySubInvId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubInvId", this.invNo);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            this.connDB.aTable = dt; // Update connDB.aTable for PrintDeliveryList
                            this.PrintDeliveryList(""); // Pass empty string or modify PrintDeliveryList to use connDB.aTable directly
                        }
                    }
                }
                this.dgMag.Visible = true;
                this.txtInput.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing delivery list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPlast_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new Connect().ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetMagazineItemsByDate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubInvId", this.invNo);
                        cmd.Parameters.AddWithValue("@EnterDate", this.dptStart.Value.Date); // Pass null for latest date
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            this.connDB.aTable = dt; // Update connDB.aTable for PrintDeliveryList
                            this.PrintDeliveryList(""); // Pass empty string or modify PrintDeliveryList
                        }
                    }
                }
                this.dgMag.Visible = true;
                this.txtInput.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing last delivery list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDeliveryList(string strQuery)
        {
            ConfigurationReader reader = new ConfigurationReader();
            CompanyData companyData = reader.CompanyInfo();

            this.connDB.QueryTable(strQuery);
            if (shopName.Length > 40)
            {
                shopName = shopName.Substring(0, 30);
            }
            int copies = 0;
            string str1 = "\x001B!\x0002" + "\x001D!\0" + "\x001B \x0001" + "\x001B3D" + "\x001BE\x0001" + this.AddBlankLeft(1, companyData.CompanyName) + "\n" + "\x001Ba\x0001" + "Delivery List for " + shopName + "\n" + "\x001BE\0" + "\x001Ba\0" + " --------------------------------------" + "\n" + this.AddBlankRight(4, this.AddBlankRight(23, " ITEM") + "RRP") + "QTY" + "\n" + " --------------------------------------" + "\n";
            for (int index = 0; index < this.connDB.aTable.Rows.Count; ++index)
            {
                string in_str1 = this.AddBlankLeft(1, this.connDB.aTable.Rows[index]["descr"].ToString().Trim());
                int length = in_str1.Length;
                string in_str2 = length <= 22 ? this.AddBlankRight(22 - length, in_str1) : in_str1.Substring(0, 22);
                string str2 = this.CurrencyFotmat(this.connDB.aTable.Rows[index]["rrp"].ToString());
                string in_str3 = this.AddBlankRight(10 - str2.Length, in_str2) + str2;
                copies = (int)this.connDB.aTable.Rows[index]["supply"] + copies;
                string str3 = this.AddBlankRight(6 - this.connDB.aTable.Rows[index]["supply"].ToString().Trim().Length, in_str3) + this.connDB.aTable.Rows[index]["supply"].ToString().Trim();
                str1 = str1 + str3 + "\n";
            }
            string str4 = " --------------------------------------";
            string szString = str1 + str4 + "\n " + "Total : " + copies.ToString() + "\n Date: " + DateTime.Now + "\n" + "\x001Bd\x0006\x001DV\x0001";
            RawPrinterHelper.SendStringToPrinter(companyData.PosPrinter, szString);
        }

        private string AddBlankLeft(int num, string in_str)
        {
            string str = in_str;
            for (int index = 0; index < num; ++index)
                str = " " + str;
            return str;
        }

        private string AddBlankRight(int num, string in_str)
        {
            string str = in_str;
            for (int index = 0; index < num; ++index)
                str += " ";
            return str;
        }

        private string CurrencyFotmat(string inStr)
        {
            string str = inStr.Trim();
            int num = str.IndexOf(".");
            int length = str.Length;
            return num != -1 ? (length - num != 2 ? "$" + str : "$" + str + "0") : "$" + str + ".00";
        }

        private void DgMag_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.dgMag.Rows[e.Row.Index - 1].Cells["Date"].Value = DateTime.Now.ToString("dd MMM");
            this.dgMag.Rows[e.Row.Index - 1].Cells["Rate"].Value = this.c_rate;
        }

        private void FrmMag_Click(object sender, EventArgs e)
        {
            this.txtInput.Focus();
        }

        private void DgSum_Click(object sender, EventArgs e)
        {
            this.txtInput.Focus();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            this.dgMag.Visible = true;
            this.txtInput.Focus();
        }

        private void DgSum_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtInput.Focus();
        }

        private void DgMag_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (!(this.dgMag.Columns[e.ColumnIndex].Name == "DATE") || e == null)
                return;
            if (e.Value != null)
            {
                try
                {
                    e.Value = DateTime.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
                catch (FormatException ex)
                {
                    string error = ex.Message;
                    Console.WriteLine(error);
                    DateTime now = DateTime.Now;
                    e.Value = now;
                    e.ParsingApplied = true;
                }
            }
        }

        #region components

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMag));
            this.dgMag = new System.Windows.Forms.DataGridView();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.dptStart = new System.Windows.Forms.DateTimePicker();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tmrLoad = new System.Windows.Forms.Timer(this.components);
            this.dgSum = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnSup = new QiPOS.CustomButton();
            this.customButton5 = new QiPOS.CustomButton();
            this.customButton4 = new QiPOS.CustomButton();
            this.BtnPall = new QiPOS.CustomButton();
            this.BtnPlast = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgMag)).BeginInit();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSum)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            //
            // dgMag
            //
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightYellow;
            this.dgMag.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMag.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgMag.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMag.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMag.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgMag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMag.Location = new System.Drawing.Point(3, 34);
            this.dgMag.MultiSelect = false;
            this.dgMag.Name = "dgMag";
            this.tableLayoutPanel1.SetRowSpan(this.dgMag, 2);
            this.dgMag.RowTemplate.Height = 30;
            this.dgMag.Size = new System.Drawing.Size(688, 702);
            this.dgMag.TabIndex = 1;
            this.dgMag.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.DgMag_CellParsing);
            this.dgMag.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgMag_EditingControlShowing);
            this.dgMag.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgMag_UserAddedRow);
            this.dgMag.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DgMag_MouseClick);

            //
            // pnlControl
            //
            this.pnlControl.Controls.Add(this.BtnSup);
            this.pnlControl.Controls.Add(this.customButton5);
            this.pnlControl.Controls.Add(this.customButton4);
            this.pnlControl.Controls.Add(this.BtnPall);
            this.pnlControl.Controls.Add(this.BtnPlast);
            this.pnlControl.Controls.Add(this.customButton2);
            this.pnlControl.Controls.Add(this.dptStart);
            this.pnlControl.Controls.Add(this.txtInput);
            this.pnlControl.Location = new System.Drawing.Point(694, 31);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(200, 354);
            this.pnlControl.TabIndex = 19;

            //
            // dptStart
            //
            this.dptStart.CustomFormat = "dd MMM yy";
            this.dptStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptStart.Location = new System.Drawing.Point(25, 222);
            this.dptStart.Name = "dptStart";
            this.dptStart.Size = new System.Drawing.Size(160, 29);
            this.dptStart.TabIndex = 24;

            //
            // txtInput
            //
            this.txtInput.Location = new System.Drawing.Point(26, 5);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(160, 29);
            this.txtInput.TabIndex = 20;
            this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtInput_KeyUp);

            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(194, 31);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Magazine List";

            //
            // tmrLoad
            //
            this.tmrLoad.Enabled = true;
            this.tmrLoad.Tick += new System.EventHandler(this.TmrLoad_Tick);

            //
            // dgSum
            //
            this.dgSum.AllowUserToAddRows = false;
            this.dgSum.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Azure;
            this.dgSum.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSum.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSum.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgSum.Location = new System.Drawing.Point(697, 388);
            this.dgSum.Name = "dgSum";
            this.dgSum.ReadOnly = true;
            this.dgSum.RowHeadersVisible = false;
            this.dgSum.RowTemplate.Height = 30;
            this.dgSum.Size = new System.Drawing.Size(210, 348);
            this.dgSum.TabIndex = 21;
            this.dgSum.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSum_CellContentClick);
            this.dgSum.Click += new System.EventHandler(this.DgSum_Click);

            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgSum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgMag, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlControl, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(910, 739);
            this.tableLayoutPanel1.TabIndex = 22;

            //
            // BtnSup
            //
            this.BtnSup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSup.CornerRadius = 60;
            this.BtnSup.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSup.ForeColor = System.Drawing.Color.Blue;
            this.BtnSup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSup.Location = new System.Drawing.Point(26, 40);
            this.BtnSup.Name = "BtnSup";
            this.BtnSup.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.BtnSup.Size = new System.Drawing.Size(160, 40);
            this.BtnSup.TabIndex = 25;
            this.BtnSup.Text = "Mag Supply";
            this.BtnSup.Click += new System.EventHandler(this.BtnSup_Click);

            //
            // customButton5
            //
            this.customButton5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.customButton5.CornerRadius = 60;
            this.customButton5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton5.ForeColor = System.Drawing.Color.Blue;
            this.customButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customButton5.Location = new System.Drawing.Point(26, 85);
            this.customButton5.Name = "customButton5";
            this.customButton5.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton5.Size = new System.Drawing.Size(160, 40);
            this.customButton5.TabIndex = 25;
            this.customButton5.Text = "Save";
            this.customButton5.Click += new System.EventHandler(this.BtnSave_Click);

            //
            // customButton4
            //
            this.customButton4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.customButton4.CornerRadius = 60;
            this.customButton4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton4.ForeColor = System.Drawing.Color.Blue;
            this.customButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customButton4.Location = new System.Drawing.Point(26, 130);
            this.customButton4.Name = "customButton4";
            this.customButton4.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton4.Size = new System.Drawing.Size(160, 40);
            this.customButton4.TabIndex = 25;
            this.customButton4.Text = "Show";
            this.customButton4.Click += new System.EventHandler(this.BtnShow_Click);

            //
            // BtnPall
            //
            this.BtnPall.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnPall.CornerRadius = 60;
            this.BtnPall.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPall.ForeColor = System.Drawing.Color.Blue;
            this.BtnPall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnPall.Location = new System.Drawing.Point(26, 175);
            this.BtnPall.Name = "BtnPall";
            this.BtnPall.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.BtnPall.Size = new System.Drawing.Size(160, 40);
            this.BtnPall.TabIndex = 25;
            this.BtnPall.Text = "Print All";
            this.BtnPall.Click += new System.EventHandler(this.BtnPall_Click);

            //
            // BtnPlast
            //
            this.BtnPlast.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnPlast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnPlast.CornerRadius = 60;
            this.BtnPlast.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPlast.ForeColor = System.Drawing.Color.Blue;
            this.BtnPlast.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnPlast.Location = new System.Drawing.Point(26, 257);
            this.BtnPlast.Name = "BtnPlast";
            this.BtnPlast.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.BtnPlast.Size = new System.Drawing.Size(160, 40);
            this.BtnPlast.TabIndex = 25;
            this.BtnPlast.Text = "Print Last";
            this.BtnPlast.Click += new System.EventHandler(this.BtnPlast_Click);

            //
            // customButton2
            //
            this.customButton2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.customButton2.CornerRadius = 60;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customButton2.Location = new System.Drawing.Point(26, 304);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(160, 40);
            this.customButton2.TabIndex = 25;
            this.customButton2.Text = "Close";
            this.customButton2.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // FrmMag
            //
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(910, 739);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));            
            this.Name = "FrmMag";
            this.Text = "FrmMag";
            this.Click += new System.EventHandler(this.FrmMag_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmMag_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.dgMag)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSum)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion components
    }
}

