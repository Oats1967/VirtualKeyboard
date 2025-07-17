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
using VirtualKeyboard.Platforms.Windows.Services;


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
                    fonts.AddFont("JetBrainsMono-VariableFont_wght.ttf", "JetBrainsMono");
                    fonts.AddFont("JetBrainsMono-Italic-VariableFont_wght.ttf", "JetBrainsMonoItalic");
                    fonts.AddFont("Saira-Bold.ttf", "SairaBold");
                    fonts.AddFont("Saira-Regular.ttf", "SairaRegular");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterPages()
                .RegisterTemplates();
#if WINDOWS
            WindowsWindowService.Setup(builder);
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
            
            return builder.Build();

            


        }

        
        
        private static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }
        private static MauiAppBuilder RegisterTemplates(this MauiAppBuilder builder)
        {
            // Add new Contentviews here
            builder.Services.AddSingleton<NumericKeyboard>();
            builder.Services.AddSingleton<GermanKeyboard>();
            builder.Services.AddSingleton<EnglishKeyboard>();
            builder.Services.AddSingleton<DutchKeyboard>();
            return builder;
        }
        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPageViewModel>(); // only one for all keyboards
            
            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ITCPService,TCPService>();
            builder.Services.AddSingleton<IKeyboardService, KeyboardService>();
            builder.Services.AddSingleton<IProcessMessageService, ProcessTCPMessageService>();
            builder.Services.AddSingleton<LayoutToKeyboardConverter>();
            builder.Services.AddSingleton(LogOperatorRetriever.Instance);
            builder.Services.AddSingleton<ILayoutSettings>(sp =>
            {
                var layouts = new Dictionary<Layouts, (int x, int y, int width, int height)>
                {
                    [Layouts.Numeric] = (0, 0, 0, 0),
                    [Layouts.German] = (0, 0, 0, 0),
                    [Layouts.English] = (0, 0, 0, 0),
                    [Layouts.Dutch] = (0, 0, 0, 0),
                    [Layouts.French] = (0, 0, 0, 0),
                    [Layouts.Polish] = (0, 0, 0, 0),
                };

                return new LayoutSettings(layouts);
            });
#if WINDOWS
            builder.Services.AddSingleton<IWindowService, WindowsWindowService>();
#endif

            return builder;
        }

      



       


    }


}
