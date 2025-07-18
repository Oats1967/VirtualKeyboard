namespace VirtualKeyboard.Messages
{
    public class TKSetShow
    {
        public TKSetShow(int layout)
        {
            Layout = Enum.IsDefined(typeof(Layouts), layout)
            ? (Layouts)layout
            : Layouts.NotUsed;
        }
        public Layouts Layout { get; }
    }
}
