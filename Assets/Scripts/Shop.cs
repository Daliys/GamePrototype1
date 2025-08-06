using UnityEngine;

public class Shop : MonoBehaviour
{
    public Item[] stock;
    public PlayerInventory inventory;

    public void Buy(int index)
    {
        if (index < 0 || index >= stock.Length) return;
        if (inventory != null)
        {
            inventory.Purchase(stock[index]);
        }
    }
}
