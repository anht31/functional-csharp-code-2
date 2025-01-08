using static System.Console;
namespace DesignPatterns.Remember.State;

class Context
{
    private IState _state;
    void SetState(IState state) => _state = state;

}

interface IState
{

}

class ConcreteStateA : IState
{
}

class ConcreteStateB : IState
{
}