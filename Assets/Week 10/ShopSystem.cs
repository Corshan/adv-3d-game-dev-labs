using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public List<Item> shopItems;
    public GameObject shopItemsComponent;

    GameObject[] shopItemComponents;
    int totalPurchase = 0;
    int initialMoney;
    public int moneyLeft;
    float topLeftX, topLeftY;
    // Start is called before the first frame update
    void Start()
    {
        Innit();
    }

    public void Innit()
    {
        initialMoney = 1000;
        moneyLeft = initialMoney;

        topLeftX = 50;
        topLeftY = 300;

        shopItems = new List<Item>();
        shopItems.Add(new Item(Item.ItemType.APPLE));
        shopItems.Add(new Item(Item.ItemType.YELLOW_DIAMOND));
        shopItems.Add(new Item(Item.ItemType.MEAT));
        shopItems.Add(new Item(Item.ItemType.BLUE_DIAMOND));
        shopItems.Add(new Item(Item.ItemType.RED_DIAMOND));
        shopItems.Add(new Item(Item.ItemType.SWORD));

        shopItemComponents = new GameObject[shopItems.Count];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
