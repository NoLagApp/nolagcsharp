namespace NoLagClient.Shared;

public class ActionEnumeration(int id, string name, string actionKey) : Enumeration(id, name)
{
    public static ActionEnumeration Add = new ActionEnumeration(0, nameof(Add), "a");
    public static ActionEnumeration Delete = new ActionEnumeration(1, nameof(Delete), "d");
}