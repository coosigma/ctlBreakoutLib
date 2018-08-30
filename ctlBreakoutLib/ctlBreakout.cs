using System;
using System.Collections;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    public partial class ctlBreakout : UserControl
    {
        public bool Resizing { get; set; }
        public Form pForm;
        GameControl gc;
        public Bitmap Backbuffer { get; set; }
        public Size PlayGround { get; set; }
        public ctlBreakout()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            gc = new GameControl(this);
            PlayGround = new Size(500, 500);
        }
        void ctlBreakout_Paint(object sender, PaintEventArgs e)
        {
            if (Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }
        }
        private void ctlBreakout_Load_and_CreateBackBuffer(object sender, EventArgs e)
        {
            SoundPlayer sndVoid = new SoundPlayer(ctlBreakoutLib.Properties.Resources.Void);
            sndVoid.Play();
            var parent = this.Parent;
            while (!(parent is Form)) parent = parent.Parent;
            pForm = parent as Form; ;
            if (Backbuffer != null)
                Backbuffer.Dispose();
            Backbuffer = new Bitmap(PlayGround.Width, PlayGround.Height);
        }

        private void ctlBreakout_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ctlBreakout_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //pbxCover.Hide();
            gc.StartGame();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            gc.StopGame();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            gc.PauseGame();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            //pbxCover.Hide();
            gc.RestartGame();

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            pForm.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//取消方向键对控件的焦点的控件，用自己自定义的函数处理各个方向键的处理函数
        {
            int ControlSpeed = 15;
            switch (keyData)
            {
                case Keys.Left:
                    if (gc.plate != null)
                        gc.plate.Move(-ControlSpeed, 0);
                    return true;
                case Keys.A:
                    if (gc.plate != null)
                        gc.plate.Move(-ControlSpeed, 0);
                    return true;
                case Keys.Right:
                    if (gc.plate != null)
                        gc.plate.Move(ControlSpeed, 0);
                    return true;
                case Keys.D:
                    if (gc.plate != null)
                        gc.plate.Move(ControlSpeed, 0);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
