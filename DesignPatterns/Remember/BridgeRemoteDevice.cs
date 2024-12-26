using static System.Console;

namespace DesignPatterns.Remember.BridgeRemoteDevice;

class Remote
{
    protected IDevice _device;
    public Remote(IDevice device) => _device = device;
    public void VolumeUp()
    {
        var curentVolume = _device.GetVolume();
        _device.SetVolume(++curentVolume);
    }
}

class AdvancedRemote : Remote
{
    public AdvancedRemote(IDevice device) : base(device)
    {
    }

    public void Mute() => _device.SetVolume(0);
}

interface IDevice
{
    int GetVolume();
    void SetVolume(int level);
}
class Tv : IDevice
{
    public int GetVolume() => new Random().Next();

    public void SetVolume(int level) => WriteLine($"Volume up to {level}");
}

class Radio : IDevice
{
    public int GetVolume() => new Random().Next();

    public void SetVolume(int level) => WriteLine($"Volume up to {level}");
}


class Client
{
    public void Run()
    {
        var tv = new Tv();
        var remote = new Remote(tv);
        remote.VolumeUp();
    }
}