using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    class GameControl
    {
        enum BrickColors { DodgerBlue, ForestGreen, MediumVioletRed, Goldenrod }

        ctlBreakout CB;

        Ball ball;
        public Plate plate;
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
        public int Score { set; get; }
        Timer GameTimer = new Timer { Interval = 10 };
        public GameControl(ctlBreakout cb)
        {
            CB = cb;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            sndGameOver.Load();
            sndComplete.Load();
            sndHitPlate.Load();
            sndHitWall.Load();
            sndHitBrick.Load();
        }

        private void GameControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            int ControlSpeed = 15;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) // left
                if (plate != null)
                    plate.Move(-ControlSpeed, 0);
                else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) // right
                    if (plate != null)
                        plate.Move(ControlSpeed, 0);
        }

        private void InitializeGame()
        {
            Random rnd = new Random();
            double bo = rnd.NextDouble() - 0.5;
            // Create Figure objects.           
            plate = new Plate(CB, new Vector(200d, CB.PlayGround.Height - 20), new Size(50, 11), Color.White);
            ball = new Ball(CB, new Vector(plate.Position.x + plate.Size.Width / 2, plate.Position.y - plate.Size.Height), new Size(10, 10), Color.Yellow) { Offset = new Vector(bo, -1.5) };
            house = new House(CB, new Vector(0, 0), new Size(CB.PlayGround.Width, CB.PlayGround.Height + ball.Size.Height), Color.Black);
            ceiling = new Brick(CB, new Vector(0, 50d), new Size(CB.PlayGround.Width, 10), Color.Gray);
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
        public void StopGame(bool restart = false)
        {
            if (Count <= 0)
                return;
            if (restart)
            {
                GameTimer.Stop();                
            }
            else
            {
                GameOver();
            }
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
            StopGame(true);
            StartGame();
        }
        private void GameOver(bool completed = false)
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
            if (CB.Backbuffer != null)
            {
                using (var g = Graphics.FromImage(CB.Backbuffer))
                {
                    String g_o = (completed) ? "Congratulations" : "Game Over";
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
                CB.Invalidate();
            }

        }
        private void CreateBricks()
        {
            Bricks = new ArrayList();
            Random randomGen = new Random();
            String[] names = Enum.GetNames(typeof(BrickColors));
            Size bSize = new Size(50, 12);
            int sPointY = Convert.ToInt32(ceiling.Position.y);
            for (int i = 30; i < CB.PlayGround.Width; i += 80)
            {
                for (int j = sPointY + 40; j < sPointY + 300; j += 40)
                {
                    String rcName = names[randomGen.Next(names.Length)];
                    Color randomColor = Color.FromName(rcName);
                    Bricks.Add(new Brick(this, new Vector(i, j), bSize, randomColor));
                }
            }
        }

        void ctlBreakout_Paint(object sender, PaintEventArgs e)
        {
            if (CB.Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(CB.Backbuffer, Point.Empty);
            }
        }

        void Draw()
        {
            if (CB.Backbuffer != null)
            {
                using (var g = Graphics.FromImage(CB.Backbuffer))
                {
                    g.Clear(Color.Black);
                    plate.Draw(g);
                    ball.Draw(g);
                    ceiling.Draw(g);
                    ShowInfo(g);
                    foreach (Brick b in Bricks)
                    {
                        b.Draw(g);
                    }
                }
                CB.Invalidate();
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
            Draw();
            ball.Update();
            if (ball.CheckOutside(CB.PlayGround))
            {
                GameOver();
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
            if (ball.Position.y > CB.PlayGround.Height)
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
    }
}
