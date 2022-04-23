using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;
using Microsoft.Win32;

namespace Binary_Clock;

public partial class Widget : Window
{
    public const string name = "BinaryClock";
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int HWND_BOTTOM = 0x1;
    public const uint SWP_NOSIZE = 0x1;
    public const uint SWP_NOMOVE = 0x2;
    public const uint SWP_SHOWWINDOW = 0x40;
    private const char BINARY_ZERO = '0';
    private const char BINARY_ONE = '1';
    private static Color _colorFor0 = Colors.DimGray;
    private static Color _colorFor1 = Colors.Orange;

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
        this.Left = Properties.Settings.Default.Left;
        this.Top = Properties.Settings.Default.Top;
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        var clock = GetClockMatrix(BinaryClock.Now);
        RecolorClock(clock);
    }

    private char[][] GetClockMatrix(Clock clock)
    {
        char[][] result = new char[][]
        {
            clock.HourFirstDigit.ToCharArray(),
            clock.HourSecondDigit.ToCharArray(),
            clock.MinuteFirstDigit.ToCharArray(),
            clock.MinuteSecondDigit.ToCharArray(),
            clock.SecondFirstDigit.ToCharArray(),
            clock.SecondSecondDigit.ToCharArray()
        };
        return Rotate(result);
    }
    private void RecolorClock(char[][] clock)
    { 
        for (int i = 0; i < this.Clock.RowDefinitions.Count; i++)
        {
            for (int j = 0; j < this.Clock.ColumnDefinitions.Count; j++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(ChooseColorBrush(clock[i][j]));
                ellipse.Margin = new Thickness(2, 2, 2, 2);
                Grid.SetRow(ellipse, i);
                Grid.SetColumn(ellipse, j);
                Clock.Children.Add(ellipse);
            }
        }
    }

    private Color ChooseColorBrush(char digit) => digit switch
    {
        BINARY_ZERO => _colorFor0,
        BINARY_ONE => _colorFor1,
        _ => _colorFor0
    };

    private static T[][] Rotate<T>(T[][] input)
    {
        int length = input[0].Length;
        T[][] retVal = new T[length][];
        for (int x = 0; x < length; x++)
        {
            retVal[x] = input.Select(p => p[x]).ToArray();
        }
        return retVal;
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Properties.Settings.Default.Top = this.Top;
        Properties.Settings.Default.Left = this.Left;
        Properties.Settings.Default.Save();
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
