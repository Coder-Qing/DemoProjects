using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPublicDemo.Pages
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

        private void ClickMe_Click(object sender, RoutedEventArgs e)
        {
            if (ImageTop.Opacity > 0)
            {
                DoubleAnimation doubleAnimationImageTop = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(3)
                };
                doubleAnimationImageTop.Completed += (s, a) => { ClickMe.Content = "再点我一下哦~"; };
                ImageTop.BeginAnimation(OpacityProperty, doubleAnimationImageTop);
            }
            else
            {
                DoubleAnimation doubleAnimationImageTop = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(5)
                };
                doubleAnimationImageTop.Completed += (s, Args) => { MessageBox.Show("爱你哦~彩彩"); };
                ImageSecond.BeginAnimation(OpacityProperty, doubleAnimationImageTop);
                
            }
        }
    }

}
