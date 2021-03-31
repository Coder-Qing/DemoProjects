using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfStylet.ViewModels
{

    //public class MainViewModel : Conductor<IScreen>.StackNavigation
    public class MainViewModel : BaseViewModel
    {
        

        public SecondWindowViewModel SecondWindowViewModel { get; private set; }

        private readonly IEventAggregator _eventAggregator;
        public MainViewModel(IWindowManager windowManager, IContainer container,IEventAggregator eventAggregator) : base(windowManager, container)
        {
            _eventAggregator = eventAggregator;
            SecondWindowViewModel = _container.Get<SecondWindowViewModel>();
            SecondWindowViewModel.ConductWith(this);
        }

        //protected override void OnInitialActivate()
        //{
        //    this.ActivateItem(_container.Get<SecondWindowViewModel>());
        //}

        public void DoSomething()
        {
            MessageBoxViewModel.ButtonLabels[MessageBoxResult.No] = "No, thanks";


           var result = this._windowManager.ShowMessageBox("Do you want breakfast?",
                                   buttons: MessageBoxButton.YesNo,
                                   icon:MessageBoxImage.Information,
                                   buttonLabels: new Dictionary<MessageBoxResult, string>()
                {
                    { MessageBoxResult.Yes, "Yes please!" },
                });
            if (result == MessageBoxResult.Yes )
            {
                _eventAggregator.Publish("我发送了第二个窗口数据", "VM_SecondWindow");
                //Execute.OnUIThreadSync
            }
        }
    }
}
