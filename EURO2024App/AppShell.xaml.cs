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
            Routing.RegisterRoute(nameof(View.InfoPage), typeof(View.InfoPage));
            Routing.RegisterRoute(nameof(View.StadiumPage), typeof(View.StadiumPage));
            Routing.RegisterRoute(nameof(View.EventPage), typeof(View.EventPage));
            Routing.RegisterRoute(nameof(View.GruppenPage), typeof(View.GruppenPage));
            Routing.RegisterRoute(nameof(View.AddEventPage), typeof(View.AddEventPage));
            Routing.RegisterRoute(nameof(View.GamesPage), typeof(View.GamesPage));
            Routing.RegisterRoute(nameof(View.StatistikPage), typeof(View.StatistikPage));
        }
    }
}
