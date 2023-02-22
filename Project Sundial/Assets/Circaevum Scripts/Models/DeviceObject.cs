[System.Serializable]
public class DeviceObject
{
    public string Collection;
    public string CreateTime;
    public string Key;
    public string PermissionRead;
    public string PermissionWrite;
    public string UpdateTime = "Unknown";
    public string UserId;
    public DeviceID Value;
    public string Version;
}
[System.Serializable]
public class DeviceID
{
    public string deviceId;
}