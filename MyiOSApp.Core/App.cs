using MvvmCross;
using MvvmCross.ViewModels;
using MyiOSApp.Core.Models;
using MyiOSApp.Core.Services;
using MyiOSApp.Core.ViewModels;

namespace MyiOSApp.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        // Register MessageRecipient for result handling
        Mvx.IoCProvider?.RegisterType<IMessageRecipient<DetailResult>, MessageRecipient<DetailResult>>();

        // Set the starting ViewModel
        RegisterAppStart<FirstViewModel>();
    }
}
