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
    public partial class Start : Form
    {
        
        private Form inventoryForm = new Form()
        {
            ControlBox = false, MaximizeBox = false, FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual

        };
        

        










        public PictureBox picturebox;
        Random spawn = new Random();
        public int PosSpawnx;
        public int PosSpawny;

        private int enemydamageradius = 60;
        private int enemyHealth = 100;
        int enemydamage = 2;
        private int playerHealth = 100;
        int playerdamage = 10;
        int XPamount = 0;
        int weapondamage = 0;
        private int distance = 7;
        private bool movingUp, movingDown, movingLeft, movingRight;
        Label PlayerhealthLabel = new Label();

        bool enemy1dead = false;

        public Start()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(960, 0);

            InitializeComponent();
            UpdatePlayerHealthLabel();
            Wall(150, 150);
            UpdateHealthLabel();
            this.KeyPreview = true;
            this.KeyDown += MoveControl;
            this.KeyUp += Start_KeyUp;
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            regentimer.Start();
            InitializeInventoryForm();
            GenerateItem();
            inventoryForm.Location = new Point(0, 0);
            
        }
        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void InitializeInventoryForm()
        {
            // Create a TableLayoutPanel
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 5,
                Dock = DockStyle.Fill
            };

            // Set row and column styles
            for (int i = 0; i < 5; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }

            // Add buttons to each cell of the grid
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        Text = string.Empty // Empty button for now, you can add more details later
                    };
                    tableLayoutPanel.Controls.Add(button, col, row);
                }
            }

            // Add the TableLayoutPanel to the inventoryForm
            inventoryForm.Controls.Add(tableLayoutPanel);
            inventoryForm.ClientSize = new System.Drawing.Size(400, 400);
            inventoryForm.Text = "Inventory";
        }


        static void GenerateItem()
        {
            PictureBox itemPictureBox = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Red // Set the background color of the PictureBox
            };

            // Display the PictureBox in a form (optional)
            Form form = new Form
            {
                Width = 100,
                Height = 100,
                Controls = { itemPictureBox }
            };
        }















        private void Wall(int xPos, int yPos)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Black;
            pictureBox.Location = new Point(xPos, yPos);
            pictureBox.Width = 50;
            pictureBox.Height = 50;
            Controls.Add(pictureBox);
            if (movingRight && pictureBox1.Right + distance <= pictureBox.Width)
            {
                pictureBox1.Left = Math.Min(pictureBox.Width - pictureBox1.Width, pictureBox1.Left + distance);
            }
            if (movingLeft && pictureBox1.Left - distance >= 0)
            {
                pictureBox1.Left = Math.Max(0, pictureBox1.Left - distance);
            }
            if (movingDown && pictureBox1.Bottom + distance <= pictureBox.Height)
            {
                pictureBox1.Top = Math.Min(pictureBox.Height - pictureBox1.Height, pictureBox1.Top + distance);
            }
            if (movingUp && pictureBox1.Top - distance >= 75)
            {
                pictureBox1.Top = Math.Max(0, pictureBox1.Top - distance);
            }
        }

        private void UpdateHealthLabelPosition()
        {
            // Update health label position relative to the picture box
            healthLabel.Location = new Point(
                pictureBox2.Left + (pictureBox1.Width - healthLabel.Width) / 2,
                pictureBox2.Top - healthLabel.Height - 5); // Adjust the vertical offset as needed
        }
        private void UpdateHealthLabel()
        {
            // Update health label text with current health value
            healthLabel.Text = $"Health: {enemyHealth}";
        }
        private void UpdatePlayerHealthLabelPosition()
        {
            // Update health label position relative to the picture box
            PlayerhealthLabel.Location = new Point(
                pictureBox1.Left - 25, pictureBox1.Top - 25); // Adjust the vertical offset as needed
        }
        private void UpdatePlayerHealthLabel()
        {
            // Update health label text with current health value
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
                case Keys.W:
                    movingUp = true;
                    break;
                case Keys.S:
                    movingDown = true;
                    break;
                case Keys.A:
                    movingLeft = true;
                    break;
                case Keys.D:
                    movingRight = true;
                    break;
            }
            if (e.KeyCode == Keys.I)
            {
                inventoryForm.Show();
            }
        }

        

        private void Start_KeyUp(object sender, KeyEventArgs e)
        {
            

            switch (e.KeyCode)
            {
                case Keys.W:
                    movingUp = false;
                    break;
                case Keys.S:
                    movingDown = false;
                    break;
                case Keys.A:
                    movingLeft = false;
                    break;
                case Keys.D:
                    movingRight = false;
                    break;
            }
            if (e.KeyCode == Keys.I)
            {
                inventoryForm.Hide();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Border
            if (movingRight && pictureBox1.Right + distance <= ClientSize.Width)
            {
                pictureBox1.Left = Math.Min(ClientSize.Width - pictureBox1.Width, pictureBox1.Left + distance);
            }
            if (movingLeft && pictureBox1.Left - distance >= 0)
            {
                pictureBox1.Left = Math.Max(0, pictureBox1.Left - distance);
            }
            if (movingDown && pictureBox1.Bottom + distance <= ClientSize.Height)
            {
                pictureBox1.Top = Math.Min(ClientSize.Height - pictureBox1.Height, pictureBox1.Top + distance);
            }
            if (movingUp && pictureBox1.Top - distance >= 75)
            {
                pictureBox1.Top = Math.Max(0, pictureBox1.Top - distance);
            }
            // Enemy Follow Player
            if (enemy1dead == false)
            {

                if (pictureBox2.Location.X < pictureBox1.Location.X + 50)
                {
                    pictureBox2.Left = Math.Min(ClientSize.Width - pictureBox2.Width, pictureBox2.Left + (distance / 3));
                }
                if (pictureBox2.Location.X > pictureBox1.Location.X - 50)
                {
                    pictureBox2.Left = Math.Max(0, pictureBox2.Left - (distance / 3));
                }
                if (pictureBox2.Location.Y < pictureBox1.Location.Y + 50)
                {
                    pictureBox2.Top = Math.Min(ClientSize.Height - pictureBox2.Height, pictureBox2.Top + (distance / 3));
                }
                if (pictureBox2.Location.Y > pictureBox1.Location.Y - 50)
                {
                    pictureBox2.Top = Math.Max(0, pictureBox2.Top - (distance / 3));
                }
            }
            
            UpdateHealthLabelPosition();
            UpdatePlayerHealthLabelPosition();
            CheckPLayerDamage();
        }


        private void Start_Click(object sender, EventArgs e)
        {

        }

        private void takeDMG(object sender, EventArgs e)
        {
            enemyHealth -= playerdamage + weapondamage; // You can adjust the amount of damage per click

            // Update health bar
            UpdateHealthLabel(); // Update health label

            // Check if enemy health is zero or below
            if (enemyHealth <= 0)
            {
                XPamount += 10;
                UpdateXPLabel();
                enemy1dead = true;
                // Remove the enemy from the form
                Controls.Remove(pictureBox2);
                pictureBox2.Dispose(); // Clean up resources
                Controls.Remove(healthLabel); // Remove health label if enemy is defeated
            }
            else
            {
                UpdateHealthLabelPosition(); // Update health label position
            }
        }
        private void PlayerDamage()
        {
            if (playerHealth > 0 && enemy1dead == false)
            {
                playerHealth -= enemydamage;
                UpdatePlayerHealthLabel();
            }


            if (playerdamage == 0)
            {
                bool Gameover = true;
                if (Gameover)
                {
                    pictureBox1.Dispose();
                }
            }
            else
            {
                UpdatePlayerHealthLabel();
            }
        }
        private void CheckPLayerDamage()
        {
            if (pictureBox2.Location.X <= pictureBox1.Location.X + enemydamageradius && pictureBox2.Location.X >= pictureBox1.Location.X - enemydamageradius)
            {
                if (pictureBox2.Location.Y <= pictureBox1.Location.Y + enemydamageradius && pictureBox2.Location.Y >= pictureBox1.Location.Y - enemydamageradius)
                {
                    PlayerDamage();
                }
            }
            if (playerHealth <= 0)
            {
                pictureBox1.Dispose();
                timer1.Stop();
                string gameover = "Game over";
                MessageBox.Show(gameover);
                Application.Exit();

            }
        }
        private void regen()
        {
            if (playerHealth > 0 && playerHealth < 100)
            {
                playerHealth += 1;
                UpdatePlayerHealthLabel();
            }
        }
        private void enemyspawn()
        {

        }


        private void healthLabel_Click(object sender, EventArgs e)
        {

        }


        private void Start_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void regentimer_Tick(object sender, EventArgs e)
        {
            int regentick = 0;
            if (regentick == 0)
            {
                regen();
                regentick += 1;
            }
            if (regentick == 1)
            {
                regentick -= 1;
            }
            else
            {
                regen();
                regentick = 0;
            }

        }

        public void spawer()
        {
            
            PosSpawnx = spawn.Next(50, 910);
            PosSpawny = spawn.Next(125, 490);
            picturebox.BackColor = Color.Red;
            Height = 50;
            Width = 50;
            picturebox.Location = new Point(PosSpawnx, PosSpawny);
            timer1.Start();
            Controls.Add(picturebox);
            if (enemy1dead == false)
            {

                if (picturebox.Location.X < pictureBox1.Location.X + 50)
                {
                    picturebox.Left = Math.Min(ClientSize.Width - picturebox.Width, picturebox.Left + (distance / 3));
                }
                if (picturebox.Location.X > pictureBox1.Location.X - 50)
                {
                    picturebox.Left = Math.Max(0, picturebox.Left - (distance / 3));
                }
                if (picturebox.Location.Y < pictureBox1.Location.Y + 50)
                {
                    picturebox.Top = Math.Min(ClientSize.Height - picturebox.Height, picturebox.Top + (distance / 3));
                }
                if (picturebox.Location.Y > pictureBox1.Location.Y - 50)
                {
                    picturebox.Top = Math.Max(0, picturebox.Top - (distance / 3));
                }
            }
        


        }
        private void spawning()
        {
            
        }

        private void TimerEnemySpawn_Tick(object sender, EventArgs e)
        {
            Random hori = new Random();
            Random vert = new Random();
            int Horiz = hori.Next(50, 910);
            int Verti = vert.Next(125, 490);
            TimerEnemySpawn.Start();
            for (int i = 0; i < 3; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.Black;
                pictureBox.Location = new Point(Horiz, Verti);
                pictureBox.Width = 50;
                pictureBox.Height = 50;
                if (enemy1dead == false)
                {

                    if (pictureBox.Location.X < pictureBox1.Location.X + 50)
                    {
                        pictureBox.Left = Math.Min(ClientSize.Width - pictureBox.Width, pictureBox.Left + (distance / 3));
                    }
                    if (pictureBox.Location.X > pictureBox1.Location.X - 50)
                    {
                        pictureBox.Left = Math.Max(0, pictureBox.Left - (distance / 3));
                    }
                    if (pictureBox.Location.Y < pictureBox1.Location.Y + 50)
                    {
                        pictureBox.Top = Math.Min(ClientSize.Height - pictureBox.Height, pictureBox.Top + (distance / 3));
                    }
                    if (pictureBox.Location.Y > pictureBox1.Location.Y - 50)
                    {
                        pictureBox.Top = Math.Max(0, pictureBox.Top - (distance / 3));
                    }
                }
            }
        }

        private void EnemyDMG_tick_Tick(object sender, EventArgs e)
        {

        }

        private void TimerEnemySpawn_Tick_1(object sender, EventArgs e)
        {
            Random hori = new Random();
            Random vert = new Random();
            int Horiz = hori.Next(50, 910);
            int Verti = vert.Next(125, 490);
            TimerEnemySpawn.Start();
            for (int i = 0; i < 3; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.Black;
                pictureBox.Location = new Point(Horiz, Verti);
                pictureBox.Width = 50;
                pictureBox.Height = 50;
                if (enemy1dead == false)
                {

                    if (pictureBox.Location.X < pictureBox1.Location.X + 50)
                    {
                        pictureBox.Left = Math.Min(ClientSize.Width - pictureBox.Width, pictureBox.Left + (distance / 3));
                    }
                    if (pictureBox.Location.X > pictureBox1.Location.X - 50)
                    {
                        pictureBox.Left = Math.Max(0, pictureBox.Left - (distance / 3));
                    }
                    if (pictureBox.Location.Y < pictureBox1.Location.Y + 50)
                    {
                        pictureBox.Top = Math.Min(ClientSize.Height - pictureBox.Height, pictureBox.Top + (distance / 3));
                    }
                    if (pictureBox.Location.Y > pictureBox1.Location.Y - 50)
                    {
                        pictureBox.Top = Math.Max(0, pictureBox.Top - (distance / 3));
                    }
                }
            }
        }
        
    }
    public class enemy
    {
        
        
        //


    }
}
    
