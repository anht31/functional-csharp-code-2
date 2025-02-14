using static System.Console;
namespace DesignPatterns.RelatePatterns.PrototypeCommandEditor;

interface IPrototype
{
    Command Clone();
}
abstract class Command : IPrototype
{
    protected Editor _editor;
    protected Application _app;
    protected string backup = string.Empty;

    public Command(Editor editor, Application app)
    {
        _editor = editor;
        _app = app;
    }
    public void SaveBackup() => backup = _editor.Text; // after -> push stack
    public void Undo() => _editor.Text = backup;
    public abstract bool Execute();

    public abstract Command Clone();
    private protected Command(Command command)
    {
        this._editor = command._editor;
        this._app = command._app;
        this.backup = command.backup;
    }
}
class CopyCommand : Command
{
    public CopyCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        _app.Clipboard = _editor.GetSelection();
        return false;
    }

    public override Command Clone() => new CopyCommand(this);
    private CopyCommand(CopyCommand command) : base(command) { }
}
class CutCommand : Command
{
    public CutCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        SaveBackup();
        _app.Clipboard = _editor.GetSelection();
        _editor.DeleteSelection();
        return true; // flag for push stack
    }

    public override Command Clone() => new CutCommand(this);
    private CutCommand(CutCommand command) : base(command) { }
}
class PasteCommand : Command
{
    public PasteCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        SaveBackup();
        _editor.ReplaceSelection(_app.Clipboard);
        return true;
    }

    public override Command Clone() => new PasteCommand(this);
    private PasteCommand(PasteCommand command) : base(command) { }
}
class UndoCommand : Command
{
    public UndoCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        _app.Undo();
        return false;
    }

    public override Command Clone() => new UndoCommand(this);
    private UndoCommand(UndoCommand command) : base(command) { }
}

// Receiver
class Editor
{
    public string Text;
    public string GetSelection() => "Return selection text...";
    public void DeleteSelection() => WriteLine("Delete selection text...");
    public void ReplaceSelection(string text) => WriteLine($"Replace selection {text}...");
}
// Custom
class CommandHistory
{
    Stack<Command> history = new Stack<Command>();
    public void Push(Command command) => history.Push(command);
    public Command Pop() => history.Pop();
}
// Client
class Application
{
    private List<Editor> editors = new List<Editor>();
    private Editor activeEditor;
    internal string Clipboard;
    private CommandHistory history;

    public void CreateUI()
    {
        var copyOriginalCommand = new CopyCommand(activeEditor, this);
        var executeCopy = () => ExecuteCommand(copyOriginalCommand);
        var executeCopyClone = () => ExecuteCommand(copyOriginalCommand.Clone());
        // UI Element - Invoker
        //copyButton.SetCommand(executeCopy);
        //shortcuts.onKeyPress("Ctrl+C", executeCopyClone);

        var cut = () => ExecuteCommand(new CutCommand(activeEditor, this));
        //cutButton.SetCommand(cut);
        //shortcuts.onKeyPress("Ctrl+X", cut);

        var paste = () => ExecuteCommand(new PasteCommand(activeEditor, this));
        //pasteButton.SetCommand(paste);
        //shortcuts.onKeyPress("Ctrl+V", paste);

        var undo = () => ExecuteCommand(new UndoCommand(activeEditor, this));
        //undoButton.SetCommand(undo);
        //shortcuts.onKeyPress("Ctrl+Z", undo);
    }

    public void ExecuteCommand(Command command)
    {
        if (command.Execute())
            history.Push(command);
    }
    public void Undo()
    {
        var command = history.Pop();
        if (command != null)
            command.Undo();
    }
}