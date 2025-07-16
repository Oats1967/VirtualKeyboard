namespace VirtualKeyboard.Services
{
    public interface ILayoutService
    {
        public Dictionary<Layouts, (int x, int y, int width, int height)> Layouts { get; }
     
    }
}
