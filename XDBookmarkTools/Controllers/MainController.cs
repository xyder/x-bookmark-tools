using XDBookmarkTools.Views;

namespace XDBookmarkTools.Controllers
{
    internal class MainController
    {
        //slated for removal
        public Logger Logger;
        public MainView MainView;

        public void Init()
        {
            Logger = new Logger();
            MainView = new MainView(Logger);
        }
    }
}