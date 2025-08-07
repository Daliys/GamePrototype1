using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUIBootstrap : MonoBehaviour
{
    private Text goldText;
    private Shop shop;
    private PlayerInventory inventory;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;
        new GameObject("ShopUIBootstrap").AddComponent<ShopUIBootstrap>().Setup();
    }

    void Setup()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        if (inventory == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null) inventory = player.AddComponent<PlayerInventory>();
        }
        if (inventory != null && inventory.gold <= 0) inventory.gold = 100;

        var shopObj = new GameObject("RuntimeShop");
        shop = shopObj.AddComponent<Shop>();
        shop.inventory = inventory;
        shop.stock = CreateTestItems();

        CreateCanvas();
        UpdateGoldText();
    }

    private Item[] CreateTestItems()
    {
        return new Item[]
        {
            new Item
            {
                itemName = "Knife",
                cost = 30,
                modifiers = new StatModifier[]
                {
                    new StatModifier{ stat = StatType.Damage, amount = 2 }
                }
            },
            new Item
            {
                itemName = "Boots",
                cost = 20,
                modifiers = new StatModifier[]
                {
                    new StatModifier{ stat = StatType.Speed, amount = 1 }
                }
            },
            new Item
            {
                itemName = "Berry",
                cost = 25,
                modifiers = new StatModifier[]
                {
                    new StatModifier{ stat = StatType.Health, amount = 1 }
                }
            }
        };
    }

    private void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("ShopCanvas");
        var canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvasObj.transform);
        var panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color32(40, 40, 40, 200);
        var panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.7f, 0f);
        panelRect.anchorMax = new Vector2(1f, 1f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        GameObject goldObj = new GameObject("GoldText");
        goldObj.transform.SetParent(panelObj.transform);
        goldText = goldObj.AddComponent<Text>();
        goldText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        goldText.fontSize = 24;
        goldText.alignment = TextAnchor.UpperCenter;
        goldText.color = new Color32(255, 230, 109, 255);
        var goldRect = goldObj.GetComponent<RectTransform>();
        goldRect.anchorMin = new Vector2(0, 1);
        goldRect.anchorMax = new Vector2(1, 1);
        goldRect.pivot = new Vector2(0.5f, 1);
        goldRect.anchoredPosition = new Vector2(0, -10);
        goldRect.sizeDelta = new Vector2(0, 30);

        for (int i = 0; i < shop.stock.Length; i++)
        {
            CreateItemButton(shop.stock[i], i, panelObj.transform);
        }
    }

    private void CreateItemButton(Item item, int index, Transform parent)
    {
        GameObject buttonObj = new GameObject(item.itemName + "Button");
        buttonObj.transform.SetParent(parent);
        var image = buttonObj.AddComponent<Image>();
        image.color = new Color32(70, 75, 85, 255);
        var button = buttonObj.AddComponent<Button>();
        var rect = buttonObj.GetComponent<RectTransform>();
        float height = 40f;
        rect.anchorMin = new Vector2(0.1f, 1f - (index + 1) * 0.15f);
        rect.anchorMax = new Vector2(0.9f, 1f - index * 0.15f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);
        var text = textObj.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        text.text = item.itemName + " - " + item.cost;
        var textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        button.onClick.AddListener(() =>
        {
            if (shop.Buy(index))
            {
                UpdateGoldText();
            }
        });
    }

    private void UpdateGoldText()
    {
        if (goldText != null && inventory != null)
        {
            goldText.text = "Gold: " + inventory.gold;
        }
    }
}
