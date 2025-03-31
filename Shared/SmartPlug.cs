namespace Solace.Shared;

public class SmartPlug : DeviceBase, IDevice
{
    private double _CurrentAmp { get; set; }
    public required Location Location { get; set; }
    public const string Domain = Domains.Plug;
    public bool IsActive { get; set; }
    public ushort MaxAmp { get; set; }

    public double CurrentAmp
    {
        get => _CurrentAmp;
        set
        {
            if (Math.Abs(value - _CurrentAmp) == 0) return;
            _CurrentAmp = value;  
            NotifyPropertyChanged(nameof(CurrentAmp));
        }
    }
}