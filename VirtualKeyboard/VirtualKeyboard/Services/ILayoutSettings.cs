namespace VirtualKeyboard.Services;

public interface ILayoutSettings
{
    public bool ContainsKey(Layouts layout);
    (int x, int y, int width, int height) this[Layouts layout] { get; set; }
}