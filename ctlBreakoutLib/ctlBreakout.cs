using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    public partial class ctlBreakout : UserControl
    {
        public bool Resizing { get; set; }

        public Form pForm;

        Bitmap Backbuffer;

        Ball ball;
        Plate plate;
        ArrayList Bricks = new ArrayList();
        House house;

        public ctlBreakout()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            Timer GameTimer = new Timer { Interval = 10 };
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();

            // Create Figure objects.
            ball = new Ball(this, new Vector(10d, 10d), new Size(10, 10), Color.Yellow);
            ball.Offset = new Vector(-0.5, 1);
            plate = new Plate(this, new Vector(10d, 450d), new Size(50, 11), Color.White);
            CreateBricks(Bricks);
            house = new House(this, new Vector(0, 0), new Size(this.Size.Width, this.Size.Height), Color.Black);
        }

        private void CreateBricks(ArrayList bs)
        {
            Size bSize = new Size(50, 12);
            for (int i = 30; i < this.Size.Width; i += 80)
            {
                for (int j = 40; j < 300; j += 40)
                {
                    bs.Add(new Brick(this, new Vector(i, j), bSize, Color.Green));
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
                    foreach(Brick b in Bricks) {
                       b.Draw(g);
                    }
                }
                Invalidate();
            }
        }

        void GameTimer_Tick(object sender, EventArgs e)
        {
            Draw();
            ball.Update();
            Collision();
        }
        private void Collision()
        {
            //if (ball.Collision(plate))
            //    return;
            //if (ball.Collision(house))
            //    return;
            if (ball.Collision(house))
                return;
        }
        private void ctlBreakout_Load_and_CreateBackBuffer(object sender, EventArgs e)
        {
            var parent = this.Parent;
            while (!(parent is Form)) parent = parent.Parent;
            pForm = parent as Form;
            if (Backbuffer != null)
                Backbuffer.Dispose();
            Backbuffer = new Bitmap(this.Size.Width, this.Size.Height);
        }

        private void ctlBreakout_KeyDown(object sender, KeyEventArgs e)
        {
            int ControlSpeed = 10;
            if (e.KeyCode == Keys.A) // left
                plate.Move(-ControlSpeed, 0);
            else if (e.KeyCode == Keys.D) // right
                plate.Move(ControlSpeed, 0);
            else if (e.KeyCode == Keys.W) // up
                plate.Move(0, -ControlSpeed);
            else if (e.KeyCode == Keys.S) // down
                plate.Move(0, ControlSpeed);
        }

        private void ctlBreakout_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}
