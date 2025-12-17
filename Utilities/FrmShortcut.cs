using System;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmShortcut : Form
    {
        #region declarations

        private Panel pnlKeys;
        private Button Btnq;
        private Button Btno;
        private Button Btni;
        private Button Btnu;
        private Button Btny;
        private Button Btnt;
        private Button Btnr;
        private Button Btne;
        private Button BtnW;
        private Button Btnp;
        private Button Btnj;
        private Button Btnh;
        private Button Btng;
        private Button Btnf;
        private Button Btnd;
        private Button Btns;
        private Button Btna;
        private Button Btnm;
        private Button Btnn;
        private Button Btnb;
        private Button Btnv;
        private Button Btnc;
        private Button Btnx;
        private Button Btnz;
        private Button Btnl;
        private Button Btnk;
        private DataGridView dgContents;
        private Button Btnb3;
        private Button Btnb2;
        private Button Btnb1;
        private Button Btnb6;
        private CustomButton customButton2;
        private Button Btnb5;

        #endregion declarations

        public FrmShortcut()
        {
            this.InitializeComponent();
        }

        private void ShowContents(string keystr)
        {
            Connect connect = new Connect();
            string queryStr1 = "SELECT pos_look_up.stock_id FROM pos_look_up join pos_stock on (pos_look_up.stock_id=pos_stock.stock_id) WHERE key_value='" + keystr + "'";
            string queryStr2 = connect.GetInt32(queryStr1) <= 0 ? "SELECT '', acc_name, ''  FROM pos_look_up join account_list on (pos_look_up.acc_number=account_list.acc_number) WHERE acc_type_id=4 and key_value='" + keystr + "'" : "SELECT barcode, acc_name, descr FROM pos_look_up join account_list on (pos_look_up.acc_number=account_list.acc_number) join pos_stock on (pos_look_up.stock_id=pos_stock.stock_id)WHERE acc_type_id=4 and key_value='" + keystr + "'";
            connect.QueryTable(queryStr2);
            this.dgContents.DataSource = connect.aTable;
            this.dgContents.Columns[0].HeaderText = "BARCODE";
            this.dgContents.Columns[1].HeaderText = "CATEGORY";
            this.dgContents.Columns[2].HeaderText = "DESCRIPTION";
            this.dgContents.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgContents.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ButtonEnter(Button thisButton, string whichButton)
        {
            thisButton.ForeColor = Color.Red;
            this.ShowContents(whichButton);
        }

        private void ButtonLeave(Button thisButton)
        {
            thisButton.ForeColor = SystemColors.ControlText;
            this.dgContents.DataSource = null;
        }

        private void ButtonMouseEnter(Button thisButton, string whichButton)
        {
            thisButton.ForeColor = Color.Red;
            this.ShowContents(whichButton);
        }

        private void ButtonClick(string whichButton)
        {
            FrmDefineShortcut frmDefineShortcut = new FrmDefineShortcut(whichButton);
            this.AddOwnedForm(frmDefineShortcut);
            if (frmDefineShortcut.ShowDialog(this) != DialogResult.Yes)
                return;
            this.ShowContents(whichButton);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void PnlKeys_MouseEnter(object sender, EventArgs e)
        {
            this.Btnq.ForeColor = SystemColors.ControlText;
            this.dgContents.DataSource = null;
            this.Btnq.ForeColor = SystemColors.ControlText;
            this.Btna.ForeColor = SystemColors.ControlText;
            this.BtnW.ForeColor = SystemColors.ControlText;
            this.Btne.ForeColor = SystemColors.ControlText;
            this.Btnr.ForeColor = SystemColors.ControlText;
            this.Btnt.ForeColor = SystemColors.ControlText;
            this.Btny.ForeColor = SystemColors.ControlText;
            this.Btnu.ForeColor = SystemColors.ControlText;
            this.Btni.ForeColor = SystemColors.ControlText;
            this.Btno.ForeColor = SystemColors.ControlText;
            this.Btnp.ForeColor = SystemColors.ControlText;
            this.Btns.ForeColor = SystemColors.ControlText;
            this.Btnd.ForeColor = SystemColors.ControlText;
            this.Btnf.ForeColor = SystemColors.ControlText;
            this.Btng.ForeColor = SystemColors.ControlText;
            this.Btnh.ForeColor = SystemColors.ControlText;
            this.Btnj.ForeColor = SystemColors.ControlText;
            this.Btnk.ForeColor = SystemColors.ControlText;
            this.Btnl.ForeColor = SystemColors.ControlText;
            this.Btnz.ForeColor = SystemColors.ControlText;
            this.Btnx.ForeColor = SystemColors.ControlText;
            this.Btnc.ForeColor = SystemColors.ControlText;
            this.Btnv.ForeColor = SystemColors.ControlText;
            this.Btnb.ForeColor = SystemColors.ControlText;
            this.Btnn.ForeColor = SystemColors.ControlText;
            this.Btnm.ForeColor = SystemColors.ControlText;
            this.Btnb1.ForeColor = SystemColors.ControlText;
            this.Btnb2.ForeColor = SystemColors.ControlText;
            this.Btnb3.ForeColor = SystemColors.ControlText;
            this.Btnb5.ForeColor = SystemColors.ControlText;
            this.Btnb6.ForeColor = SystemColors.ControlText;
        }

        private void Btnq_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnq, "Q");
        }

        private void Btnq_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnq);
        }

        private void Btnq_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnq, "Q");
        }

        private void Btnq_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Q");
        }

        private void Btna_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnq, "Q");
        }

        private void Btna_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btna);
        }

        private void Btna_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btna, "A");
        }

        private void Btna_Click(object sender, EventArgs e)
        {
            this.ButtonClick("A");
        }

        private void BtnW_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.BtnW, "W");
        }

        private void BtnW_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.BtnW);
        }

        private void BtnW_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.BtnW, "W");
        }

        private void BtnW_Click(object sender, EventArgs e)
        {
            this.ButtonClick("W");
        }

        private void Btne_Click(object sender, EventArgs e)
        {
            this.ButtonClick("E");
        }

        private void Btne_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btne, "E");
        }

        private void Btne_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btne);
        }

        private void Btne_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btne, "E");
        }

        private void Btnr_Click(object sender, EventArgs e)
        {
            this.ButtonClick("R");
        }

        private void Btnr_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnr, "R");
        }

        private void Btnr_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnr);
        }

        private void Btnr_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnr, "R");
        }

        private void Btnt_Click(object sender, EventArgs e)
        {
            this.ButtonClick("T");
        }

        private void Btnt_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnt, "T");
        }

        private void Btnt_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnt);
        }

        private void Btnt_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnt, "T");
        }

        private void Btny_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Y");
        }

        private void Btny_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btny, "Y");
        }

        private void Btny_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btny);
        }

        private void Btny_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btny, "Y");
        }

        private void Btnu_Click(object sender, EventArgs e)
        {
            this.ButtonClick("U");
        }

        private void Btnu_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnu, "U");
        }

        private void Btnu_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnu);
        }

        private void Btnu_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnu, "U");
        }

        private void Btni_Click(object sender, EventArgs e)
        {
            this.ButtonClick("I");
        }

        private void Btni_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btni, "I");
        }

        private void Btni_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btni);
        }

        private void Btni_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btni, "I");
        }

        private void Btno_Click(object sender, EventArgs e)
        {
            this.ButtonClick("O");
        }

        private void Btno_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btno, "O");
        }

        private void Btno_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btno);
        }

        private void Btno_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btno, "O");
        }

        private void Btnp_Click(object sender, EventArgs e)
        {
            this.ButtonClick("P");
        }

        private void Btnp_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnp, "P");
        }

        private void Btnp_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnp);
        }

        private void Btnp_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnp, "P");
        }

        private void Btns_Click(object sender, EventArgs e)
        {
            this.ButtonClick("S");
        }

        private void Btns_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btns, "S");
        }

        private void Btns_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btns);
        }

        private void Btns_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btns, "S");
        }

        private void Btnd_Click(object sender, EventArgs e)
        {
            this.ButtonClick("D");
        }

        private void Btnd_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnd, "D");
        }

        private void Btnd_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnd);
        }

        private void Btnd_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnd, "D");
        }

        private void Btnf_Click(object sender, EventArgs e)
        {
            this.ButtonClick("F");
        }

        private void Btnf_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnf, "F");
        }

        private void Btnf_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnf);
        }

        private void Btnf_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnf, "F");
        }

        private void Btng_Click(object sender, EventArgs e)
        {
            this.ButtonClick("G");
        }

        private void Btng_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btng, "G");
        }

        private void Btng_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btng);
        }

        private void Btng_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btng, "G");
        }

        private void Btnh_Click(object sender, EventArgs e)
        {
            this.ButtonClick("H");
        }

        private void Btnh_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnh, "H");
        }

        private void Btnh_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnh);
        }

        private void Btnh_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnh, "H");
        }

        private void Btnj_Click(object sender, EventArgs e)
        {
            this.ButtonClick("J");
        }

        private void Btnj_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnj, "J");
        }

        private void Btnj_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnj);
        }

        private void Btnj_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnj, "J");
        }

        private void Btnk_Click(object sender, EventArgs e)
        {
            this.ButtonClick("K");
        }

        private void Btnk_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnk, "K");
        }

        private void Btnk_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnk);
        }

        private void Btnk_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnk, "K");
        }

        private void Btnl_Click(object sender, EventArgs e)
        {
            this.ButtonClick("L");
        }

        private void Btnl_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnl, "L");
        }

        private void Btnl_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnl);
        }

        private void Btnl_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnl, "L");
        }

        private void Btnz_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Z");
        }

        private void Btnz_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnz, "Z");
        }

        private void Btnz_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnz);
        }

        private void Btnz_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnz, "Z");
        }

        private void Btnx_Click(object sender, EventArgs e)
        {
            this.ButtonClick("X");
        }

        private void Btnx_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnx, "X");
        }

        private void Btnx_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnx);
        }

        private void Btnx_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnx, "X");
        }

        private void Btnc_Click(object sender, EventArgs e)
        {
            this.ButtonClick("C");
        }

        private void Btnc_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnc, "C");
        }

        private void Btnc_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnc);
        }

        private void Btnc_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnc, "C");
        }

        private void Btnv_Click(object sender, EventArgs e)
        {
            this.ButtonClick("V");
        }

        private void Btnv_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnv, "V");
        }

        private void Btnv_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnv);
        }

        private void Btnv_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnv, "V");
        }

        private void Btnb_Click(object sender, EventArgs e)
        {
            this.ButtonClick("B");
        }

        private void Btnb_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb, "B");
        }

        private void Btnb_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb);
        }

        private void Btnb_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb, "B");
        }

        private void Btnn_Click(object sender, EventArgs e)
        {
            this.ButtonClick("N");
        }

        private void Btnn_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnn, "N");
        }

        private void Btnn_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnn);
        }

        private void Btnn_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnn, "N");
        }

        private void Btnm_Click(object sender, EventArgs e)
        {
            this.ButtonClick("M");
        }

        private void Btnm_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnm, "M");
        }

        private void Btnm_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnm);
        }

        private void Btnm_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnm, "M");
        }

        private void Btnb1_Click(object sender, EventArgs e)
        {
            this.ButtonClick("OemOpenBrackets");
        }

        private void Btnb1_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb1, "OemOpenBrackets");
        }

        private void Btnb1_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb1);
        }

        private void Btnb1_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb1, "OemOpenBrackets");
        }

        private void Btnb2_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Oem6");
        }

        private void Btnb2_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb2, "Oem6");
        }

        private void Btnb2_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb2);
        }

        private void Btnb2_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb2, "Oem6");
        }

        private void Btnb3_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Oem1");
        }

        private void Btnb3_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb3, "Oem1");
        }

        private void Btnb3_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb3);
        }

        private void Btnb3_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb3, "Oem1");
        }

        private void Btnb5_Click(object sender, EventArgs e)
        {
            this.ButtonClick("Oemcomma");
        }

        private void Btnb5_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb5, "Oemcomma");
        }

        private void Btnb5_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb5);
        }

        private void Btnb5_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb5, "Oemcomma");
        }

        private void Btnb6_Click(object sender, EventArgs e)
        {
            this.ButtonClick("OemPeriod");
        }

        private void Btnb6_Enter(object sender, EventArgs e)
        {
            this.ButtonEnter(this.Btnb6, "OemPeriod");
        }

        private void Btnb6_Leave(object sender, EventArgs e)
        {
            this.ButtonLeave(this.Btnb6);
        }

        private void Btnb6_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonMouseEnter(this.Btnb6, "OemPeriod");
        }


        #region components

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlKeys = new System.Windows.Forms.Panel();
            this.Btnb6 = new System.Windows.Forms.Button();
            this.Btnb5 = new System.Windows.Forms.Button();
            this.Btnb3 = new System.Windows.Forms.Button();
            this.Btnb2 = new System.Windows.Forms.Button();
            this.Btnb1 = new System.Windows.Forms.Button();
            this.Btnm = new System.Windows.Forms.Button();
            this.Btnn = new System.Windows.Forms.Button();
            this.Btnb = new System.Windows.Forms.Button();
            this.Btnv = new System.Windows.Forms.Button();
            this.Btnc = new System.Windows.Forms.Button();
            this.Btnx = new System.Windows.Forms.Button();
            this.Btnz = new System.Windows.Forms.Button();
            this.Btnl = new System.Windows.Forms.Button();
            this.Btnk = new System.Windows.Forms.Button();
            this.Btnj = new System.Windows.Forms.Button();
            this.Btnh = new System.Windows.Forms.Button();
            this.Btng = new System.Windows.Forms.Button();
            this.Btnf = new System.Windows.Forms.Button();
            this.Btnd = new System.Windows.Forms.Button();
            this.Btns = new System.Windows.Forms.Button();
            this.Btna = new System.Windows.Forms.Button();
            this.Btnp = new System.Windows.Forms.Button();
            this.Btno = new System.Windows.Forms.Button();
            this.Btni = new System.Windows.Forms.Button();
            this.Btnu = new System.Windows.Forms.Button();
            this.Btny = new System.Windows.Forms.Button();
            this.Btnt = new System.Windows.Forms.Button();
            this.Btnr = new System.Windows.Forms.Button();
            this.Btne = new System.Windows.Forms.Button();
            this.BtnW = new System.Windows.Forms.Button();
            this.Btnq = new System.Windows.Forms.Button();
            this.dgContents = new System.Windows.Forms.DataGridView();
            this.customButton2 = new QiPOS.CustomButton();
            this.pnlKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgContents)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlKeys
            // 
            this.pnlKeys.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlKeys.Controls.Add(this.Btnb6);
            this.pnlKeys.Controls.Add(this.Btnb5);
            this.pnlKeys.Controls.Add(this.Btnb3);
            this.pnlKeys.Controls.Add(this.Btnb2);
            this.pnlKeys.Controls.Add(this.Btnb1);
            this.pnlKeys.Controls.Add(this.Btnm);
            this.pnlKeys.Controls.Add(this.Btnn);
            this.pnlKeys.Controls.Add(this.Btnb);
            this.pnlKeys.Controls.Add(this.Btnv);
            this.pnlKeys.Controls.Add(this.Btnc);
            this.pnlKeys.Controls.Add(this.Btnx);
            this.pnlKeys.Controls.Add(this.Btnz);
            this.pnlKeys.Controls.Add(this.Btnl);
            this.pnlKeys.Controls.Add(this.Btnk);
            this.pnlKeys.Controls.Add(this.Btnj);
            this.pnlKeys.Controls.Add(this.Btnh);
            this.pnlKeys.Controls.Add(this.Btng);
            this.pnlKeys.Controls.Add(this.Btnf);
            this.pnlKeys.Controls.Add(this.Btnd);
            this.pnlKeys.Controls.Add(this.Btns);
            this.pnlKeys.Controls.Add(this.Btna);
            this.pnlKeys.Controls.Add(this.Btnp);
            this.pnlKeys.Controls.Add(this.Btno);
            this.pnlKeys.Controls.Add(this.Btni);
            this.pnlKeys.Controls.Add(this.Btnu);
            this.pnlKeys.Controls.Add(this.Btny);
            this.pnlKeys.Controls.Add(this.Btnt);
            this.pnlKeys.Controls.Add(this.Btnr);
            this.pnlKeys.Controls.Add(this.Btne);
            this.pnlKeys.Controls.Add(this.BtnW);
            this.pnlKeys.Controls.Add(this.Btnq);
            this.pnlKeys.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlKeys.Location = new System.Drawing.Point(50, 304);
            this.pnlKeys.Name = "pnlKeys";
            this.pnlKeys.Size = new System.Drawing.Size(820, 210);
            this.pnlKeys.TabIndex = 0;
            this.pnlKeys.MouseEnter += new System.EventHandler(this.PnlKeys_MouseEnter);
            // 
            // Btnb6
            // 
            this.Btnb6.Location = new System.Drawing.Point(594, 140);
            this.Btnb6.Name = "Btnb6";
            this.Btnb6.Size = new System.Drawing.Size(60, 60);
            this.Btnb6.TabIndex = 31;
            this.Btnb6.Text = ".";
            this.Btnb6.UseVisualStyleBackColor = true;
            this.Btnb6.Click += new System.EventHandler(this.Btnb6_Click);
            this.Btnb6.Enter += new System.EventHandler(this.Btnb6_Enter);
            this.Btnb6.Leave += new System.EventHandler(this.Btnb6_Leave);
            this.Btnb6.MouseEnter += new System.EventHandler(this.Btnb6_MouseEnter);
            // 
            // Btnb5
            // 
            this.Btnb5.Location = new System.Drawing.Point(528, 140);
            this.Btnb5.Name = "Btnb5";
            this.Btnb5.Size = new System.Drawing.Size(60, 60);
            this.Btnb5.TabIndex = 30;
            this.Btnb5.Text = ",";
            this.Btnb5.UseVisualStyleBackColor = true;
            this.Btnb5.Click += new System.EventHandler(this.Btnb5_Click);
            this.Btnb5.Enter += new System.EventHandler(this.Btnb5_Enter);
            this.Btnb5.Leave += new System.EventHandler(this.Btnb5_Leave);
            this.Btnb5.MouseEnter += new System.EventHandler(this.Btnb5_MouseEnter);
            // 
            // Btnb3
            // 
            this.Btnb3.Location = new System.Drawing.Point(640, 76);
            this.Btnb3.Name = "Btnb3";
            this.Btnb3.Size = new System.Drawing.Size(60, 60);
            this.Btnb3.TabIndex = 28;
            this.Btnb3.Text = ";";
            this.Btnb3.UseVisualStyleBackColor = true;
            this.Btnb3.Click += new System.EventHandler(this.Btnb3_Click);
            this.Btnb3.Enter += new System.EventHandler(this.Btnb3_Enter);
            this.Btnb3.Leave += new System.EventHandler(this.Btnb3_Leave);
            this.Btnb3.MouseEnter += new System.EventHandler(this.Btnb3_MouseEnter);
            // 
            // Btnb2
            // 
            this.Btnb2.Location = new System.Drawing.Point(752, 8);
            this.Btnb2.Name = "Btnb2";
            this.Btnb2.Size = new System.Drawing.Size(60, 60);
            this.Btnb2.TabIndex = 27;
            this.Btnb2.Text = "]";
            this.Btnb2.UseVisualStyleBackColor = true;
            this.Btnb2.Click += new System.EventHandler(this.Btnb2_Click);
            this.Btnb2.Enter += new System.EventHandler(this.Btnb2_Enter);
            this.Btnb2.Leave += new System.EventHandler(this.Btnb2_Leave);
            this.Btnb2.MouseEnter += new System.EventHandler(this.Btnb2_MouseEnter);
            // 
            // Btnb1
            // 
            this.Btnb1.Location = new System.Drawing.Point(686, 8);
            this.Btnb1.Name = "Btnb1";
            this.Btnb1.Size = new System.Drawing.Size(60, 60);
            this.Btnb1.TabIndex = 26;
            this.Btnb1.Text = "[";
            this.Btnb1.UseVisualStyleBackColor = true;
            this.Btnb1.Click += new System.EventHandler(this.Btnb1_Click);
            this.Btnb1.Enter += new System.EventHandler(this.Btnb1_Enter);
            this.Btnb1.Leave += new System.EventHandler(this.Btnb1_Leave);
            this.Btnb1.MouseEnter += new System.EventHandler(this.Btnb1_MouseEnter);
            // 
            // Btnm
            // 
            this.Btnm.BackColor = System.Drawing.SystemColors.Control;
            this.Btnm.Location = new System.Drawing.Point(462, 140);
            this.Btnm.Name = "Btnm";
            this.Btnm.Size = new System.Drawing.Size(60, 60);
            this.Btnm.TabIndex = 25;
            this.Btnm.Text = "M";
            this.Btnm.UseVisualStyleBackColor = false;
            this.Btnm.Click += new System.EventHandler(this.Btnm_Click);
            this.Btnm.Enter += new System.EventHandler(this.Btnm_Enter);
            this.Btnm.Leave += new System.EventHandler(this.Btnm_Leave);
            this.Btnm.MouseEnter += new System.EventHandler(this.Btnm_MouseEnter);
            // 
            // Btnn
            // 
            this.Btnn.BackColor = System.Drawing.SystemColors.Control;
            this.Btnn.Location = new System.Drawing.Point(394, 140);
            this.Btnn.Name = "Btnn";
            this.Btnn.Size = new System.Drawing.Size(60, 60);
            this.Btnn.TabIndex = 24;
            this.Btnn.Text = "N";
            this.Btnn.UseVisualStyleBackColor = false;
            this.Btnn.Click += new System.EventHandler(this.Btnn_Click);
            this.Btnn.Enter += new System.EventHandler(this.Btnn_Enter);
            this.Btnn.Leave += new System.EventHandler(this.Btnn_Leave);
            this.Btnn.MouseEnter += new System.EventHandler(this.Btnn_MouseEnter);
            // 
            // Btnb
            // 
            this.Btnb.BackColor = System.Drawing.SystemColors.Control;
            this.Btnb.Location = new System.Drawing.Point(326, 140);
            this.Btnb.Name = "Btnb";
            this.Btnb.Size = new System.Drawing.Size(60, 60);
            this.Btnb.TabIndex = 23;
            this.Btnb.Text = "B";
            this.Btnb.UseVisualStyleBackColor = false;
            this.Btnb.Click += new System.EventHandler(this.Btnb_Click);
            this.Btnb.Enter += new System.EventHandler(this.Btnb_Enter);
            this.Btnb.Leave += new System.EventHandler(this.Btnb_Leave);
            this.Btnb.MouseEnter += new System.EventHandler(this.Btnb_MouseEnter);
            // 
            // Btnv
            // 
            this.Btnv.BackColor = System.Drawing.SystemColors.Control;
            this.Btnv.Location = new System.Drawing.Point(260, 140);
            this.Btnv.Name = "Btnv";
            this.Btnv.Size = new System.Drawing.Size(60, 60);
            this.Btnv.TabIndex = 22;
            this.Btnv.Text = "V";
            this.Btnv.UseVisualStyleBackColor = false;
            this.Btnv.Click += new System.EventHandler(this.Btnv_Click);
            this.Btnv.Enter += new System.EventHandler(this.Btnv_Enter);
            this.Btnv.Leave += new System.EventHandler(this.Btnv_Leave);
            this.Btnv.MouseEnter += new System.EventHandler(this.Btnv_MouseEnter);
            // 
            // Btnc
            // 
            this.Btnc.BackColor = System.Drawing.SystemColors.Control;
            this.Btnc.Location = new System.Drawing.Point(190, 140);
            this.Btnc.Name = "Btnc";
            this.Btnc.Size = new System.Drawing.Size(60, 60);
            this.Btnc.TabIndex = 21;
            this.Btnc.Text = "C";
            this.Btnc.UseVisualStyleBackColor = false;
            this.Btnc.Click += new System.EventHandler(this.Btnc_Click);
            this.Btnc.Enter += new System.EventHandler(this.Btnc_Enter);
            this.Btnc.Leave += new System.EventHandler(this.Btnc_Leave);
            this.Btnc.MouseEnter += new System.EventHandler(this.Btnc_MouseEnter);
            // 
            // Btnx
            // 
            this.Btnx.BackColor = System.Drawing.SystemColors.Control;
            this.Btnx.Location = new System.Drawing.Point(122, 140);
            this.Btnx.Name = "Btnx";
            this.Btnx.Size = new System.Drawing.Size(60, 60);
            this.Btnx.TabIndex = 20;
            this.Btnx.Text = "X";
            this.Btnx.UseVisualStyleBackColor = false;
            this.Btnx.Click += new System.EventHandler(this.Btnx_Click);
            this.Btnx.Enter += new System.EventHandler(this.Btnx_Enter);
            this.Btnx.Leave += new System.EventHandler(this.Btnx_Leave);
            this.Btnx.MouseEnter += new System.EventHandler(this.Btnx_MouseEnter);
            // 
            // Btnz
            // 
            this.Btnz.BackColor = System.Drawing.SystemColors.Control;
            this.Btnz.Location = new System.Drawing.Point(54, 140);
            this.Btnz.Name = "Btnz";
            this.Btnz.Size = new System.Drawing.Size(60, 60);
            this.Btnz.TabIndex = 19;
            this.Btnz.Text = "Z";
            this.Btnz.UseVisualStyleBackColor = false;
            this.Btnz.Click += new System.EventHandler(this.Btnz_Click);
            this.Btnz.Enter += new System.EventHandler(this.Btnz_Enter);
            this.Btnz.Leave += new System.EventHandler(this.Btnz_Leave);
            this.Btnz.MouseEnter += new System.EventHandler(this.Btnz_MouseEnter);
            // 
            // Btnl
            // 
            this.Btnl.BackColor = System.Drawing.SystemColors.Control;
            this.Btnl.Location = new System.Drawing.Point(574, 76);
            this.Btnl.Name = "Btnl";
            this.Btnl.Size = new System.Drawing.Size(60, 60);
            this.Btnl.TabIndex = 18;
            this.Btnl.Text = "L";
            this.Btnl.UseVisualStyleBackColor = false;
            this.Btnl.Click += new System.EventHandler(this.Btnl_Click);
            this.Btnl.Enter += new System.EventHandler(this.Btnl_Enter);
            this.Btnl.Leave += new System.EventHandler(this.Btnl_Leave);
            this.Btnl.MouseEnter += new System.EventHandler(this.Btnl_MouseEnter);
            // 
            // Btnk
            // 
            this.Btnk.BackColor = System.Drawing.SystemColors.Control;
            this.Btnk.Location = new System.Drawing.Point(506, 76);
            this.Btnk.Name = "Btnk";
            this.Btnk.Size = new System.Drawing.Size(60, 60);
            this.Btnk.TabIndex = 17;
            this.Btnk.Text = "K";
            this.Btnk.UseVisualStyleBackColor = false;
            this.Btnk.Click += new System.EventHandler(this.Btnk_Click);
            this.Btnk.Enter += new System.EventHandler(this.Btnk_Enter);
            this.Btnk.Leave += new System.EventHandler(this.Btnk_Leave);
            this.Btnk.MouseEnter += new System.EventHandler(this.Btnk_MouseEnter);
            // 
            // Btnj
            // 
            this.Btnj.BackColor = System.Drawing.SystemColors.Control;
            this.Btnj.Location = new System.Drawing.Point(438, 76);
            this.Btnj.Name = "Btnj";
            this.Btnj.Size = new System.Drawing.Size(60, 60);
            this.Btnj.TabIndex = 16;
            this.Btnj.Text = "J";
            this.Btnj.UseVisualStyleBackColor = false;
            this.Btnj.Click += new System.EventHandler(this.Btnj_Click);
            this.Btnj.Enter += new System.EventHandler(this.Btnj_Enter);
            this.Btnj.Leave += new System.EventHandler(this.Btnj_Leave);
            this.Btnj.MouseEnter += new System.EventHandler(this.Btnj_MouseEnter);
            // 
            // Btnh
            // 
            this.Btnh.BackColor = System.Drawing.SystemColors.Control;
            this.Btnh.Location = new System.Drawing.Point(370, 76);
            this.Btnh.Name = "Btnh";
            this.Btnh.Size = new System.Drawing.Size(60, 60);
            this.Btnh.TabIndex = 15;
            this.Btnh.Text = "H";
            this.Btnh.UseVisualStyleBackColor = false;
            this.Btnh.Click += new System.EventHandler(this.Btnh_Click);
            this.Btnh.Enter += new System.EventHandler(this.Btnh_Enter);
            this.Btnh.Leave += new System.EventHandler(this.Btnh_Leave);
            this.Btnh.MouseEnter += new System.EventHandler(this.Btnh_MouseEnter);
            // 
            // Btng
            // 
            this.Btng.BackColor = System.Drawing.SystemColors.Control;
            this.Btng.Location = new System.Drawing.Point(302, 76);
            this.Btng.Name = "Btng";
            this.Btng.Size = new System.Drawing.Size(60, 60);
            this.Btng.TabIndex = 14;
            this.Btng.Text = "G";
            this.Btng.UseVisualStyleBackColor = false;
            this.Btng.Click += new System.EventHandler(this.Btng_Click);
            this.Btng.Enter += new System.EventHandler(this.Btng_Enter);
            this.Btng.Leave += new System.EventHandler(this.Btng_Leave);
            this.Btng.MouseEnter += new System.EventHandler(this.Btng_MouseEnter);
            // 
            // Btnf
            // 
            this.Btnf.BackColor = System.Drawing.SystemColors.Control;
            this.Btnf.Location = new System.Drawing.Point(234, 76);
            this.Btnf.Name = "Btnf";
            this.Btnf.Size = new System.Drawing.Size(60, 60);
            this.Btnf.TabIndex = 13;
            this.Btnf.Text = "F";
            this.Btnf.UseVisualStyleBackColor = false;
            this.Btnf.Click += new System.EventHandler(this.Btnf_Click);
            this.Btnf.Enter += new System.EventHandler(this.Btnf_Enter);
            this.Btnf.Leave += new System.EventHandler(this.Btnf_Leave);
            this.Btnf.MouseEnter += new System.EventHandler(this.Btnf_MouseEnter);
            // 
            // Btnd
            // 
            this.Btnd.BackColor = System.Drawing.SystemColors.Control;
            this.Btnd.Location = new System.Drawing.Point(166, 76);
            this.Btnd.Name = "Btnd";
            this.Btnd.Size = new System.Drawing.Size(60, 60);
            this.Btnd.TabIndex = 12;
            this.Btnd.Text = "D";
            this.Btnd.UseVisualStyleBackColor = false;
            this.Btnd.Click += new System.EventHandler(this.Btnd_Click);
            this.Btnd.Enter += new System.EventHandler(this.Btnd_Enter);
            this.Btnd.Leave += new System.EventHandler(this.Btnd_Leave);
            this.Btnd.MouseEnter += new System.EventHandler(this.Btnd_MouseEnter);
            // 
            // Btns
            // 
            this.Btns.BackColor = System.Drawing.SystemColors.Control;
            this.Btns.Location = new System.Drawing.Point(98, 76);
            this.Btns.Name = "Btns";
            this.Btns.Size = new System.Drawing.Size(60, 60);
            this.Btns.TabIndex = 11;
            this.Btns.Text = "S";
            this.Btns.UseVisualStyleBackColor = false;
            this.Btns.Click += new System.EventHandler(this.Btns_Click);
            this.Btns.Enter += new System.EventHandler(this.Btns_Enter);
            this.Btns.Leave += new System.EventHandler(this.Btns_Leave);
            this.Btns.MouseEnter += new System.EventHandler(this.Btns_MouseEnter);
            // 
            // Btna
            // 
            this.Btna.BackColor = System.Drawing.SystemColors.Control;
            this.Btna.Location = new System.Drawing.Point(30, 76);
            this.Btna.Name = "Btna";
            this.Btna.Size = new System.Drawing.Size(60, 60);
            this.Btna.TabIndex = 10;
            this.Btna.Text = "A";
            this.Btna.UseVisualStyleBackColor = false;
            this.Btna.Click += new System.EventHandler(this.Btna_Click);
            this.Btna.Enter += new System.EventHandler(this.Btna_Enter);
            this.Btna.Leave += new System.EventHandler(this.Btna_Leave);
            this.Btna.MouseEnter += new System.EventHandler(this.Btna_MouseEnter);
            // 
            // Btnp
            // 
            this.Btnp.BackColor = System.Drawing.SystemColors.Control;
            this.Btnp.Location = new System.Drawing.Point(620, 8);
            this.Btnp.Name = "Btnp";
            this.Btnp.Size = new System.Drawing.Size(60, 60);
            this.Btnp.TabIndex = 9;
            this.Btnp.Text = "P";
            this.Btnp.UseVisualStyleBackColor = false;
            this.Btnp.Click += new System.EventHandler(this.Btnp_Click);
            this.Btnp.Enter += new System.EventHandler(this.Btnp_Enter);
            this.Btnp.Leave += new System.EventHandler(this.Btnp_Leave);
            this.Btnp.MouseEnter += new System.EventHandler(this.Btnp_MouseEnter);
            // 
            // Btno
            // 
            this.Btno.BackColor = System.Drawing.SystemColors.Control;
            this.Btno.Location = new System.Drawing.Point(552, 8);
            this.Btno.Name = "Btno";
            this.Btno.Size = new System.Drawing.Size(60, 60);
            this.Btno.TabIndex = 8;
            this.Btno.Text = "O";
            this.Btno.UseVisualStyleBackColor = false;
            this.Btno.Click += new System.EventHandler(this.Btno_Click);
            this.Btno.Enter += new System.EventHandler(this.Btno_Enter);
            this.Btno.Leave += new System.EventHandler(this.Btno_Leave);
            this.Btno.MouseEnter += new System.EventHandler(this.Btno_MouseEnter);
            // 
            // Btni
            // 
            this.Btni.BackColor = System.Drawing.SystemColors.Control;
            this.Btni.Location = new System.Drawing.Point(484, 8);
            this.Btni.Name = "Btni";
            this.Btni.Size = new System.Drawing.Size(60, 60);
            this.Btni.TabIndex = 7;
            this.Btni.Text = "I";
            this.Btni.UseVisualStyleBackColor = false;
            this.Btni.Click += new System.EventHandler(this.Btni_Click);
            this.Btni.Enter += new System.EventHandler(this.Btni_Enter);
            this.Btni.Leave += new System.EventHandler(this.Btni_Leave);
            this.Btni.MouseEnter += new System.EventHandler(this.Btni_MouseEnter);
            // 
            // Btnu
            // 
            this.Btnu.BackColor = System.Drawing.SystemColors.Control;
            this.Btnu.Location = new System.Drawing.Point(416, 8);
            this.Btnu.Name = "Btnu";
            this.Btnu.Size = new System.Drawing.Size(60, 60);
            this.Btnu.TabIndex = 6;
            this.Btnu.Text = "U";
            this.Btnu.UseVisualStyleBackColor = false;
            this.Btnu.Click += new System.EventHandler(this.Btnu_Click);
            this.Btnu.Enter += new System.EventHandler(this.Btnu_Enter);
            this.Btnu.Leave += new System.EventHandler(this.Btnu_Leave);
            this.Btnu.MouseEnter += new System.EventHandler(this.Btnu_MouseEnter);
            // 
            // Btny
            // 
            this.Btny.BackColor = System.Drawing.SystemColors.Control;
            this.Btny.Location = new System.Drawing.Point(348, 8);
            this.Btny.Name = "Btny";
            this.Btny.Size = new System.Drawing.Size(60, 60);
            this.Btny.TabIndex = 5;
            this.Btny.Text = "Y";
            this.Btny.UseVisualStyleBackColor = false;
            this.Btny.Click += new System.EventHandler(this.Btny_Click);
            this.Btny.Enter += new System.EventHandler(this.Btny_Enter);
            this.Btny.Leave += new System.EventHandler(this.Btny_Leave);
            this.Btny.MouseEnter += new System.EventHandler(this.Btny_MouseEnter);
            // 
            // Btnt
            // 
            this.Btnt.BackColor = System.Drawing.SystemColors.Control;
            this.Btnt.Location = new System.Drawing.Point(280, 8);
            this.Btnt.Name = "Btnt";
            this.Btnt.Size = new System.Drawing.Size(60, 60);
            this.Btnt.TabIndex = 4;
            this.Btnt.Text = "T";
            this.Btnt.UseVisualStyleBackColor = false;
            this.Btnt.Click += new System.EventHandler(this.Btnt_Click);
            this.Btnt.Enter += new System.EventHandler(this.Btnt_Enter);
            this.Btnt.Leave += new System.EventHandler(this.Btnt_Leave);
            this.Btnt.MouseEnter += new System.EventHandler(this.Btnt_MouseEnter);
            // 
            // Btnr
            // 
            this.Btnr.BackColor = System.Drawing.SystemColors.Control;
            this.Btnr.Location = new System.Drawing.Point(212, 8);
            this.Btnr.Name = "Btnr";
            this.Btnr.Size = new System.Drawing.Size(60, 60);
            this.Btnr.TabIndex = 3;
            this.Btnr.Text = "R";
            this.Btnr.UseVisualStyleBackColor = false;
            this.Btnr.Click += new System.EventHandler(this.Btnr_Click);
            this.Btnr.Enter += new System.EventHandler(this.Btnr_Enter);
            this.Btnr.Leave += new System.EventHandler(this.Btnr_Leave);
            this.Btnr.MouseEnter += new System.EventHandler(this.Btnr_MouseEnter);
            // 
            // Btne
            // 
            this.Btne.BackColor = System.Drawing.SystemColors.Control;
            this.Btne.Location = new System.Drawing.Point(144, 8);
            this.Btne.Name = "Btne";
            this.Btne.Size = new System.Drawing.Size(60, 60);
            this.Btne.TabIndex = 2;
            this.Btne.Text = "E";
            this.Btne.UseVisualStyleBackColor = false;
            this.Btne.Click += new System.EventHandler(this.Btne_Click);
            this.Btne.Enter += new System.EventHandler(this.Btne_Enter);
            this.Btne.Leave += new System.EventHandler(this.Btne_Leave);
            this.Btne.MouseEnter += new System.EventHandler(this.Btne_MouseEnter);
            // 
            // BtnW
            // 
            this.BtnW.BackColor = System.Drawing.SystemColors.Control;
            this.BtnW.Location = new System.Drawing.Point(76, 8);
            this.BtnW.Name = "BtnW";
            this.BtnW.Size = new System.Drawing.Size(60, 60);
            this.BtnW.TabIndex = 1;
            this.BtnW.Text = "W";
            this.BtnW.UseVisualStyleBackColor = false;
            this.BtnW.Click += new System.EventHandler(this.BtnW_Click);
            this.BtnW.Enter += new System.EventHandler(this.BtnW_Enter);
            this.BtnW.Leave += new System.EventHandler(this.BtnW_Leave);
            this.BtnW.MouseEnter += new System.EventHandler(this.BtnW_MouseEnter);
            // 
            // Btnq
            // 
            this.Btnq.BackColor = System.Drawing.SystemColors.Control;
            this.Btnq.Location = new System.Drawing.Point(8, 8);
            this.Btnq.Name = "Btnq";
            this.Btnq.Size = new System.Drawing.Size(60, 60);
            this.Btnq.TabIndex = 0;
            this.Btnq.Text = "Q";
            this.Btnq.UseVisualStyleBackColor = false;
            this.Btnq.Click += new System.EventHandler(this.Btnq_Click);
            this.Btnq.Enter += new System.EventHandler(this.Btnq_Enter);
            this.Btnq.Leave += new System.EventHandler(this.Btnq_Leave);
            this.Btnq.MouseEnter += new System.EventHandler(this.Btnq_MouseEnter);
            // 
            // dgContents
            // 
            this.dgContents.AllowUserToAddRows = false;
            this.dgContents.AllowUserToDeleteRows = false;
            this.dgContents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgContents.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgContents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgContents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgContents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgContents.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgContents.Location = new System.Drawing.Point(50, 45);
            this.dgContents.Name = "dgContents";
            this.dgContents.ReadOnly = true;
            this.dgContents.RowHeadersVisible = false;
            this.dgContents.RowHeadersWidth = 46;
            this.dgContents.RowTemplate.Height = 28;
            this.dgContents.RowTemplate.ReadOnly = true;
            this.dgContents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgContents.Size = new System.Drawing.Size(820, 240);
            this.dgContents.TabIndex = 1;
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.customButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.customButton2.CornerRadius = 60;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Black;
            this.customButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customButton2.Location = new System.Drawing.Point(376, 532);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(196, 57);
            this.customButton2.TabIndex = 10;
            this.customButton2.Text = "Close";
            this.customButton2.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmShortcut
            // 
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(911, 611);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.dgContents);
            this.Controls.Add(this.pnlKeys);
            this.Location = new System.Drawing.Point(0, 30);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmShortcut";
            this.Text = "Short Cut Keys";
            this.pnlKeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgContents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion components
    }
}

