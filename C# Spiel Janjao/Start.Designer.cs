namespace C__Spiel_Janjao
{
    partial class Start
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
            components = new System.ComponentModel.Container();
            Close = new Button();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox2 = new PictureBox();
            healthLabel = new Label();
            label1 = new Label();
            regentimer = new System.Windows.Forms.Timer(components);
            TimerEnemySpawn = new System.Windows.Forms.Timer(components);
            EnemyDMG_tick = new System.Windows.Forms.Timer(components);
            Timer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // Close
            // 
            Close.Location = new Point(12, 12);
            Close.Name = "Close";
            Close.Size = new Size(150, 50);
            Close.TabIndex = 4;
            Close.Text = "QUIT";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Lime;
            pictureBox1.Location = new Point(430, 240);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Red;
            pictureBox2.Location = new Point(698, 240);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(50, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Click += takeDMG;
            // 
            // healthLabel
            // 
            healthLabel.AutoSize = true;
            healthLabel.Location = new Point(698, 217);
            healthLabel.Name = "healthLabel";
            healthLabel.Size = new Size(0, 20);
            healthLabel.TabIndex = 7;
            healthLabel.Click += healthLabel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(223, 29);
            label1.Name = "label1";
            label1.Size = new Size(80, 35);
            label1.TabIndex = 8;
            label1.Text = "XP : 0";
            // 
            // regentimer
            // 
            regentimer.Interval = 1000;
            regentimer.Tick += regentimer_Tick;
            // 
            // TimerEnemySpawn
            // 
            TimerEnemySpawn.Enabled = true;
            TimerEnemySpawn.Tick += TimerEnemySpawn_Tick_1;
            // 
            // EnemyDMG_tick
            // 
            EnemyDMG_tick.Tick += EnemyDMG_tick_Tick;
            // 
            // Start
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 493);
            Controls.Add(label1);
            Controls.Add(healthLabel);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(Close);
            MaximizeBox = false;
            Name = "Start";
            TopMost = true;
            Load += Start_Load;
            Click += Start_Click;
            KeyDown += MoveControl;
            KeyUp += MoveControl;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Close;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox2;
        private Label healthLabel;
        private Label label1;
        private System.Windows.Forms.Timer regentimer;
        private System.Windows.Forms.Timer TimerEnemySpawn;
        private System.Windows.Forms.Timer EnemyDMG_tick;
        private System.Windows.Forms.Timer Timer;
    }
}