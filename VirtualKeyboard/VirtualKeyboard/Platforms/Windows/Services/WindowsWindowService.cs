

using System.Runtime.InteropServices;
#if WINDOWS
using Windows.Graphics;
using Microsoft.Extensions.Logging;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using VirtualKeyboard.Services;
using WinRT.Interop;

using Microsoft.Maui.LifecycleEvents;


namespace VirtualKeyboard.Platforms.Windows.Services
{
    public class WindowsWindowService : WindowService
    {
        public WindowsWindowService(ILogger<WindowService> logger, ILayoutSettings layoutSettings) : base(logger, layoutSettings)
        {
        }

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

#if DEBUG
                        // Apply region to window
                        var x = 0;
                        var y = 0; 
                        var width = 0;
                        var height = 0;
#else
                        var x = 0;
                        var y = 0; 
                        var width = 0;
                        var height = 0;

#endif
                        // Close window on initialization with bug workaround
                        var a = 0; var b = 0; var c = 0; var d = 0;
                        if (width > 0 && height > 0)
                        {
                            a = 4; b = 4; c = -2; d = -2;
                        }

                        SetWindowRgn(nativeWindowHandle, CreateRoundRectRgn(0 + a, 0 +b, width + c, height + d, 0, 0), true);
                        winuiAppWindow.MoveAndResize(new RectInt32(x - c, y -d, width, height));

                    });
                    
                });
            });       
        }

        

        public override void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16)
            {


            var window = Application.Current?.Windows.First(uiWindow => uiWindow.Handler?.PlatformView is MauiWinUIWindow);

            var platformView = window!.Handler?.PlatformView as MauiWinUIWindow;
            var nativeWindowHandle = WindowNative.GetWindowHandle(platformView);
            var winUiWindow = platformView!.AppWindow;

            // This is used to round windowcorners and remove white borders bug workaround!!!
            var a = 0; var b = 0; var c = 0; var d = 0;
            if (width > 0 && height > 0) 
            {
                 a = 4;  b = 4;  c = -2;  d = -2;
            }
           
            var region = CreateRoundRectRgn(
                0 + a,
                0 + b,
                width + c,
                height + d,
                cornerRadius,
                cornerRadius);

            // Apply region to window
             SetWindowRgn(nativeWindowHandle, region, true);
                
            winUiWindow.MoveAndResize(new RectInt32(x-c, y-d, width, height));
          


           

        }


        
    }
}
#endif