using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        APPLE,
        MEAT,
        GOLD,
        RED_DIAMOND,
        BLUE_DIAMOND,
        YELLOW_DIAMOND,
        SWORD,
        BATON,
    }

    public enum ItemFamilyType
    {
        FOOD,
        LOOT,
        WEAPON,
    }

    public string name, description;
    public int price, healthBenefits, damage, nb, maxNb;
    public ItemType type;
    public ItemFamilyType familyType;
    public string article;

    public Item(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.APPLE:
                name = "Apple";
                price = 50;
                healthBenefits = 10;
                damage = 0;
                nb = 1;
                maxNb = 5;
                description = "An apple";
                familyType = ItemFamilyType.FOOD;
                article = "an";
                break;

            case ItemType.MEAT:
                name = "Meat";
                price = 50;
                healthBenefits = 30;
                damage = 0;
                nb = 1;
                maxNb = 2;
                description = "some meat";
                familyType = ItemFamilyType.FOOD;
                article = "some";
                break;

            case ItemType.RED_DIAMOND:
                name = "Red Diamond";
                price = 250;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNb = 20;
                description = "Valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

            case ItemType.YELLOW_DIAMOND:
                name = "Yellow Diamond";
                price = 250;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNb = 10;
                description = "valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

            case ItemType.BLUE_DIAMOND:
                name = "Blue Diamond";
                price = 2500;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNb = 10;
                description = "valuable diamond";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

            case ItemType.GOLD:
                name = "gold";
                price = 250;
                healthBenefits = 0;
                damage = 0;
                nb = 1;
                maxNb = 10;
                description = "gold";
                familyType = ItemFamilyType.LOOT;
                article = "some";
                break;

            case ItemType.SWORD:
                name = "sword";
                price = 100;
                healthBenefits = 0;
                damage = 10;
                nb = 1;
                maxNb = 1;
                description = "a powerful sword";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;

            case ItemType.BATON:
                name = "Blue Diamond";
                price = 100;
                healthBenefits = 0;
                damage = 5;
                nb = 1;
                maxNb = 1;
                description = "a powerful baton";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;
        }
    }
    public string ItemInfo()
    {
        string info = $"name = {name}, health Benifits = {healthBenefits}, damage = {damage}, nb = {nb}";
        return info;
    }

    public Texture GetTexture()
    {
        Texture2D tx;

        if (this.familyType == ItemFamilyType.WEAPON) return Resources.Load<Texture2D>($"Inventory Images/weapons/{this.name.Replace(" ", "_")}");
        else if (this.familyType == ItemFamilyType.FOOD) return Resources.Load<Texture2D>($"Inventory Images/food/{this.name.Replace(" ", "_")}");
        else if (this.familyType == ItemFamilyType.LOOT) return Resources.Load<Texture2D>($"Inventory Images/loot/{this.name.Replace(" ", "_")}");
        else return null;
    }
}
