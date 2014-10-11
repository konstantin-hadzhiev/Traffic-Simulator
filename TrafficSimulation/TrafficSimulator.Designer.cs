namespace TrafficSimulation
{
    partial class TrafficSimulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrafficSimulator));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_StartSimulation = new System.Windows.Forms.Button();
            this.btn_PauseSimulation = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeCrossingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CrossingOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_StopSimulation = new System.Windows.Forms.Button();
            this.btn_stat = new System.Windows.Forms.Button();
            this.A = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoScrollMargin = new System.Drawing.Size(0, 200);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(149, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 900);
            this.panel1.TabIndex = 0;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
            // 
            // btn_StartSimulation
            // 
            this.btn_StartSimulation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_StartSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StartSimulation.Location = new System.Drawing.Point(12, 291);
            this.btn_StartSimulation.Name = "btn_StartSimulation";
            this.btn_StartSimulation.Size = new System.Drawing.Size(111, 33);
            this.btn_StartSimulation.TabIndex = 4;
            this.btn_StartSimulation.Text = "Start";
            this.btn_StartSimulation.UseVisualStyleBackColor = true;
            this.btn_StartSimulation.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_PauseSimulation
            // 
            this.btn_PauseSimulation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_PauseSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PauseSimulation.Location = new System.Drawing.Point(12, 340);
            this.btn_PauseSimulation.Name = "btn_PauseSimulation";
            this.btn_PauseSimulation.Size = new System.Drawing.Size(111, 37);
            this.btn_PauseSimulation.TabIndex = 5;
            this.btn_PauseSimulation.Text = "Pause ";
            this.btn_PauseSimulation.UseVisualStyleBackColor = true;
            this.btn_PauseSimulation.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeCrossingToolStripMenuItem,
            this.CrossingOptions});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // removeCrossingToolStripMenuItem
            // 
            this.removeCrossingToolStripMenuItem.Name = "removeCrossingToolStripMenuItem";
            this.removeCrossingToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.removeCrossingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeCrossingToolStripMenuItem.Text = "&Remove Crossing";
            this.removeCrossingToolStripMenuItem.Click += new System.EventHandler(this.removeCrossingToolStripMenuItem_Click);
            // 
            // CrossingOptions
            // 
            this.CrossingOptions.Name = "CrossingOptions";
            this.CrossingOptions.Size = new System.Drawing.Size(180, 22);
            this.CrossingOptions.Text = "Crossing options";
            this.CrossingOptions.Click += new System.EventHandler(this.CrossingOptions_Click);
            // 
            // btn_StopSimulation
            // 
            this.btn_StopSimulation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_StopSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StopSimulation.Location = new System.Drawing.Point(12, 392);
            this.btn_StopSimulation.Name = "btn_StopSimulation";
            this.btn_StopSimulation.Size = new System.Drawing.Size(111, 37);
            this.btn_StopSimulation.TabIndex = 7;
            this.btn_StopSimulation.Text = "Stop ";
            this.btn_StopSimulation.UseVisualStyleBackColor = true;
            this.btn_StopSimulation.Click += new System.EventHandler(this.btn_StopSimulation_Click);
            // 
            // btn_stat
            // 
            this.btn_stat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_stat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stat.Location = new System.Drawing.Point(12, 447);
            this.btn_stat.Name = "btn_stat";
            this.btn_stat.Size = new System.Drawing.Size(111, 33);
            this.btn_stat.TabIndex = 8;
            this.btn_stat.Text = "Statistics";
            this.btn_stat.UseVisualStyleBackColor = true;
            this.btn_stat.Click += new System.EventHandler(this.btn_stat_Click);
            // 
            // A
            // 
            this.A.AutoSize = true;
            this.A.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.A.Location = new System.Drawing.Point(299, 21);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(15, 15);
            this.A.TabIndex = 9;
            this.A.Text = "A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(599, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(899, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "C";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1199, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "D";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(129, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(129, 490);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(130, 790);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::TrafficSimulation.Properties.Resources.CcrossingType2;
            this.pictureBox2.Location = new System.Drawing.Point(12, 158);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(111, 96);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::TrafficSimulation.Properties.Resources.type1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // btn_save
            // 
            this.btn_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(12, 523);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(111, 33);
            this.btn_save.TabIndex = 13;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Location = new System.Drawing.Point(12, 571);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(111, 33);
            this.btn_load.TabIndex = 14;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // TrafficSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1001, 685);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.A);
            this.Controls.Add(this.btn_stat);
            this.Controls.Add(this.btn_StopSimulation);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_PauseSimulation);
            this.Controls.Add(this.btn_StartSimulation);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TrafficSimulator";
            this.Text = "Traffic Simulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_StartSimulation;
        private System.Windows.Forms.Button btn_PauseSimulation;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeCrossingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CrossingOptions;
        private System.Windows.Forms.Button btn_StopSimulation;
        private System.Windows.Forms.Button btn_stat;
        private System.Windows.Forms.Label A;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
    }
}

