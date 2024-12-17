using static System.Console;
namespace DesignPatterns.Study.FactoryMethod;

public abstract class Dialog
{
    public abstract Button CreateButton();
    public void Render() => CreateButton().Render();
}
class WindowDialog : Dialog
{
    public override Button CreateButton() => new WindowButton();
}
class WebDialog : Dialog
{
    public override Button CreateButton() => new HTMLButton();
}

public interface Button
{
    void Render();
}
class WindowButton : Button
{
    public void Render() => WriteLine("WindowButton Render...");
}
class HTMLButton : Button
{
    public void Render() => WriteLine("HTMLButton Render...");
}

public class App
{
    public void Startup() => ConfigSystem().Render();
    public Dialog ConfigSystem(string os = "Windows")
        => os switch
        {
            "Windows" => new WindowDialog(),
            "Web" => new WebDialog(),
            _ => throw new Exception("Unknow operating system.")
        };
}