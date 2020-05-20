using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace lab_3.View
{
    public class PointsToPathConverter : IMultiValueConverter
    {

        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var points = values[0] as IEnumerable<Point>;
            if (points == null)
                return null;

            var w = (double)values[1];
            var h = (double)values[2];
            var pg = new PathGeometry();
            var ps = new List<PathSegment>();
            var scaleX = w / 133.3;
            var scaleY = h / 66.6;
            points = points.Select(p => new Point(p.X * scaleX, p.Y * scaleY));
            ps.Add(new PolyLineSegment(points, true));
            var pf = new PathFigure(points.FirstOrDefault(), ps, false);
            pg.Figures.Add(pf);
            return pg;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}