using System;
using SkiaSharp;

namespace C3.Core
{
    public static class SKExt
    {
        public static SKPoint TopLeft(this SKRect r) => new SKPoint(r.Left, r.Top);
    }

    public struct IndexedGridTransform
    {
        public  int Width { get; }

        public IndexedGridTransform(int width)
        {
            Width = width;
        }

        public SKPoint this[int idx] => new SKPoint( idx % Width, idx / Width);

    }

    public struct GridTransform
    {
        private float width;
        private float height;

        public static GridTransform CellSize(float size)
        {
            return new GridTransform()
            {
                width = size,
                height = size
            };
        }

        public static GridTransform CellSize(float width, float height)
        {
            return new GridTransform()
            {
                width = width,
                height = height
            };
        }
        public static GridTransform RowsAndCols(IC3DrawingSystem geo, int cols, int rows)
        {
            return new GridTransform()
            {
                width = geo.Width / cols,
                height = geo.Height / rows
            };

        }
        
        public SKRect this[SKPoint cell] => new SKRect(
            cell.X * width,         cell.Y * height,
            cell.X * width + width, cell.Y * height + height);

    }

    public struct OffsetTransform
    {
        private SKPoint offset;

        public OffsetTransform(SKPoint offset)
        {
            this.offset = offset;
        }

        public SKPoint this[SKPoint point] => new SKPoint(point.X + offset.X, point.Y + offset.Y);
        public SKRect this[SKRect rect] => new SKRect(rect.Left + offset.X, rect.Top + offset.Y, rect.Right + offset.X, rect.Bottom + offset.Y);

    }

    public static class Helper
    {
        public static Random Random = new Random((int)DateTime.Now.Ticks);

        public static T RandomSelect<T>(params Func<T>[] get) => get[Random.Next(0, get.Length)]();
    }
}
