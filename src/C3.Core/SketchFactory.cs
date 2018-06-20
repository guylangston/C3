using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using SkiaSharp;

namespace C3.Core
{
    public class SketchFactory
    {

        private class DynamicSketch : IC3Sketch
        {
            private Func<IC3DrawingSystem, SKSurface, string> draw;

            public DynamicSketch(Func<IC3DrawingSystem, SKSurface, string> draw)
            {
                this.draw = draw;
            }

            public void InitWorld(IGeometry geometry)
            {
            }

            public void StepWorld(IGeometry geometry)
            {
            }

            public string Draw(IC3DrawingSystem system, SKSurface surface)
            {
                return draw(system, surface);
            }
        }

        public class Globals
        {
            public Globals(IC3DrawingSystem drawSystem, SKSurface surface)
            {
                DrawSystem = drawSystem;
                Surface = surface;
            }

            public IC3DrawingSystem DrawSystem { get; }
            public SKSurface Surface { get; }

            public object Tag { get; set; }
        }

        public IC3Sketch GetSketch(string name)
        {
            var code = @"//using C3.Core;
// using SkiaSharp;
//public void Draw(IC3DrawingSystem system, SKSurface surface)

    Surface.Canvas.DrawRect(0, 0, 800, 800,new SKPaint()
    {
        Color = new SKColor(100, 100, 100)
    });
    Surface.Canvas.DrawText(""Hello World "" + DateTime.Now.ToString(), new SKPoint(10,10), new SKPaint()
    {
        Color = new SKColor(0, 0, 0)
    });
return ""OK"";
";
            var options = ScriptOptions.Default
                .WithReferences(typeof(IC3DrawingSystem).Assembly)
                .WithReferences(typeof(SKSurface).Assembly)
                .WithImports(new [] {"SkiaSharp", "System"});
            var script = CSharpScript.Create<string>(code, options, typeof(Globals), new InteractiveAssemblyLoader());
           
            var del = script.CreateDelegate();
            
            return new DynamicSketch( 
                (system, surface) => del(new Globals(system, surface)
                {
                    
                }).Result
                );

        }
    }
}
