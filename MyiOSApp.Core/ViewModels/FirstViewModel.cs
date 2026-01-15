using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MyiOSApp.Core.Models;
using MyiOSApp.Core.Services;

namespace MyiOSApp.Core.ViewModels;

public class FirstViewModel : MvxViewModel
{
    private readonly IMvxNavigationService _navigationService;
    private readonly IMessageRecipient<DetailResult> _messageRecipient;
    private string _resultMessage = "Tap the button to navigate to detail screen";

    public FirstViewModel(
        IMvxNavigationService navigationService,
        IMessageRecipient<DetailResult> messageRecipient)
    {
        _navigationService = navigationService;
        _messageRecipient = messageRecipient;
        NavigateCommand = new MvxAsyncCommand(NavigateToDetail);
    }

    public IMvxAsyncCommand NavigateCommand { get; }

    public string ResultMessage
    {
        get => _resultMessage;
        set => SetProperty(ref _resultMessage, value);
    }

    private async Task NavigateToDetail()
    {
        var param = new DetailParameter
        {
            Title = "Edit Item",
            ItemId = 42
        };

        // Navigate to detail screen
        await _navigationService.Navigate<DetailViewModel, DetailParameter>(param);

        // Wait for the result via messenger
        var result = await _messageRecipient.WaitAsync();

        // Handle the result
        if (result != null && result.IsSuccess)
        {
            ResultMessage = $"Saved successfully! Value: {result.SavedValue}";
        }
        else
        {
            ResultMessage = "Operation cancelled";
        }
    }
}
