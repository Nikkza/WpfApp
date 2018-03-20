using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        CircleHandler _circlehandler;
        private Line _line;
        private Point _currentPoint;
        private Point _startPoint;
        private Point _circleAncourPoint;

        private Rectangle _rect;
        private Ellipse _elips;

        private bool _butttonDrawClick;
        private bool _butttonLineClick;
        private bool _buttonRecClick;
        private bool _buttonCircelClick;
        private SolidColorBrush _changeColorFill;
        private SolidColorBrush _changeColorStroke;

        private Brush _bindingColorFill;
        public Brush BindingColorFill
        {
            get => _bindingColorFill;
            set
            {
                _bindingColorFill = value;
                RaisePropertyChanged("BindingColorFill");
            }
        }

        private Brush _bindingColorStroke;
        public Brush BindingColorFillStroke
        {
            get => _bindingColorStroke;
            set
            {
                _bindingColorStroke = value;
                RaisePropertyChanged("BindingColorFillStroke");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            _currentPoint = new Point();
            _circlehandler = new CircleHandler();
            DataContext = this;
        }

       

        private void FillColor(SolidColorBrush c, string color, Label l)
        {
            c = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            l.Content = c.Color;
            BindingColorFill = c;

        }

        private void RectangleGreen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {           
            _changeColorFill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f50057"));
            LabelFillColor.Content = _changeColorFill;
            BindingColorFill = _changeColorFill;
        }

        private void RectangleRed_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _changeColorFill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c51162"));
            LabelFillColor.Content = _changeColorFill;
            BindingColorFill = _changeColorFill;
        }

        private void RectangleYellow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _changeColorFill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f06292"));
            LabelFillColor.Content = _changeColorFill;
            BindingColorFill = _changeColorFill;

        }

        private void RectangleStrokeGreen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _changeColorStroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f50057"));
            LabelColor.Content = _changeColorStroke.Color;
            BindingColorFillStroke = _changeColorStroke;
        }

        private void RectangleStrokeRed_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _changeColorStroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c51162"));
            LabelColor.Content = _changeColorStroke.Color;
            BindingColorFillStroke = _changeColorStroke;
        }

        private void RectangleStrokeYellow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _changeColorStroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f06292"));
            LabelColor.Content = _changeColorStroke.Color;
            BindingColorFillStroke = _changeColorStroke;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            _butttonLineClick = true;
            _butttonDrawClick = false;
            _buttonRecClick = false;
            _buttonCircelClick = false;

        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            _buttonCircelClick = true;
            _butttonLineClick = false;
            _butttonDrawClick = false;
            _buttonRecClick = false;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            _butttonLineClick = false;
            _butttonDrawClick = false;
            _buttonRecClick = true;
            _buttonCircelClick = false;
        }

        private void FreeHandButton_Click(object sender, RoutedEventArgs e)
        {
            _butttonLineClick = false;
            _butttonDrawClick = true;
            _buttonRecClick = false;
            _buttonCircelClick = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
        }

        private void MouseDownDraw(MouseButtonEventArgs e)
        {
            _currentPoint = e.GetPosition(MyCanvas);
        }

        private void MouseDownRctangel(MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(MyCanvas);
            _rect = new Rectangle
            {
                Stroke = _changeColorStroke,
                StrokeThickness = 3,
                Fill = _changeColorFill
            };
            Canvas.SetLeft(_rect, _startPoint.X);
            Canvas.SetTop(_rect, _startPoint.Y);
            MyCanvas.Children.Add(_rect);
        }

        private void MouseDownLine(MouseButtonEventArgs e)
        {
            var startPoint = e.GetPosition(MyCanvas);
            var line = new Line
            {
                Stroke = _changeColorStroke,
                StrokeThickness = 5,
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = startPoint.X,
                Y2 = startPoint.Y,
            };
            MyCanvas.Children.Add(line);
        }

        private void MouseDownCircle(MouseButtonEventArgs e)
        {
            _circleAncourPoint = e.MouseDevice.GetPosition(MyCanvas);

            _elips = new Ellipse
            {
                Stroke = _changeColorStroke,
                StrokeThickness = 3,
                Fill = _changeColorFill
            };
            MyCanvas.Children.Add(_elips);
        }

        private void MouseMoveDraw(MouseEventArgs e)
        {
            Point lineEnd = e.GetPosition(MyCanvas);
            _line = new Line();
            _line.StrokeThickness = 5;
            _line.Stroke = _changeColorStroke;
            _line.X1 = _currentPoint.X;
            _line.Y1 = _currentPoint.Y;
            _line.X2 = lineEnd.X;
            _line.Y2 = lineEnd.Y;
            MyCanvas.Children.Add(_line);
            _currentPoint = lineEnd;
        }

        private void MouseMoveCircle(MouseEventArgs e)
        {
            Point location = e.GetPosition(MyCanvas);

            double minX = Math.Min(location.X, _circleAncourPoint.X);
            double minY = Math.Min(location.Y, _circleAncourPoint.Y);
            double maxX = Math.Max(location.X, _circleAncourPoint.X);
            double maxY = Math.Max(location.Y, _circleAncourPoint.Y);
            Canvas.SetTop(_elips, minY);
            Canvas.SetLeft(_elips, minX);
            double height = maxY - minY;
            double width = maxX - minX;
            _elips.Height = Math.Abs(height);
            _elips.Width = Math.Abs(width);
            _elips.Height = _elips.Width;
        }

        private void MouseMoveRecTangle(MouseEventArgs e)
        {
            var pos = e.GetPosition(MyCanvas);

            var x = Math.Min(pos.X, _startPoint.X);
            var y = Math.Min(pos.Y, _startPoint.Y);
            var w = Math.Max(pos.X, _startPoint.X) - x;
            var h = Math.Max(pos.Y, _startPoint.Y) - y;
            _rect.Width = w;
            _rect.Height = h;
            Canvas.SetLeft(_rect, x);
            Canvas.SetTop(_rect, y);
        }

        private void MouseMoveLine(MouseEventArgs e)
        {
            _line = MyCanvas.Children.OfType<Line>().LastOrDefault();

            if (_line != null)
            {
                Point endPoint = e.GetPosition(MyCanvas);
                _line.X2 = endPoint.X;
                _line.Y2 = endPoint.Y;
            }
        }

        private void Canvas_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && _butttonDrawClick)
            {
                MouseDownDraw(e);
            }

            if (e.ButtonState == MouseButtonState.Pressed && _buttonRecClick)
            {
                MouseDownRctangel(e);
            }

            if (e.ButtonState == MouseButtonState.Pressed && _butttonLineClick)
            {
                MouseDownLine(e);
            }

            if (e.ButtonState == MouseButtonState.Pressed && _buttonCircelClick)
            {

                MouseDownCircle(e);
                //_circlehandler.CircleAdd(_circleAncourPoint, e, MyCanvas, _changeColorStroke, _changeColorFill);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _rect = null;
        }

        private void Canvas_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _butttonDrawClick)
            {
                MouseMoveDraw(e);
            }

            if (e.LeftButton == MouseButtonState.Pressed && _buttonCircelClick)
            {
                if (e.LeftButton == MouseButtonState.Released || _elips == null)
                    return;

                MouseMoveCircle(e);
            }

            if (e.LeftButton == MouseButtonState.Pressed && _buttonRecClick)
            {
                if (e.LeftButton == MouseButtonState.Released || _rect == null)
                    return;

                MouseMoveRecTangle(e);
            }

            if (e.LeftButton == MouseButtonState.Pressed && _butttonLineClick)
            {
                MouseMoveLine(e);
            }
        }


    }

}
