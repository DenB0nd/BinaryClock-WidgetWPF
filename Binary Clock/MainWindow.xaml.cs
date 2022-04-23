using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Binary_Clock
{
    public partial class Widget : Window
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int HWND_BOTTOM = 0x1;
        public const uint SWP_NOSIZE = 0x1;
        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_SHOWWINDOW = 0x40;

        private static SolidColorBrush _colorFor0 = new(Colors.DimGray);
        private static SolidColorBrush _colorFor1 = new(Colors.Orange);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y,
int cx, int cy, uint uFlags);

        private IntPtr Handle => new WindowInteropHelper(this).Handle;


        public Widget()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            for(int i = 0; i < this.Clock.RowDefinitions.Count; i++)
            {
                for(int j = 0; j < this.Clock.ColumnDefinitions.Count; j++)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = new SolidColorBrush(Colors.Orange);
                    ellipse.Margin = new Thickness(2, 2, 2, 2);
                    Grid.SetRow(ellipse, i);
                    Grid.SetColumn(ellipse, j);
                    Clock.Children.Add(ellipse);
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }

        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Menu.IsOpen = true;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideFromAltTab(Handle);
            ShoveToBackground();
        }

        public static void HideFromAltTab(IntPtr Handle)
        {
            SetWindowLong(Handle,
                          GWL_EXSTYLE,
                          GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }

        private void ShoveToBackground()
        {
            SetWindowPos((int)this.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE |
                SWP_NOSIZE | SWP_SHOWWINDOW);
        }       
    }
}
