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
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件
    /// 
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfPayDemo"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfPayDemo;assembly=WpfPayDemo"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ChatMessageControl/>
    ///
    /// </summary>
    [TemplatePart(Name = TextBlockTemplateName, Type = typeof(TextBlock))]
    [TemplatePart(Name = RichTextBoxTemplateName, Type = typeof(RichTextBox))]
    public class ChatMessageControl : Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ChatMessageControl), new PropertyMetadata(default(string), OnTextChanged));

        private const string RichTextBoxTemplateName = "PART_RichTextBox";
        private const string TextBlockTemplateName = "PART_TextBlock";

        private static readonly Dictionary<string, string> Emotions = new Dictionary<string, string>
        {
            ["doge"] = "pack://application:,,,/WpfQQChat;component/Images/doge.png",
            ["喵喵"] = "pack://application:,,,/WpfQQChat;component/Images/喵喵.png"
        };

        private RichTextBox _richTextBox;
        private TextBlock _textBlock;

        static ChatMessageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChatMessageControl), new FrameworkPropertyMetadata(typeof(ChatMessageControl)));
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public override void OnApplyTemplate()
        {
            _textBlock = (TextBlock)GetTemplateChild(TextBlockTemplateName);
            _richTextBox = (RichTextBox)GetTemplateChild(RichTextBoxTemplateName);

            UpdateVisual();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (ChatMessageControl)d;

            obj.UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (_textBlock == null || _richTextBox == null)
            {
                return;
            }

            _textBlock.Inlines.Clear();
            _richTextBox.Document.Blocks.Clear();

            var paragraph = new Paragraph();

            var buffer = new StringBuilder();
            foreach (var c in Text)
            {
                switch (c)
                {
                    case '[':
                        _textBlock.Inlines.Add(buffer.ToString());
                        paragraph.Inlines.Add(buffer.ToString());
                        buffer.Clear();
                        buffer.Append(c);
                        break;

                    case ']':
                        var current = buffer.ToString();
                        if (current.StartsWith("["))
                        {
                            var emotionName = current.Substring(1);
                            if (Emotions.ContainsKey(emotionName))
                            {
                                {
                                    var image = new Image
                                    {
                                        Width = 16,
                                        Height = 16
                                    };// 占位图像不需要加载 Source 了
                                    _textBlock.Inlines.Add(new InlineUIContainer(image));
                                }
                                {
                                    var image = new Image
                                    {
                                        Width = 16,
                                        Height = 16,
                                        Source = new BitmapImage(new Uri(Emotions[emotionName]))
                                    };
                                    paragraph.Inlines.Add(new InlineUIContainer(image));
                                }

                                buffer.Clear();
                                continue;
                            }
                        }

                        buffer.Append(c);
                        _textBlock.Inlines.Add(buffer.ToString());
                        paragraph.Inlines.Add(buffer.ToString());
                        buffer.Clear();
                        break;

                    default:
                        buffer.Append(c);
                        break;
                }
            }

            _textBlock.Inlines.Add(buffer.ToString());
            paragraph.Inlines.Add(buffer.ToString());

            _richTextBox.Document.Blocks.Add(paragraph);
        }
    }
}
