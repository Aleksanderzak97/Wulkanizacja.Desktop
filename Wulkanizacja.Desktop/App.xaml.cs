﻿using Microsoft.Extensions.DependencyInjection;
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

namespace Wulkanizacja.User;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public ServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
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
        services.AddSingleton<BusyIndicatorService>();
        services.AddSingleton<BusyIndicator>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<GeneralViewModel>();
        services.AddTransient<ContentControlViewModel>();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<HttpClient>(provider =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5884")
            };
            return client;
        });

        services.AddSingleton<WebServiceClient>();
        services.AddSingleton<TireRepository>();

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

