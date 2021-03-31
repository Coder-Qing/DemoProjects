using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPayDemo.Pages
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimePickerUserControl timePickerUserControl = new TimePickerUserControl();
        }

        private void text_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CalendarDate.IsDropDownOpen = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LostFocus.Focus();
            if (CalendarDate.IsDropDownOpen)
            {
                CalendarDate.IsDropDownOpen = false;
            }
        }
    }
}
