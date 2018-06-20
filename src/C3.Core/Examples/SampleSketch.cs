﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.Core;
using SkiaSharp;

namespace C3.Core.Examples
{
    public class SampleSketch : IC3Sketch
    {
        private Random rnd = new Random();
        private List<Ball> balls = new List<Ball>();
        
        SKPaint fill =  new SKPaint()
        {
            Color = new SKColor(50,50,50)
        };

        public void InitWorld(IGeometry geometry)
        {
            for (int cc = 0; cc < 100; cc++)
            {
                balls.Add(new Ball()
                {
                    X = rnd.Next(0, geometry.Width),
                    Y = rnd.Next(0, geometry.Height),
                    CX = rnd.Next(-10, 10)/10f,
                    CY = rnd.Next(-10, 10)/10f,
                    Size = rnd.Next(2, 20),
                    Paint =  new SKPaint()
                    {
                        IsStroke = true,
                        Color = new SKColor(100, 100, 100, 100),
                        StrokeWidth =  rnd.Next(1, 6)
                    }
                });
            }
        }

        public void StepWorld(IGeometry geometry)
        {
            foreach (var ball in balls)
            {
                ball.Step(geometry);
            }
        }

        public void Draw(IC3DrawingSystem system, SKSurface surface)
        {
            surface.Canvas.DrawRect(0,0, system.Width, system.Height, fill);
            foreach (var ball in balls)
            {
                ball.Draw(this, surface.Canvas);
            }
        }



        class Ball
        {
            public float X { get; set; }
            public float Y { get; set; }

            public float CX { get; set; }
            public float CY { get; set; }
            public int Size { get; set; } = 10;

            public SKPaint Paint { get; set; }

            public void Draw(SampleSketch sampleSketch, SKCanvas canvas)
            {
                canvas.DrawCircle(X, Y, Size, Paint);
            }

            public void Step(IGeometry sampleSketch)
            {
                X += CX;
                Y += CY;

                if (X < 0)
                {
                    X = 0; 
                    CX *= -1;
                }
                if (X > sampleSketch.Width)
                {
                    X = sampleSketch.Width; 
                    CX *= -1;
                }
                if (Y < 0)
                {
                    Y = 0; 
                    CY *= -1;
                }
                if (Y > sampleSketch.Height)
                {
                    Y = sampleSketch.Height; 
                    CY *= -1;
                }
            }
        }
    }
}
