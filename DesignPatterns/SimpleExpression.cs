using System;
using System.Collections.Generic;
namespace SimpleExpression;

class Context
{
    private Dictionary<string, int> _variables = new Dictionary<string, int>();
    public void Assign(string key, int value) => _variables[key] = value;
    public int GetValue(string key) => _variables.ContainsKey(key) ? _variables[key] : 0;
}

// AbstractExpression
interface IExpression
{
    int Interpret(Context context);
}
// TerminalExpression
class VariableExpression : IExpression
{
    private string _variable;
    public VariableExpression(string variable) => _variable = variable;
    public int Interpret(Context context) => context.GetValue(_variable);
}

// NonTerminalExpression
class AddExpression : IExpression
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

class App
{
    public void Run()
    {
        var context = new Context();

        // Assign values to variables in context
        context.Assign("x", 5);
        context.Assign("y", 3);
        context.Assign("z", 2);

        // Build expression: x (y - z)
        IExpression expression = new AddExpression(
            new VariableExpression("x"),
            new SubtractExpression(
                new VariableExpression("y"),
                new VariableExpression("z")
            )
        );

        Console.WriteLine($"Result: {expression.Interpret(context)}"); // Output: 6
    }
}
