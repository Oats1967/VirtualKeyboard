﻿using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;


#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterPages();

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false; /*This is important to prevent your app content extends into the title bar area.*/
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.IsMinimizable = false;
                            p.IsMaximizable = false;
                            p.IsResizable = false;
                            p.IsAlwaysOnTop = true;
                            p.SetBorderAndTitleBar(false, false);
                            
                          
                            
                            
                        }
                        
                        // toDo uncomment
                       //WindowSizeService.ResizeWindow(0,0,0,0);

                    });
                   

                });
            });

#endif

            builder.Logging
#if DEBUG
                        .AddTraceLogger(
                            options =>
                            {
                                options.MinLevel = LogLevel.Trace;
                                options.MaxLevel = LogLevel.Critical;
                            }); // Will write to the Debug Output
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

        
        
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
             builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<NumericKeyboardPage>();
            builder.Services.AddSingleton<AlphabeticKeyboardPage>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<NumericKeyboardViewModel>();
            builder.Services.AddSingleton<AlphabeticKeyboardViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ITCPService,TCPService>();
            return builder;
        }
    }

   
}
