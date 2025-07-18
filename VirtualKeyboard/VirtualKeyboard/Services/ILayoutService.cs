namespace VirtualKeyboard.Services;

public interface ILayoutService
{
    public bool ContainsKey(Layouts layout);
    LayoutData this[Layouts layout] { get;  }
}