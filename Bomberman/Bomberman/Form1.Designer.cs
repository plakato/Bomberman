namespace Bomberman
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BNovaHra = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.BSkusitZnovu = new System.Windows.Forms.Button();
            this.BVzdatTo = new System.Windows.Forms.Button();
            this.TCasomiera = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BNovaHra
            // 
            this.BNovaHra.BackColor = System.Drawing.Color.Transparent;
            this.BNovaHra.FlatAppearance.BorderSize = 0;
            this.BNovaHra.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.BNovaHra.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.BNovaHra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BNovaHra.Font = new System.Drawing.Font("Microsoft YaHei UI", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BNovaHra.ForeColor = System.Drawing.Color.Cornsilk;
            this.BNovaHra.Location = new System.Drawing.Point(117, 393);
            this.BNovaHra.Name = "BNovaHra";
            this.BNovaHra.Size = new System.Drawing.Size(313, 82);
            this.BNovaHra.TabIndex = 0;
            this.BNovaHra.Text = "Nová hra";
            this.BNovaHra.UseVisualStyleBackColor = false;
            this.BNovaHra.Click += new System.EventHandler(this.BNovaHra_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // BSkusitZnovu
            // 
            this.BSkusitZnovu.BackColor = System.Drawing.Color.Transparent;
            this.BSkusitZnovu.FlatAppearance.BorderSize = 0;
            this.BSkusitZnovu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BSkusitZnovu.Font = new System.Drawing.Font("Microsoft YaHei UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSkusitZnovu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BSkusitZnovu.Location = new System.Drawing.Point(66, 65);
            this.BSkusitZnovu.Name = "BSkusitZnovu";
            this.BSkusitZnovu.Size = new System.Drawing.Size(267, 140);
            this.BSkusitZnovu.TabIndex = 1;
            this.BSkusitZnovu.Text = "Skúsiť znovu";
            this.BSkusitZnovu.UseVisualStyleBackColor = false;
            this.BSkusitZnovu.Visible = false;
            this.BSkusitZnovu.Click += new System.EventHandler(this.BSkusitZnovu_Click);
            // 
            // BVzdatTo
            // 
            this.BVzdatTo.BackColor = System.Drawing.Color.Transparent;
            this.BVzdatTo.FlatAppearance.BorderSize = 0;
            this.BVzdatTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BVzdatTo.Font = new System.Drawing.Font("Microsoft YaHei UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BVzdatTo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.BVzdatTo.Location = new System.Drawing.Point(429, 107);
            this.BVzdatTo.Name = "BVzdatTo";
            this.BVzdatTo.Size = new System.Drawing.Size(274, 114);
            this.BVzdatTo.TabIndex = 2;
            this.BVzdatTo.Text = "Vzdať to";
            this.BVzdatTo.UseVisualStyleBackColor = false;
            this.BVzdatTo.Visible = false;
            this.BVzdatTo.Click += new System.EventHandler(this.BVzdatTo_Click);
            // 
            // TCasomiera
            // 
            this.TCasomiera.AutoSize = true;
            this.TCasomiera.BackColor = System.Drawing.Color.Red;
            this.TCasomiera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TCasomiera.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCasomiera.Location = new System.Drawing.Point(687, 602);
            this.TCasomiera.Name = "TCasomiera";
            this.TCasomiera.Size = new System.Drawing.Size(121, 30);
            this.TCasomiera.TabIndex = 3;
            this.TCasomiera.Text = "casomiera";
            this.TCasomiera.Visible = false;
            this.TCasomiera.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Bomberman.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(734, 611);
            this.Controls.Add(this.TCasomiera);
            this.Controls.Add(this.BVzdatTo);
            this.Controls.Add(this.BSkusitZnovu);
            this.Controls.Add(this.BNovaHra);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Bomberman";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BNovaHra;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button BSkusitZnovu;
        private System.Windows.Forms.Button BVzdatTo;
        private System.Windows.Forms.Label TCasomiera;
    }
}

