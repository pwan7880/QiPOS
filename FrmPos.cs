using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace QiPOS
{
    /// <summary>
    /// updated 3/1/2025
    /// </summary>
    public partial class FrmPos : Form
    {

        //Buglist: adding a new plu item throws null reference exception
        private static string version = "version 2026.1.3 build 0014";
        #region Initialisation objects

        private Connect conn;
        private DataTable CurrentTable;
        private SaleService saleService;
        private StockService stockService;
        private RefundService refundService;
        private string printerNameStr;
        private string displayNameStr;
        private DataTable cacheTable;
        private DataTable cacheSecondTable;
        private SearchItem searchCache; 
        private string tmpAccNo;
        private Decimal tmpRate;
        private bool endOfSaleFlag;
        private DataGridViewEditingControlShowingEventArgs controlArg;
        private bool cellEditHandler;
        private bool cellEditingBeginingFlag;
        private bool cellEditedFlag;
        private object objCurSeq_id; 
        private bool refundFlag;
        private IContainer components;
        private DataGridView dgItemList;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewSelectedCellColumn dataGridViewSelectedCellColumn1;
        private TextBox txtAmount;
        private Label lblTotal;
        private TextBox txtCat;
        private Label lblChange;
        private Label lblDesc;
        private Panel pnlCash;
        private RadioButton rbtCash;
        private RadioButton rBtnoCash;
        private Timer timerClean;
        private Panel pnlDateBar;
        private DateTimePicker DtpCurrent;
        private Label lblDate;
        private RichTextBox rich;
        private Panel panel1;
        private Panel panel2;
        private readonly int rownumber = 14;
        private readonly int gridHeight = 495;
        private int trans_id;
        private CustomButton subagentButton;
        private CustomButton ButtonSearch;
        private CustomButton customButton3;
        private CustomButton ButtonShortcut;
        private CustomButton ButtonTransactions;
        private CustomButton ButtonProducts;
        private CustomButton ButtonCash;
        private CustomButton ButtonStats;
        private CustomButton ButtonPrevSales;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButton4;
        private CustomButton customButton5;
        private CustomButton ButtonRefund;
        private CustomButton customButton7;
        private CustomButton ButtonOpen;
        private CustomButton BtnQuit;
        private CustomButton ButtonHundred;
        private CustomButton customButton6;
        private CustomButton customButton8;
        private CustomButton customButton9;
        private CustomButton customButton10;
        private CustomButton customButton11;
        private string controlstring = UIStyles.Empty;        
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewSelectedCellColumn dataGridViewSelectedCellColumn2;
        private DataGridViewSelectedCellColumn dataGridViewSelectedCellColumn3;
        private DataGridViewSelectedCellColumn dataGridViewSelectedCellColumn4;
        private DataGridViewSelectedCellColumn dataGridViewSelectedCellColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private CustomButton customButtonConfig;
        private Label VersionLabel;
        private CustomButton customButton1;
        private DataGridViewTextBoxColumn seq;
        private DataGridViewSelectedCellColumn item;
        private DataGridViewSelectedCellColumn unitPrice;
        private DataGridViewSelectedCellColumn qty;
        private DataGridViewSelectedCellColumn gst;
        private DataGridViewTextBoxColumn total;
        private int supplierIndexCards = 0;


        #endregion declared 
        /// <summary>
        /// new version with sale service and other improvements
        /// </summary>
        public FrmPos()
        {
            // Initialize the connection and services, check if db exists
            try
            {
                var db = new Connect();
                db.ValidateLocalDb();
            }
            catch (TableMissingException ex)
            {
                MessageBox.Show(ex.Message + UIStyles.TwoNewlines + ex.InnerException?.Message, "Data Table Missing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            catch (DatabaseUnavailableException ex)
            {
                MessageBox.Show(ex.Message + UIStyles.TwoNewlines + ex.InnerException?.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            this.Tag = null;
            cellEditHandler = true;
            cellEditedFlag = false;
            refundFlag = false;

            printerNameStr = GetPrinterName();
            displayNameStr = GetDisplayPortName();

            InitializeComponent();// Initialize GUI components

            timerClean.Enabled = true;

            // Prepare initial session data (not UI)
            InitializePOS();
                      
            cacheTable = CurrentTable;
            cacheSecondTable = CurrentTable.Copy();
            cacheSecondTable.TableName = "NewTable";
            endOfSaleFlag = false;

            // prefetch return info data
            string queryStr = "SELECT acc_name AS Category, acc_number, markup_rate FROM account_list WHERE acc_type_id =4 AND status =1 ORDER BY Category";
            new Connect().QueryTable(queryStr); // or store if needed

            SetReturnInfo();
        }
        

        #region Initialization Methods
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSelectedCellColumn1 = new QiPOS.DataGridViewSelectedCellColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.rBtnoCash = new System.Windows.Forms.RadioButton();
            this.rbtCash = new System.Windows.Forms.RadioButton();
            this.timerClean = new System.Windows.Forms.Timer(this.components);
            this.pnlDateBar = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.DtpCurrent = new System.Windows.Forms.DateTimePicker();
            this.rich = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customButton5 = new QiPOS.CustomButton();
            this.ButtonOpen = new QiPOS.CustomButton();
            this.customButton7 = new QiPOS.CustomButton();
            this.ButtonRefund = new QiPOS.CustomButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ButtonHundred = new QiPOS.CustomButton();
            this.customButton6 = new QiPOS.CustomButton();
            this.customButton11 = new QiPOS.CustomButton();
            this.customButton10 = new QiPOS.CustomButton();
            this.customButton9 = new QiPOS.CustomButton();
            this.customButton8 = new QiPOS.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customButton1 = new QiPOS.CustomButton();
            this.ButtonPrevSales = new QiPOS.CustomButton();
            this.ButtonStats = new QiPOS.CustomButton();
            this.ButtonCash = new QiPOS.CustomButton();
            this.ButtonProducts = new QiPOS.CustomButton();
            this.customButton3 = new QiPOS.CustomButton();
            this.ButtonShortcut = new QiPOS.CustomButton();
            this.ButtonSearch = new QiPOS.CustomButton();
            this.ButtonTransactions = new QiPOS.CustomButton();
            this.customButtonConfig = new QiPOS.CustomButton();
            this.subagentButton = new QiPOS.CustomButton();
            this.BtnQuit = new QiPOS.CustomButton();
            this.customButton4 = new QiPOS.CustomButton();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSelectedCellColumn2 = new QiPOS.DataGridViewSelectedCellColumn();
            this.dataGridViewSelectedCellColumn3 = new QiPOS.DataGridViewSelectedCellColumn();
            this.dataGridViewSelectedCellColumn4 = new QiPOS.DataGridViewSelectedCellColumn();
            this.dataGridViewSelectedCellColumn5 = new QiPOS.DataGridViewSelectedCellColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item = new QiPOS.DataGridViewSelectedCellColumn();
            this.unitPrice = new QiPOS.DataGridViewSelectedCellColumn();
            this.qty = new QiPOS.DataGridViewSelectedCellColumn();
            this.gst = new QiPOS.DataGridViewSelectedCellColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.pnlCash.SuspendLayout();
            this.pnlDateBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgItemList
            // 
            this.dgItemList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgItemList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItemList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgItemList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgItemList.ColumnHeadersHeight = 40;
            this.dgItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.item,
            this.unitPrice,
            this.qty,
            this.gst,
            this.total});
            this.dgItemList.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgItemList.Location = new System.Drawing.Point(0, 0);
            this.dgItemList.MultiSelect = false;
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.RowHeadersWidth = 50;
            this.dgItemList.RowTemplate.Height = 32;
            this.dgItemList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItemList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgItemList.Size = new System.Drawing.Size(1264, 500);
            this.dgItemList.TabIndex = 4;
            this.dgItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgItemList_CellClick);
            this.dgItemList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgItemList_CellLeave);
            this.dgItemList.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.DgItemList_CellStateChanged);
            this.dgItemList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgItemList_EditingControlShowing);
            this.dgItemList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgItemList_RowEnter);
            this.dgItemList.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgItemList_UserDeletedRow);
            this.dgItemList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DgItemList_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewSelectedCellColumn1
            // 
            this.dataGridViewSelectedCellColumn1.HeaderText = "ITEMS";
            this.dataGridViewSelectedCellColumn1.MinimumWidth = 10;
            this.dataGridViewSelectedCellColumn1.Name = "dataGridViewSelectedCellColumn1";
            this.dataGridViewSelectedCellColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedCellColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewSelectedCellColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ITEMS";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit Price";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "GST";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "QTY";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Total";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtAmount.Location = new System.Drawing.Point(447, 505);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(230, 40);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Text = "$0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Enter += new System.EventHandler(this.TxtAmount_Enter);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.TxtAmount_Leave);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTotal.Font = new System.Drawing.Font("Palatino Linotype", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotal.Location = new System.Drawing.Point(856, 500);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(408, 83);
            this.lblTotal.TabIndex = 101;
            this.lblTotal.Text = "$0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblTotal_MouseClick);
            // 
            // txtCat
            // 
            this.txtCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtCat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(240)))), ((int)(((byte)(205)))));
            this.txtCat.Location = new System.Drawing.Point(428, 505);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(10, 31);
            this.txtCat.TabIndex = 1;
            this.txtCat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtCat_KeyUp);
            // 
            // lblChange
            // 
            this.lblChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblChange.Font = new System.Drawing.Font("Palatino Linotype", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.Purple;
            this.lblChange.Location = new System.Drawing.Point(730, 569);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(534, 89);
            this.lblChange.TabIndex = 102;
            this.lblChange.Text = "Change : $0.00";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblChange.Click += new System.EventHandler(this.LblChange_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblDesc.Location = new System.Drawing.Point(3, 500);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(3);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(320, 51);
            this.lblDesc.TabIndex = 100;
            this.lblDesc.Text = "Ready for a New Sale";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDesc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblDesc_MouseClick);
            // 
            // pnlCash
            // 
            this.pnlCash.Controls.Add(this.rBtnoCash);
            this.pnlCash.Controls.Add(this.rbtCash);
            this.pnlCash.Location = new System.Drawing.Point(351, 613);
            this.pnlCash.Name = "pnlCash";
            this.pnlCash.Size = new System.Drawing.Size(205, 41);
            this.pnlCash.TabIndex = 107;
            this.pnlCash.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PnlCash_MouseClick);
            // 
            // rBtnoCash
            // 
            this.rBtnoCash.AutoSize = true;
            this.rBtnoCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rBtnoCash.Checked = true;
            this.rBtnoCash.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rBtnoCash.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBtnoCash.Location = new System.Drawing.Point(0, 0);
            this.rBtnoCash.Name = "rBtnoCash";
            this.rBtnoCash.Size = new System.Drawing.Size(99, 35);
            this.rBtnoCash.TabIndex = 1;
            this.rBtnoCash.TabStop = true;
            this.rBtnoCash.Text = "Eftpos";
            this.rBtnoCash.UseVisualStyleBackColor = false;
            this.rBtnoCash.CheckedChanged += new System.EventHandler(this.RBtnoCash_CheckedChanged);
            // 
            // rbtCash
            // 
            this.rbtCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtCash.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbtCash.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCash.Location = new System.Drawing.Point(112, 0);
            this.rbtCash.Name = "rbtCash";
            this.rbtCash.Size = new System.Drawing.Size(93, 34);
            this.rbtCash.TabIndex = 0;
            this.rbtCash.Text = "Cash";
            this.rbtCash.UseVisualStyleBackColor = false;
            this.rbtCash.CheckedChanged += new System.EventHandler(this.RbtCash_CheckedChanged);
            // 
            // timerClean
            // 
            this.timerClean.Interval = 300000;
            this.timerClean.Tick += new System.EventHandler(this.TmrClean_Tick);
            // 
            // pnlDateBar
            // 
            this.pnlDateBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDateBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlDateBar.Controls.Add(this.lblDate);
            this.pnlDateBar.Controls.Add(this.DtpCurrent);
            this.pnlDateBar.Location = new System.Drawing.Point(1054, 768);
            this.pnlDateBar.Name = "pnlDateBar";
            this.pnlDateBar.Size = new System.Drawing.Size(210, 114);
            this.pnlDateBar.TabIndex = 114;
            this.pnlDateBar.Click += new System.EventHandler(this.PnlDateBar_Click);
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDate.Location = new System.Drawing.Point(16, 27);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(172, 30);
            this.lblDate.TabIndex = 123;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DtpCurrent
            // 
            this.DtpCurrent.CalendarMonthBackground = System.Drawing.Color.White;
            this.DtpCurrent.CustomFormat = "dd MMM yy";
            this.DtpCurrent.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpCurrent.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpCurrent.Location = new System.Drawing.Point(18, 71);
            this.DtpCurrent.Name = "DtpCurrent";
            this.DtpCurrent.Size = new System.Drawing.Size(170, 39);
            this.DtpCurrent.TabIndex = 105;
            this.DtpCurrent.ValueChanged += new System.EventHandler(this.DtpCurrent_ValueChanged);
            // 
            // rich
            // 
            this.rich.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rich.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rich.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rich.Location = new System.Drawing.Point(5, 815);
            this.rich.Name = "rich";
            this.rich.ReadOnly = true;
            this.rich.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rich.Size = new System.Drawing.Size(214, 90);
            this.rich.TabIndex = 124;
            this.rich.Text = "";
            this.rich.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Rich_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customButton5);
            this.panel1.Controls.Add(this.ButtonOpen);
            this.panel1.Controls.Add(this.customButton7);
            this.panel1.Controls.Add(this.ButtonRefund);
            this.panel1.Location = new System.Drawing.Point(0, 551);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 107);
            this.panel1.TabIndex = 125;
            // 
            // customButton5
            // 
            this.customButton5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton5.CornerRadius = 45;
            this.customButton5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton5.ForeColor = System.Drawing.Color.DarkBlue;
            this.customButton5.Location = new System.Drawing.Point(172, 3);
            this.customButton5.Margin = new System.Windows.Forms.Padding(0);
            this.customButton5.Name = "customButton5";
            this.customButton5.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton5.Size = new System.Drawing.Size(162, 45);
            this.customButton5.TabIndex = 128;
            this.customButton5.Text = "Cancel";
            this.customButton5.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ButtonOpen.CornerRadius = 45;
            this.ButtonOpen.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpen.ForeColor = System.Drawing.Color.DarkBlue;
            this.ButtonOpen.Location = new System.Drawing.Point(3, 58);
            this.ButtonOpen.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonOpen.Size = new System.Drawing.Size(162, 45);
            this.ButtonOpen.TabIndex = 128;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // customButton7
            // 
            this.customButton7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton7.CornerRadius = 45;
            this.customButton7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton7.ForeColor = System.Drawing.Color.DarkBlue;
            this.customButton7.Location = new System.Drawing.Point(3, 3);
            this.customButton7.Margin = new System.Windows.Forms.Padding(0);
            this.customButton7.Name = "customButton7";
            this.customButton7.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton7.Size = new System.Drawing.Size(162, 45);
            this.customButton7.TabIndex = 128;
            this.customButton7.Text = "Save";
            this.customButton7.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ButtonRefund
            // 
            this.ButtonRefund.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ButtonRefund.CornerRadius = 45;
            this.ButtonRefund.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRefund.ForeColor = System.Drawing.Color.DarkBlue;
            this.ButtonRefund.Location = new System.Drawing.Point(172, 58);
            this.ButtonRefund.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonRefund.Name = "ButtonRefund";
            this.ButtonRefund.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonRefund.Size = new System.Drawing.Size(162, 45);
            this.ButtonRefund.TabIndex = 128;
            this.ButtonRefund.Text = "Refund";
            this.ButtonRefund.Click += new System.EventHandler(this.BtnRefund_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.ButtonHundred);
            this.panel2.Controls.Add(this.customButton6);
            this.panel2.Controls.Add(this.customButton11);
            this.panel2.Controls.Add(this.customButton10);
            this.panel2.Controls.Add(this.customButton9);
            this.panel2.Controls.Add(this.customButton8);
            this.panel2.Location = new System.Drawing.Point(293, 766);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 222);
            this.panel2.TabIndex = 126;
            // 
            // ButtonHundred
            // 
            this.ButtonHundred.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonHundred.BackgroundImage = global::QiPOS.Properties.Resources._100_dollar;
            this.ButtonHundred.CornerRadius = 50;
            this.ButtonHundred.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonHundred.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonHundred.Location = new System.Drawing.Point(0, 13);
            this.ButtonHundred.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonHundred.Name = "ButtonHundred";
            this.ButtonHundred.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonHundred.Size = new System.Drawing.Size(144, 88);
            this.ButtonHundred.TabIndex = 128;
            this.ButtonHundred.Click += new System.EventHandler(this.Button100_Click);
            // 
            // customButton6
            // 
            this.customButton6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton6.BackgroundImage = global::QiPOS.Properties.Resources._10_dollar;
            this.customButton6.CornerRadius = 50;
            this.customButton6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton6.Location = new System.Drawing.Point(0, 109);
            this.customButton6.Margin = new System.Windows.Forms.Padding(0);
            this.customButton6.Name = "customButton6";
            this.customButton6.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton6.Size = new System.Drawing.Size(144, 88);
            this.customButton6.TabIndex = 128;
            this.customButton6.Click += new System.EventHandler(this.Button10_Click);
            // 
            // customButton11
            // 
            this.customButton11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton11.BackgroundImage = global::QiPOS.Properties.Resources._30_dollar;
            this.customButton11.CornerRadius = 50;
            this.customButton11.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton11.Location = new System.Drawing.Point(312, 109);
            this.customButton11.Margin = new System.Windows.Forms.Padding(0);
            this.customButton11.Name = "customButton11";
            this.customButton11.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton11.Size = new System.Drawing.Size(144, 88);
            this.customButton11.TabIndex = 128;
            this.customButton11.Text = "30";
            this.customButton11.Click += new System.EventHandler(this.Button30_Click);
            // 
            // customButton10
            // 
            this.customButton10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton10.BackgroundImage = global::QiPOS.Properties.Resources._40_dollar;
            this.customButton10.CornerRadius = 50;
            this.customButton10.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton10.Location = new System.Drawing.Point(312, 13);
            this.customButton10.Margin = new System.Windows.Forms.Padding(0);
            this.customButton10.Name = "customButton10";
            this.customButton10.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton10.Size = new System.Drawing.Size(144, 88);
            this.customButton10.TabIndex = 128;
            this.customButton10.Text = "40";
            this.customButton10.Click += new System.EventHandler(this.Button40_Click);
            // 
            // customButton9
            // 
            this.customButton9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton9.BackgroundImage = global::QiPOS.Properties.Resources._50_dollar;
            this.customButton9.CornerRadius = 50;
            this.customButton9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton9.Location = new System.Drawing.Point(156, 13);
            this.customButton9.Margin = new System.Windows.Forms.Padding(0);
            this.customButton9.Name = "customButton9";
            this.customButton9.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton9.Size = new System.Drawing.Size(144, 88);
            this.customButton9.TabIndex = 128;
            this.customButton9.Click += new System.EventHandler(this.Button50_Click);
            // 
            // customButton8
            // 
            this.customButton8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton8.BackgroundImage = global::QiPOS.Properties.Resources._20_dollar;
            this.customButton8.CornerRadius = 50;
            this.customButton8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton8.Location = new System.Drawing.Point(156, 109);
            this.customButton8.Margin = new System.Windows.Forms.Padding(0);
            this.customButton8.Name = "customButton8";
            this.customButton8.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton8.Size = new System.Drawing.Size(144, 88);
            this.customButton8.TabIndex = 128;
            this.customButton8.Click += new System.EventHandler(this.Button20_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Controls.Add(this.customButton1, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonPrevSales, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonStats, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ButtonCash, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonProducts, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customButton3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ButtonShortcut, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ButtonSearch, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonTransactions, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customButtonConfig, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.subagentButton, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 669);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 85);
            this.tableLayoutPanel1.TabIndex = 127;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customButton1.Location = new System.Drawing.Point(770, 0);
            this.customButton1.Margin = new System.Windows.Forms.Padding(0);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(150, 40);
            this.customButton1.TabIndex = 130;
            this.customButton1.Text = "Define";
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // ButtonPrevSales
            // 
            this.ButtonPrevSales.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonPrevSales.CornerRadius = 40;
            this.ButtonPrevSales.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPrevSales.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonPrevSales.Location = new System.Drawing.Point(0, 0);
            this.ButtonPrevSales.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPrevSales.Name = "ButtonPrevSales";
            this.ButtonPrevSales.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonPrevSales.Size = new System.Drawing.Size(154, 40);
            this.ButtonPrevSales.TabIndex = 128;
            this.ButtonPrevSales.Text = "Prev Sales";
            this.ButtonPrevSales.Click += new System.EventHandler(this.BtnPrevSale_Click);
            // 
            // ButtonStats
            // 
            this.ButtonStats.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonStats.CornerRadius = 40;
            this.ButtonStats.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonStats.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonStats.Location = new System.Drawing.Point(0, 42);
            this.ButtonStats.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonStats.Name = "ButtonStats";
            this.ButtonStats.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonStats.Size = new System.Drawing.Size(154, 40);
            this.ButtonStats.TabIndex = 128;
            this.ButtonStats.Text = "Statistics";
            this.ButtonStats.Click += new System.EventHandler(this.BtnSum_Click);
            // 
            // ButtonCash
            // 
            this.ButtonCash.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonCash.CornerRadius = 40;
            this.ButtonCash.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCash.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonCash.Location = new System.Drawing.Point(154, 0);
            this.ButtonCash.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonCash.Name = "ButtonCash";
            this.ButtonCash.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonCash.Size = new System.Drawing.Size(152, 40);
            this.ButtonCash.TabIndex = 128;
            this.ButtonCash.Text = "Daily Cash";
            this.ButtonCash.Click += new System.EventHandler(this.BtnCashForm_Click);
            // 
            // ButtonProducts
            // 
            this.ButtonProducts.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonProducts.CornerRadius = 40;
            this.ButtonProducts.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonProducts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonProducts.Location = new System.Drawing.Point(154, 42);
            this.ButtonProducts.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonProducts.Name = "ButtonProducts";
            this.ButtonProducts.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonProducts.Size = new System.Drawing.Size(152, 40);
            this.ButtonProducts.TabIndex = 128;
            this.ButtonProducts.Text = "Products";
            this.ButtonProducts.Click += new System.EventHandler(this.BtnProducts_Click);
            // 
            // customButton3
            // 
            this.customButton3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButton3.CornerRadius = 40;
            this.customButton3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.customButton3.Location = new System.Drawing.Point(462, 42);
            this.customButton3.Margin = new System.Windows.Forms.Padding(0);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton3.Size = new System.Drawing.Size(150, 40);
            this.customButton3.TabIndex = 128;
            this.customButton3.Text = "Receipt";
            this.customButton3.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // ButtonShortcut
            // 
            this.ButtonShortcut.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonShortcut.CornerRadius = 40;
            this.ButtonShortcut.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonShortcut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonShortcut.Location = new System.Drawing.Point(308, 42);
            this.ButtonShortcut.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonShortcut.Name = "ButtonShortcut";
            this.ButtonShortcut.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonShortcut.Size = new System.Drawing.Size(154, 40);
            this.ButtonShortcut.TabIndex = 128;
            this.ButtonShortcut.Text = "Shortcut";
            this.ButtonShortcut.Click += new System.EventHandler(this.BtnShortCut_Click);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonSearch.CornerRadius = 40;
            this.ButtonSearch.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonSearch.Location = new System.Drawing.Point(462, 0);
            this.ButtonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonSearch.Size = new System.Drawing.Size(150, 40);
            this.ButtonSearch.TabIndex = 128;
            this.ButtonSearch.Text = "Search";
            this.ButtonSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // ButtonTransactions
            // 
            this.ButtonTransactions.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonTransactions.CornerRadius = 40;
            this.ButtonTransactions.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonTransactions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonTransactions.Location = new System.Drawing.Point(308, 0);
            this.ButtonTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonTransactions.Name = "ButtonTransactions";
            this.ButtonTransactions.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.ButtonTransactions.Size = new System.Drawing.Size(154, 40);
            this.ButtonTransactions.TabIndex = 128;
            this.ButtonTransactions.Text = "Transactions";
            this.ButtonTransactions.Click += new System.EventHandler(this.BtnTransactions_Click);
            // 
            // customButtonConfig
            // 
            this.customButtonConfig.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customButtonConfig.CornerRadius = 40;
            this.customButtonConfig.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customButtonConfig.Location = new System.Drawing.Point(616, 42);
            this.customButtonConfig.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonConfig.Name = "customButtonConfig";
            this.customButtonConfig.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButtonConfig.Size = new System.Drawing.Size(150, 40);
            this.customButtonConfig.TabIndex = 129;
            this.customButtonConfig.Text = "Config";
            this.customButtonConfig.Click += new System.EventHandler(this.CustomButtonConfig_Click);
            // 
            // subagentButton
            // 
            this.subagentButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.subagentButton.CornerRadius = 40;
            this.subagentButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subagentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.subagentButton.Location = new System.Drawing.Point(616, 0);
            this.subagentButton.Margin = new System.Windows.Forms.Padding(0);
            this.subagentButton.Name = "subagentButton";
            this.subagentButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.subagentButton.Size = new System.Drawing.Size(150, 40);
            this.subagentButton.TabIndex = 128;
            this.subagentButton.Text = "Subagent";
            this.subagentButton.Click += new System.EventHandler(this.BtnSubagent_Click);
            // 
            // BtnQuit
            // 
            this.BtnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnQuit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnQuit.CornerRadius = 45;
            this.BtnQuit.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnQuit.ForeColor = System.Drawing.Color.Black;
            this.BtnQuit.Location = new System.Drawing.Point(1076, 917);
            this.BtnQuit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnQuit.Size = new System.Drawing.Size(150, 40);
            this.BtnQuit.TabIndex = 128;
            this.BtnQuit.Text = "Quit";
            this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // customButton4
            // 
            this.customButton4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton4.CornerRadius = 45;
            this.customButton4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customButton4.Location = new System.Drawing.Point(419, 554);
            this.customButton4.Margin = new System.Windows.Forms.Padding(0);
            this.customButton4.Name = "customButton4";
            this.customButton4.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton4.Size = new System.Drawing.Size(180, 45);
            this.customButton4.TabIndex = 128;
            this.customButton4.Text = "End Sale";
            this.customButton4.Click += new System.EventHandler(this.BtnEndSale_Click);
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // dataGridViewSelectedCellColumn2
            // 
            this.dataGridViewSelectedCellColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewSelectedCellColumn2.HeaderText = "Items";
            this.dataGridViewSelectedCellColumn2.MinimumWidth = 10;
            this.dataGridViewSelectedCellColumn2.Name = "dataGridViewSelectedCellColumn2";
            this.dataGridViewSelectedCellColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedCellColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewSelectedCellColumn3
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewSelectedCellColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewSelectedCellColumn3.HeaderText = "Unit Price";
            this.dataGridViewSelectedCellColumn3.MinimumWidth = 150;
            this.dataGridViewSelectedCellColumn3.Name = "dataGridViewSelectedCellColumn3";
            this.dataGridViewSelectedCellColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedCellColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewSelectedCellColumn3.Width = 150;
            // 
            // dataGridViewSelectedCellColumn4
            // 
            this.dataGridViewSelectedCellColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewSelectedCellColumn4.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewSelectedCellColumn4.HeaderText = "Qty";
            this.dataGridViewSelectedCellColumn4.MinimumWidth = 100;
            this.dataGridViewSelectedCellColumn4.Name = "dataGridViewSelectedCellColumn4";
            this.dataGridViewSelectedCellColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedCellColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewSelectedCellColumn4.Width = 200;
            // 
            // dataGridViewSelectedCellColumn5
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewSelectedCellColumn5.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewSelectedCellColumn5.HeaderText = "GST";
            this.dataGridViewSelectedCellColumn5.MinimumWidth = 150;
            this.dataGridViewSelectedCellColumn5.Name = "dataGridViewSelectedCellColumn5";
            this.dataGridViewSelectedCellColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedCellColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewSelectedCellColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn8.FillWeight = 18F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Total";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 200;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 200;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(7, 972);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(47, 12);
            this.VersionLabel.TabIndex = 129;
            this.VersionLabel.Text = "version";
            // 
            // seq
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.seq.DefaultCellStyle = dataGridViewCellStyle3;
            this.seq.HeaderText = "";
            this.seq.MinimumWidth = 50;
            this.seq.Name = "seq";
            this.seq.ReadOnly = true;
            this.seq.Width = 50;
            // 
            // item
            // 
            this.item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item.HeaderText = "Items";
            this.item.MinimumWidth = 10;
            this.item.Name = "item";
            this.item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // unitPrice
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.unitPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.unitPrice.HeaderText = "Unit Price";
            this.unitPrice.MinimumWidth = 150;
            this.unitPrice.Name = "unitPrice";
            this.unitPrice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unitPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.unitPrice.Width = 150;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.qty.HeaderText = "Qty";
            this.qty.MinimumWidth = 100;
            this.qty.Name = "qty";
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.qty.Width = 200;
            // 
            // gst
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.gst.DefaultCellStyle = dataGridViewCellStyle6;
            this.gst.HeaderText = "GST";
            this.gst.MinimumWidth = 150;
            this.gst.Name = "gst";
            this.gst.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gst.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.gst.Width = 150;
            // 
            // total
            // 
            this.total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle7;
            this.total.FillWeight = 18F;
            this.total.HeaderText = "Total";
            this.total.MinimumWidth = 180;
            this.total.Name = "total";
            this.total.Width = 180;
            // 
            // FrmPos
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1264, 986);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rich);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.pnlDateBar);
            this.Controls.Add(this.pnlCash);
            this.Controls.Add(this.customButton4);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgItemList);
            this.Controls.Add(this.txtCat);
            this.Controls.Add(this.txtAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmPos";
            this.Text = "QI Point Of Sale";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmPos_Activated);
            this.Load += new System.EventHandler(this.FrmPos_Load);
            this.Shown += new System.EventHandler(this.FrmPos_Shown);
            this.TextChanged += new System.EventHandler(this.FrmPos_TextChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmPos_MouseClick);
            this.Resize += new System.EventHandler(this.FrmPos_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.pnlCash.ResumeLayout(false);
            this.pnlCash.PerformLayout();
            this.pnlDateBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void customButton1_Click(object sender, EventArgs e)
        {
            FrmDefine defineForm = new FrmDefine();
            defineForm.ShowDialog();
        }
    }

}