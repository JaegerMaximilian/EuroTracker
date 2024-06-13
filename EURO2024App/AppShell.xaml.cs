namespace EURO2024App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("InfoPage", typeof(View.InfoPage));
            Routing.RegisterRoute("StadiumPage", typeof(View.StadiumPage));
            Routing.RegisterRoute("GamePage", typeof(View.GamePage));
            Routing.RegisterRoute("GroupAPage", typeof(View.GroupAPage));
            Routing.RegisterRoute("GroupBPage", typeof(View.GroupBPage));
            Routing.RegisterRoute("EventPage", typeof(View.EventPage));
        }
    }
}
