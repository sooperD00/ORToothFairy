using Microsoft.Extensions.Logging;
using ORToothFairy.MAUI.Services;

namespace ORToothFairy.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

        // Configure HttpClient for API calls
        builder.Services.AddHttpClient<SearchService>(client =>
        {
            //client.BaseAddress = new Uri("http://localhost:5167/"); // Your API URL
			#if DEBUG
				client.BaseAddress = new Uri("http://localhost:5167/");
			#else
						client.BaseAddress = new Uri("https://your-azure-url.azurewebsites.net/");
			#endif
            client.Timeout = TimeSpan.FromSeconds(30);
        });

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}
