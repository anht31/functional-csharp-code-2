using static System.Console;
namespace DesignPatterns.RelatePatterns.CommandMemento;

abstract class Command
{
    protected Editor _editor;
    protected Application _app;
    
    public Command(Editor editor, Application app)
    {
        _editor = editor;
        _app = app;
    }
    public void SaveBackup() => _editor.Backup(); // after -> push stack
    public abstract bool Execute();
}

#region command
class CopyCommand : Command
{
    public CopyCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        _app.Clipboard = _editor.GetSelection();
        return false;
    }
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
}
class UndoCommand : Command
{
    public UndoCommand(Editor editor, Application app) : base(editor, app) { }
    public override bool Execute()
    {
        _app.Undo();
        return false;
    }
}
#endregion command

interface IMemento
{
    void Restore();
}
class ConcreteMemento(Editor editor, string state) : IMemento
{
    public void Restore() => editor.Restore(state);
}
// Receiver - Originator
class Editor
{
    private string _text;
    protected string backup = string.Empty;
    public string Text { get => _text; set => _text = value; }
    public string GetSelection() => "Return selection text...";
    public void DeleteSelection()
    {
        Text = "[Content]: Delete selection text";
        WriteLine("Delete selection text...");
    }

    public void ReplaceSelection(string text)
    {
        Text = $"[Content]: Replace selection {text}";
        WriteLine(Text = $"Replace selection {text}...");
    }

    public void Backup() => backup = _text;
    public void Restore(string state)
    {
        _text = state;
        WriteLine($"[Restore]: {_text}");
    }

    public IMemento Save() => new ConcreteMemento(this, backup);
}
// Caretaker
class HistoryCaretaker
{
    Stack<IMemento> history = new Stack<IMemento>();
    public void Push(IMemento memento)
    {
        history.Push(memento);
    }

    public IMemento Pop() => history.Pop();
}

// Client
class Application
{
    private Editor activeEditor = new Editor();
    internal string Clipboard;
    private HistoryCaretaker history = new HistoryCaretaker();

    public void Run() => CreateUI();

    public void CreateUI()
    {
        activeEditor.Text = "Init text line 1\nSome text line 2";

        var copy = () => ExecuteCommand(new CopyCommand(activeEditor, this));
        // UI Element - Invoker
        //copyButton.SetCommand(copy);
        //shortcuts.onKeyPress("Ctrl+C", copy);

        var cut = () => ExecuteCommand(new CutCommand(activeEditor, this));
        //cutButton.SetCommand(cut);
        //shortcuts.onKeyPress("Ctrl+X", cut);

        var paste = () => ExecuteCommand(new PasteCommand(activeEditor, this));
        //pasteButton.SetCommand(paste);
        //shortcuts.onKeyPress("Ctrl+V", paste);

        var undo = () => ExecuteCommand(new UndoCommand(activeEditor, this));
        //undoButton.SetCommand(undo);
        //shortcuts.onKeyPress("Ctrl+Z", undo);

        copy();
        cut();
        paste();
        undo();
        undo();
    }

    public void ExecuteCommand(Command command)
    {
        if (command.Execute())
            history.Push(activeEditor.Save());
    }
    public void Undo()
    {
        var memento = history.Pop();
        if (memento != null)
            memento.Restore();
    }
}