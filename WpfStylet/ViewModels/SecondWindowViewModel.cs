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
    public class SecondWindowViewModel:BaseViewModel,IHandle<string>
    {

        private readonly IEventAggregator _eventAggregator;

        public string HandleArgument { get; set; } = "我是第二个窗口";

        public SecondWindowViewModel(IWindowManager windowManager, IContainer container,IEventAggregator eventAggregator) : base(windowManager, container)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this,"VM_SecondWindow");
        }

        public void DoSomething(string argument)
        {
            MessageBox.Show("My Second ViewModel Say:" + argument);
        }

        public void Handle(string message)
        {
                HandleArgument = message;
         
            
        }
        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}
