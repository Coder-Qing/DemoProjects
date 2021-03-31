using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyletStudy.ViewModel
{
    public class BaseViewModel:Screen
    {
        protected readonly IWindowManager _windowManager;
        protected readonly IContainer _container;
        protected readonly IEventAggregator _eventAggregator;

        public BaseViewModel(IWindowManager windowManager, IContainer container, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _container = container;
            _eventAggregator = eventAggregator;
        }
    }
}
