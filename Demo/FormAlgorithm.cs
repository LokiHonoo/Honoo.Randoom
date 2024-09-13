using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Demo
{
    public partial class FormAlgorithm : Form
    {
        private readonly List<byte> _seed = new();
        private readonly TimeSpan _span = TimeSpan.FromMilliseconds(50);
        private string _hash = "SHA1";
        private DateTime _time = DateTime.Now;

        public FormAlgorithm()
        {
            InitializeComponent();
        }

        public string Hash => _hash;
        public List<byte> Seed => _seed;

        private void ComboBoxHashAlgorithm_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _hash = this.ComboBoxHashAlgorithm.Text;
        }

        private void FormAlgorithm_Load(object sender, System.EventArgs e)
        {
            this.ComboBoxHashAlgorithm.SelectedIndex = 0;
        }

        private void PanelInput_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ProgressBarStep.Value < 100)
            {
                DateTime now = DateTime.Now;
                if (now - _time >= _span)
                {
                    _seed.Add((byte)(e.X | e.Y));
                    this.ProgressBarStep.Value++;
                    _time = now;
                }
            }
        }
    }
}