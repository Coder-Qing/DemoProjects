using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfStylet.ViewModels;

namespace WpfStylet
{
    public class Bootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void OnStart()
        {
            // 这是在应用程序启动之后，但在IoC容器设置之前调用的。
            // 设置日志记录等
        }

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // 绑定自己的类型。具体类型是自动自绑定的。
            //builder.Bind<IMyInterface>().To<MyType>();
        }

        protected override void Configure()
        {
            //在Stylet创建IoC容器后调用这个函数。容器存在，但在启动根视图模型之前。
            //在这里配置你的服务，等等
        }

        protected override void OnLaunch()
        {
            // 在根视图模型被启动后调用
            // 显示对话框的版本检查可能从这里启动
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Called on Application.Exit
        }

        //未处理异常情况
        protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            // Called on Application.DispatcherUnhandledException
        }
    }
}
