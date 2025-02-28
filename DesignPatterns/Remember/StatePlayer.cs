using static System.Console;
namespace DesignPatterns.Remember.StatePlayer;

// Context
class AudioPlayer
{
    State _state;
    bool isPlaying = false;
    public bool IsPlaying => isPlaying;
    //public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public AudioPlayer()
    {
        this._state = new ReadyState(this);

        // Some UI Render
        // Some Events click handler
    }

    public void ChangeState(State state) => this._state = state;
    
    // UI click
    public void ClickPlay() => _state.ExecutePlaying();
    public void ClickLock() => _state.ExecuteLockOrUnlock();


    // Some player function
    public void StartPlayback()
    {
        isPlaying = true;
        WriteLine($"Device start play");
    }
    public void StopPlayback()
    {
        isPlaying = false;
        WriteLine($"Devide stop play");
    }
}

// State
abstract class State
{
    private protected AudioPlayer _player;
    public State(AudioPlayer player) => _player = player;
    public abstract void ExecutePlaying();
    public abstract void ExecuteLockOrUnlock();
}
class ReadyState : State
{
    public ReadyState(AudioPlayer player) : base(player) { }
    public override void ExecutePlaying()
    {
        _player.StartPlayback();
        _player.ChangeState(new PlayingState(_player));
    }
    public override void ExecuteLockOrUnlock() => _player.ChangeState(new LockState(_player));
}
class LockState : State
{
    public LockState(AudioPlayer player) : base(player) { }
    public override void ExecutePlaying()
    {
        // Do nothing
    }
    public override void ExecuteLockOrUnlock()
    {
        if (_player.IsPlaying)
            _player.ChangeState(new PlayingState(_player));
        else
            _player.ChangeState(new ReadyState(_player));
    }
}
class PlayingState : State
{
    public PlayingState(AudioPlayer player) : base(player) { }
    public override void ExecutePlaying()
    {
        _player.StopPlayback();
        _player.ChangeState(new ReadyState(_player));
    }

    public override void ExecuteLockOrUnlock() => _player.ChangeState(new LockState(_player));
}