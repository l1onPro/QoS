using QoS.AppPackage;
using QoS.Class_of_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace QoS.RouterApp
{
    class MyGraphica
    {       
        /// <summary>
        /// поле отрисовки
        /// </summary>
        Canvas paint;     
        int[] listTop = new int[8] { 150, 210, 270, 330, 390, 450, 510, 570 };
        int x0 = 1100;
        int xn = 10;
        public MyGraphica(Canvas paint)
        {            
            this.paint = paint;
        }

        private void AddEllipse(int x, int y, int length)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 50;
            ellipse.Width = length;
            ellipse.Fill = Brushes.LightBlue;
            ellipse.Stroke = Brushes.Black;

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            paint.Children.Add(ellipse);
        }

        private void AddRestangle(int x, int y, int length, SolidColorBrush colorBrush )
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = 50;
            rectangle.Width = length;
            rectangle.Fill = colorBrush;
            rectangle.Stroke = Brushes.Black;
            rectangle.RadiusX = 15;
            rectangle.RadiusY= 15;            

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            paint.Children.Add(rectangle);
        }

        private void AddPolygonTriangle(int x, int y, int length)
        {
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.LightPink;
            polygon.Stroke = Brushes.Black;

            List<Point> points = new List<Point>();
            points.Add(new Point(x, y));
            points.Add(new Point((x + x + length) / 2, y + 25));
            points.Add(new Point(x + length, y));
            
            polygon.Points = new PointCollection(points);

            Canvas.SetLeft(polygon, x);
            Canvas.SetTop(polygon, y);
            paint.Children.Add(polygon);
        }

        private void AddRestangle(int x, int y, int length)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = 50;
            rectangle.Width = length;
            rectangle.Fill = Brushes.LightBlue;
            rectangle.Stroke = Brushes.Black;            

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            paint.Children.Add(rectangle);
        }

        /// <summary>
        /// Нарисовать границу очереди
        /// </summary>
        private void AddLine(int x1, int y1, int x2, int y2)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y1;
            line.Stroke = Brushes.Red;
            
            paint.Children.Add(line);
        }

        /// <summary>
        /// Нарисовать границы очередей
        /// </summary>
        /// <param name="count">Количество очередей</param>
        public void PaintLineQueues(int count)
        {      
            for (int i = 0; i < count; i++)
            {
                AddLine(x0, listTop[i], xn, listTop[i]);                
            }
        }

        private void PaintQueue(int i, Queue<Package> packets)
        {
            int x = x0;
            int y = listTop[i] - 50;

            foreach (Package item in packets)
            {
                int length = item.Length;
                x -= length;

                SolidColorBrush colorBrush = new SolidColorBrush();
                
                if (item.Color == GradColor.red)  colorBrush = Brushes.Red;
                if (item.Color == GradColor.yellow)  colorBrush = Brushes.Yellow;
                if (item.Color == GradColor.green) colorBrush = Brushes.Green;

                switch (item.CoS)
                {
                    case PHB.DF:
                        AddRestangle(x, y, length);
                        break;
                    case PHB.AF1:
                        AddRestangle(x, y, length, colorBrush);
                        break;
                    case PHB.AF2:
                        AddRestangle(x, y, length, colorBrush);
                        break;
                    case PHB.AF3:
                        AddRestangle(x, y, length, colorBrush);
                        break;
                    case PHB.AF4:
                        AddRestangle(x, y, length, colorBrush);
                        break;
                    case PHB.EF:
                        AddPolygonTriangle(x, y, length);
                        break;
                    case PHB.CS6:
                        AddEllipse(x, y, length);
                        break;
                    case PHB.CS7:                        
                        AddEllipse(x, y, length);                       
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public void PaintQueues(List<Queuering> list)
        {
            int i = 0;
            foreach (Queuering item in list)
            {
                PaintQueue(i, item.GetAllPackages());
                i++;
            }
        }

        public void Clear()
        {
            paint.Children.Clear();
        }       
    }
}
