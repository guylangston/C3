using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace C3.Core
{
    public interface IGeometry
    {
        int Width { get; }
        int Height { get; }
        float xDPI { get; set; }
        float yDPI { get; set; }
    }

    public interface IC3DrawingSystem : IGeometry
    {
        int FrameCount { get; }
        float FramesPerSecond { get; }

        void Setup(int width, int height, float framesPerSecond);
        void InitWorld();

        void Start();
        void Stop();
        
        void StepWorld();
        void Draw();
    }

    public interface IC3DrawingSystemSkiaSharp : IC3DrawingSystem
    {
        void Draw(SKSurface surface);
    }

    public interface IC3Sketch
    {
        void InitWorld(IGeometry geometry);
        void StepWorld(IGeometry geometry);
        void Draw(IC3DrawingSystem system, SKSurface surface);
    }

    


}
