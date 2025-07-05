using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

using VirtualKeyboard.Controls;
using VirtualKeyboard.Converter;







#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;


#endif

namespace VirtualKeyboard
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcon");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterPages()
                .RegisterContentViews();

#if WINDOWS

            WindowService.Setup(builder);
            

#endif

            builder.Logging
#if DEBUG
                        .AddTraceLogger(
                            options =>
                            {
                                options.MinLevel = LogLevel.Trace;
                                options.MaxLevel = LogLevel.Critical;
                            }) // Will write to the Debug Output
                        .AddStreamingFileLogger(
                            options =>
                            {
                                options.RetainDays = 2;
                                options.FolderPath = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                "MetroLogs");
                            });
#endif

#if RELEASE


                       
                        .AddConsoleLogger(
                            options =>
                            {
                                options.MinLevel = LogLevel.Information;
                                options.MaxLevel = LogLevel.Critical;
                            }); // Will write to the Console Output (logcat for android)
#endif
            builder.Services.AddSingleton(LogOperatorRetriever.Instance);
            return builder.Build();
          
        }

        
        
        private static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }
        private static MauiAppBuilder RegisterContentViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<NumericKeyboard>();
            builder.Services.AddSingleton<GermanKeyboard>();
            builder.Services.AddSingleton<EnglishKeyboard>();
            builder.Services.AddSingleton<DutchKeyboard>();
            return builder;
        }
        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<KeyboardViewModel>();
            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ITCPService,TCPService>();
            builder.Services.AddSingleton<IKeyboardService, KeyboardService>();
            builder.Services.AddSingleton<IProcessMessageService, ProcessMessageService>();
            builder.Services.AddSingleton<ILayoutService, LayoutService>();
            builder.Services.AddSingleton<LayoutToKeyboardConverter>();
            return builder;
        }

      



       


    }


}
