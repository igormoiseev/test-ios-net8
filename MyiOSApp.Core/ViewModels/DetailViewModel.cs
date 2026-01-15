using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using MyiOSApp.Core.Messages;
using MyiOSApp.Core.Models;

namespace MyiOSApp.Core.ViewModels;

public class DetailViewModel : MvxViewModel<DetailParameter>
{
    private readonly IMvxNavigationService _navigationService;
    private readonly IMvxMessenger _messenger;
    private string _title = string.Empty;
    private string _inputValue = string.Empty;
    private int _itemId;

    public DetailViewModel(
        IMvxNavigationService navigationService,
        IMvxMessenger messenger)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        SaveCommand = new MvxAsyncCommand(Save);
        CancelCommand = new MvxAsyncCommand(Cancel);
    }

    public IMvxAsyncCommand SaveCommand { get; }
    public IMvxAsyncCommand CancelCommand { get; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string InputValue
    {
        get => _inputValue;
        set => SetProperty(ref _inputValue, value);
    }

    public int ItemId
    {
        get => _itemId;
        set => SetProperty(ref _itemId, value);
    }

    public override void Prepare(DetailParameter parameter)
    {
        Title = parameter.Title;
        ItemId = parameter.ItemId;
        InputValue = $"Item {ItemId} data";
    }

    private async Task Save()
    {
        var result = new DetailResult
        {
            IsSuccess = true,
            SavedValue = InputValue
        };

        await _navigationService.Close(this);
        _messenger.Publish(new GenericMessage<DetailResult>(this, result));
    }

    private async Task Cancel()
    {
        var result = new DetailResult
        {
            IsSuccess = false,
            SavedValue = null
        };

        await _navigationService.Close(this);
        _messenger.Publish(new GenericMessage<DetailResult>(this, result));
    }
}
