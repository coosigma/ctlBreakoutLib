using System;
using System.Collections;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    public partial class ctlBreakout : UserControl
    {
        enum BrickColors { DodgerBlue, ForestGreen, MediumVioletRed, Goldenrod }
        public bool Resizing { get; set; }

        public Form pForm;

        Bitmap Backbuffer;

        Ball ball;
        Plate plate;
        ArrayList Bricks;
        House house;
        Brick ceiling;
        SoundPlayer sndVoid = new SoundPlayer(ctlBreakoutLib.Properties.Resources.Void);
        SoundPlayer sndGameOver = new SoundPlayer(ctlBreakoutLib.Properties.Resources.gameover);
        SoundPlayer sndComplete = new SoundPlayer(ctlBreakoutLib.Properties.Resources.complete);
        SoundPlayer sndHitPlate = new SoundPlayer(ctlBreakoutLib.Properties.Resources.hitPlate);
        SoundPlayer sndHitWall = new SoundPlayer(ctlBreakoutLib.Properties.Resources.hitWall);
        SoundPlayer sndHitBrick = new SoundPlayer(ctlBreakoutLib.Properties.Resources.hitBrick);

        public int Count = 0;
        public int Second = 0;
        public int Score { set;  get; }
        Timer GameTimer = new Timer { Interval = 10 };
        public ctlBreakout()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            GameTimer.Tick += new EventHandler(GameTimer_Tick);

            if (Backbuffer != null)
            {
                using (var g = Graphics.FromImage(Backbuffer))
                {

                }
            }
            sndVoid.Load();
            sndGameOver.Load();
            sndComplete.Load();
            sndHitPlate.Load();
            sndHitWall.Load();
            sndHitBrick.Load();
        }

        private void InitializeGame()
        {
            Random rnd = new Random();
            double bo = rnd.NextDouble()-0.5;
            // Create Figure objects.           
            plate = new Plate(this, new Vector(200d, this.Size.Height-20), new Size(50, 11), Color.White);
            ball = new Ball(this, new Vector(plate.Position.x+plate.Size.Width/2, plate.Position.y-plate.Size.Height), new Size(10, 10), Color.Yellow) { Offset = new Vector(bo, -1.5) };
            house = new House(this, new Vector(0, 0), new Size(this.Size.Width, this.Size.Height + ball.Size.Height), Color.Black);
            ceiling = new Brick(this, new Vector(0, 50d), new Size(this.Size.Width, 10), Color.Gray);
            CreateBricks();
            Count = 0;
            Second = 0;
            Score = 0;
        }
        public void StartGame()
        {
            if (!GameTimer.Enabled)
            {
                InitializeGame();
                GameTimer.Start();
                sndHitPlate.Play();
            }
        }
        public void StopGame()
        {
            if (Count <= 0)
                return;
            GameOver();
        }
        public void PauseGame()
        {
            if (Count <= 0)
                return;
            if (GameTimer.Enabled)
            {
                GameTimer.Stop();
            }
            else
            {
                GameTimer.Start();
            }
        }
        public void RestartGame()
        {
            StopGame();
            StartGame();
        }
        private void GameOver(bool completed=false)
        {
            if (completed)
            {
                sndComplete.Play();
            }                
            else
            {
                sndGameOver.Play();
            }
            
            if (GameTimer.Enabled)
            {
                GameTimer.Stop();
            }
            int score = Score;
            if (Backbuffer != null)
            {
                using (var g = Graphics.FromImage(Backbuffer))
                {
                    String g_o = (completed)? "Congratulations" : "Game Over";
                    String y_s = "Your Score is: " + score.ToString();
                    String time = String.Format("Your time is {0:D2} : {1:D2}", Second / 60, Second % 60);
                    Font drawFont = new Font("Arial", 24);
                    SolidBrush drawBrush = new SolidBrush(Color.White);
                    float mx = (completed) ? 120.0F : 150.0F;
                    PointF drawPoint = new PointF(mx, 110.0F);
                    PointF drawPoint1 = new PointF(110.0F, 210.0F);
                    PointF drawPoint2 = new PointF(80.0F, 310.0F);
                    g.Clear(Color.Black);
                    g.DrawString(g_o, drawFont, drawBrush, drawPoint);
                    g.DrawString(y_s, drawFont, drawBrush, drawPoint1);
                    g.DrawString(time, drawFont, drawBrush, drawPoint2);
                }
                Invalidate();
            }

        }
        private void CreateBricks()
        {
            Bricks = new ArrayList();
            Random randomGen = new Random();
            String[] names = Enum.GetNames(typeof(BrickColors));
            Size bSize = new Size(50, 12);
            int sPointY = Convert.ToInt32(ceiling.Position.y);
            for (int i = 30; i < Size.Width; i += 80)
            {
                for (int j = sPointY+40; j < sPointY+300; j += 40)
                {                    
                    String rcName = names[randomGen.Next(names.Length)];
                    Color randomColor = Color.FromName(rcName);
                    Bricks.Add(new Brick(this, new Vector(i, j), bSize, randomColor));
                    Console.WriteLine("i: " + i + ", j: " + j + " = " + randomColor);
                }
            }
        }

        void ctlBreakout_Paint(object sender, PaintEventArgs e)
        {
            if (Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }
        }

        void Draw()
        {
            if (Backbuffer != null)
            {
                using (var g = Graphics.FromImage(Backbuffer))
                {
                    g.Clear(Color.Black);
                    plate.Draw(g);
                    ball.Draw(g);
                    ceiling.Draw(g);
                    ShowInfo(g);
                    foreach (Brick b in Bricks) {
                        b.Draw(g);
                    }
                }
                Invalidate();
            }
        }

        private void ShowInfo(Graphics g)
        {
            String score = Score.ToString().PadLeft(3, '0');
            Font drawFont = new Font("Arial", 24);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            String time = String.Format("{0:D2} : {1:D2}", Second / 60, Second % 60);
            PointF drawPoint = new PointF(30.0F, 5.0F);
            g.DrawString(time, drawFont, drawBrush, drawPoint);
            drawPoint = new PointF(400.0F, 5.0F);
            g.DrawString(score, drawFont, drawBrush, drawPoint);
        }

        void GameTimer_Tick(object sender, EventArgs e)
        {
            Second = Count / 100;
            Console.WriteLine("s: "+Second);
            Draw();
            ball.Update();
            if (ball.CheckOutside())
            {
                GameOver();
                Console.WriteLine("game over");
            }
            Collision();
            CheckGameOver();
            if (Bricks.Count == 0)
                GameOver(true);
            if (Count % 10 == 0)
                plate.Update();
            Count++;
        }

        private void CheckGameOver()
        {
            if (ball.Position.y > Size.Height)
                GameOver();
        }

        private void Collision()
        {
            if (ball.Collision(plate))
            {
                sndHitPlate.Play();
                return;
            }
                
            if (ball.Collision(house))
            {
                sndHitWall.Play();
                return;
            }
                
            if (ball.Collision(ceiling))
            {
                sndHitWall.Play();
                return;
            }
            foreach (Brick b in Bricks)
            {
                if (ball.Collision(b))
                {
                    sndHitBrick.Play();
                    Bricks.Remove(b);
                    Score++;
                    return;
                }

            }

        }
        private void ctlBreakout_Load_and_CreateBackBuffer(object sender, EventArgs e)
        {
            sndVoid.Play();
            var parent = this.Parent;
            while (!(parent is Form)) parent = parent.Parent;
            pForm = parent as Form; ;
            if (Backbuffer != null)
                Backbuffer.Dispose();
            Backbuffer = new Bitmap(this.Size.Width, this.Size.Height);
        }

        private void ctlBreakout_KeyDown(object sender, KeyEventArgs e)
        {
            int ControlSpeed = 15;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left ) // left
                plate.Move(-ControlSpeed, 0);
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) // right
                plate.Move(ControlSpeed, 0);
            //else if (e.KeyCode == Keys.W) // up
            //    plate.Move(0, -ControlSpeed);
            //else if (e.KeyCode == Keys.S) // down
            //    plate.Move(0, ControlSpeed);
        }

        private void ctlBreakout_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}
