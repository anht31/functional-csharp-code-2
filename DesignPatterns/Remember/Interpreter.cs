using static System.Console;
namespace DesignPatterns.Remember.Interpreter;

class Context
{
    private Dictionary<string, int> _variables = new Dictionary<string, int>();
    public void Assign(string key, int value) => _variables[key] = value;
    public int GetValue(string key) => _variables.ContainsKey(key) ? _variables[key] : 0;
}

interface IExpression
{
    int Interpret(Context context);
}
class VariableExpression : IExpression
{
    private string _variable;
    public VariableExpression(string variable) => _variable = variable;
    public int Interpret(Context context) => context.GetValue(_variable);
}
class AddExpression: IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;
    public AddExpression(IExpression left, IExpression right)
    {
        _leftExpression = left;
        _rightExpression = right;
    }
    public int Interpret(Context context) 
        => _leftExpression.Interpret(context) + _rightExpression.Interpret(context);
}
class SubtractExpression : IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;
    public SubtractExpression(IExpression left, IExpression right)
    {
        _leftExpression = left;
        _rightExpression = right;
    }
    public int Interpret(Context context)
        => _leftExpression.Interpret(context) - _rightExpression.Interpret(context);
}

class Client
{
    public void Run()
    {
        var context = new Context();
        context.Assign("x", 5);
        context.Assign("y", 3);
        context.Assign("z", 2);

        IExpression composeExpression = new AddExpression(
            new VariableExpression("x"),
            new SubtractExpression(
                new VariableExpression("y"),
                new VariableExpression("z")
            )
        );
        int result = composeExpression.Interpret(context);
        WriteLine($"Result after Interpreter is {result}");
    }
}