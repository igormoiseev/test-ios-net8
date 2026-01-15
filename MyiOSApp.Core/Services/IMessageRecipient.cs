namespace MyiOSApp.Core.Services;

public interface IMessageRecipient<T>
{
    Task<T> WaitAsync();
}
