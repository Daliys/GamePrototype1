using UnityEngine;

public class Shop : MonoBehaviour
{
    public Item[] stock;
    public PlayerInventory inventory;

    public bool Buy(int index)
    {
        if (index < 0 || index >= stock.Length) return false;
        if (inventory != null)
        {
            return inventory.Purchase(stock[index]);
        }
        return false;
    }
}
