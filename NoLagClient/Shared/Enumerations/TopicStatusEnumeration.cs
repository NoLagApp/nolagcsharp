namespace NoLagClient.Shared;

public class TopicStatusEnumeration(int id, string name) : Enumeration(id, name)
{
    public static TopicStatusEnumeration Active = new TopicStatusEnumeration(0, nameof(Active));
    public static TopicStatusEnumeration InActive = new TopicStatusEnumeration(1, nameof(InActive));
}