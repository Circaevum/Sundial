[System.Serializable]
public class EmailObject
{
    public string Id;
    public string ThreadId;
    public string[] LabelIds;
    public string Snippet;
    public string HistoryId;
    public string InternalDate;
    public MessagePart Paylod;
    public int SizeEstimate;
    public string Raw;
}
[System.Serializable]
public class MessagePart
{
    public string PartId;
    public string MimeType;
    public string Filename;
    public Header Headers;
    public MessagePartBody Body;
    public MessagePart Parts;
}
[System.Serializable]
public class Header
{
    public string Name;
    public string Value;
}
[System.Serializable]
public class MessagePartBody
{
    public string Body;
}
