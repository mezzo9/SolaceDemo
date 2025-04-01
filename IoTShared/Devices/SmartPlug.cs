namespace IoTShared.Devices;

public class SmartPlug : DeviceBase, IDevice
{
    // I'm intentional about this naming convention.
    // _ means it is Private
    private double _CurrentAmp { get; set; }
    public const string Domain = Domains.Plug;
    public int DeviceId { get; set; }
    public required Room Room { get; set; }
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