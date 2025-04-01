namespace IoTShared.Devices;

public class Thermostat : DeviceBase, IDevice
{
    // I'm intentional about this naming convention.
    // _ means it is Private
    private int _CurrentTempreture { get; set; }
    public string Domain = Domains.Thermostat;
    public int DeviceId { get; set; }
    public required Room Room { get; set; }
    public int CurrentTempreture
    {
        get => _CurrentTempreture;
        set
        {
            if (value == _CurrentTempreture) return;
            _CurrentTempreture = value;
            NotifyPropertyChanged(nameof(CurrentTempreture));
        }
    }
}