using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int cost;
    public StatModifier[] modifiers;
}

[System.Serializable]
public struct StatModifier
{
    public StatType stat;
    public float amount;
}

public enum StatType
{
    Health,
    Damage,
    Speed
}
