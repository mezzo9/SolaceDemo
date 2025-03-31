namespace Solace.Shared;

public class Thermostat : DeviceBase
{
    public string Domain = Domains.Thermostat;
    public ushort CurrentTempreture { get; set; }
    
}