using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
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

namespace WpfTreeViewDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
        }

        private void CountDownBtn_Click(object sender, RoutedEventArgs e)
        {
            CountDownMethod(10)(
                currentTime => 
                {
                    App.Current.Dispatcher.Invoke(() => 
                    {
                        CountDownBtn.Content =  currentTime;
                    });
                }, 
                () =>
                {
                    App.Current.Dispatcher.Invoke(() => 
                    {
                    CountDownBtn.Content = "我结束了";
                    });
                });
            
        }
        public static Func<Action<int>, Action, Task> CountDownMethod(int second)
          => (ShowCurrentTime, CloseCountDownTimer) =>
          {
              return Task.Run(async () =>
              {
                  while (second >= 0)
                  {
                      ShowCurrentTime(second);
                      second--;
                      await Task.Delay(new TimeSpan(0,0,1));
                  }
                  CloseCountDownTimer();
              });
          };
    }

}
