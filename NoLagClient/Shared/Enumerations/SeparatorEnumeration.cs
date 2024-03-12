namespace NoLagClient.Shared;

public class SeparatorEnumeration(int id, string name, int separatorAscii) : Enumeration(id, name)
{
    public static SeparatorEnumeration Group = new SeparatorEnumeration(0, nameof(Group), 29);
    public static SeparatorEnumeration Record = new SeparatorEnumeration(1, nameof(Record), 30);
    public static SeparatorEnumeration Unit = new SeparatorEnumeration(2, nameof(Unit), 31);
    public static SeparatorEnumeration Vertical = new SeparatorEnumeration(3, nameof(Vertical), 11);
    public static SeparatorEnumeration NegativeAck = new SeparatorEnumeration(4, nameof(NegativeAck), 21);
    public static SeparatorEnumeration BellAlert = new SeparatorEnumeration(5, nameof(BellAlert), 7);

    public readonly int SeparatorAscii = separatorAscii;
}
