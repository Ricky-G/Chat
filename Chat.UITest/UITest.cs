using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Assert = NUnit.Framework.Assert;

namespace Chat.UITest
{
    [TestFixture(Platform.Android)]
    public class UITest
    {
        IApp app;
        Platform platform;

        public UITest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void LoginTest()
        {
#if DEBUG
            app.Repl();
#endif
            app.EnterText(ui => ui.Marked("username"), "admin");
            app.EnterText(ui => ui.Marked("password"), "abcd1234");
            app.Screenshot("LoginPage");
            app.Tap(c => c.Marked("LoginButton"));
            AppResult[] results = app.WaitForElement(c => c.Marked("LogoutButton"));
            app.Screenshot("CollectionPage");
            Assert.IsTrue(results.Any());
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
                   .Debug()
                   .StartApp();
            }

                return ConfigureApp
                    .iOS
                    .InstalledApp("ms.chat.mobile")
                    .StartApp();
        }
    }
}
