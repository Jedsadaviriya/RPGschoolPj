namespace C__Spiel_Janjao
{
    partial class room1
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
            Close = new Button();
            SuspendLayout();
            // 
            // Close
            // 
            Close.Location = new Point(12, 12);
            Close.Name = "Close";
            Close.Size = new Size(150, 50);
            Close.TabIndex = 5;
            Close.Text = "QUIT";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click;
            // 
            // room1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 493);
            Controls.Add(Close);
            Location = new Point(0, 0);
            Name = "room1";
            Text = "room1";
            Controls.SetChildIndex(Close, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Close;
        private Label healthLabel;
        private Label label1;
        
    }
}