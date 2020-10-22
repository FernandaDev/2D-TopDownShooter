public interface IDrop
{
    ItemRarity ItemRarity { get; }
    bool IsDropped { get; }
    void GetDropped();
}

public enum ItemRarity
{
    commom = 70,
    uncommom = 30,
    rare = 5,
    super_rare = 3,
    legendary = 1
}
