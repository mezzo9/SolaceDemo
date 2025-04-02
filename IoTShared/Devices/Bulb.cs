namespace IoTShared.Devices;

public class Bulb : DeviceBase, IDevice
{
    public required Room Room { get; set; }
    public string Domain = Domains.Lighting;
    private bool _IsOn { get; set; }
    public int DeviceId { get; set; }
    public bool IsOn  
    {
        get => _IsOn;
        set
        {
            if (value == _IsOn) return;
            _IsOn = value;
            NotifyPropertyChanged(nameof(IsOn));
        }
    }
}