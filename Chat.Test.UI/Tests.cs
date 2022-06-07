using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Assert = NUnit.Framework.Assert;

namespace Chat.UITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public async Task Test()
        {
#if DEBUG
           app.Repl();
#endif
            app.EnterText(ui => ui.Marked("Username"), "admin");
            app.EnterText(ui => ui.Marked("Password"), "abcd1234");
            app.Tap(ui => ui.Marked("Login"));

            app.Tap(ui => ui.Marked("Fruit"));

            app.Tap(ui => ui.Marked("Add"));
            app.Tap(ui => ui.Marked("Add"));
            app.Tap(ui => ui.Marked("Add"));
            app.Tap(ui => ui.Marked("Add"));
            await Task.Delay(2000);
            app.Screenshot("Should be 4 fruits");

            app.Tap(ui => ui.Marked("Remove"));
            app.Tap(ui => ui.Marked("Remove"));
            await Task.Delay(2000);
            app.Screenshot("Should be 2 fruits");

            app.Tap(ui => ui.Marked("Profile"));
            app.Tap(ui => ui.Marked("Monitor"));
            app.Tap(ui => ui.Marked("Monitor"));

            await Task.Delay(6000);
            app.Screenshot("CPU High");
            await Task.Delay(30000);

            AppResult[] monitorElements = app.WaitForElement(c => c.Marked("Monitor"));
            Assert.IsTrue(monitorElements.Any());
        }
    }

    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("ms.chat.mobile")
                    .StartApp();
            }

                return ConfigureApp
                    .iOS
                    .InstalledApp("ms.chat.mobile")
                    .StartApp();
        }
    }
}
