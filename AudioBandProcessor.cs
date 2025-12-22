using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace MusicPlayer
{
    // 重命名为 AudioEqualizerProcessor，避免与 Form3(Equalizer) 冲突
    public class AudioEqualizerProcessor : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly BiQuadFilter[] filters;
        // 对应你滑动条的 10 个标准频率
        private static readonly float[] Frequencies = { 31, 62, 125, 250, 500, 1000, 2000, 4000, 8000, 16000 };

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        public AudioEqualizerProcessor(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
            this.filters = new BiQuadFilter[Frequencies.Length];

            for (int i = 0; i < Frequencies.Length; i++)
            {
                // 初始化滤波器，Q值设为 0.8，初始增益为 0dB
                filters[i] = BiQuadFilter.PeakingEQ(sourceProvider.WaveFormat.SampleRate, Frequencies[i], 0.8f, 0.0f);
            }
        }

        // 更新指定频段的增益
        public void SetGain(int bandIndex, float gainDb)
        {
            if (bandIndex >= 0 && bandIndex < filters.Length)
            {
                filters[bandIndex] = BiQuadFilter.PeakingEQ(WaveFormat.SampleRate, Frequencies[bandIndex], 0.8f, gainDb);
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);
            for (int n = 0; n < samplesRead; n++)
            {
                for (int i = 0; i < filters.Length; i++)
                {
                    buffer[offset + n] = filters[i].Transform(buffer[offset + n]);
                }
            }
            return samplesRead;
        }
    }
}