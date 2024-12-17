namespace DesignPatterns.Study.FactoryMethod_To_AbstractFactory;
using static System.Console;

public abstract class Dialog
{
    public abstract Button CreateButton();
    public abstract CheckBox CreateCheckbox();
}
class WindowDialog : Dialog
{
    public override Button CreateButton() => new WindowButton();
    public override CheckBox CreateCheckbox() => new WindowCheckBox();
}
class WebDialog : Dialog
{
    public override Button CreateButton() => new HTMLButton();
    public override CheckBox CreateCheckbox() => new HTMLCheckBox();
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

public interface CheckBox
{
    void Render();
}
class WindowCheckBox : CheckBox
{
    public void Render() => WriteLine("WindowCheckBox Render...");
}
class HTMLCheckBox : CheckBox
{
    public void Render() => WriteLine("HTMLCheckBox Render...");
}

public class App
{
    public void Startup()
    {
        var app = ConfigSystem();
        app.CreateButton().Render();
        app.CreateCheckbox().Render();
    }

    public Dialog ConfigSystem(string os = "Windows")
        => os switch
        {
            "Windows" => new WindowDialog(),
            "Web" => new WebDialog(),
            _ => throw new Exception("Unknow operating system.")
        };
}