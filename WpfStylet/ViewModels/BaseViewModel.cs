using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStylet.ViewModels
{
    public class BaseViewModel:Screen
    {
        protected readonly IWindowManager _windowManager;
        protected readonly IContainer _container;

        public BaseViewModel(IWindowManager windowManager,IContainer container)
        {
            _windowManager = windowManager;
            _container = container;
        }
    }
}
