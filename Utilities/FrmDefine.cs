using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmDefine : Form
    {
        private readonly Button[] BtnMenu;
        private readonly IContainer components;
        private Panel pnlMenu;
        private Button Btns12;
        private Button Btns6;
        private Button Btns11;
        private Button Btnm6;
        private Button Btns10;
        private Button Btns9;
        private Button Btns5;
        private Button Btns8;
        private Button Btnm5;
        private Button Btns7;
        private Button Btns4;
        private Button Btnm4;
        private Button Btns3;
        private Button Btns2;
        private Button Btnm3;
        private Button Btns1;
        private Button Btnm2;
        private Button Btnm1;
        private Label lblTitle;
        private Button Btns18;
        private Button Btns17;
        private Button Btns16;
        private Button Btns15;
        private Button Btns14;
        private CustomButton BtnClose;
        private CustomButton BtnDefine;
        private Button Btns13;

        public FrmDefine()
        {
            BtnMenu = new Button[24];
            components = (IContainer)null;

            InitializeComponent();
            Initiate();
        }

        private void Initiate()
        {
            Location = new Point(Width / 10, Height / 15);
            int y = Width / 15;
            pnlMenu.Width = y * 9 + 8;
            pnlMenu.Height = y * 2 + y * 2 / 3 + 8;
            lblTitle.Location = new Point(y * 2, y / 2);
            pnlMenu.Location = new Point(y * 3 / 2, y);
            BtnDefine.Location = new Point(Width / 2 - 180, pnlMenu.Location.Y + pnlMenu.Height + 50);
            BtnClose.Location = new Point(Width / 2 - 70, BtnDefine.Location.Y + BtnDefine.Height + 30);
            BtnMenu[0] = Btnm1;
            BtnMenu[1] = Btnm2;
            BtnMenu[2] = Btnm3;
            BtnMenu[3] = Btnm4;
            BtnMenu[4] = Btnm5;
            BtnMenu[5] = Btnm6;
            BtnMenu[6] = Btns1;
            BtnMenu[7] = Btns2;
            BtnMenu[8] = Btns3;
            BtnMenu[9] = Btns4;
            BtnMenu[10] = Btns5;
            BtnMenu[11] = Btns6;
            BtnMenu[12] = Btns7;
            BtnMenu[13] = Btns8;
            BtnMenu[14] = Btns9;
            BtnMenu[15] = Btns10;
            BtnMenu[16] = Btns11;
            BtnMenu[17] = Btns12;
            BtnMenu[18] = Btns13;
            BtnMenu[19] = Btns14;
            BtnMenu[20] = Btns15;
            BtnMenu[21] = Btns16;
            BtnMenu[22] = Btns17;
            BtnMenu[23] = Btns18;
            for (int index = 0; index < 24; ++index)
            {
                BtnMenu[index].Width = y * 3 / 2 - 4;
                BtnMenu[index].Height = y * 2 / 3 - 4;
                BtnMenu[index].Location = new Point(index % 6 * y * 3 / 2 + 5, index / 6 * y * 2 / 3 + 5);
                if (Width < 1100)
                    BtnMenu[index].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
                else
                    BtnMenu[index].Font = new Font("Microsoft Sans Serif", 21.75f, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
            }
            DisplayButtonText();
        }

        private void DisplayButtonText()
        {
            Connect connect = new Connect();
            string queryStr1 = "SELECT * FROM pos_look_up WHERE  NOT (menu_Btn IS NULL) AND (short_Btn IS NULL OR short_Btn='')";
            connect.QueryTable(queryStr1);
            for (int index1 = 0; index1 < connect.aTable.Rows.Count; ++index1)
            {
                for (int index2 = 0; index2 < 6; ++index2)
                {
                    if (BtnMenu[index2].Name == connect.aTable.Rows[index1]["menu_Btn"].ToString())
                        BtnMenu[index2].Text = connect.aTable.Rows[index1]["commnets"].ToString();
                }
            }
            string queryStr2 = "SELECT * FROM pos_look_up WHERE  menu_Btn='' AND short_Btn!=''";
            connect.QueryTable(queryStr2);
            for (int index1 = 0; index1 < connect.aTable.Rows.Count; ++index1)
            {
                for (int index2 = 6; index2 < 24; ++index2)
                {
                    if (BtnMenu[index2].Name == connect.aTable.Rows[index1]["short_Btn"].ToString())
                        BtnMenu[index2].Text = connect.aTable.Rows[index1]["commnets"].ToString();
                }
            }
        }

        private void MenuButtonDef(string whichButton)
        {
            if (BtnDefine.Text == "Display Name")
            {
                FrmMenuName frmMenuName = new FrmMenuName(whichButton, "");
                AddOwnedForm(frmMenuName);
                if (frmMenuName.ShowDialog() != DialogResult.Yes)
                    return;
                DisplayButtonText();
            }
            else
            {
                FrmMenu frmMenu = new FrmMenu(whichButton);
                AddOwnedForm(frmMenu);
                ((Control)frmMenu).Show();
            }
        }

        private void ShortCutButton(string whichButton)
        {
            if (BtnDefine.Text == "Display Name")
            {
                FrmMenuName frmMenuName = new FrmMenuName("", whichButton);
                AddOwnedForm(frmMenuName);
                if (frmMenuName.ShowDialog() != DialogResult.Yes)
                    return;
                DisplayButtonText();
            }
            else
            {
                FrmDefineShortcut frmDefineShortcut = new FrmDefineShortcut("", whichButton);
                AddOwnedForm(frmDefineShortcut);
                if ((frmDefineShortcut).ShowDialog() == DialogResult.Yes)
                    DisplayButtonText();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
            base.Dispose();
        }

        private void BtnDefine_Click(object sender, EventArgs e)
        {
            if (BtnDefine.Text == "Display Name")
            {
                BtnDefine.Text = "Define Content";
                BtnDefine.ForeColor = Color.FromArgb(192, 64, 0);
            }
            else
            {
                BtnDefine.Text = "Display Name";
                BtnDefine.ForeColor = Color.FromArgb(0, 0, 192);
            }
        }

        private void Btnm1_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm1");
        }

        private void Btnm2_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm2");
        }

        private void Btnm3_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm3");
        }

        private void Btnm4_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm4");
        }

        private void Btnm5_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm5");
        }

        private void Btnm6_Click(object sender, EventArgs e)
        {
            MenuButtonDef("Btnm6");
        }

        private void Btns1_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns1");
        }

        private void Btns2_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns2");
        }

        private void Btns3_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns3");
        }

        private void Btns4_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns4");
        }

        private void Btns5_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns5");
        }

        private void Btns6_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns6");
        }

        private void Btns7_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns7");
        }

        private void Btns8_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns8");
        }

        private void Btns9_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns9");
        }

        private void Btns10_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns10");
        }

        private void Btns11_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns11");
        }

        private void Btns12_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns12");
        }

        private void Btns13_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns13");
        }

        private void Btns14_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns14");
        }

        private void Btns15_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns15");
        }

        private void Btns16_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns16");
        }

        private void Btns17_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns17");
        }

        private void Btns18_Click(object sender, EventArgs e)
        {
            ShortCutButton("Btns18");
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.Btns18 = new System.Windows.Forms.Button();
            this.Btns17 = new System.Windows.Forms.Button();
            this.Btns16 = new System.Windows.Forms.Button();
            this.Btns15 = new System.Windows.Forms.Button();
            this.Btns14 = new System.Windows.Forms.Button();
            this.Btns13 = new System.Windows.Forms.Button();
            this.Btns12 = new System.Windows.Forms.Button();
            this.Btns6 = new System.Windows.Forms.Button();
            this.Btns11 = new System.Windows.Forms.Button();
            this.Btnm6 = new System.Windows.Forms.Button();
            this.Btns10 = new System.Windows.Forms.Button();
            this.Btns9 = new System.Windows.Forms.Button();
            this.Btns5 = new System.Windows.Forms.Button();
            this.Btns8 = new System.Windows.Forms.Button();
            this.Btnm5 = new System.Windows.Forms.Button();
            this.Btns7 = new System.Windows.Forms.Button();
            this.Btns4 = new System.Windows.Forms.Button();
            this.Btnm4 = new System.Windows.Forms.Button();
            this.Btns3 = new System.Windows.Forms.Button();
            this.Btns2 = new System.Windows.Forms.Button();
            this.Btnm3 = new System.Windows.Forms.Button();
            this.Btns1 = new System.Windows.Forms.Button();
            this.Btnm2 = new System.Windows.Forms.Button();
            this.Btnm1 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.BtnClose = new QiPOS.CustomButton();
            this.BtnDefine = new QiPOS.CustomButton();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.Btns18);
            this.pnlMenu.Controls.Add(this.Btns17);
            this.pnlMenu.Controls.Add(this.Btns16);
            this.pnlMenu.Controls.Add(this.Btns15);
            this.pnlMenu.Controls.Add(this.Btns14);
            this.pnlMenu.Controls.Add(this.Btns13);
            this.pnlMenu.Controls.Add(this.Btns12);
            this.pnlMenu.Controls.Add(this.Btns6);
            this.pnlMenu.Controls.Add(this.Btns11);
            this.pnlMenu.Controls.Add(this.Btnm6);
            this.pnlMenu.Controls.Add(this.Btns10);
            this.pnlMenu.Controls.Add(this.Btns9);
            this.pnlMenu.Controls.Add(this.Btns5);
            this.pnlMenu.Controls.Add(this.Btns8);
            this.pnlMenu.Controls.Add(this.Btnm5);
            this.pnlMenu.Controls.Add(this.Btns7);
            this.pnlMenu.Controls.Add(this.Btns4);
            this.pnlMenu.Controls.Add(this.Btnm4);
            this.pnlMenu.Controls.Add(this.Btns3);
            this.pnlMenu.Controls.Add(this.Btns2);
            this.pnlMenu.Controls.Add(this.Btnm3);
            this.pnlMenu.Controls.Add(this.Btns1);
            this.pnlMenu.Controls.Add(this.Btnm2);
            this.pnlMenu.Controls.Add(this.Btnm1);
            this.pnlMenu.Location = new System.Drawing.Point(147, 55);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(583, 373);
            this.pnlMenu.TabIndex = 2;
            // 
            // Btns18
            // 
            this.Btns18.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns18.Location = new System.Drawing.Point(486, 270);
            this.Btns18.Name = "Btns18";
            this.Btns18.Size = new System.Drawing.Size(91, 82);
            this.Btns18.TabIndex = 24;
            this.Btns18.Text = "s12";
            this.Btns18.UseVisualStyleBackColor = true;
            this.Btns18.Click += new System.EventHandler(this.Btns18_Click);
            // 
            // Btns17
            // 
            this.Btns17.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns17.Location = new System.Drawing.Point(391, 270);
            this.Btns17.Name = "Btns17";
            this.Btns17.Size = new System.Drawing.Size(91, 82);
            this.Btns17.TabIndex = 23;
            this.Btns17.Text = "s11";
            this.Btns17.UseVisualStyleBackColor = true;
            this.Btns17.Click += new System.EventHandler(this.Btns17_Click);
            // 
            // Btns16
            // 
            this.Btns16.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns16.Location = new System.Drawing.Point(294, 270);
            this.Btns16.Name = "Btns16";
            this.Btns16.Size = new System.Drawing.Size(91, 82);
            this.Btns16.TabIndex = 22;
            this.Btns16.Text = "s10";
            this.Btns16.UseVisualStyleBackColor = true;
            this.Btns16.Click += new System.EventHandler(this.Btns16_Click);
            // 
            // Btns15
            // 
            this.Btns15.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns15.Location = new System.Drawing.Point(197, 270);
            this.Btns15.Name = "Btns15";
            this.Btns15.Size = new System.Drawing.Size(91, 82);
            this.Btns15.TabIndex = 21;
            this.Btns15.Text = "s9";
            this.Btns15.UseVisualStyleBackColor = true;
            this.Btns15.Click += new System.EventHandler(this.Btns15_Click);
            // 
            // Btns14
            // 
            this.Btns14.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns14.Location = new System.Drawing.Point(100, 270);
            this.Btns14.Name = "Btns14";
            this.Btns14.Size = new System.Drawing.Size(91, 82);
            this.Btns14.TabIndex = 20;
            this.Btns14.Text = "s8";
            this.Btns14.UseVisualStyleBackColor = true;
            this.Btns14.Click += new System.EventHandler(this.Btns14_Click);
            // 
            // Btns13
            // 
            this.Btns13.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns13.Location = new System.Drawing.Point(3, 270);
            this.Btns13.Name = "Btns13";
            this.Btns13.Size = new System.Drawing.Size(91, 82);
            this.Btns13.TabIndex = 19;
            this.Btns13.Text = "s7";
            this.Btns13.UseVisualStyleBackColor = true;
            this.Btns13.Click += new System.EventHandler(this.Btns13_Click);
            // 
            // Btns12
            // 
            this.Btns12.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns12.Location = new System.Drawing.Point(486, 182);
            this.Btns12.Name = "Btns12";
            this.Btns12.Size = new System.Drawing.Size(91, 82);
            this.Btns12.TabIndex = 18;
            this.Btns12.Text = "s12";
            this.Btns12.UseVisualStyleBackColor = true;
            this.Btns12.Click += new System.EventHandler(this.Btns12_Click);
            // 
            // Btns6
            // 
            this.Btns6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns6.Location = new System.Drawing.Point(486, 91);
            this.Btns6.Name = "Btns6";
            this.Btns6.Size = new System.Drawing.Size(91, 82);
            this.Btns6.TabIndex = 18;
            this.Btns6.Text = "s6";
            this.Btns6.UseVisualStyleBackColor = true;
            this.Btns6.Click += new System.EventHandler(this.Btns6_Click);
            // 
            // Btns11
            // 
            this.Btns11.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns11.Location = new System.Drawing.Point(391, 182);
            this.Btns11.Name = "Btns11";
            this.Btns11.Size = new System.Drawing.Size(91, 82);
            this.Btns11.TabIndex = 17;
            this.Btns11.Text = "s11";
            this.Btns11.UseVisualStyleBackColor = true;
            this.Btns11.Click += new System.EventHandler(this.Btns11_Click);
            // 
            // Btnm6
            // 
            this.Btnm6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm6.Location = new System.Drawing.Point(486, 3);
            this.Btnm6.Name = "Btnm6";
            this.Btnm6.Size = new System.Drawing.Size(91, 82);
            this.Btnm6.TabIndex = 18;
            this.Btnm6.Text = "m6";
            this.Btnm6.UseVisualStyleBackColor = true;
            this.Btnm6.Click += new System.EventHandler(this.Btnm6_Click);
            // 
            // Btns10
            // 
            this.Btns10.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns10.Location = new System.Drawing.Point(294, 182);
            this.Btns10.Name = "Btns10";
            this.Btns10.Size = new System.Drawing.Size(91, 82);
            this.Btns10.TabIndex = 16;
            this.Btns10.Text = "s10";
            this.Btns10.UseVisualStyleBackColor = true;
            this.Btns10.Click += new System.EventHandler(this.Btns10_Click);
            // 
            // Btns9
            // 
            this.Btns9.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns9.Location = new System.Drawing.Point(197, 182);
            this.Btns9.Name = "Btns9";
            this.Btns9.Size = new System.Drawing.Size(91, 82);
            this.Btns9.TabIndex = 15;
            this.Btns9.Text = "s9";
            this.Btns9.UseVisualStyleBackColor = true;
            this.Btns9.Click += new System.EventHandler(this.Btns9_Click);
            // 
            // Btns5
            // 
            this.Btns5.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns5.Location = new System.Drawing.Point(391, 91);
            this.Btns5.Name = "Btns5";
            this.Btns5.Size = new System.Drawing.Size(91, 82);
            this.Btns5.TabIndex = 17;
            this.Btns5.Text = "s5";
            this.Btns5.UseVisualStyleBackColor = true;
            this.Btns5.Click += new System.EventHandler(this.Btns5_Click);
            // 
            // Btns8
            // 
            this.Btns8.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns8.Location = new System.Drawing.Point(100, 182);
            this.Btns8.Name = "Btns8";
            this.Btns8.Size = new System.Drawing.Size(91, 82);
            this.Btns8.TabIndex = 14;
            this.Btns8.Text = "s8";
            this.Btns8.UseVisualStyleBackColor = true;
            this.Btns8.Click += new System.EventHandler(this.Btns8_Click);
            // 
            // Btnm5
            // 
            this.Btnm5.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm5.Location = new System.Drawing.Point(391, 4);
            this.Btnm5.Name = "Btnm5";
            this.Btnm5.Size = new System.Drawing.Size(91, 82);
            this.Btnm5.TabIndex = 17;
            this.Btnm5.Text = "m5";
            this.Btnm5.UseVisualStyleBackColor = true;
            this.Btnm5.Click += new System.EventHandler(this.Btnm5_Click);
            // 
            // Btns7
            // 
            this.Btns7.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns7.Location = new System.Drawing.Point(3, 182);
            this.Btns7.Name = "Btns7";
            this.Btns7.Size = new System.Drawing.Size(91, 82);
            this.Btns7.TabIndex = 13;
            this.Btns7.Text = "s7";
            this.Btns7.UseVisualStyleBackColor = true;
            this.Btns7.Click += new System.EventHandler(this.Btns7_Click);
            // 
            // Btns4
            // 
            this.Btns4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns4.Location = new System.Drawing.Point(294, 91);
            this.Btns4.Name = "Btns4";
            this.Btns4.Size = new System.Drawing.Size(91, 82);
            this.Btns4.TabIndex = 16;
            this.Btns4.Text = "s4";
            this.Btns4.UseVisualStyleBackColor = true;
            this.Btns4.Click += new System.EventHandler(this.Btns4_Click);
            // 
            // Btnm4
            // 
            this.Btnm4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm4.Location = new System.Drawing.Point(294, 3);
            this.Btnm4.Name = "Btnm4";
            this.Btnm4.Size = new System.Drawing.Size(91, 82);
            this.Btnm4.TabIndex = 16;
            this.Btnm4.Text = "m4";
            this.Btnm4.UseVisualStyleBackColor = true;
            this.Btnm4.Click += new System.EventHandler(this.Btnm4_Click);
            // 
            // Btns3
            // 
            this.Btns3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns3.Location = new System.Drawing.Point(197, 91);
            this.Btns3.Name = "Btns3";
            this.Btns3.Size = new System.Drawing.Size(91, 82);
            this.Btns3.TabIndex = 15;
            this.Btns3.Text = "s3";
            this.Btns3.UseVisualStyleBackColor = true;
            this.Btns3.Click += new System.EventHandler(this.Btns3_Click);
            // 
            // Btns2
            // 
            this.Btns2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns2.Location = new System.Drawing.Point(100, 91);
            this.Btns2.Name = "Btns2";
            this.Btns2.Size = new System.Drawing.Size(91, 82);
            this.Btns2.TabIndex = 14;
            this.Btns2.Text = "s2";
            this.Btns2.UseVisualStyleBackColor = true;
            this.Btns2.Click += new System.EventHandler(this.Btns2_Click);
            // 
            // Btnm3
            // 
            this.Btnm3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm3.Location = new System.Drawing.Point(197, 3);
            this.Btnm3.Name = "Btnm3";
            this.Btnm3.Size = new System.Drawing.Size(91, 82);
            this.Btnm3.TabIndex = 15;
            this.Btnm3.Text = "m3";
            this.Btnm3.UseVisualStyleBackColor = true;
            this.Btnm3.Click += new System.EventHandler(this.Btnm3_Click);
            // 
            // Btns1
            // 
            this.Btns1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btns1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Btns1.Location = new System.Drawing.Point(3, 91);
            this.Btns1.Name = "Btns1";
            this.Btns1.Size = new System.Drawing.Size(91, 82);
            this.Btns1.TabIndex = 13;
            this.Btns1.Text = "s1";
            this.Btns1.UseVisualStyleBackColor = true;
            this.Btns1.Click += new System.EventHandler(this.Btns1_Click);
            // 
            // Btnm2
            // 
            this.Btnm2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm2.Location = new System.Drawing.Point(100, 3);
            this.Btnm2.Name = "Btnm2";
            this.Btnm2.Size = new System.Drawing.Size(91, 82);
            this.Btnm2.TabIndex = 14;
            this.Btnm2.Text = "m2";
            this.Btnm2.UseVisualStyleBackColor = true;
            this.Btnm2.Click += new System.EventHandler(this.Btnm2_Click);
            // 
            // Btnm1
            // 
            this.Btnm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btnm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Btnm1.Location = new System.Drawing.Point(3, 3);
            this.Btnm1.Name = "Btnm1";
            this.Btnm1.Size = new System.Drawing.Size(91, 82);
            this.Btnm1.TabIndex = 13;
            this.Btnm1.Text = "m1";
            this.Btnm1.UseVisualStyleBackColor = true;
            this.Btnm1.Click += new System.EventHandler(this.Btnm1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(173, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(491, 31);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Define Sub Menu and Short Cut Key ";
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.SystemColors.Control;
            this.BtnClose.CornerRadius = 40;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.Blue;
            this.BtnClose.Location = new System.Drawing.Point(360, 523);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnClose.Size = new System.Drawing.Size(160, 50);
            this.BtnClose.TabIndex = 142;
            this.BtnClose.Text = "Close";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnDefine
            // 
            this.BtnDefine.BackColor = System.Drawing.SystemColors.Control;
            this.BtnDefine.CornerRadius = 40;
            this.BtnDefine.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDefine.ForeColor = System.Drawing.Color.Blue;
            this.BtnDefine.Location = new System.Drawing.Point(309, 452);
            this.BtnDefine.Name = "BtnDefine";
            this.BtnDefine.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnDefine.Size = new System.Drawing.Size(254, 50);
            this.BtnDefine.TabIndex = 142;
            this.BtnDefine.Text = "Define Content";
            this.BtnDefine.Click += new System.EventHandler(this.BtnDefine_Click);
            // 
            // FrmDefine
            // 
            this.ClientSize = new System.Drawing.Size(911, 605);
            this.Controls.Add(this.BtnDefine);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlMenu);
            this.Name = "FrmDefine";
            this.Text = "Define";
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}

