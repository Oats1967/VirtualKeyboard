#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using Windows.Graphics;

using WinRT.Interop;
#endif

namespace VirtualKeyboard.Services
{
    internal static class WindowSizeService
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


        public static void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16)
            {

#if WINDOWS
            var window = Application.Current?.Windows.First(uiWindow =>
                
            {
                    if (uiWindow.Handler?.PlatformView is MauiWinUIWindow winUIWindow)
                    {

                        return true;
                    }
                    return false;
                });
            var platformView = window!.Handler?.PlatformView as MauiWinUIWindow;
            var winUiWindow = platformView!.AppWindow;
            if (winUiWindow.Presenter is OverlappedPresenter presenter)
            {
                winUiWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
                presenter.IsAlwaysOnTop = true;
               

            }
            winUiWindow.MoveAndResize(new RectInt32(x, y, width, height));

            IntPtr region = CreateRoundRectRgn(
              0,
              0,
              width + 1,
              height + 1,
              cornerRadius,
              cornerRadius);

            IntPtr nativeWindowHandle = WindowNative.GetWindowHandle(platformView);

           
            // Apply region to window
            SetWindowRgn(nativeWindowHandle, region, true);

#else
            throw new NotImplementedException();
#endif
        }

       

        

       





    }
}
