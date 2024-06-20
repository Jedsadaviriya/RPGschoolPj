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
            ControlBox = false,
            MaximizeBox = false,
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual
        };

        private PictureBox door;
        private List<PictureBox> walls = new List<PictureBox>();
        private List<PictureBox> doors = new List<PictureBox>();
        public PictureBox picturebox;
        private Random spawn = new Random();
        private int enemydamageradius = 60;
        private int enemyHealth = 100;
        private int enemydamage = 2;
        private int playerHealth = 100;
        private int playerdamage = 10;
        private int XPamount = 0;
        private int weapondamage = 0;
        private int distance = 7;
        private bool movingUp, movingDown, movingLeft, movingRight;
        public Label PlayerhealthLabel = new Label();
        private bool enemy1dead = false;

        public Start()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(960, 0);
            InitializeComponent();
            UpdatePlayerHealthLabel();
            Wall(0, 0, 500, 10);
            Wall(930, 0, 500, 15);
            Wall(0, 75, 10, 930);
            Wall(0, 480, 15, 930);
            Door(920, 250, 50, 10);
            UpdateHealthLabel();
            this.KeyPreview = true;
            this.KeyDown += MoveControl;
            this.KeyUp += Start_KeyUp;
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            regentimer.Start();
            InitializeInventoryForm();
            inventoryForm.Location = new Point(0, 0);
            InitializeEnemy();
        }

        

        private void InitializeInventoryForm()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 5,
                Dock = DockStyle.Fill
            };

            for (int i = 0; i < 5; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        Text = string.Empty
                    };
                    tableLayoutPanel.Controls.Add(button, col, row);
                }
            }

            inventoryForm.Controls.Add(tableLayoutPanel);
            inventoryForm.ClientSize = new Size(400, 400);
            inventoryForm.Text = "Inventory";
        }

        private void Door(int xPos, int yPos, int hight, int width)
        {
            PictureBox door = new PictureBox
            {
                BackColor = Color.Gold,
                Location = new Point(xPos, yPos),
                Width = width,
                Height = hight
            };
            Controls.Add(door);
            doors.Add(door);
            
        }

        private void nextRoom()
        {
            // Store the current XP and other necessary states
            int currentXP = XPamount;
            Point newLocation = new Point(200, 200);
            pictureBox1.Location = newLocation;
            // Initialize the new room
            room1 newRoom = new room1(currentXP);

            // Show the new room and hide the current form
            newRoom.Show();
            this.Hide();
            
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

        //private bool IsCollidingWithDoor(PictureBox player)
        //{
        //    foreach (var door in doors)
        //    {
        //        if (player.Bounds.IntersectsWith(door.Bounds))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        private bool IsCollidingWithDoor(PictureBox player)
        {
            foreach (var door in doors)
            {
                if (player.Bounds.IntersectsWith(door.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point originalPosition = pictureBox1.Location;
            

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

            

            if (IsCollidingWithWall(pictureBox1))
            {
                pictureBox1.Location = originalPosition;
            }

            if (IsCollidingWithDoor(pictureBox1))
            {
                nextRoom();
            }

            if (enemy1dead == false)
            {
                FollowPlayer(enemy1);
            }


            

            UpdateHealthLabelPosition();
            UpdatePlayerHealthLabelPosition();
            CheckPLayerDamage();
        }


        private void FollowPlayer(Enemy enemy)
        {
            if (!enemy1dead)
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
        }
        private void takeDMG(object sender, EventArgs e)
    {
        enemyHealth -= playerdamage + weapondamage; // You can adjust the amount of damage per click

        // Update health bar
        UpdateHealthLabel(); // Update health label

            // Check if enemy health is zero or below
            if (enemyHealth <= 0)
            {
                enemy1dead = true;
                XPamount += 10;
                UpdateXPLabel();
                pictureBox2.Dispose(); // Hide the enemy     
                Controls.Remove(pictureBox2);
                Controls.Remove(healthLabel);

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


            if (playerHealth == 0)
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

        private void regentimer_Tick(object sender, EventArgs e)
        {
            regen();
        }

        public void spawer()
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private Enemy enemy1;

        private void InitializeEnemy()
        {
            PictureBox enemyPictureBox = new PictureBox
            {
                BackColor = Color.Red,
                Size = new Size(50, 50),
                Location = new Point(200, 200) // Initial position
            };
            Controls.Add(enemyPictureBox);
            

            //enemy1 = new Enemy(enemyPictureBox, enemyPictureBox.Location);
        }

    }

    public class Enemy
    {
        public PictureBox PictureBox { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        private int speed;
        private Point initialPosition;

        public Enemy(Point initialPosition, int health, int damage, int speed, Color color)
        {
            this.initialPosition = initialPosition;
            this.Health = health;
            this.Damage = damage;
            this.speed = speed;
            InitializeEnemy(color);
        }

        private void InitializeEnemy(Color color)
        {
            PictureBox = new PictureBox
            {
                Size = new Size(50, 50),
                Location = initialPosition,
                BackColor = color
            };
        }

        public void FollowPlayer(PictureBox player)
        {
            if (player.Left < PictureBox.Left)
                PictureBox.Left -= speed;
            else if (player.Left > PictureBox.Left)
                PictureBox.Left += speed;

            if (player.Top < PictureBox.Top)
                PictureBox.Top -= speed;
            else if (player.Top > PictureBox.Top)
                PictureBox.Top += speed;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                PictureBox.Hide();
            }
        }

        public bool IsCollidingWithPlayer(PictureBox player, int damageRadius)
        {
            return PictureBox.Bounds.IntersectsWith(new Rectangle(
                player.Location.X - damageRadius,
                player.Location.Y - damageRadius,
                player.Width + damageRadius * 2,
                player.Height + damageRadius * 2
            ));
        }
    }




}
