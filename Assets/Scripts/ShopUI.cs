using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public Shop shop;

    private Text goldText;
    private Transform itemContainer;

    private void Awake()
    {
        if (inventory == null) inventory = GetComponent<PlayerInventory>() ?? FindObjectOfType<PlayerInventory>();
        if (shop == null) shop = GetComponent<Shop>();
        PlayerInventory.OnGoldChanged += UpdateGold;
    }

    private void OnDestroy()
    {
        PlayerInventory.OnGoldChanged -= UpdateGold;
    }

    private void Start()
    {
        if (inventory != null)
        {
            inventory.AddGold(10);
        }
        CreateCanvas();
        CreateTestItems();
    }

    private void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("ShopCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.layer = LayerMask.NameToLayer("UI");

        GameObject panel = new GameObject("Panel", typeof(RectTransform), typeof(Image), typeof(VerticalLayoutGroup));
        panel.transform.SetParent(canvasObj.transform, false);
        panel.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        itemContainer = panel.transform;

        goldText = CreateText(canvasObj.transform, new Vector2(10, -10), "Gold: 0");
    }

    private void CreateTestItems()
    {
        if (shop == null) return;
        shop.inventory = inventory;
        shop.stock = new Item[]
        {
            new Item { itemName = "Potato Gun", cost = 5, modifiers = new StatModifier[0] },
            new Item { itemName = "Spud Shield", cost = 3, modifiers = new StatModifier[0] }
        };

        for (int i = 0; i < shop.stock.Length; i++)
        {
            int index = i;
            CreateButton(itemContainer, shop.stock[i].itemName + " - " + shop.stock[i].cost, () => shop.Buy(index));
        }
    }

    private Text CreateText(Transform parent, Vector2 anchoredPosition, string text)
    {
        GameObject obj = new GameObject("Text", typeof(RectTransform), typeof(Text));
        obj.transform.SetParent(parent, false);
        Text t = obj.GetComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.fontSize = 24;
        t.color = Color.yellow;
        t.alignment = TextAnchor.UpperLeft;
        t.text = text;
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = anchoredPosition;
        return t;
    }

    private void CreateButton(Transform parent, string text, UnityEngine.Events.UnityAction action)
    {
        GameObject obj = new GameObject("Button", typeof(RectTransform), typeof(Image), typeof(Button));
        obj.transform.SetParent(parent, false);
        Image img = obj.GetComponent<Image>();
        img.color = new Color(0.3f, 0.3f, 0.3f, 1f);

        GameObject txtObj = new GameObject("Text", typeof(RectTransform), typeof(Text));
        txtObj.transform.SetParent(obj.transform, false);
        Text txt = txtObj.GetComponent<Text>();
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = 20;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.text = text;
        RectTransform rect = txt.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Button btn = obj.GetComponent<Button>();
        btn.onClick.AddListener(action);
    }

    private void UpdateGold(int value)
    {
        if (goldText != null) goldText.text = "Gold: " + value;
    }
}

