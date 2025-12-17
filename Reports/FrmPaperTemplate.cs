using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmPaperTemplate : Form
    {
        #region template

        private readonly IContainer components;
        private ComboBox cbxCol1;
        private ComboBox cbxCol2;
        private ComboBox cbxCol3;
        private ComboBox cbxCol4;
        private ComboBox cbxCol5;
        private Label lblPrice;
        private DataGridView dgContent;
        private Label lblPrice2;
        private ComboBox cbxCol25;
        private ComboBox cbxCol24;
        private ComboBox cbxCol23;
        private ComboBox cbxCol22;
        private ComboBox cbxCol21;
        private Label lblPublish;
        private Label lblPublish1;
        private DataGridView dgContent1;
        private Label lblTitle;
        private ComboBox cbxTempName;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn publish;
        private DataGridViewTextBoxColumn price;
        private DataGridViewTextBoxColumn commi;
        private DataGridViewTextBoxColumn p1;
        private DataGridViewTextBoxColumn p2;
        private DataGridViewTextBoxColumn p3;
        private DataGridViewTextBoxColumn p4;
        private DataGridViewTextBoxColumn p5;
        private DataGridViewTextBoxColumn pid1;
        private DataGridViewTextBoxColumn pid2;
        private DataGridViewTextBoxColumn pid3;
        private DataGridViewTextBoxColumn pid4;
        private DataGridViewTextBoxColumn pid5;
        private DataGridViewTextBoxColumn publish2;
        private DataGridViewTextBoxColumn price2;
        private DataGridViewTextBoxColumn fee;
        private DataGridViewTextBoxColumn p12;
        private DataGridViewTextBoxColumn p22;
        private DataGridViewTextBoxColumn p32;
        private DataGridViewTextBoxColumn p42;
        private DataGridViewTextBoxColumn p52;
        private DataGridViewTextBoxColumn pid12;
        private DataGridViewTextBoxColumn pid22;
        private DataGridViewTextBoxColumn pid32;
        private DataGridViewTextBoxColumn pid42;
        private DataGridViewTextBoxColumn pid52;
        private string queryStr;
        private string ValueStr;
        private Connect connDB;
        private string temp_id;
        private CustomButton closeButton;
        private CustomButton deleteButton;
        private CustomButton saveButton;
        private readonly ComboBox[] titleBox;
        #endregion

        public FrmPaperTemplate()
        {
            this.components = (IContainer)null;
            this.titleBox = new ComboBox[10];
            this.InitializeComponent();
            this.Initiate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        #region init components
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbxCol1 = new System.Windows.Forms.ComboBox();
            this.cbxCol2 = new System.Windows.Forms.ComboBox();
            this.cbxCol3 = new System.Windows.Forms.ComboBox();
            this.cbxCol4 = new System.Windows.Forms.ComboBox();
            this.cbxCol5 = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.dgContent = new System.Windows.Forms.DataGridView();
            this.publish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPrice2 = new System.Windows.Forms.Label();
            this.cbxCol25 = new System.Windows.Forms.ComboBox();
            this.cbxCol24 = new System.Windows.Forms.ComboBox();
            this.cbxCol23 = new System.Windows.Forms.ComboBox();
            this.cbxCol22 = new System.Windows.Forms.ComboBox();
            this.cbxCol21 = new System.Windows.Forms.ComboBox();
            this.lblPublish = new System.Windows.Forms.Label();
            this.lblPublish1 = new System.Windows.Forms.Label();
            this.dgContent1 = new System.Windows.Forms.DataGridView();
            this.publish2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbxTempName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new QiPOS.CustomButton();
            this.deleteButton = new QiPOS.CustomButton();
            this.saveButton = new QiPOS.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContent1)).BeginInit();
            this.SuspendLayout();

            //
            // cbxCol1
            //
            this.cbxCol1.FormattingEnabled = true;
            this.cbxCol1.Location = new System.Drawing.Point(358, 77);
            this.cbxCol1.Name = "cbxCol1";
            this.cbxCol1.Size = new System.Drawing.Size(130, 32);
            this.cbxCol1.TabIndex = 0;

            //
            // cbxCol2
            //
            this.cbxCol2.FormattingEnabled = true;
            this.cbxCol2.Location = new System.Drawing.Point(488, 77);
            this.cbxCol2.Name = "cbxCol2";
            this.cbxCol2.Size = new System.Drawing.Size(130, 32);
            this.cbxCol2.TabIndex = 1;

            //
            // cbxCol3
            //
            this.cbxCol3.FormattingEnabled = true;
            this.cbxCol3.Location = new System.Drawing.Point(618, 77);
            this.cbxCol3.Name = "cbxCol3";
            this.cbxCol3.Size = new System.Drawing.Size(130, 32);
            this.cbxCol3.TabIndex = 2;

            //
            // cbxCol4
            //
            this.cbxCol4.FormattingEnabled = true;
            this.cbxCol4.Location = new System.Drawing.Point(748, 77);
            this.cbxCol4.Name = "cbxCol4";
            this.cbxCol4.Size = new System.Drawing.Size(130, 32);
            this.cbxCol4.TabIndex = 3;

            //
            // cbxCol5
            //
            this.cbxCol5.FormattingEnabled = true;
            this.cbxCol5.Location = new System.Drawing.Point(878, 77);
            this.cbxCol5.Name = "cbxCol5";
            this.cbxCol5.Size = new System.Drawing.Size(130, 32);
            this.cbxCol5.TabIndex = 4;

            //
            // lblPrice
            //
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(186, 80);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(53, 24);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Price";

            //
            // dgContent
            //
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.LightBlue;
            this.dgContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle34;
            this.dgContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgContent.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgContent.ColumnHeadersVisible = false;
            this.dgContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.publish,
            this.price,
            this.commi,
            this.p1,
            this.p2,
            this.p3,
            this.p4,
            this.p5,
            this.pid1,
            this.pid2,
            this.pid3,
            this.pid4,
            this.pid5});
            this.dgContent.Location = new System.Drawing.Point(10, 111);
            this.dgContent.Name = "dgContent";
            this.dgContent.RowTemplate.Height = 30;
            this.dgContent.Size = new System.Drawing.Size(1000, 210);
            this.dgContent.TabIndex = 6;
            this.dgContent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgContent_CellDoubleClick);

            //
            // publish
            //
            this.publish.FillWeight = 28.90199F;
            this.publish.HeaderText = "publish";
            this.publish.Name = "publish";

            //
            // price
            //
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle35.Format = "C2";
            dataGridViewCellStyle35.NullValue = null;
            this.price.DefaultCellStyle = dataGridViewCellStyle35;
            this.price.FillWeight = 14.9493F;
            this.price.HeaderText = "price";
            this.price.Name = "price";

            //
            // commi
            //
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle36.Format = "C4";
            dataGridViewCellStyle36.NullValue = null;
            this.commi.DefaultCellStyle = dataGridViewCellStyle36;
            this.commi.FillWeight = 21F;
            this.commi.HeaderText = "fee";
            this.commi.Name = "commi";

            //
            // p1
            //
            this.p1.FillWeight = 27.7401F;
            this.p1.HeaderText = "p1";
            this.p1.Name = "p1";
            this.p1.ReadOnly = true;

            //
            // p2
            //
            this.p2.FillWeight = 27.7401F;
            this.p2.HeaderText = "p2";
            this.p2.Name = "p2";
            this.p2.ReadOnly = true;

            //
            // p3
            //
            this.p3.FillWeight = 27.7401F;
            this.p3.HeaderText = "p3";
            this.p3.Name = "p3";
            this.p3.ReadOnly = true;

            //
            // p4
            //
            this.p4.FillWeight = 27.7401F;
            this.p4.HeaderText = "p4";
            this.p4.Name = "p4";
            this.p4.ReadOnly = true;

            //
            // p5
            //
            this.p5.FillWeight = 27.7401F;
            this.p5.HeaderText = "p5";
            this.p5.Name = "p5";
            this.p5.ReadOnly = true;

            //
            // pid1
            //
            this.pid1.HeaderText = "pid1";
            this.pid1.Name = "pid1";
            this.pid1.Visible = false;

            //
            // pid2
            //
            this.pid2.HeaderText = "pid2";
            this.pid2.Name = "pid2";
            this.pid2.Visible = false;

            //
            // pid3
            //
            this.pid3.HeaderText = "pid3";
            this.pid3.Name = "pid3";
            this.pid3.Visible = false;

            //
            // pid4
            //
            this.pid4.HeaderText = "pid4";
            this.pid4.Name = "pid4";
            this.pid4.Visible = false;

            //
            // pid5
            //
            this.pid5.HeaderText = "pid5";
            this.pid5.Name = "pid5";
            this.pid5.Visible = false;

            //
            // lblPrice2
            //
            this.lblPrice2.AutoSize = true;
            this.lblPrice2.Location = new System.Drawing.Point(186, 339);
            this.lblPrice2.Name = "lblPrice2";
            this.lblPrice2.Size = new System.Drawing.Size(53, 24);
            this.lblPrice2.TabIndex = 12;
            this.lblPrice2.Text = "Price";

            //
            // cbxCol25
            //
            this.cbxCol25.FormattingEnabled = true;
            this.cbxCol25.Location = new System.Drawing.Point(877, 336);
            this.cbxCol25.Name = "cbxCol25";
            this.cbxCol25.Size = new System.Drawing.Size(130, 32);
            this.cbxCol25.TabIndex = 11;

            //
            // cbxCol24
            //
            this.cbxCol24.FormattingEnabled = true;
            this.cbxCol24.Location = new System.Drawing.Point(747, 336);
            this.cbxCol24.Name = "cbxCol24";
            this.cbxCol24.Size = new System.Drawing.Size(130, 32);
            this.cbxCol24.TabIndex = 10;

            //
            // cbxCol23
            //
            this.cbxCol23.FormattingEnabled = true;
            this.cbxCol23.Location = new System.Drawing.Point(617, 336);
            this.cbxCol23.Name = "cbxCol23";
            this.cbxCol23.Size = new System.Drawing.Size(130, 32);
            this.cbxCol23.TabIndex = 9;

            //
            // cbxCol22
            //
            this.cbxCol22.FormattingEnabled = true;
            this.cbxCol22.Location = new System.Drawing.Point(487, 336);
            this.cbxCol22.Name = "cbxCol22";
            this.cbxCol22.Size = new System.Drawing.Size(130, 32);
            this.cbxCol22.TabIndex = 8;

            //
            // cbxCol21
            //
            this.cbxCol21.FormattingEnabled = true;
            this.cbxCol21.Location = new System.Drawing.Point(357, 336);
            this.cbxCol21.Name = "cbxCol21";
            this.cbxCol21.Size = new System.Drawing.Size(130, 32);
            this.cbxCol21.TabIndex = 7;

            //
            // lblPublish
            //
            this.lblPublish.AutoSize = true;
            this.lblPublish.Location = new System.Drawing.Point(64, 80);
            this.lblPublish.Name = "lblPublish";
            this.lblPublish.Size = new System.Drawing.Size(89, 24);
            this.lblPublish.TabIndex = 14;
            this.lblPublish.Text = "Publisher";

            //
            // lblPublish1
            //
            this.lblPublish1.AutoSize = true;
            this.lblPublish1.Location = new System.Drawing.Point(64, 339);
            this.lblPublish1.Name = "lblPublish1";
            this.lblPublish1.Size = new System.Drawing.Size(89, 24);
            this.lblPublish1.TabIndex = 15;
            this.lblPublish1.Text = "Publisher";

            //
            // dgContent1
            //
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.LightBlue;
            this.dgContent1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle37;
            this.dgContent1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgContent1.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgContent1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgContent1.ColumnHeadersVisible = false;
            this.dgContent1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.publish2,
            this.price2,
            this.fee,
            this.p12,
            this.p22,
            this.p32,
            this.p42,
            this.p52,
            this.pid12,
            this.pid22,
            this.pid32,
            this.pid42,
            this.pid52});
            this.dgContent1.Location = new System.Drawing.Point(10, 367);
            this.dgContent1.Name = "dgContent1";
            this.dgContent1.RowTemplate.Height = 30;
            this.dgContent1.Size = new System.Drawing.Size(1000, 220);
            this.dgContent1.TabIndex = 16;
            this.dgContent1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgContent1_CellDoubleClick);

            //
            // publish2
            //
            this.publish2.FillWeight = 29F;
            this.publish2.HeaderText = "publish";
            this.publish2.Name = "publish2";

            //
            // price2
            //
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle38.Format = "C2";
            dataGridViewCellStyle38.NullValue = null;
            this.price2.DefaultCellStyle = dataGridViewCellStyle38;
            this.price2.FillWeight = 15F;
            this.price2.HeaderText = "price";
            this.price2.Name = "price2";

            //
            // fee
            //
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle39.Format = "C4";
            dataGridViewCellStyle39.NullValue = null;
            this.fee.DefaultCellStyle = dataGridViewCellStyle39;
            this.fee.FillWeight = 21F;
            this.fee.HeaderText = "fee";
            this.fee.Name = "fee";

            //
            // p12
            //
            this.p12.FillWeight = 27.83418F;
            this.p12.HeaderText = "p1";
            this.p12.Name = "p12";
            this.p12.ReadOnly = true;

            //
            // p22
            //
            this.p22.FillWeight = 27.83418F;
            this.p22.HeaderText = "p2";
            this.p22.Name = "p22";
            this.p22.ReadOnly = true;

            //
            // p32
            //
            this.p32.FillWeight = 27.83418F;
            this.p32.HeaderText = "p3";
            this.p32.Name = "p32";
            this.p32.ReadOnly = true;

            //
            // p42
            //
            this.p42.FillWeight = 27.83418F;
            this.p42.HeaderText = "p4";
            this.p42.Name = "p42";
            this.p42.ReadOnly = true;

            //
            // p52
            //
            this.p52.FillWeight = 27.83418F;
            this.p52.HeaderText = "p5";
            this.p52.Name = "p52";
            this.p52.ReadOnly = true;

            //
            // pid12
            //
            this.pid12.HeaderText = "pid1";
            this.pid12.Name = "pid12";
            this.pid12.Visible = false;

            //
            // pid22
            //
            this.pid22.HeaderText = "pid2";
            this.pid22.Name = "pid22";
            this.pid22.Visible = false;

            //
            // pid32
            //
            this.pid32.HeaderText = "pid3";
            this.pid32.Name = "pid32";
            this.pid32.Visible = false;

            //
            // pid42
            //
            this.pid42.HeaderText = "pid4";
            this.pid42.Name = "pid42";
            this.pid42.Visible = false;

            //
            // pid52
            //
            this.pid52.HeaderText = "pid5";
            this.pid52.Name = "pid52";
            this.pid52.Visible = false;

            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(135, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(247, 29);
            this.lblTitle.TabIndex = 17;
            this.lblTitle.Text = "Newspaper Template";

            //
            // cbxTempName
            //
            this.cbxTempName.FormattingEnabled = true;
            this.cbxTempName.Location = new System.Drawing.Point(388, 22);
            this.cbxTempName.Name = "cbxTempName";
            this.cbxTempName.Size = new System.Drawing.Size(390, 32);
            this.cbxTempName.TabIndex = 18;
            this.cbxTempName.SelectedIndexChanged += new System.EventHandler(this.CbxTempName_SelectedIndexChanged);

            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 24);
            this.label1.TabIndex = 22;
            this.label1.Text = "Fee";

            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 24);
            this.label2.TabIndex = 23;
            this.label2.Text = "Fee";

            //
            // dataGridViewTextBoxColumn1
            //
            this.dataGridViewTextBoxColumn1.FillWeight = 29F;
            this.dataGridViewTextBoxColumn1.HeaderText = "publish";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 136;

            //
            // dataGridViewTextBoxColumn2
            //
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle40.Format = "C2";
            dataGridViewCellStyle40.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridViewTextBoxColumn2.FillWeight = 15F;
            this.dataGridViewTextBoxColumn2.HeaderText = "price";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 70;

            //
            // dataGridViewTextBoxColumn3
            //
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle41.Format = "C2";
            dataGridViewCellStyle41.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn3.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn3.HeaderText = "p1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 130;

            //
            // dataGridViewTextBoxColumn4
            //
            this.dataGridViewTextBoxColumn4.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn4.HeaderText = "p2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 130;

            //
            // dataGridViewTextBoxColumn5
            //
            this.dataGridViewTextBoxColumn5.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn5.HeaderText = "p3";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 131;

            //
            // dataGridViewTextBoxColumn6
            //
            this.dataGridViewTextBoxColumn6.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn6.HeaderText = "p4";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 130;

            //
            // dataGridViewTextBoxColumn7
            //
            this.dataGridViewTextBoxColumn7.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn7.HeaderText = "p5";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 130;

            //
            // dataGridViewTextBoxColumn8
            //
            this.dataGridViewTextBoxColumn8.FillWeight = 27.7401F;
            this.dataGridViewTextBoxColumn8.HeaderText = "pid1";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 130;

            //
            // dataGridViewTextBoxColumn9
            //
            this.dataGridViewTextBoxColumn9.HeaderText = "pid2";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;

            //
            // dataGridViewTextBoxColumn10
            //
            this.dataGridViewTextBoxColumn10.HeaderText = "pid3";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;

            //
            // dataGridViewTextBoxColumn11
            //
            this.dataGridViewTextBoxColumn11.HeaderText = "pid4";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;

            //
            // dataGridViewTextBoxColumn12
            //
            this.dataGridViewTextBoxColumn12.HeaderText = "pid5";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Visible = false;

            //
            // dataGridViewTextBoxColumn13
            //
            this.dataGridViewTextBoxColumn13.FillWeight = 29F;
            this.dataGridViewTextBoxColumn13.HeaderText = "publish";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Visible = false;
            this.dataGridViewTextBoxColumn13.Width = 136;

            //
            // dataGridViewTextBoxColumn14
            //
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle42.Format = "C2";
            dataGridViewCellStyle42.NullValue = null;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle42;
            this.dataGridViewTextBoxColumn14.FillWeight = 15F;
            this.dataGridViewTextBoxColumn14.HeaderText = "price";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 70;

            //
            // dataGridViewTextBoxColumn15
            //
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle43.Format = "C2";
            dataGridViewCellStyle43.NullValue = null;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridViewTextBoxColumn15.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn15.HeaderText = "p1";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 130;

            //
            // dataGridViewTextBoxColumn16
            //
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle44.Format = "C2";
            dataGridViewCellStyle44.NullValue = null;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridViewTextBoxColumn16.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn16.HeaderText = "p2";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 130;

            //
            // dataGridViewTextBoxColumn17
            //
            this.dataGridViewTextBoxColumn17.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn17.HeaderText = "p3";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 131;

            //
            // dataGridViewTextBoxColumn18
            //
            this.dataGridViewTextBoxColumn18.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn18.HeaderText = "p4";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Width = 130;

            //
            // dataGridViewTextBoxColumn19
            //
            this.dataGridViewTextBoxColumn19.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn19.HeaderText = "p5";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 130;

            //
            // dataGridViewTextBoxColumn20
            //
            this.dataGridViewTextBoxColumn20.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn20.HeaderText = "pid1";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Visible = false;
            this.dataGridViewTextBoxColumn20.Width = 130;

            //
            // dataGridViewTextBoxColumn21
            //
            this.dataGridViewTextBoxColumn21.FillWeight = 27.83418F;
            this.dataGridViewTextBoxColumn21.HeaderText = "pid2";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn21.Width = 130;

            //
            // dataGridViewTextBoxColumn22
            //
            this.dataGridViewTextBoxColumn22.HeaderText = "pid3";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.Visible = false;

            //
            // dataGridViewTextBoxColumn23
            //
            this.dataGridViewTextBoxColumn23.HeaderText = "pid4";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.Visible = false;

            //
            // dataGridViewTextBoxColumn24
            //
            this.dataGridViewTextBoxColumn24.HeaderText = "pid5";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.Visible = false;

            //
            // dataGridViewTextBoxColumn25
            //
            this.dataGridViewTextBoxColumn25.HeaderText = "pid5";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Visible = false;

            //
            // dataGridViewTextBoxColumn26
            //
            this.dataGridViewTextBoxColumn26.HeaderText = "pid5";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.Visible = false;

            //
            // closeButton
            //
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Blue;
            this.closeButton.Location = new System.Drawing.Point(613, 618);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(165, 40);
            this.closeButton.TabIndex = 143;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // deleteButton
            //
            this.deleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.deleteButton.CornerRadius = 40;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.Blue;
            this.deleteButton.Location = new System.Drawing.Point(419, 618);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.deleteButton.Size = new System.Drawing.Size(165, 40);
            this.deleteButton.TabIndex = 142;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.BtnDelete_Click);

            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.CornerRadius = 40;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.Blue;
            this.saveButton.Location = new System.Drawing.Point(226, 618);
            this.saveButton.Name = "saveButton";
            this.saveButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.saveButton.Size = new System.Drawing.Size(165, 40);
            this.saveButton.TabIndex = 141;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.BtnSave_Click);

            //
            // FrmTemplate
            //
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1042, 731);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTempName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgContent1);
            this.Controls.Add(this.lblPublish1);
            this.Controls.Add(this.lblPublish);
            this.Controls.Add(this.lblPrice2);
            this.Controls.Add(this.cbxCol25);
            this.Controls.Add(this.cbxCol24);
            this.Controls.Add(this.cbxCol23);
            this.Controls.Add(this.cbxCol22);
            this.Controls.Add(this.cbxCol21);
            this.Controls.Add(this.dgContent);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.cbxCol5);
            this.Controls.Add(this.cbxCol4);
            this.Controls.Add(this.cbxCol3);
            this.Controls.Add(this.cbxCol2);
            this.Controls.Add(this.cbxCol1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmTemplate";
            this.Text = "Newspaper Template";
            this.Load += new System.EventHandler(this.FrmTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContent1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private void FrmTemplate_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }
        private void Initiate()
        {
            try
            {
                connDB = new Connect();
                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetTemplates", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable aTable = new DataTable();
                            adapter.Fill(aTable);
                            DataRow row = aTable.NewRow();
                            row[0] = 0;
                            row[1] = "";
                            aTable.Rows.InsertAt(row, 0);
                            cbxTempName.DataSource = aTable;
                            cbxTempName.DisplayMember = "template_name";
                            cbxTempName.ValueMember = "paper_template_id";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading templates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgContent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 2 || this.dgContent.Rows[e.RowIndex].Cells[0].Value == null || this.dgContent.Rows[e.RowIndex].Cells[1].Value == null)
                return;

            string weekday = "Mon";
            if (e.ColumnIndex == 4)
            {
                weekday = "Tue";
            }
            if (e.ColumnIndex == 5)
            {
                weekday = "Wed";
            }
            if (e.ColumnIndex == 6)
            {
                weekday = "Thu";
            }
            if (e.ColumnIndex == 7)
            {
                weekday = "Fri";
            }

            SearchItem item = new SearchItem
            {
                category = "Newspaper",
                supplier = "ALL",
                result = dgContent.Rows[e.RowIndex].Cells[0].Value.ToString() + " " + weekday
            };


            FrmSearch frmSearch = new FrmSearch
            {
                currentItem = item,
                funIdentifier = "Search"
            };
            this.AddOwnedForm(frmSearch);
            if (frmSearch.ShowDialog(this) == DialogResult.Yes)
            {
                this.queryStr = "SELECT * FROM pos_stock WHERE stock_id=" + Convert.ToInt32(this.Tag.ToString());
                this.connDB.QueryTable(this.queryStr);
                this.dgContent.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.connDB.aTable.Rows[0]["Descr"].ToString();
                this.dgContent.Rows[e.RowIndex].Cells[e.ColumnIndex + 5].Value = this.connDB.aTable.Rows[0]["stock_id"];
            }
        }

        private void DgContent1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 2 || this.dgContent1.Rows[e.RowIndex].Cells[0].Value == null || this.dgContent1.Rows[e.RowIndex].Cells[1].Value == null)
                return;
            SearchItem item = new SearchItem
            {
                category = "Newspaper",
                supplier = "ALL",
                result = dgContent1.Rows[e.RowIndex].Cells[0].Value.ToString()
            };

            FrmSearch frmSearch = new FrmSearch
            {
                currentItem = item,
                funIdentifier = "Search"
            };
            this.AddOwnedForm(frmSearch);
            object obj = frmSearch.ShowDialog(this);
            if (obj.Equals(DialogResult.Yes))
            {
                this.queryStr = "SELECT * FROM pos_stock WHERE stock_id=" + Convert.ToInt32(this.Tag.ToString());
                this.connDB.QueryTable(this.queryStr);
                this.dgContent1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.connDB.aTable.Rows[0]["Descr"].ToString();
                this.dgContent1.Rows[e.RowIndex].Cells[e.ColumnIndex + 5].Value = this.connDB.aTable.Rows[0]["stock_id"];
            }
            else if (obj.Equals(DialogResult.No))
            {
                this.dgContent1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                this.dgContent1.Rows[e.RowIndex].Cells[e.ColumnIndex + 5].Value = 0;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(temp_id) || temp_id == "0")
                {
                    MessageBox.Show("Please select a valid template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                connDB = new Connect();
                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();

                    // Save dgContent (dgContentId=1)
                    for (int index1 = 0; index1 < dgContent.Rows.Count - 1; ++index1)
                    {
                        using (SqlCommand cmd = new SqlCommand("SaveTemplateDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DgContentId", 1);
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@RowId", index1 + 1);

                            // RowTitle
                            var rowTitle = dgContent.Rows[index1].Cells[0].Value?.ToString()?.Trim();
                            cmd.Parameters.AddWithValue("@RowTitle", string.IsNullOrEmpty(rowTitle) ? DBNull.Value : rowTitle);

                            // RowPrice and RowFee
                            if (!decimal.TryParse(dgContent.Rows[index1].Cells[1].Value?.ToString(), out decimal rowPrice))
                            {
                                MessageBox.Show($"Invalid price format in dgContent row {index1 + 1}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cmd.Parameters.AddWithValue("@RowPrice", rowPrice);

                            if (!decimal.TryParse(dgContent.Rows[index1].Cells[2].Value?.ToString(), out decimal rowFee))
                            {
                                MessageBox.Show($"Invalid fee format in dgContent row {index1 + 1}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cmd.Parameters.AddWithValue("@RowFee", rowFee);

                            // Column parameters
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                // Day
                                var colDay = titleBox[index2].SelectedValue;
                                cmd.Parameters.AddWithValue($"@Col{index2 + 1}Day", colDay ?? DBNull.Value);

                                // Desc
                                var colDesc = dgContent.Rows[index1].Cells[index2 + 3].Value?.ToString();
                                var descParam = cmd.Parameters.Add($"@Col{index2 + 1}Desc", SqlDbType.NVarChar, 255);
                                descParam.Value = string.IsNullOrEmpty(colDesc) ? DBNull.Value : colDesc;

                                // StockId
                                var colStockId = dgContent.Rows[index1].Cells[index2 + 8].Value?.ToString();
                                cmd.Parameters.AddWithValue($"@Col{index2 + 1}StockId", string.IsNullOrEmpty(colStockId) ? 0 : Convert.ToInt32(colStockId));
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Save dgContent1 (dgContentId=2)
                    for (int index1 = 0; index1 < dgContent1.Rows.Count - 1; ++index1)
                    {
                        using (SqlCommand cmd = new SqlCommand("SaveTemplateDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DgContentId", 2);
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@RowId", index1 + 1);

                            // RowTitle
                            var rowTitle = dgContent1.Rows[index1].Cells[0].Value?.ToString()?.Trim();
                            cmd.Parameters.AddWithValue("@RowTitle", string.IsNullOrEmpty(rowTitle) ? DBNull.Value : rowTitle);

                            // RowPrice and RowFee
                            if (!decimal.TryParse(dgContent1.Rows[index1].Cells[1].Value?.ToString(), out decimal rowPrice))
                            {
                                MessageBox.Show($"Invalid price format in dgContent1 row {index1 + 1}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cmd.Parameters.AddWithValue("@RowPrice", rowPrice);

                            if (!decimal.TryParse(dgContent1.Rows[index1].Cells[2].Value?.ToString(), out decimal rowFee))
                            {
                                MessageBox.Show($"Invalid fee format in dgContent1 row {index1 + 1}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cmd.Parameters.AddWithValue("@RowFee", rowFee);

                            // Column parameters
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                // Day
                                var colDay = titleBox[index2 + 5].SelectedValue;
                                cmd.Parameters.AddWithValue($"@Col{index2 + 1}Day", colDay ?? DBNull.Value);

                                // Desc
                                var colDesc = dgContent1.Rows[index1].Cells[index2 + 3].Value?.ToString();
                                var descParam = cmd.Parameters.Add($"@Col{index2 + 1}Desc", SqlDbType.NVarChar, 255);
                                descParam.Value = string.IsNullOrEmpty(colDesc) ? DBNull.Value : colDesc;

                                // StockId
                                var colStockId = dgContent1.Rows[index1].Cells[index2 + 8].Value?.ToString();
                                cmd.Parameters.AddWithValue($"@Col{index2 + 1}StockId", string.IsNullOrEmpty(colStockId) ? 0 : Convert.ToInt32(colStockId));
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Initiate();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error saving template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CbxTempName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgContent.Rows.Clear();
                dgContent1.Rows.Clear();
                temp_id = cbxTempName.SelectedValue?.ToString();
                if (temp_id == "System.Data.DataRowView" || string.IsNullOrEmpty(temp_id))
                    temp_id = "0";

                if (temp_id != "0")
                {
                    connDB = new Connect();
                    using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                    {
                        conn.Open();

                        // Load dgContent (dgContentId=1)
                        using (SqlCommand cmd = new SqlCommand("GetTemplateDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@DgContentId", 1);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                foreach (DataRow row in dt.Rows)
                                {
                                    dgContent.Rows.Add(row.ItemArray);
                                }
                            }
                        }

                        // Load dgContent1 (dgContentId=2)
                        using (SqlCommand cmd = new SqlCommand("GetTemplateDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@DgContentId", 2);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                foreach (DataRow row in dt.Rows)
                                {
                                    dgContent1.Rows.Add(row.ItemArray);
                                }
                            }
                        }

                        // Load titleBox[0-4] (dgContentId=1)
                        using (SqlCommand cmd = new SqlCommand("GetTemplateColumnDays", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@DgContentId", 1);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int index = 0; index < 5; ++index)
                                        titleBox[index].SelectedValue = dt.Rows[0][index].ToString();
                                }
                            }
                        }

                        // Load titleBox[5-9] (dgContentId=2)
                        using (SqlCommand cmd = new SqlCommand("GetTemplateColumnDays", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaperTemplateId", temp_id);
                            cmd.Parameters.AddWithValue("@DgContentId", 2);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int index = 0; index < 5; ++index)
                                        titleBox[index + 5].SelectedValue = dt.Rows[0][index].ToString();
                                }
                            }
                        }
                    }

                    // Format decimal values
                    for (int index = 0; index < dgContent.Rows.Count - 1; ++index)
                    {
                        if (decimal.TryParse(dgContent.Rows[index].Cells[1].Value?.ToString(), out decimal num1))
                            dgContent.Rows[index].Cells[1].Value = num1;
                        if (decimal.TryParse(dgContent.Rows[index].Cells[2].Value?.ToString(), out decimal num2))
                            dgContent.Rows[index].Cells[2].Value = num2;
                    }
                    for (int index = 0; index < dgContent1.Rows.Count - 1; ++index)
                    {
                        if (decimal.TryParse(dgContent1.Rows[index].Cells[1].Value?.ToString(), out decimal num1))
                            dgContent1.Rows[index].Cells[1].Value = num1;
                        if (decimal.TryParse(dgContent1.Rows[index].Cells[2].Value?.ToString(), out decimal num2))
                            dgContent1.Rows[index].Cells[2].Value = num2;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading template details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = cbxTempName.SelectedValue?.ToString();
                if (str == "System.Data.DataRowView" || string.IsNullOrEmpty(str))
                    return;

                if (MessageBox.Show("Delete This Template ?", "Template Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    connDB = new Connect();
                    using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("DeleteTemplate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaperTemplateId", str);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Initiate();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error deleting template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private bool IsNumber(string inStr)
        {
            bool flag = true;
            inStr = inStr.Trim();
            inStr = inStr.Replace(".", "");
            inStr = inStr.Replace("$", "");
            if (inStr == "")
            {
                flag = false;
            }
            char[] chArray = inStr.ToCharArray();
            for (int index = 0; index < inStr.Length; ++index)
            {
                if (!char.IsDigit(chArray[index]))
                {
                    flag = false;
                }
            }
            return flag;
        }
    }
}

