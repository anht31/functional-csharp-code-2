using static System.Console;
namespace DesignPatterns.RelatePatterns.BuilderBridgeRemote;

// Abstract, Director
class RemoteDirector
{
    protected IDeviceController _deviceController;
    public RemoteDirector(IDeviceController builder) => _deviceController = builder;
    public void TogglePower()
    {
        if (_deviceController.IsPowerOn())
            _deviceController.PowerOff();
        else
        {
            _deviceController.PowerOn();
            var volumn = _deviceController.GetVolume();
            if (volumn > 50)
            {
                _deviceController.SetVolume(50);
            }
        }
    }
    public void VolumeUp()
    {
        if (!_deviceController.IsPowerOn()) return;
        var curentVolume = _deviceController.GetVolume();
        _deviceController.SetVolume(++curentVolume);
    }
}
class AdvancedRemoteDirector : RemoteDirector
{
    public AdvancedRemoteDirector(IDeviceController builder) : base(builder) { }
    public void Mute() => _deviceController.SetVolume(0);
}

class TvLog
{
    public List<string> _logicStep = new List<string>();
    public void LogAction(string logicStep) => _logicStep.Add(logicStep);
    public string GetLogicSteps() => _logicStep.Aggregate("Tv:", (acc, value) => $"{acc} +{value}");
}
class RadioLog
{
    public List<string> _logicStep = new List<string>();
    public void LogAction(string logicStep) => _logicStep.Add(logicStep);
    public string GetLogicSteps() => _logicStep.Aggregate("Radio:", (acc, value) => $"{acc} +{value}");
}

// Implement, Builder
interface IDeviceController 
{
    public bool IsPowerOn();
    public void PowerOn();
    public void PowerOff();
    public int GetVolume();
    public void SetVolume(int level);
}
class Tv : IDeviceController
{
    bool _isPowerOn;
    TvLog _log = new TvLog();
    public bool IsPowerOn() => _isPowerOn;
    public void PowerOn()
    {
        _isPowerOn = true;
        _log.LogAction("on");
    }
    public void PowerOff()
    {
        _isPowerOn = false;
        _log.LogAction("off");
    }
    public int GetVolume()
    {
        var currentVolumn = new Random().Next(100);
        _log.LogAction($"GetVolumn:{currentVolumn}");
        return currentVolumn;
    }
    public void SetVolume(int level) => _log.LogAction($"SetVolume:{level}");
    public TvLog GetProductLog() => _log;
}
class Radio : IDeviceController
{
    bool _isPowerOn;
    RadioLog _log = new RadioLog();
    public bool IsPowerOn() => _isPowerOn;
    public void PowerOn()
    {
        _isPowerOn = true;
        _log.LogAction("on");
    }

    public void PowerOff()
    {
        _isPowerOn = false;
        _log.LogAction("off");
    }
    public int GetVolume()
    {
        var currentVolumn = new Random().Next(100);
        _log.LogAction($"GetVolumn: {currentVolumn}");
        return currentVolumn;
    }

    public void SetVolume(int level) => _log.LogAction($"SetVolumn: {level}");
    public RadioLog GetProductLog() => _log;
}

class Client
{
    public void Run()
    {
        var tv = new Tv();
        var remote = new RemoteDirector(tv);
        remote.TogglePower();
        remote.VolumeUp();
        var productLog = tv.GetProductLog();
        WriteLine(productLog.GetLogicSteps());
    }
}