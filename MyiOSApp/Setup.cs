using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Ios.Core;
using MyiOSApp.Core;

namespace MyiOSApp;

public class Setup : MvxIosSetup<App>
{
    protected override ILoggerFactory? CreateLogFactory()
    {
        return null;
    }

    protected override ILoggerProvider? CreateLogProvider()
    {
        return null;
    }
}
