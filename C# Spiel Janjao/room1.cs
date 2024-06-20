using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C__Spiel_Janjao.Resources;
using Microsoft.VisualBasic.ApplicationServices;

namespace C__Spiel_Janjao
{
    public partial class room1 : Form
    {
        private Form inventoryForm = new Form()
        {
            ControlBox = false,
            MaximizeBox = false,
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual
        };
        private PictureBox pictureBox1 = new PictureBox();
        private PictureBox pictureBox2 = new PictureBox();
        private int playerXP;
        private List<PictureBox> walls = new List<PictureBox>();
        private List<PictureBox> doors = new List<PictureBox>();
        public PictureBox picturebox;
        private int enemyHealth = 100;
        private int enemydamage = 2;
        private int playerHealth = 100;
        private int playerdamage = 10;
        private int XPamount = 0;
        private int weapondamage = 0;
        private int distance = 7;
        private bool movingUp, movingDown, movingLeft, movingRight;
        private bool enemy1dead = false;
        public Label PlayerhealthLabel = new Label();
        // Constructor that accepts an XP value
        public room1(int XP)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(960, 0);
            InitializeComponent();
            playerXP = XP; // Store the XP value

            // Example of how to use the XP value
            Label xpLabel = new Label();
            {
                Text = $"XP: {playerXP}";
                Location = new Point(170, 16);
                AutoSize = true;
            };
            xpLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            Controls.Add(xpLabel);
            Wall(0, 0, 500, 10);
            Wall(930, 0, 500, 15);
            Wall(0, 75, 10, 930);
            Wall(0, 480, 15, 930);


        }
        private void Wall(int xPos, int yPos, int hight, int width)
        {
            PictureBox wall = new PictureBox
            {
                BackColor = Color.Black,
                Location = new Point(xPos, yPos),
                Width = width,
                Height = hight
            };
            Controls.Add(wall);
            walls.Add(wall);
        }

        

        private bool IsCollidingWithWall(PictureBox player)
        {
            foreach (var wall in walls)
            {
                if (player.Bounds.IntersectsWith(wall.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void UpdateHealthLabelPosition()
        {
            healthLabel.Location = new Point(
                pictureBox2.Left + (pictureBox1.Width - healthLabel.Width) / 2,
                pictureBox2.Top - healthLabel.Height - 5);
        }

        private void UpdateHealthLabel()
        {
            healthLabel.Text = $"Health: {enemyHealth}";
        }

        private void UpdatePlayerHealthLabelPosition()
        {
            PlayerhealthLabel.Location = new Point(
                pictureBox1.Left - 25, pictureBox1.Top - 25);
        }

        private void UpdatePlayerHealthLabel()
        {
            PlayerhealthLabel.Text = $"Health: {playerHealth}";
            PlayerhealthLabel.AutoSize = true;
            PlayerhealthLabel.Font = new Font("Calibri", 10);
            PlayerhealthLabel.ForeColor = Color.Black;
            PlayerhealthLabel.Padding = new Padding(6);
            this.Controls.Add(PlayerhealthLabel);
        }

        private void UpdateXPLabel()
        {
            label1.Text = $"XP: {XPamount}";
        }

        private void MoveControl(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: movingUp = true; break;
                case Keys.S: movingDown = true; break;
                case Keys.A: movingLeft = true; break;
                case Keys.D: movingRight = true; break;
                case Keys.I: inventoryForm.Show(); break;
            }
        }

        private void Start_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: movingUp = false; break;
                case Keys.S: movingDown = false; break;
                case Keys.A: movingLeft = false; break;
                case Keys.D: movingRight = false; break;
                case Keys.I: inventoryForm.Hide(); break;
            }
        }

        
    }
    class Player()
    {
        PictureBox pictureBox1;
    }
}
