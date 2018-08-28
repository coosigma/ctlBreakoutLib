using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class frmBreakout : Form
    {
        public frmBreakout()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ctlBreakout.StartGame();
            ctlBreakout.Focus();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ctlBreakout.RestartGame();
            ctlBreakout.Focus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ctlBreakout.StopGame();
            ctlBreakout.Focus();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            ctlBreakout.PauseGame();
            ctlBreakout.Focus();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
