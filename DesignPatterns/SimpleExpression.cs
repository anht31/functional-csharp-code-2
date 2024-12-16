namespace SimpleExpression;
using System;
using System.Collections.Generic;

// AbstractExpression
interface IExpression
{
    int Interpret(Dictionary<string, int> context);
}

// TerminalExpression
class NumberExpression : IExpression
{
    private int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public int Interpret(Dictionary<string, int> context)
    {
        return _number;
    }
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

    public int Interpret(Dictionary<string, int> context)
    {
        return _leftExpression.Interpret(context) + _rightExpression.Interpret(context);
    }
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

    public int Interpret(Dictionary<string, int> context)
    {
        return _leftExpression.Interpret(context) - _rightExpression.Interpret(context);
    }
}

// Client
class App
{
    public void Run()
    {
        // Context is empty in this case
        var context = new Dictionary<string, int>();

        // Construct the expression: 5 + (3 - 2)
        IExpression expression = new AddExpression(
            new NumberExpression(5),
            new SubtractExpression(
                new NumberExpression(3),
                new NumberExpression(2)
            )
        );

        Console.WriteLine($"Result: {expression.Interpret(context)}"); // Output: 6
        Console.WriteLine(context.Count);
    }
}
