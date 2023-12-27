using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Graphics;

namespace VirtualKeyboard.Services
{
    internal static class WindowSizeService
    {
        public static void ResizeWindow( int x, int y, int width, int height)
        {
            var window = Microsoft.UI.Xaml.Window.Current;
            window.ExtendsContentIntoTitleBar = false; /*This is important to prevent your app content extends into the title bar area.*/
            IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
            AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
            if (winuiAppWindow.Presenter is OverlappedPresenter p)
            {

                p.SetBorderAndTitleBar(false, false);

            }
            winuiAppWindow.MoveAndResize(new RectInt32(0, 0, 0, 0));
        }
    }
}
