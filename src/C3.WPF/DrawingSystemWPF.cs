using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using C3.Core;

namespace C3.WPF
{
    
    public abstract class C3DrawingSystemWpf : IC3DrawingSystem
    {
        protected WriteableBitmap bmp;
        Stopwatch watch;
        DispatcherTimer timer;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public float xDPI { get; set; } = 96;
        public float yDPI { get; set; } = 96;
        public int FrameCount { get; private set; }
        public int SkippedFrames { get; private set; }
        public float FramesPerSecond { get; private set; }

        public ImageSource GetImageSource() => bmp;

        public Action<string> UpdateToolbar { get; set; }

        public IC3Sketch Sketch { get; }

        protected C3DrawingSystemWpf(IC3Sketch sketch)
        {
            Sketch = sketch;
            watch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Tick += OnTimerOnTick;
        }

        private bool busy = false;
        void OnTimerOnTick(object s, EventArgs e)
        {
            if (busy)
            {
                SkippedFrames++;
                return;
            }

            busy = true;
            StepWorld();
            Draw();
            FrameCount++;
            UpdateToolbar?.Invoke($"FPS: {FrameCount/watch.Elapsed.TotalSeconds:0.00}, skipped: {SkippedFrames}");
            busy = false;
        }

        public void Setup(int width, int height, float framesPerSecond)
        {
            Width = width;
            Height = height;
            FramesPerSecond = framesPerSecond;
            timer.Interval = TimeSpan.FromSeconds(1f/FramesPerSecond);
            this.bmp = new WriteableBitmap(width, height, xDPI, yDPI, PixelFormats.Bgra32, BitmapPalettes.Halftone256Transparent);

            InitWorld();
        }

        public void Start()
        {
            if (bmp == null) throw new Exception("Call Setup first");
            watch.Start();
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public abstract void InitWorld();
        public abstract void StepWorld();
        public abstract void Draw();
    }
}
