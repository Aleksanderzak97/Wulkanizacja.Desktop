using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using Wulkanizacja.Desktop.Services;
using Wulkanizacja.Desktop.Views;
using Wulkanizacja.User.Services;
using Wulkanizacja.User.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Wulkanizacja.User;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public ServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var builder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();
        SwitchToDarkTheme();
        //pobieranie jezyka z systemu
        var systemLanguage = CultureInfo.InstalledUICulture.Name; 
        var culture = new CultureInfo(systemLanguage);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<TokenService>();

        services.AddSingleton(Configuration);

        services.AddSingleton<BusyIndicatorService>();
        services.AddSingleton<BusyIndicator>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<GeneralViewModel>();
        services.AddTransient<ContentControlViewModel>();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddHttpClient("UserClient")
            .ConfigureHttpClient((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["ServiceUrls:Wulkanizacja.Auth"]);
            });

        services.AddHttpClient("TireClient")
            .ConfigureHttpClient((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["ServiceUrls:Wulkanizacja.Service"]);
            });

        services.AddTransient<UserRepository>(sp =>
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient("UserClient");
            var tokenService = sp.GetRequiredService<TokenService>();
            return new UserRepository(new WebServiceClient(httpClient, tokenService));
        });

        services.AddTransient<TireRepository>(sp =>
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient("TireClient");
            var tokenService = sp.GetRequiredService<TokenService>();
            return new TireRepository(new WebServiceClient(httpClient, tokenService));
        });

        services.AddTransient<WebServiceClient>(sp =>
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient("TireClient");
            var tokenService = sp.GetRequiredService<TokenService>();
            return new WebServiceClient(httpClient, tokenService);
        });

    }

    private void SwitchToDarkTheme()
    {
        // Załaduj ResourceDictionary z ciemnym motywem
        var darkTheme = new ResourceDictionary
        {
            Source = new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute)
        };

        // Dodaj do kolekcji MergedDictionaries w Application lub w konkretnym Window
        Application.Current.Resources.MergedDictionaries.Add(darkTheme);
    }
}

