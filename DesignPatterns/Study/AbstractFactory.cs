
namespace DesignPatterns.Study.AbstractFactory;

using static System.Console;

public interface GUIFactory
{
    public Button CreateButton();
}
public class WindowFactory : GUIFactory
{
    public Button CreateButton() => new WinButton();
}
public class MacFactory : GUIFactory
{
    public Button CreateButton() => new MacButton();
}


public interface Button
{
    void Paint();
}
public class WinButton : Button
{
    public void Paint() => WriteLine("Render WinButton...");
}
public class MacButton : Button
{
    public void Paint() => WriteLine("Render MacButton...");
}

public class App
{
    public void Startup() => ConfigSystem()
        .CreateButton()
        .Paint();

    public GUIFactory ConfigSystem(string os = "Windows")
        => os switch
        {
            "Windows" => new WindowFactory(),
            "Web" => new MacFactory(),
            _ => throw new Exception("Unknow operating system.")
        };
}

//class Application {
//    private GUIFactory factory;
//    private Button button;
//    public Application(GUIFactory factory)
//    {
//        this.factory = factory;
//    }
//    public void CreateUI()
//    {
//        button = factory.CreateButton();
//    }
//    public void Paint() => button.Paint();
//}

//class ApplicaionConfigurator
//{
//    public void Main(string config = "Windows")
//    {
//        GUIFactory factory = config switch
//        {
//            "Windows" => new WindowFactory(),
//            "Mac" => new MacFactory(),
//            _ => throw new Exception("Error! Unknow operating system.")
//        };
//        Application app = new Application(factory);
//        app.CreateUI();
//        app.Paint();
//    }
//}