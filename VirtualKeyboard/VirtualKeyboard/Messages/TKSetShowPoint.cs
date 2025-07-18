namespace VirtualKeyboard.Messages
{
    public class TKSetShowPoint
    {
        public TKSetShowPoint(int layout, int x, int y)
        {
            Layout = Enum.IsDefined(typeof(Layouts), layout)
             ? (Layouts)layout
             : Layouts.NotUsed;
            X = Math.Clamp(x, 0, (int)DeviceDisplay.Current.MainDisplayInfo.Width);
            Y = Math.Clamp(y, 0, (int)DeviceDisplay.Current.MainDisplayInfo.Height);

        }
        public int X { get; }
        public int Y { get; }
        public Layouts Layout { get; }
    }
}
