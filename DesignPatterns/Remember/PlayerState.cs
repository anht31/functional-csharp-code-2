using static System.Console;
namespace DesignPatterns.Remember.PlayerState;

// Context
class Player
{
    private State _state;
    private string UI;
    private string volume;
    private string playlist;
    private string currentSong;
    public bool playing;
    public Player() { }
    public void ChangeState(State state)
    {
        WriteLine($"\tState: {state.GetType().Name}");
        _state = state;
    }

    // Delegate for active state
    public void ClickLock() => _state.Lock();
    public void ClickPlay()
    {
        playing = !playing;
        _state.Play();
    }

    public void ClickNext() => _state.Next();
    public void ClickPrevious() => _state.Previous();

    // Some Service method on the context
    public void StartPlayback() => WriteLine("Player -> Start Playback");
    public void StopPlayback() => WriteLine("Player -> Stop Playback");
    public void NextSong() => WriteLine("Player -> Next Song");
    public void PreviousSong() => WriteLine("Player -> Previous Song");
    public void FastForward() => WriteLine("Player -> Fast Forward");
    public void Rewind() => WriteLine("Player -> Rewind");
}


abstract class State
{
    protected Player _player;
    public State(Player player) => _player = player;

    public abstract void Lock();
    public abstract void Play();
    public abstract void Next();
    public abstract void Previous();
}

class ReadyState : State
{
    public ReadyState(Player player) : base(player) { }

    public override void Lock() => _player.ChangeState(new LockedState(_player));
    public override void Next() => _player.NextSong();
    public override void Play()
    {
        _player.StartPlayback();
        _player.ChangeState(new PlayingState(_player));
    }
    public override void Previous() => _player.PreviousSong();
}
class LockedState : State
{
    public LockedState(Player player) : base(player) { }

    public override void Lock() // Unlock
    {
        if (_player.playing)
            _player.ChangeState(new PlayingState(_player));
        else
            _player.ChangeState(new ReadyState(_player));
    }
    public override void Next() => throw new NotImplementedException();
    public override void Play() => throw new NotImplementedException();
    public override void Previous() => throw new NotImplementedException();
}
class PlayingState : State
{
    public PlayingState(Player player) : base(player) { }

    public override void Lock()
    {
        _player.ChangeState(new LockedState(_player));
    }
    public override void Next()
    {
        _player.NextSong();
        //if (event.doubleclick)
        //    player.nextSong()
        //else
        //    player.fastForward(5)
    }
    public override void Play()
    {
        _player.StopPlayback();
        _player.ChangeState(new ReadyState(_player));
    }
    public override void Previous()
    {
        _player.PreviousSong();
        //if (event.doubleclick)
        //    player.previous()
        //else
        //    player.rewind(5)
    }
}

class Client
{
    public void Run()
    {
        var player = new Player();
        player.ChangeState(new ReadyState(player));

        player.ClickPlay();
        player.ClickLock();
        player.ClickLock();
        player.ClickNext();
        player.ClickPlay();
    }
}