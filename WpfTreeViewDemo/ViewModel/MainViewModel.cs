using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeViewDemo.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private List<String> _myList;

        public List<String> MyList
        {
            get
            {
                return _myList;
            }
            set
            {
                _myList = value;
            }
        }

        


        public MainViewModel()
        {
            MyList = GetCountries();
        }

        public static List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            for (int i = 0; i < 15; i++)
            {
                countries.Add("序号：" + i);
            }
            return countries.ToList();
        }
    }
}
