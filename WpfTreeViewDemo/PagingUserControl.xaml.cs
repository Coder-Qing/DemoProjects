using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfTreeViewDemo
{
    /// <summary>
    /// PagingUserControl.xaml 的交互逻辑
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class PagingUserControl : UserControl
    {

        #region 构造函数
        public PagingUserControl()
        {
            InitializeComponent();
            DataContext = this;
            this.itemsControl.ItemsSource = _collection;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 分页事件
        /// </summary>
        public event EventHandler<PageChangedEventArgs> PageChanged;
        #endregion

        #region 变量
        private static ObservableCollection<PageControlItemModel> _collection = new ObservableCollection<PageControlItemModel>();
        private List<PageControlItemModel> _list = null;
        #endregion

        #region 属性
        public bool LastBtnIsEnabled { get; set; }
        public bool NextBtnIsEnabled { get; set; }
        
        #endregion

        #region 分页相关属性


        public RelyCommand<int> PageChangedCommand
        {
            get { return (RelyCommand<int>)GetValue(PageChangedCommandProperty); }
            set { SetValue(PageChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageChangedCommandProperty =
            DependencyProperty.Register("PageChangedCommand", typeof(RelyCommand<int>), typeof(PagingUserControl), new UIPropertyMetadata(
                null,
                new PropertyChangedCallback(OnPageChangedCommandPropertyChanged)
                ));

        private static void OnPageChangedCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PagingUserControl pagingUserControl = d as PagingUserControl;

            if (pagingUserControl == null)
            {
                return;
            }

            pagingUserControl.PageChangedCommand = (RelyCommand<int>)e.NewValue;
        }


        /// <summary>
        /// 显示的分页数量
        /// </summary>
        public int PagingNum
        {
            get { return (int)GetValue(PagingNumProperty); }
            set { SetValue(PagingNumProperty, value); }

        }

        // Using a DependencyProperty as the backing store for PagingNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagingNumProperty =
            DependencyProperty.Register("PagingNum", typeof(int), typeof(PagingUserControl), new PropertyMetadata(5));




        /// <summary>
        /// 总页数
        /// </summary>
        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register(
             "PageCount", typeof(int), typeof(PagingUserControl), new PropertyMetadata(
                default(int),
                new PropertyChangedCallback(OnPageCountPropertyChanged)
                ));

        private static void OnPageCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            InitPagingList((d as PagingUserControl), Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); CalcPageNumList(); }
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page", typeof(int), typeof(PagingUserControl), new PropertyMetadata(1));

        public int Page
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }
        #endregion

        #region 单击页码事件
        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PageControlItemModel itemModel = btn.CommandParameter as PageControlItemModel;
            if (itemModel.Page != Page)
            {
                Page = itemModel.Page;
                CalcPageNumList();
            }
        }
        #endregion

        #region 计算页码
        /// <summary>
        /// 计算页码
        /// </summary>
        private void CalcPageNumList()
        {
            _list = new List<PageControlItemModel>();

            //开始数字
            int itemCnt = 1;

           
            //如果当前页数 少于

            if (Page < 4 && Page > 0)
            {
                itemCnt = 1;
            }
            else if (Page > PageCount - 3)
            {
                itemCnt = PageCount >= 5 ? PageCount - 4 : 1;
            }
            else if (Page >= 4 || Page <= PageCount - 3)
            {
                itemCnt = Page - 2;
            }

            for (int i = itemCnt; i <= (PageCount <= 5 ? PageCount: itemCnt + 4); i++)
            {
                _list.Add(new PageControlItemModel(i,Page) );
            }

            //上一页  下一页
            BtnPrePageVisibilityJudge();

            _collection.Clear();
            _list.ForEach(a => { _collection.Add(a); });
            PageChangedCommand?.Execute(Page);
        }

        public static void InitPagingList(PagingUserControl pagingUserControl,int pageCount)
        {

            pagingUserControl.Page = 1;
            if (pageCount == 1)
            {
                pagingUserControl.LastBtnIsEnabled = false;
                pagingUserControl.NextBtnIsEnabled = false;
            }
            else
            {
                pagingUserControl.NextBtnIsEnabled = true;
            }

            _collection.Clear();
            for (int i = 1; i <= (pageCount <= 5 ? pageCount : 5) ; i++)
            {
                _collection.Add(new PageControlItemModel(i, 1));
            }
           
        }

        private void BtnPrePageVisibilityJudge()
        {
            if (Page == 1)
            {
                LastBtnIsEnabled = false;
            }
            else
            {
                LastBtnIsEnabled = true;

            }
            if (Page == PageCount)
            {
                NextBtnIsEnabled = false;
            }
            else
            {
                NextBtnIsEnabled = true;
            }
        }
        #endregion

        #region 上一页
        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            int prePage = Page - 1;
            if (prePage < 1) prePage = 1;
            if (prePage != Page)
            {
                Page = prePage;
                CalcPageNumList();

                //PageChangedEventArgs args = new PageChangedEventArgs(prePage);
                //PageChanged(sender, args);
            }
        }
        #endregion

        #region 下一页
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            int nextPage = Page + 1;
            if (nextPage > PageCount) nextPage = PageCount;
            if (nextPage != Page)
            {

                Page = nextPage;
                CalcPageNumList();

                //PageChangedEventArgs args = new PageChangedEventArgs(nextPage);
                //PageChanged(sender, args);
            }
        }
        #endregion


        private void MakeSure_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TB_Skip.Text,out int Spik_Num))
            {
                Page = Spik_Num< PageCount ? Spik_Num : PageCount;
                TB_Skip.Text = Page.ToString();
                CalcPageNumList();
            }
            else
            {
                TB_Skip.Text = string.Empty;
            }
            
        }

        private void TB_Skip_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]");
            e.Handled = re.IsMatch(e.Text);
        }
    }

    #region 分页控件Item Model
    /// <summary>
    /// 分页控件Item Model
    /// </summary>
    public class PageControlItemModel : INotifyPropertyChanged
    {
        private bool _IsCurrentPage;
        /// <summary>
        /// 是否当前页码
        /// </summary>
        public bool IsCurrentPage
        {
            get { return _IsCurrentPage; }
            set
            {
                _IsCurrentPage = value;
                OnPropertyChanged("IsCurrentPage");

                if (_IsCurrentPage)
                {
                    CurrentPageColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    CurrentPageBg_Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014E97"));
                }
                else
                {
                    CurrentPageColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014E97"));
                    CurrentPageBg_Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2"));
                }
            }
        }

        private SolidColorBrush _CurrentPageColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#014E97"));
        /// <summary>
        /// 当前页码颜色
        /// </summary>
        public SolidColorBrush CurrentPageColor
        {
            get { return _CurrentPageColor; }
            set
            {
                _CurrentPageColor = value;
                OnPropertyChanged("CurrentPageColor");
            }
        }


        private SolidColorBrush _CurrentPageBg_Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2"));
        /// <summary>
        /// 当前页码背景颜色
        /// </summary>
        public SolidColorBrush CurrentPageBg_Color
        {
            get { return _CurrentPageBg_Color; }
            set
            {
                _CurrentPageBg_Color = value;
                OnPropertyChanged("CurrentPageBg_Color");
            }
        }

        private int _Page;
        /// <summary>
        /// 页码
        /// </summary>
        public int Page
        {
            get { return _Page; }
            set
            {
                _Page = value;
                OnPropertyChanged("Page");
            }
        }


        /// <summary>
        /// 分页控件Item Model
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="currentPage">当前页码</param>
        public PageControlItemModel(int page, int currentPage)
        {
            Page = page;
            IsCurrentPage = page == currentPage;
        }

        #region INotifyPropertyChanged接口
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
    #endregion

    #region 分页事件参数
    /// <summary>
    /// 分页事件参数
    /// </summary>
    public class PageChangedEventArgs : EventArgs
    {
        private int _Page = 1;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page
        {
            get
            {
                return _Page;
            }
        }

        /// <summary>
        /// 分页事件参数
        /// </summary>
        /// <param name="page">当前页码</param>
        public PageChangedEventArgs(int page)
        {
            _Page = page;
        }
    }
    #endregion


    public class RelyCommand<T> : ICommand
    {
        Action<T> mAction;

        public RelyCommand(Action<T> action)
        {
            mAction = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction?.Invoke((T)parameter);
        }
    }

}
