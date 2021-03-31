using System;
using PropertyChanged;
using Stylet;
using StyletIoC;
using StyletStudy.ViewModel;

namespace StyletStudy.Pages
{
    [AddINotifyPropertyChangedInterface]
    public class ShellViewModel : BaseViewModel
    {

        public string Name { get; set; } = "青";

        public void SayHello() => _windowManager.ShowDialog(_container.Get<FirstViewModel>());

        public bool CanSayHello => !string.IsNullOrEmpty(Name);

        private IWindowManager _windowManager;
        private IContainer _container;
        private IEventAggregator _eventAggregator;

        public ShellViewModel(IWindowManager windowManager, IContainer container, IEventAggregator eventAggregator) : base(windowManager, container, eventAggregator)
        {
            _windowManager = windowManager;
            _container = container;
            _eventAggregator = eventAggregator;
        }
    }
}
