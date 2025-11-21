namespace BezpieczenstwoDanych
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtHaystack;
        private System.Windows.Forms.TextBox txtNeedle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUserBenchmark;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lblValidation;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtHaystack = new System.Windows.Forms.TextBox();
            this.txtNeedle = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUserBenchmark = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.lblValidation = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtHaystack
            this.txtHaystack.Location = new System.Drawing.Point(20, 20);
            this.txtHaystack.Multiline = true;
            this.txtHaystack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHaystack.WordWrap = false;
            this.txtHaystack.Size = new System.Drawing.Size(740, 150);
            this.txtHaystack.PlaceholderText = "Podaj tekst, w którym szukasz...";

            // txtNeedle
            this.txtNeedle.Location = new System.Drawing.Point(20, 180);
            this.txtNeedle.Size = new System.Drawing.Size(400, 23);
            this.txtNeedle.PlaceholderText = "Podaj tekst do wyszukania...";

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(440, 180);
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.Text = "Szukaj";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // btnUserBenchmark
            this.btnUserBenchmark.Location = new System.Drawing.Point(560, 180);
            this.btnUserBenchmark.Size = new System.Drawing.Size(200, 23);
            this.btnUserBenchmark.Text = "Test użytkownika";
            this.btnUserBenchmark.Click += new System.EventHandler(this.BtnUserBenchmark_Click);

            // lstResults
            this.lstResults.Location = new System.Drawing.Point(20, 220);
            this.lstResults.Size = new System.Drawing.Size(740, 200);

            // lblValidation
            this.lblValidation.Location = new System.Drawing.Point(20, 430);
            this.lblValidation.Size = new System.Drawing.Size(740, 23);

            // Form1
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.txtHaystack);
            this.Controls.Add(this.txtNeedle);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnUserBenchmark);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.lblValidation);
            this.Text = "Boyer-Moore Search";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
