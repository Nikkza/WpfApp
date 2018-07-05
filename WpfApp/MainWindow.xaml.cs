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
        private Line _line;
        private Point _currentPoint;
        private Point _startPoint;
        private Point _circleAncourPoint;
        private Rectangle _rect;
        private Ellipse _elips;
        private Button _clickedButton;
        private SolidColorBrush _changeColorFill;
        private SolidColorBrush _changeColorStroke;

        private SolidColorBrush _bindingColorFill;
        public SolidColorBrush BindingColorFill
        {
            get => _bindingColorFill;
            set
            {
                _bindingColorFill = value;
                RaisePropertyChanged("BindingColorFill");
            }
        }

        private SolidColorBrush _bindingColorStroke;
        public SolidColorBrush BindingColorFillStroke
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
            _changeColorFill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            _changeColorStroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            DataContext = this;
        }

        #region RectaangleMouseDownEvnts Colors
        private void RectangleGreen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFill = FillColor(_changeColorFill, "#f50057");
            LabelFillColor.Content = BindingColorFill;
            _changeColorFill = BindingColorFill;
        }

        private void RectangleRed_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFill = FillColor(_changeColorFill, "#c51162");
            LabelFillColor.Content = BindingColorFill;
            _changeColorFill = BindingColorFill;
        }

        private void RectangleYellow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFill = FillColor(_changeColorFill, "#f06292");
            LabelFillColor.Content = BindingColorFill;
            _changeColorFill = BindingColorFill;
        }

        private void RectangleStrokeGreen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFillStroke = FillColor(_changeColorFill, "#f50057");
            LabelColor.Content = BindingColorFillStroke;
            _changeColorStroke = BindingColorFillStroke;
        }

        private void RectangleStrokeRed_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFillStroke = FillColor(_changeColorFill, "#c51162");
            LabelColor.Content = BindingColorFillStroke;
            _changeColorStroke = BindingColorFillStroke;
        }

        private void RectangleStrokeYellow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            BindingColorFillStroke = FillColor(_changeColorFill, "#f06292");
            LabelColor.Content = BindingColorFillStroke;
            _changeColorStroke = BindingColorFillStroke;
        }
        #endregion

        #region ButtonClickevent

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _clickedButton = sender as Button;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
        }

        #endregion

        #region Methods for all the shapes
        private SolidColorBrush FillColor(SolidColorBrush c, string color)
        {
            c = (SolidColorBrush)new BrushConverter().ConvertFrom(color);
            return c;
        }

        private void MouseDownDraw(MouseButtonEventArgs e)
        {
            _currentPoint = e.GetPosition(MyCanvas);
        }

        private void MouseDownRectangle(MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(MyCanvas);
            _rect = new Rectangle
            {
                Stroke = _changeColorStroke,
                StrokeThickness = SL2.Value,
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
                StrokeThickness = SL2.Value,
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
                StrokeThickness = SL2.Value,
                Fill = _changeColorFill
            };
            Canvas.SetLeft(_elips, _circleAncourPoint.X);
            Canvas.SetTop(_elips, _circleAncourPoint.Y);
            MyCanvas.Children.Add(_elips);
        }

        private void MouseMoveDraw(MouseEventArgs e)
        {
            Point lineEnd = e.GetPosition(MyCanvas);
            _line = new Line();
            _line.StrokeThickness = SL2.Value;
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

        #endregion

        #region MouseEvents
        private void Canvas_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                switch (_clickedButton.Name)
                {
                    case "EllipseButton":
                        MouseDownCircle(e);
                        break;

                    case "FreeHandButton":
                        MouseDownDraw(e);
                        break;

                    case "LineButton":
                        MouseDownLine(e);
                        break;

                    case "RectangleButton":
                        MouseDownRectangle(e);
                        break;
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _rect = null;
        }

        private void Canvas_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                switch (_clickedButton.Content)
                {
                    case "Circle":
                        MouseMoveCircle(e);
                        break;

                    case "Draw":
                        MouseMoveDraw(e);
                        break;

                    case "Line":
                        MouseMoveLine(e);
                        break;

                    case "Rectangle":
                        if (e.LeftButton == MouseButtonState.Released || _rect == null)
                            return;

                        MouseMoveRecTangle(e);
                        break;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void SL2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = SL2.Value;
            LabelSliderValue.Content = "Value: " + value.ToString("0") + "/" + SL2.Maximum;
        }
    }
}
