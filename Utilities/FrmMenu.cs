using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmMenu : Form
    {
        #region stuff

        private readonly IContainer components;
        private Button BtnS7;
        private Button BtnS6;
        private Button BtnS5;
        private Button BtnS4;
        private Button BtnS3;
        private Button BtnS2;
        private Button BtnS1;
        private Button BtnS8;
        private Button BtnS21;
        private Button BtnS20;
        private Button BtnS19;
        private Button BtnS18;
        private Button BtnS17;
        private Button BtnS16;
        private Button BtnS15;
        private Button BtnS14;
        private Button BtnS13;
        private Button BtnS12;
        private Button BtnS11;
        private Button BtnS10;
        private Button BtnS9;
        private Button BtnS24;
        private Button BtnS23;
        private Button BtnS22;
        private Label lblQty;
        private RadioButton rbtMulti;
        private RadioButton rbtSingle;
        private Button[] BtnMenu;
        private readonly string menu_Btn;
        private readonly bool isMenu;
        private string curStockId;
        private TableLayoutPanel gridLayout;
        private Panel panel1;
        private TableLayoutPanel SplitTableLayout;
        private CustomButton customButton1;
        private CustomButton BtnDefine;
        private int curQty;

        #endregion stuff


        public FrmMenu(string in_menu_Btn)
        {
            this.components = null;
            this.InitializeComponent();
            Initiate();
            this.menu_Btn = in_menu_Btn;
            this.isMenu = false;
        }

        public FrmMenu(string in_menu_Btn, bool in_isMenu)
        {
            this.components = null;
            this.InitializeComponent();
            Initiate();
            this.menu_Btn = in_menu_Btn;
            this.isMenu = in_isMenu;
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
            this.BtnS24 = new System.Windows.Forms.Button();
            this.BtnS23 = new System.Windows.Forms.Button();
            this.BtnS22 = new System.Windows.Forms.Button();
            this.BtnS21 = new System.Windows.Forms.Button();
            this.BtnS20 = new System.Windows.Forms.Button();
            this.BtnS19 = new System.Windows.Forms.Button();
            this.BtnS18 = new System.Windows.Forms.Button();
            this.BtnS17 = new System.Windows.Forms.Button();
            this.BtnS16 = new System.Windows.Forms.Button();
            this.BtnS15 = new System.Windows.Forms.Button();
            this.BtnS14 = new System.Windows.Forms.Button();
            this.BtnS13 = new System.Windows.Forms.Button();
            this.BtnS12 = new System.Windows.Forms.Button();
            this.BtnS11 = new System.Windows.Forms.Button();
            this.BtnS10 = new System.Windows.Forms.Button();
            this.BtnS9 = new System.Windows.Forms.Button();
            this.BtnS8 = new System.Windows.Forms.Button();
            this.BtnS7 = new System.Windows.Forms.Button();
            this.BtnS6 = new System.Windows.Forms.Button();
            this.BtnS5 = new System.Windows.Forms.Button();
            this.BtnS4 = new System.Windows.Forms.Button();
            this.BtnS3 = new System.Windows.Forms.Button();
            this.BtnS2 = new System.Windows.Forms.Button();
            this.BtnS1 = new System.Windows.Forms.Button();
            this.lblQty = new System.Windows.Forms.Label();
            this.rbtMulti = new System.Windows.Forms.RadioButton();
            this.rbtSingle = new System.Windows.Forms.RadioButton();
            this.gridLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SplitTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.customButton1 = new QiPOS.CustomButton();
            this.BtnDefine = new QiPOS.CustomButton();
            this.gridLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SplitTableLayout.SuspendLayout();
            this.SuspendLayout();

            //
            // BtnS24
            //
            this.BtnS24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS24.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS24.Location = new System.Drawing.Point(798, 384);
            this.BtnS24.Name = "BtnS24";
            this.BtnS24.Size = new System.Drawing.Size(154, 122);
            this.BtnS24.TabIndex = 22;
            this.BtnS24.UseVisualStyleBackColor = true;
            this.BtnS24.Click += new System.EventHandler(this.BtnS24_Click);

            //
            // BtnS23
            //
            this.BtnS23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS23.Location = new System.Drawing.Point(639, 384);
            this.BtnS23.Name = "BtnS23";
            this.BtnS23.Size = new System.Drawing.Size(153, 122);
            this.BtnS23.TabIndex = 21;
            this.BtnS23.UseVisualStyleBackColor = true;
            this.BtnS23.Click += new System.EventHandler(this.BtnS23_Click);

            //
            // BtnS22
            //
            this.BtnS22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS22.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS22.Location = new System.Drawing.Point(480, 384);
            this.BtnS22.Name = "BtnS22";
            this.BtnS22.Size = new System.Drawing.Size(153, 122);
            this.BtnS22.TabIndex = 20;
            this.BtnS22.UseVisualStyleBackColor = true;
            this.BtnS22.Click += new System.EventHandler(this.BtnS22_Click);

            //
            // BtnS21
            //
            this.BtnS21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS21.Location = new System.Drawing.Point(321, 384);
            this.BtnS21.Name = "BtnS21";
            this.BtnS21.Size = new System.Drawing.Size(153, 122);
            this.BtnS21.TabIndex = 19;
            this.BtnS21.UseVisualStyleBackColor = true;
            this.BtnS21.Click += new System.EventHandler(this.BtnS21_Click);

            //
            // BtnS20
            //
            this.BtnS20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS20.Location = new System.Drawing.Point(162, 384);
            this.BtnS20.Name = "BtnS20";
            this.BtnS20.Size = new System.Drawing.Size(153, 122);
            this.BtnS20.TabIndex = 18;
            this.BtnS20.UseVisualStyleBackColor = true;
            this.BtnS20.Click += new System.EventHandler(this.BtnS20_Click);

            //
            // BtnS19
            //
            this.BtnS19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS19.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS19.Location = new System.Drawing.Point(3, 384);
            this.BtnS19.Name = "BtnS19";
            this.BtnS19.Size = new System.Drawing.Size(153, 122);
            this.BtnS19.TabIndex = 17;
            this.BtnS19.UseVisualStyleBackColor = true;
            this.BtnS19.Click += new System.EventHandler(this.BtnS19_Click);

            //
            // BtnS18
            //
            this.BtnS18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS18.Location = new System.Drawing.Point(798, 257);
            this.BtnS18.Name = "BtnS18";
            this.BtnS18.Size = new System.Drawing.Size(154, 121);
            this.BtnS18.TabIndex = 16;
            this.BtnS18.UseVisualStyleBackColor = true;
            this.BtnS18.Click += new System.EventHandler(this.BtnS18_Click);

            //
            // BtnS17
            //
            this.BtnS17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS17.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS17.Location = new System.Drawing.Point(639, 257);
            this.BtnS17.Name = "BtnS17";
            this.BtnS17.Size = new System.Drawing.Size(153, 121);
            this.BtnS17.TabIndex = 15;
            this.BtnS17.UseVisualStyleBackColor = true;
            this.BtnS17.Click += new System.EventHandler(this.BtnS17_Click);

            //
            // BtnS16
            //
            this.BtnS16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS16.Location = new System.Drawing.Point(480, 257);
            this.BtnS16.Name = "BtnS16";
            this.BtnS16.Size = new System.Drawing.Size(153, 121);
            this.BtnS16.TabIndex = 14;
            this.BtnS16.UseVisualStyleBackColor = true;
            this.BtnS16.Click += new System.EventHandler(this.BtnS16_Click);

            //
            // BtnS15
            //
            this.BtnS15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS15.Location = new System.Drawing.Point(321, 257);
            this.BtnS15.Name = "BtnS15";
            this.BtnS15.Size = new System.Drawing.Size(153, 121);
            this.BtnS15.TabIndex = 13;
            this.BtnS15.UseVisualStyleBackColor = true;
            this.BtnS15.Click += new System.EventHandler(this.BtnS15_Click);

            //
            // BtnS14
            //
            this.BtnS14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS14.Location = new System.Drawing.Point(162, 257);
            this.BtnS14.Name = "BtnS14";
            this.BtnS14.Size = new System.Drawing.Size(153, 121);
            this.BtnS14.TabIndex = 12;
            this.BtnS14.UseVisualStyleBackColor = true;
            this.BtnS14.Click += new System.EventHandler(this.BtnS14_Click);

            //
            // BtnS13
            //
            this.BtnS13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS13.Location = new System.Drawing.Point(3, 257);
            this.BtnS13.Name = "BtnS13";
            this.BtnS13.Size = new System.Drawing.Size(153, 121);
            this.BtnS13.TabIndex = 11;
            this.BtnS13.UseVisualStyleBackColor = true;
            this.BtnS13.Click += new System.EventHandler(this.BtnS13_Click);

            //
            // BtnS12
            //
            this.BtnS12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS12.Location = new System.Drawing.Point(798, 130);
            this.BtnS12.Name = "BtnS12";
            this.BtnS12.Size = new System.Drawing.Size(154, 121);
            this.BtnS12.TabIndex = 10;
            this.BtnS12.UseVisualStyleBackColor = true;
            this.BtnS12.Click += new System.EventHandler(this.BtnS12_Click);

            //
            // BtnS11
            //
            this.BtnS11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS11.Location = new System.Drawing.Point(639, 130);
            this.BtnS11.Name = "BtnS11";
            this.BtnS11.Size = new System.Drawing.Size(153, 121);
            this.BtnS11.TabIndex = 9;
            this.BtnS11.UseVisualStyleBackColor = true;
            this.BtnS11.Click += new System.EventHandler(this.BtnS11_Click);

            //
            // BtnS10
            //
            this.BtnS10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS10.Location = new System.Drawing.Point(480, 130);
            this.BtnS10.Name = "BtnS10";
            this.BtnS10.Size = new System.Drawing.Size(153, 121);
            this.BtnS10.TabIndex = 8;
            this.BtnS10.UseVisualStyleBackColor = true;
            this.BtnS10.Click += new System.EventHandler(this.BtnS10_Click);

            //
            // BtnS9
            //
            this.BtnS9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS9.Location = new System.Drawing.Point(321, 130);
            this.BtnS9.Name = "BtnS9";
            this.BtnS9.Size = new System.Drawing.Size(153, 121);
            this.BtnS9.TabIndex = 7;
            this.BtnS9.UseVisualStyleBackColor = true;
            this.BtnS9.Click += new System.EventHandler(this.BtnS9_Click);

            //
            // BtnS8
            //
            this.BtnS8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS8.Location = new System.Drawing.Point(162, 130);
            this.BtnS8.Name = "BtnS8";
            this.BtnS8.Size = new System.Drawing.Size(153, 121);
            this.BtnS8.TabIndex = 6;
            this.BtnS8.UseVisualStyleBackColor = true;
            this.BtnS8.Click += new System.EventHandler(this.BtnS8_Click);

            //
            // BtnS7
            //
            this.BtnS7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS7.Location = new System.Drawing.Point(3, 130);
            this.BtnS7.Name = "BtnS7";
            this.BtnS7.Size = new System.Drawing.Size(153, 121);
            this.BtnS7.TabIndex = 5;
            this.BtnS7.UseVisualStyleBackColor = true;
            this.BtnS7.Click += new System.EventHandler(this.BtnS7_Click);

            //
            // BtnS6
            //
            this.BtnS6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS6.Location = new System.Drawing.Point(798, 3);
            this.BtnS6.Name = "BtnS6";
            this.BtnS6.Size = new System.Drawing.Size(154, 121);
            this.BtnS6.TabIndex = 4;
            this.BtnS6.UseVisualStyleBackColor = true;
            this.BtnS6.Click += new System.EventHandler(this.BtnS6_Click);

            //
            // BtnS5
            //
            this.BtnS5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS5.Location = new System.Drawing.Point(639, 3);
            this.BtnS5.Name = "BtnS5";
            this.BtnS5.Size = new System.Drawing.Size(153, 121);
            this.BtnS5.TabIndex = 3;
            this.BtnS5.UseVisualStyleBackColor = true;
            this.BtnS5.Click += new System.EventHandler(this.BtnS5_Click);

            //
            // BtnS4
            //
            this.BtnS4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS4.Location = new System.Drawing.Point(480, 3);
            this.BtnS4.Name = "BtnS4";
            this.BtnS4.Size = new System.Drawing.Size(153, 121);
            this.BtnS4.TabIndex = 3;
            this.BtnS4.UseVisualStyleBackColor = true;
            this.BtnS4.Click += new System.EventHandler(this.BtnS4_Click);

            //
            // BtnS3
            //
            this.BtnS3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS3.Location = new System.Drawing.Point(321, 3);
            this.BtnS3.Name = "BtnS3";
            this.BtnS3.Size = new System.Drawing.Size(153, 121);
            this.BtnS3.TabIndex = 2;
            this.BtnS3.UseVisualStyleBackColor = true;
            this.BtnS3.Click += new System.EventHandler(this.BtnS3_Click);

            //
            // BtnS2
            //
            this.BtnS2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS2.Location = new System.Drawing.Point(162, 3);
            this.BtnS2.Name = "BtnS2";
            this.BtnS2.Size = new System.Drawing.Size(153, 121);
            this.BtnS2.TabIndex = 1;
            this.BtnS2.UseVisualStyleBackColor = true;
            this.BtnS2.Click += new System.EventHandler(this.BtnS2_Click);

            //
            // BtnS1
            //
            this.BtnS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnS1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnS1.Location = new System.Drawing.Point(3, 3);
            this.BtnS1.Name = "BtnS1";
            this.BtnS1.Size = new System.Drawing.Size(153, 121);
            this.BtnS1.TabIndex = 0;
            this.BtnS1.UseVisualStyleBackColor = true;
            this.BtnS1.Click += new System.EventHandler(this.BtnS1_Click);

            //
            // lblQty
            //
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblQty.Location = new System.Drawing.Point(18, 12);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(215, 55);
            this.lblQty.TabIndex = 6;
            this.lblQty.Text = "Quantity:";
            this.lblQty.Visible = false;
            this.lblQty.Click += new System.EventHandler(this.LblQty_Click);

            //
            // rbtMulti
            //
            this.rbtMulti.AutoSize = true;
            this.rbtMulti.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtMulti.Location = new System.Drawing.Point(142, 68);
            this.rbtMulti.Name = "rbtMulti";
            this.rbtMulti.Size = new System.Drawing.Size(77, 29);
            this.rbtMulti.TabIndex = 1;
            this.rbtMulti.TabStop = true;
            this.rbtMulti.Text = "Multi";
            this.rbtMulti.UseVisualStyleBackColor = true;

            //
            // rbtSingle
            //
            this.rbtSingle.AutoSize = true;
            this.rbtSingle.Checked = true;
            this.rbtSingle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSingle.Location = new System.Drawing.Point(51, 68);
            this.rbtSingle.Name = "rbtSingle";
            this.rbtSingle.Size = new System.Drawing.Size(85, 29);
            this.rbtSingle.TabIndex = 0;
            this.rbtSingle.TabStop = true;
            this.rbtSingle.Text = "Single";
            this.rbtSingle.UseVisualStyleBackColor = true;

            //
            // gridLayout
            //
            this.gridLayout.ColumnCount = 6;
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.gridLayout.Controls.Add(this.BtnS24, 5, 3);
            this.gridLayout.Controls.Add(this.BtnS1, 0, 0);
            this.gridLayout.Controls.Add(this.BtnS23, 4, 3);
            this.gridLayout.Controls.Add(this.BtnS15, 2, 2);
            this.gridLayout.Controls.Add(this.BtnS22, 3, 3);
            this.gridLayout.Controls.Add(this.BtnS2, 1, 0);
            this.gridLayout.Controls.Add(this.BtnS21, 2, 3);
            this.gridLayout.Controls.Add(this.BtnS8, 1, 1);
            this.gridLayout.Controls.Add(this.BtnS20, 1, 3);
            this.gridLayout.Controls.Add(this.BtnS3, 2, 0);
            this.gridLayout.Controls.Add(this.BtnS19, 0, 3);
            this.gridLayout.Controls.Add(this.BtnS4, 3, 0);
            this.gridLayout.Controls.Add(this.BtnS5, 4, 0);
            this.gridLayout.Controls.Add(this.BtnS6, 5, 0);
            this.gridLayout.Controls.Add(this.BtnS18, 5, 2);
            this.gridLayout.Controls.Add(this.BtnS7, 0, 1);
            this.gridLayout.Controls.Add(this.BtnS17, 4, 2);
            this.gridLayout.Controls.Add(this.BtnS9, 2, 1);
            this.gridLayout.Controls.Add(this.BtnS16, 3, 2);
            this.gridLayout.Controls.Add(this.BtnS10, 3, 1);
            this.gridLayout.Controls.Add(this.BtnS14, 1, 2);
            this.gridLayout.Controls.Add(this.BtnS11, 4, 1);
            this.gridLayout.Controls.Add(this.BtnS13, 0, 2);
            this.gridLayout.Controls.Add(this.BtnS12, 5, 1);
            this.gridLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLayout.Location = new System.Drawing.Point(3, 3);
            this.gridLayout.Name = "gridLayout";
            this.gridLayout.RowCount = 4;
            this.gridLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.gridLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.gridLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.gridLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.gridLayout.Size = new System.Drawing.Size(955, 509);
            this.gridLayout.TabIndex = 8;

            //
            // panel1
            //
            this.panel1.Controls.Add(this.BtnDefine);
            this.panel1.Controls.Add(this.customButton1);
            this.panel1.Controls.Add(this.rbtMulti);
            this.panel1.Controls.Add(this.lblQty);
            this.panel1.Controls.Add(this.rbtSingle);
            this.panel1.Location = new System.Drawing.Point(3, 518);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 100);
            this.panel1.TabIndex = 9;

            //
            // SplitTableLayout
            //
            this.SplitTableLayout.ColumnCount = 1;
            this.SplitTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SplitTableLayout.Controls.Add(this.gridLayout, 0, 0);
            this.SplitTableLayout.Controls.Add(this.panel1, 0, 1);
            this.SplitTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitTableLayout.Location = new System.Drawing.Point(0, 0);
            this.SplitTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.SplitTableLayout.Name = "SplitTableLayout";
            this.SplitTableLayout.RowCount = 2;
            this.SplitTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SplitTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SplitTableLayout.Size = new System.Drawing.Size(961, 621);
            this.SplitTableLayout.TabIndex = 10;

            //
            // customButton1
            //
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(654, 20);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(200, 50);
            this.customButton1.TabIndex = 142;
            this.customButton1.Text = "Close";
            this.customButton1.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // BtnDefine
            //
            this.BtnDefine.BackColor = System.Drawing.SystemColors.Control;
            this.BtnDefine.CornerRadius = 40;
            this.BtnDefine.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDefine.ForeColor = System.Drawing.Color.Blue;
            this.BtnDefine.Location = new System.Drawing.Point(412, 20);
            this.BtnDefine.Name = "BtnDefine";
            this.BtnDefine.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.BtnDefine.Size = new System.Drawing.Size(221, 50);
            this.BtnDefine.TabIndex = 142;
            this.BtnDefine.Text = "Define Content";
            this.BtnDefine.Click += new System.EventHandler(this.BtnDefine_Click);

            //
            // FrmMenu
            //
            this.ClientSize = new System.Drawing.Size(961, 621);
            this.Controls.Add(this.SplitTableLayout);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMenu";
            this.Text = "Sub Menu";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.Click += new System.EventHandler(this.FrmMenu_Click);
            this.gridLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SplitTableLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion components

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            this.DisplayButtonText();
            Connect connect = new Connect();
            string queryStr = "SELECT * FROM pos_look_up WHERE  menu_Btn='" + this.menu_Btn + "' AND short_Btn!='' AND multi_select=1";
            connect.QueryTable(queryStr);
            if (connect.aTable.Rows.Count > 0)
            {
                this.rbtMulti.Checked = true;
                if (this.isMenu)
                    this.lblQty.Visible = true;
            }
            this.curStockId = "";
            this.curQty = 1;
            if (!this.isMenu)
                return;
            this.BtnDefine.Visible = false;
        }

        private void Initiate()
        {
            this.BtnMenu = new Button[30];
            this.BtnMenu[0] = this.BtnS1;
            this.BtnMenu[1] = this.BtnS2;
            this.BtnMenu[2] = this.BtnS3;
            this.BtnMenu[3] = this.BtnS4;
            this.BtnMenu[4] = this.BtnS5;
            this.BtnMenu[5] = this.BtnS6;
            this.BtnMenu[6] = this.BtnS7;
            this.BtnMenu[7] = this.BtnS8;
            this.BtnMenu[8] = this.BtnS9;
            this.BtnMenu[9] = this.BtnS10;
            this.BtnMenu[10] = this.BtnS11;
            this.BtnMenu[11] = this.BtnS12;
            this.BtnMenu[12] = this.BtnS13;
            this.BtnMenu[13] = this.BtnS14;
            this.BtnMenu[14] = this.BtnS15;
            this.BtnMenu[15] = this.BtnS16;
            this.BtnMenu[16] = this.BtnS17;
            this.BtnMenu[17] = this.BtnS18;
            this.BtnMenu[18] = this.BtnS19;
            this.BtnMenu[19] = this.BtnS20;
            this.BtnMenu[20] = this.BtnS21;
            this.BtnMenu[21] = this.BtnS22;
            this.BtnMenu[22] = this.BtnS23;
            this.BtnMenu[23] = this.BtnS24;
        }

        private void DisplayButtonText()
        {
            Connect connect = new Connect();
            string queryStr = "SELECT * FROM pos_look_up WHERE  menu_Btn='" + this.menu_Btn + "' AND short_Btn <>''";
            connect.QueryTable(queryStr);
            for (int index1 = 0; index1 < connect.aTable.Rows.Count; ++index1)
            {
                for (int index2 = 0; index2 < 24; ++index2)
                {
                    if (this.BtnMenu[index2].Name == connect.aTable.Rows[index1]["short_Btn"].ToString())
                        this.BtnMenu[index2].Text = connect.aTable.Rows[index1]["commnets"].ToString();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void BtnDefine_Click(object sender, EventArgs e)
        {
            if (this.BtnDefine.Text == "Display Name")
            {
                this.BtnDefine.Text = "Define Content";
                this.BtnDefine.ForeColor = Color.FromArgb(192, 64, 0);
            }
            else
            {
                this.BtnDefine.Text = "Display Name";
                this.BtnDefine.ForeColor = Color.FromArgb(0, 0, 192);
            }
        }

        private void SubMenuButton(string whichButton)
        {
            if (this.isMenu)
            {
                Connect connect1 = new Connect();
                Connect connect2 = new Connect();
                string queryStr1 = "SELECT * FROM pos_look_up WHERE  menu_Btn='" + this.menu_Btn + "' AND short_Btn='" + whichButton + "'";
                connect1.QueryTable(queryStr1);
                if (connect1.aTable.Rows.Count == 1)
                {
                    int num2 = (int)connect1.aTable.Rows[0]["stock_id"];
                    if (num2 == 0)
                    {
                        if (!this.rbtSingle.Checked)
                            return;
                        this.Owner.Tag = ("A" + connect1.aTable.Rows[0]["acc_number"].ToString());
                        this.DialogResult = DialogResult.Yes;
                    }
                    else if (this.rbtSingle.Checked)
                    {
                        this.Owner.Tag = ("S" + connect1.aTable.Rows[0]["stock_id"].ToString());
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        this.Owner.Text = "S" + connect1.aTable.Rows[0]["stock_id"].ToString();
                        if (connect1.aTable.Rows[0]["stock_id"].ToString() != this.curStockId)
                        {
                            this.lblQty.Text = "Quantity: 1";
                            this.curStockId = connect1.aTable.Rows[0]["stock_id"].ToString();
                            this.curQty = 1;
                        }
                        else
                        {
                            ++this.curQty;
                            this.lblQty.Text = "Quantity: " + this.curQty;
                        }
                    }
                }
                else
                {
                    if (connect1.aTable.Rows.Count <= 1)
                        return;
                    string queryStr2 = "SELECT * FROM pos_look_up WHERE  menu_Btn='" + this.menu_Btn + "' AND short_Btn='" + whichButton + "' AND dayofweek=" + this.DigitalWeek();
                    connect2.QueryTable(queryStr2);
                    if (this.rbtSingle.Checked)
                    {
                        this.Owner.Tag = ("S" + connect2.aTable.Rows[0]["stock_id"].ToString());
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        this.Owner.Text = "S" + connect1.aTable.Rows[0]["stock_id"].ToString();
                        if (connect1.aTable.Rows[0]["stock_id"].ToString() != this.curStockId)
                        {
                            this.lblQty.Text = "Quantity: 1";
                            this.curStockId = connect1.aTable.Rows[0]["stock_id"].ToString();
                            this.curQty = 1;
                        }
                        else
                        {
                            ++this.curQty;
                            this.lblQty.Text = "Quantity: " + this.curQty;
                        }
                    }
                }
            }
            else if (this.BtnDefine.Text == "Display Name")
            {
                FrmMenuName frmMenuName = new FrmMenuName(this.menu_Btn, whichButton);
                this.AddOwnedForm(frmMenuName);
                if (frmMenuName.ShowDialog() == DialogResult.Yes)
                    this.DisplayButtonText();
            }
            else
            {
                int num = 0;
                if (this.rbtMulti.Checked && whichButton == "BtnS1")
                    num = 1;
                FrmDefineShortcut frmDefineShortcut = new FrmDefineShortcut(this.menu_Btn, whichButton, num);
                this.AddOwnedForm(frmDefineShortcut);
                if ((frmDefineShortcut).ShowDialog() == DialogResult.Yes)
                    this.DisplayButtonText();
            }
        }

        private int DigitalWeek()
        {
            DateTime now = DateTime.Today;
            return (int)now.DayOfWeek;
        }

        private void BtnS1_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS1");
        }

        private void BtnS2_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS2");
        }

        private void BtnS3_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS3");
        }

        private void BtnS4_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS4");
        }

        private void BtnS5_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS5");
        }

        private void BtnS6_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS6");
        }

        private void BtnS7_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS7");
        }

        private void BtnS8_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS8");
        }

        private void BtnS9_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS9");
        }

        private void BtnS10_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS10");
        }

        private void BtnS11_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS11");
        }

        private void BtnS12_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS12");
        }

        private void BtnS13_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS13");
        }

        private void BtnS14_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS14");
        }

        private void BtnS15_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS15");
        }

        private void BtnS16_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS16");
        }

        private void BtnS17_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS17");
        }

        private void BtnS18_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS18");
        }

        private void BtnS19_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS19");
        }

        private void BtnS20_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS20");
        }

        private void BtnS21_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS21");
        }

        private void BtnS22_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS22");
        }

        private void BtnS23_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS23");
        }

        private void BtnS24_Click(object sender, EventArgs e)
        {
            this.SubMenuButton("BtnS24");
        }

        private void FrmMenu_Click(object sender, EventArgs e)
        {
            if (!this.isMenu)
                return;
            this.Close();
            base.Dispose();
        }

        private void LblQty_Click(object sender, EventArgs e)
        {
            if (!this.isMenu)
                return;
            this.Close();
            base.Dispose();
        }
    }
}

