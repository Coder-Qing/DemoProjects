using System;
using System.Collections.Generic;
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

namespace WpfPayDemo
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class TimePickerUserControl : UserControl
    {
        public TimePickerUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyCustomButton.MyProperty = 456;
        }
    }

    public class CustomStackPanel : StackPanel
    {
        public static readonly DependencyProperty MinDateProperty;

        static CustomStackPanel()
        {
            MinDateProperty = DependencyProperty.Register("MinDate", typeof(DateTime), typeof(CustomStackPanel), new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.Inherits));
        }

        public DateTime MinDate
        {
            get { return (DateTime)GetValue(MinDateProperty); }
            set
            {
                SetValue(MinDateProperty, value);
            }
        }
    }

    public class CustomButton : Button
    {
        public static readonly DependencyProperty MinDateProperty;

        static CustomButton()
        {
            MinDateProperty = CustomStackPanel.MinDateProperty.AddOwner(typeof(CustomButton), new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.Inherits));
            MyPropertyProperty.OverrideMetadata(typeof(int), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextPropertyChanged)));
        }

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("", "Changed");
        }

        public int MyProperty
        {
            get { return (int)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(CustomButton), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnValueChanged), new CoerceValueCallback(CoerceValue)), new ValidateValueCallback(IsValidValue));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("当值改变时，我们可以做的一些操作，具体可以在这里定义： {0}", e.NewValue);
            if (true)
            {

            }
        }

        private static object CoerceValue(DependencyObject d, object value)
        {
            Console.WriteLine("对值进行限定，强制值： {0}", int.TryParse(value.ToString(),out int intValue));
            if (intValue > 132)
            {
                value = 777;
            }
            return value;
        }

        private static bool IsValidValue(object value)
        {
            Console.WriteLine("验证值是否通过，返回bool值，如果返回True表示验证通过，否则会以异常的形式暴露： {0}", value);
            return true;
        }









        public static int GetMyProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(AngleProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(AngleProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.RegisterAttached("Angle", typeof(double), typeof(CustomButton), new FrameworkPropertyMetadata(0.0));


    }
}
