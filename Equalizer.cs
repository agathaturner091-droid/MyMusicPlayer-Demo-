using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MusicPlayer
{
    public partial class Equalizer : Form
    {
        private Main _mainForm;

        public Equalizer(Main main)
        {
            InitializeComponent();
            this._mainForm = main;
            SetupTrackBars();
        }

        private void SetupTrackBars()
        {
            // 将你的滑动条按顺序放入数组，方便通过索引访问
            TrackBar[] bars = {
            trackBar31hz, trackBar62hz, trackBar125hz, trackBar250hz, trackBar500hz,
            trackBar1khz, trackBar2khz, trackBar4khz, trackBar8khz, trackBar16khz
        };

            for (int i = 0; i < bars.Length; i++)
            {
                int index = i; // 闭包捕获
                bars[i].Minimum = -15; // 最小 -15dB
                bars[i].Maximum = 15;  // 最大 15dB

                bars[i].Scroll += (s, e) =>
                {
                    TrackBar tb = (TrackBar)s;
                    // 调用 Main 的接口更新音频
                    _mainForm.UpdateEqGain(index, (float)tb.Value);
                };
            }
        }
    }
}
