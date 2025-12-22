using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace MusicPlayer
{
    public class AudioEqualizerProcessor : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;
        // 修改为二维数组：filters[声道索引, 频段索引]
        private readonly BiQuadFilter[,] filters;
        private static readonly float[] Frequencies = { 31, 62, 125, 250, 500, 1000, 2000, 4000, 8000, 16000 };
        private readonly int channels;

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        public AudioEqualizerProcessor(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
            this.channels = sourceProvider.WaveFormat.Channels;
            // 初始化：声道数 x 10个频段
            this.filters = new BiQuadFilter[channels, Frequencies.Length];

            for (int ch = 0; ch < channels; ch++)
            {
                for (int i = 0; i < Frequencies.Length; i++)
                {
                    filters[ch, i] = BiQuadFilter.PeakingEQ(sourceProvider.WaveFormat.SampleRate, Frequencies[i], 0.8f, 0.0f);
                }
            }
        }

        public void SetGain(int index, float gainDb)
        {
            if (index >= 0 && index < Frequencies.Length)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    filters[ch, index] = BiQuadFilter.PeakingEQ(
                        sourceProvider.WaveFormat.SampleRate,
                        Frequencies[index],
                        0.8f,
                        gainDb
                    );
                }
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);
            for (int n = 0; n < samplesRead; n++)
            {
                // 自动判断当前采样属于哪个声道
                int ch = (offset + n) % channels;
                for (int i = 0; i < Frequencies.Length; i++)
                {
                    buffer[offset + n] = filters[ch, i].Transform(buffer[offset + n]);
                }
            }
            return samplesRead;
        }
    }
}