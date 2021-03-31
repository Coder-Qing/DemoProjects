using PropertyChanged;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyletStudy.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class FirstViewModel : BaseViewModel
    {
        private IWindowManager WindowManager;
        public FirstViewModel(IWindowManager windowManager, IContainer container, IEventAggregator eventAggregator) : base(windowManager, container, eventAggregator)
        {
            WindowManager = windowManager;
        }

        public void ShowSomeThing()
        {
            this.WindowManager.ShowMessageBox("Do you want breakfast?", 
                                    buttons: MessageBoxButton.YesNo,
                                    buttonLabels: new Dictionary<MessageBoxResult, string>()
         {
            { MessageBoxResult.Yes, "Yes please!" },
         });
        }

        
    }
}
