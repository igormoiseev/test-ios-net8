using MvvmCross.Plugin.Messenger;

namespace MyiOSApp.Core.Messages;

public class GenericMessage<T> : MvxMessage
{
    public GenericMessage(object sender, T value) : base(sender)
    {
        Value = value;
    }

    public T Value { get; }
}
