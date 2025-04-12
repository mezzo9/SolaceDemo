using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IoTShared.Devices;



// we can capture the old value if needed, by customizing the event in case we want to do that type of Audit
public class DeviceBase : INotifyPropertyChanged
{
    public required Metadata Metadata { get; set; }
    private bool _IsOnline { get; set; }
    public DateTime ChangedAt { get; set; }
    public bool IsOnline
    {
        get => _IsOnline;
        set
        {
            if (value == _IsOnline) return;
            _IsOnline = value;  
            NotifyPropertyChanged(nameof(IsOnline));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")  
    {  
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }  
}