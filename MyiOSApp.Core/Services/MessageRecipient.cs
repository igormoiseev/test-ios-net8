using MvvmCross.Plugin.Messenger;
using MyiOSApp.Core.Messages;

namespace MyiOSApp.Core.Services;

public class MessageRecipient<T> : IMessageRecipient<T>
{
    private readonly IMvxMessenger _messenger;
    private MvxSubscriptionToken? _token;
    private TaskCompletionSource<T>? _tcs;

    public MessageRecipient(IMvxMessenger messenger)
    {
        _messenger = messenger;
    }

    public Task<T> WaitAsync()
    {
        _tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        _token = _messenger.SubscribeOnMainThread<GenericMessage<T>>(SetResult, MvxReference.Strong);
        return _tcs.Task;
    }

    private void SetResult(GenericMessage<T> msg)
    {
        _tcs?.TrySetResult(msg.Value);
        _tcs = null;

        if (_token != null)
        {
            _messenger.Unsubscribe<GenericMessage<T>>(_token);
            _token = null;
        }
    }
}
