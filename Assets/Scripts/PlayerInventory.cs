using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int gold;
    public List<Item> items = new List<Item>();
    public MainCharacterController controller;

    public static event System.Action<int> OnGoldChanged;

    private void Awake()
    {
        if (controller == null) controller = GetComponent<MainCharacterController>();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    public bool Purchase(Item item)
    {
        if (gold < item.cost) return false;
        gold -= item.cost;
        items.Add(item);
        ApplyItem(item);
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    private void ApplyItem(Item item)
    {
        foreach (var mod in item.modifiers)
        {
            switch (mod.stat)
            {
                case StatType.Health:
                    controller.AddHealth((int)mod.amount);
                    break;
                case StatType.Damage:
                    controller.AddDamage(mod.amount);
                    break;
                case StatType.Speed:
                    controller.AddSpeed(mod.amount);
                    break;
            }
        }
    }
}
