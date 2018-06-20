using C3.Core;
using SkiaSharp;

namespace C3.Core.Examples
{
    public class BasicSketch : IC3Sketch
    {
        public void InitWorld(IGeometry geometry)
        {
            
        }

        public void StepWorld(IGeometry geometry)
        {
           
        }

        public string Draw(IC3DrawingSystem system, SKSurface surface)
        {
            surface.Canvas.DrawText("Hello World", new SKPoint(10,10), new SKPaint()
            {
                Color = new SKColor(0,0,0)
            } );

            return "OK";
        }
    }
}
