namespace ServerScan
{
    partial class SelectScanner
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.listScan = new System.Windows.Forms.ListBox();
            this.bt_ok = new System.Windows.Forms.Button();
            this.bt_cancell = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listScan
            // 
            this.listScan.FormattingEnabled = true;
            this.listScan.Location = new System.Drawing.Point(12, 12);
            this.listScan.Name = "listScan";
            this.listScan.Size = new System.Drawing.Size(260, 212);
            this.listScan.TabIndex = 0;
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(116, 230);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 23);
            this.bt_ok.TabIndex = 1;
            this.bt_ok.Text = "OK";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // bt_cancell
            // 
            this.bt_cancell.Location = new System.Drawing.Point(197, 230);
            this.bt_cancell.Name = "bt_cancell";
            this.bt_cancell.Size = new System.Drawing.Size(75, 23);
            this.bt_cancell.TabIndex = 2;
            this.bt_cancell.Text = "Cancel";
            this.bt_cancell.UseVisualStyleBackColor = true;
            this.bt_cancell.Click += new System.EventHandler(this.bt_cancell_Click);
            // 
            // SelectScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bt_cancell);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.listScan);
            this.Name = "SelectScanner";
            this.Text = "Select Scanner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listScan;
        private System.Windows.Forms.Button bt_ok;
        private System.Windows.Forms.Button bt_cancell;
    }
}