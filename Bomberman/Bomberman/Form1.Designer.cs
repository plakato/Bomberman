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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BNovaHra = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.BSkusitZnovu = new System.Windows.Forms.Button();
            this.BVzdatTo = new System.Windows.Forms.Button();
            this.TCasomiera = new System.Windows.Forms.Label();
            this.BDalsiLevel = new System.Windows.Forms.Button();
            this.BReplay = new System.Windows.Forms.Button();
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
            this.TCasomiera.BackColor = System.Drawing.Color.Firebrick;
            this.TCasomiera.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TCasomiera.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCasomiera.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TCasomiera.Location = new System.Drawing.Point(669, 613);
            this.TCasomiera.Name = "TCasomiera";
            this.TCasomiera.Size = new System.Drawing.Size(60, 30);
            this.TCasomiera.TabIndex = 3;
            this.TCasomiera.Text = "casomiera";
            this.TCasomiera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TCasomiera.Visible = false;
            this.TCasomiera.Click += new System.EventHandler(this.label1_Click);
            // 
            // BDalsiLevel
            // 
            this.BDalsiLevel.BackColor = System.Drawing.Color.Transparent;
            this.BDalsiLevel.FlatAppearance.BorderSize = 0;
            this.BDalsiLevel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.BDalsiLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.BDalsiLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BDalsiLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BDalsiLevel.ForeColor = System.Drawing.Color.Blue;
            this.BDalsiLevel.Location = new System.Drawing.Point(151, 449);
            this.BDalsiLevel.Name = "BDalsiLevel";
            this.BDalsiLevel.Size = new System.Drawing.Size(313, 82);
            this.BDalsiLevel.TabIndex = 4;
            this.BDalsiLevel.Text = "Ďalší level";
            this.BDalsiLevel.UseVisualStyleBackColor = false;
            this.BDalsiLevel.Visible = false;
            this.BDalsiLevel.Click += new System.EventHandler(this.BDalsiLevel_Click);
            // 
            // BReplay
            // 
            this.BReplay.BackColor = System.Drawing.Color.Transparent;
            this.BReplay.FlatAppearance.BorderSize = 0;
            this.BReplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BReplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BReplay.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.BReplay.Image = ((System.Drawing.Image)(resources.GetObject("BReplay.Image")));
            this.BReplay.Location = new System.Drawing.Point(439, 65);
            this.BReplay.Name = "BReplay";
            this.BReplay.Size = new System.Drawing.Size(322, 100);
            this.BReplay.TabIndex = 5;
            this.BReplay.UseVisualStyleBackColor = false;
            this.BReplay.Visible = false;
            this.BReplay.Click += new System.EventHandler(this.BReplay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(823, 649);
            this.Controls.Add(this.BReplay);
            this.Controls.Add(this.BDalsiLevel);
            this.Controls.Add(this.TCasomiera);
            this.Controls.Add(this.BVzdatTo);
            this.Controls.Add(this.BSkusitZnovu);
            this.Controls.Add(this.BNovaHra);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Bomberman";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BNovaHra;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button BSkusitZnovu;
        private System.Windows.Forms.Button BVzdatTo;
        private System.Windows.Forms.Label TCasomiera;
        private System.Windows.Forms.Button BDalsiLevel;
        private System.Windows.Forms.Button BReplay;
    }
}

