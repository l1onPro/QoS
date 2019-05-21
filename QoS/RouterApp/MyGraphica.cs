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
        Canvas info;

        int[] listTop; //{ 150, 210, 270, 330, 390, 450, 510, 570, 670 };
        int x0 = 1010;
        int xn = 1010 - Setting.CurSizeQueuering / 10;

        int Height = Setting.SizePaint;

        int lengthStandart = 10;       

        public MyGraphica(Canvas paint, Canvas info)
        {            
            this.paint = paint;
            this.info = info;
            SetTop();
            PaintInfoAboutPackage();
        }

        private void SetTop()
        {
            listTop = new int[10];

            int start = 150;
            int lineIndent = 10;

            //для 8 очередей
            for (int i = 0; i < 8; i++)
            {
                listTop[i] = start;
                start += Height + lineIndent;
            }

            //для генератора пакетов
            listTop[8] = 150 - Height - 20;
            

            //для результирующей
            start += Height - lineIndent;
            listTop[9] = start;
            SetLabelName(xn, start, "Конечная");
        }

        private void SetLabelName(int x, int y, String name)
        {
            Label label = new Label();
            label.Content = name + " очередь:";

            Canvas.SetLeft(label, x);
            Canvas.SetTop(label, y);
            paint.Children.Add(label);
        }

        private Ellipse GetEllipse(int length, int Height)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = Height;
            ellipse.Width = length;
            ellipse.Fill = Brushes.LightBlue;
            ellipse.Stroke = Brushes.Black;
            return ellipse;
        }

        private void AddEllipse(int x, int y, int length)
        {
            Ellipse ellipse = GetEllipse(length, Height);    
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            paint.Children.Add(ellipse);
        }

        private Rectangle GetRectangle(int length, int Height, SolidColorBrush colorBrush)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = Height;
            rectangle.Width = length;
            rectangle.Fill = colorBrush;
            rectangle.Stroke = Brushes.Black;
           
            return rectangle;
        }

        private void AddRestangle(int x, int y, int length, SolidColorBrush colorBrush )
        {
            Rectangle rectangle = GetRectangle(length, Height, colorBrush);   
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            paint.Children.Add(rectangle);
        }

        private Polygon GetPolygon(int length, int Height)
        {
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.LightBlue;
            polygon.Stroke = Brushes.Black;

            List<Point> points = new List<Point>();
            points.Add(new Point(0, Height));
            points.Add(new Point(length / 2, 0));
            points.Add(new Point(length, Height));

            polygon.Points = new PointCollection(points);
            return polygon;
        }

        private void AddPolygonTriangle(int x, int y, int length)
        {
            Polygon polygon = GetPolygon(length, Height);

            Canvas.SetLeft(polygon, x);
            Canvas.SetTop(polygon, y);
            paint.Children.Add(polygon);
        }

        private void AddRestangle(int x, int y, int length)
        {
            Rectangle rectangle = GetRectangle(length, Height, Brushes.LightBlue);             

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
            int y = listTop[i] - Height;           

            foreach (Package item in packets)
            {
                if (item == null) break;

                int length = item.Length / lengthStandart;
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
                if (item.NOTNULL())
                    PaintQueue(i, item.GetAllPackages());
                i++;
            }
        }

        public void PaintResultQueue(Queue<Package> packets)
        {
            SetLabelName(xn, listTop[9] - Height - 30, "На выходе");
            PaintQueue(9, packets);
        }

        public void PaintStartQueue(Queue<Package> packets)
        {
            SetLabelName(xn, listTop[8] - Height - 30, "На входе");
            PaintQueue(8, packets);
        }

        public void Clear()
        {
            paint.Children.Clear();            
        }     
      
        private void AddLabelForInfo(int xInfo, int yInfo, String txt)
        {
            int space = 60;
            Label label = new Label();
            label.Content = txt;
            Canvas.SetLeft(label, xInfo + space);
            Canvas.SetTop(label, yInfo + 10);
            info.Children.Add(label);
        }

        public void PaintInfoAboutPackage()
        {
            info.Children.Clear();
           
            int xInfo = 10;
            int yInfo = 10;
           
            int length = 40;
            int heigth = 40;            

            AddLabelForInfo(xInfo, yInfo, "CS6, CS7");
            Ellipse ellipse = GetEllipse(length, heigth);
            Canvas.SetLeft(ellipse, xInfo);
            Canvas.SetTop(ellipse, yInfo);
            info.Children.Add(ellipse);

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo, yInfo, "EF");
            Polygon polygon = GetPolygon(length, heigth);
            Canvas.SetLeft(polygon, xInfo);
            Canvas.SetTop(polygon, yInfo);
            info.Children.Add(polygon);

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo, yInfo, "DF");
            Rectangle rectangle = GetRectangle(length, heigth, Brushes.LightBlue);
            Canvas.SetLeft(rectangle, xInfo);
            Canvas.SetTop(rectangle, yInfo);
            info.Children.Add(rectangle);

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo - 60, yInfo, "AF4, AF3, AF2, AF1:");

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo, yInfo, "длина пакета: 1000 до 1500");
            rectangle = GetRectangle(length, heigth, Brushes.Red);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            Canvas.SetLeft(rectangle, xInfo);
            Canvas.SetTop(rectangle, yInfo);
            info.Children.Add(rectangle);

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo, yInfo, "длина пакета: 500 до 1000");
            rectangle = GetRectangle(length, heigth, Brushes.Yellow);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            Canvas.SetLeft(rectangle, xInfo);
            Canvas.SetTop(rectangle, yInfo);
            info.Children.Add(rectangle);

            yInfo += heigth + 10;
            AddLabelForInfo(xInfo, yInfo, "длина пакета: 100 до 500");
            rectangle = GetRectangle(length, heigth, Brushes.Green);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            Canvas.SetLeft(rectangle, xInfo);
            Canvas.SetTop(rectangle, yInfo);
            info.Children.Add(rectangle);
        }
    }
}
