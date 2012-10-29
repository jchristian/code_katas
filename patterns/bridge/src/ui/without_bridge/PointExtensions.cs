using System;
using System.Windows;

namespace ui.without_bridge
{
    public static class PointExtensions
    {
        public static Point translate(this Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }
        public static Point skew(this Point point, Point skew)
        {
            return new Point(point.X * skew.X, point.Y * skew.Y);
        }

        public static Point convert_to_cartesian(this Point point)
        {
            var x = point.X * Math.Cos(point.Y);
            var y = point.X * Math.Sin(point.Y);

            return new Point(x, y);
        }
    }
}