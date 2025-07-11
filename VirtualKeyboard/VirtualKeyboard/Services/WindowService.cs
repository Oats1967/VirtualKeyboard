﻿#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using Windows.Graphics;

using WinRT.Interop;
#endif

namespace VirtualKeyboard.Services
{
    internal static class WindowService
    {
        




        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);


        //Invoke declarations for setting window styles
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;


        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void Setup(MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        
                       
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        
                        winuiAppWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
                        
                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.IsMinimizable = false;
                            p.IsMaximizable = false;
                            p.IsResizable = false;
                            p.IsAlwaysOnTop = true;               
                            p.SetBorderAndTitleBar(false, false); 
                        }
                        SetWindowLong(nativeWindowHandle, GWL_EXSTYLE, GetWindowLong(nativeWindowHandle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
                        ResizeWindow(0, 0, 0, 0);
                    });
                    
                });
            });
           
        }


        public static void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16)
            {

#if WINDOWS
            var window = Application.Current?.Windows.First(uiWindow =>
            {
                return uiWindow.Handler?.PlatformView is MauiWinUIWindow;   
            });

            var platformView = window!.Handler?.PlatformView as MauiWinUIWindow;
            IntPtr nativeWindowHandle = WindowNative.GetWindowHandle(platformView);
            var winUiWindow = platformView!.AppWindow;

            // This is used to round windowcorners and remove white borders bug !!!
            var a = 0; var b = 0; var c = 0; var d = 0;
            if (width > 0 && height > 0) 
            {
                 a = 4;  b = 4;  c = -3;  d = -3;
            }
           
            IntPtr region = CreateRoundRectRgn(
                0 + a,
                0 + b,
                width + c,
                height + d,
                cornerRadius,
                cornerRadius);

            // Apply region to window
             SetWindowRgn(nativeWindowHandle, region, true);
                
            winUiWindow.MoveAndResize(new RectInt32(x, y, width, height));
          

#else
            throw new NotImplementedException();
#endif
        }



        







    }
}
