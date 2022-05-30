global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;

namespace Chat.Core;

public abstract class BaseViewModel : ObservableObject, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
    }
}
