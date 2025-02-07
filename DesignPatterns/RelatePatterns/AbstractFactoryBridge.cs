using static System.Console;
namespace DesignPatterns.RelatePatterns.AbstractFactoryBridge;

interface IAbstractFactory {
    Button CreateButton();
    Checkbox CreateCheckbox();
}
class WindowFactory : IAbstractFactory {
    public Button CreateButton() => new WindowButton(new WindowButtonEngine());
    public Checkbox CreateCheckbox() => new WindowCheckbox(new WindowCheckboxEngine());
}
class MacFactory : IAbstractFactory {
    public Button CreateButton() => new MacButton(new MacButtonEngine());
    public Checkbox CreateCheckbox() => new MacCheckbox(new MacCheckboxEngine());
}

abstract class GUIComponnet
{
    private protected IRenderEngine _engine;
    protected GUIComponnet(IRenderEngine engine) => this._engine = engine;
    public void Render() => _engine.Render();
}

abstract class Button : GUIComponnet {
    public Button(IRenderEngine engine) : base(engine) { }
}
class WindowButton : Button {
    public WindowButton(IRenderEngine engine) : base(engine) { }
}
class MacButton : Button {
    public MacButton(IRenderEngine engine) : base(engine) { }
}

abstract class Checkbox : GUIComponnet {
    protected Checkbox(IRenderEngine engine) : base(engine) { }
}
class WindowCheckbox : Checkbox {
    public WindowCheckbox(IRenderEngine engine) : base(engine) { }
}
class MacCheckbox : Checkbox {
    public MacCheckbox(IRenderEngine engine) : base(engine) { }
}

interface IRenderEngine {
    void Render();
}
class WindowButtonEngine : IRenderEngine {
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}
class WindowCheckboxEngine : IRenderEngine {
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}
class MacButtonEngine : IRenderEngine {
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}
class MacCheckboxEngine : IRenderEngine {
    public void Render() => WriteLine($"{this.GetType()} Rendering...");
}


class Client {
    IAbstractFactory Config(string os = "Window") => os switch {
        "Window" => new WindowFactory(),
        "Mac" => new MacFactory(),
        _ => throw new NotImplementedException(),
    };
    public void Run() {
        IAbstractFactory factory = Config("Mac");
        factory.CreateButton().Render();
        factory.CreateCheckbox().Render();
    }
}