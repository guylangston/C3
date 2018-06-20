using System.Windows;
using C3.Core;
using SkiaSharp;

namespace C3.WPF
{
    public class C3DrawingSystemWpfSkiaSharp : C3DrawingSystemWpf, IC3DrawingSystemSkiaSharp
    {
        public C3DrawingSystemWpfSkiaSharp(IC3Sketch sketch) : base(sketch)
        {
        }

        public override void InitWorld()
        {
            Sketch.InitWorld(this);
        }

        public override void StepWorld()
        {
            Sketch.StepWorld(this);
        }

        public override void Draw()
        {
            bmp.Lock();
            using (var surface = SKSurface.Create(
                width: Width,
                height: Height,
                colorType: SKColorType.Bgra8888,
                alphaType: SKAlphaType.Premul,
                pixels: bmp.BackBuffer,
                rowBytes: Width * 4))
            {
                Draw(surface);
            }
            bmp.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
            bmp.Unlock();
        }

        public void Draw(SKSurface surface)
        {
            var txt = Sketch.Draw(this, surface);
            base.UpdateToolbar(txt);
        }

       

        
    }
}