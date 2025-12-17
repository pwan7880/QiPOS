using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmMenuName : Form
    {
        private readonly string menu_Btn;
        private readonly string short_Btn;
        private readonly IContainer components;
        private TextBox TxtDesc;
        private Panel pnlItems;
        private Label lbl2;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private Label lblTitle;


        public FrmMenuName(string in_menu_Btn, string in_short_Btn)
        {
            this.components = (IContainer)null;

            this.InitializeComponent();
            this.menu_Btn = in_menu_Btn;
            this.short_Btn = in_short_Btn;
            this.Initiate();
        }

        private void Initiate()
        {
            this.CenterToScreen();
            Connect connect = new Connect();
            string queryStr = "";
            if (this.short_Btn == "" && this.menu_Btn != "")
            {
                queryStr = "SELECT * FROM pos_look_up WHERE menu_Btn='" + this.menu_Btn + "' AND short_Btn IS NULL";
                this.lblTitle.Text = "Menu Name";
            }
            else if (this.menu_Btn == "" && this.short_Btn != "")
                queryStr = "SELECT * FROM pos_look_up WHERE menu_Btn='' AND short_Btn='" + this.short_Btn + "'";
            else if (this.short_Btn != "" && this.menu_Btn != "")
                queryStr = "SELECT * FROM pos_look_up WHERE menu_Btn='" + this.menu_Btn + "' AND short_Btn='" + this.short_Btn + "'";
            connect.QueryTable(queryStr);
            if (connect.aTable.Rows.Count <= 0)
                return;
            this.TxtDesc.Text = connect.aTable.Rows[0]["commnets"].ToString();
        }

        private void BtnAbort_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            string inStr = this.TxtDesc.Text.Trim();
            string str = connect.AddBackslash(inStr);
            string queryStr1;
            if (this.short_Btn == "")
            {
                string queryStr2 = "SELECT * FROM pos_look_up WHERE menu_Btn='" + this.menu_Btn + "' AND short_Btn IS NULL";
                connect.QueryTable(queryStr2);
                if (connect.aTable.Rows.Count > 0)
                    queryStr1 = "UPDATE pos_look_up SET  commnets='" + str + "' WHERE menu_Btn='" + this.menu_Btn + "' AND short_Btn IS NULL";
                else
                    queryStr1 = "INSERT INTO pos_look_up ( menu_Btn, commnets, key_code, key_code1, key_value, key_value1) VALUES ( '" + this.menu_Btn + "', '" + str + "', '', '', '', '')";
            }
            else if (this.menu_Btn == "")
                queryStr1 = "UPDATE pos_look_up SET  commnets='" + str + "' WHERE menu_Btn='' AND short_Btn='" + this.short_Btn + "'";
            else
                queryStr1 = "UPDATE pos_look_up SET  commnets='" + str + "' WHERE menu_Btn='" + this.menu_Btn + "' AND short_Btn='" + this.short_Btn + "'";
            connect.NoReturnQuery(queryStr1);
            this.DialogResult = DialogResult.Yes;
            this.Close();
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TxtDesc = new System.Windows.Forms.TextBox();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.customButton1 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.pnlItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtDesc
            // 
            this.TxtDesc.Location = new System.Drawing.Point(67, 82);
            this.TxtDesc.Name = "TxtDesc";
            this.TxtDesc.Size = new System.Drawing.Size(582, 29);
            this.TxtDesc.TabIndex = 1;
            // 
            // pnlItems
            // 
            this.pnlItems.BackColor = System.Drawing.Color.LightYellow;
            this.pnlItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItems.Controls.Add(this.TxtDesc);
            this.pnlItems.Controls.Add(this.lbl2);
            this.pnlItems.Location = new System.Drawing.Point(54, 69);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(674, 283);
            this.pnlItems.TabIndex = 2001;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl2.Location = new System.Drawing.Point(72, 43);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(104, 24);
            this.lbl2.TabIndex = 2002;
            this.lbl2.Text = "Description";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(301, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(198, 25);
            this.lblTitle.TabIndex = 2004;
            this.lblTitle.Text = "Short Cut Name";
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 55;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(426, 367);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(187, 57);
            this.customButton1.TabIndex = 2005;
            this.customButton1.Text = "Abort";
            this.customButton1.Click += new System.EventHandler(this.BtnAbort_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.Control;
            this.customButton2.CornerRadius = 55;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.Location = new System.Drawing.Point(182, 367);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(187, 57);
            this.customButton2.TabIndex = 2005;
            this.customButton2.Text = "Save";
            this.customButton2.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmMenuName
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(784, 464);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMenuName";
            this.Text = "Display Name";
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

