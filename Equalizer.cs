using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;

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
            TrackBar[] bars = {
        trackBar31hz, trackBar62hz, trackBar125hz, trackBar250hz, trackBar500hz,
        trackBar1khz, trackBar2khz, trackBar4khz, trackBar8khz, trackBar16khz};

            // 1. 从 Main 窗体获取之前存好的数值
            float[] savedGains = _mainForm.GetCurrentEqGains();

            for (int i = 0; i < bars.Length; i++)
            {
                int index = i;
                bars[i].Minimum = -20;
                bars[i].Maximum = 20;

                // 2. 将滑动条位置还原到上次设置的状态
                bars[i].Value = (int)savedGains[i];

                bars[i].Scroll += (s, e) =>
                {
                    _mainForm.UpdateEqGain(index, (float)bars[index].Value);
                };
            }
        }

        private void pictureBoxSave_Click(object sender, EventArgs e)
        {
            try
            {
                float[] gains = _mainForm.GetCurrentEqGains();
                string data = string.Join(",", gains);
                File.WriteAllText("eq_config.txt", data);
                MessageBox.Show("均衡器配置已保存！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message);
            }
        }
    }
}
