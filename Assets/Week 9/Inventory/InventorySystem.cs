using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    List<Item> playerIventory;
    int currentInventoryIndex;
    bool isVisable = false;
    GameObject inventoryText, inventoryDescription, inventoryPanel, inventoryImage;

    // Start is called before the first frame update
    void Start()
    {
        playerIventory = new()
        {
            new Item(Item.ItemType.APPLE),
            new Item(Item.ItemType.MEAT),
        };

        inventoryText = GameObject.Find("InventoryText");
        inventoryImage = GameObject.Find("InventoryImage");
        inventoryDescription = GameObject.Find("InventoryDescription");
        inventoryPanel = GameObject.Find("InventoryPanel");

        foreach (var item in playerIventory)
        {
            Debug.Log(item.ItemInfo());
        }

        isVisable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisable)
        {
            DisplayUI(true);
            Item currentItem = playerIventory[currentInventoryIndex];
            GameObject.Find("InventoryText").GetComponent<TextMeshProUGUI>().text = currentItem.name + "[" + currentItem.nb + "]";
            GameObject.Find("InventoryDescription").GetComponent<TextMeshProUGUI>().text = currentItem.description;
            GameObject.Find("InventoryImage").GetComponent<RawImage>().texture = currentItem.GetTexture();

            if (Input.GetKeyDown(KeyCode.I)) currentInventoryIndex++;

            if (currentInventoryIndex >= playerIventory.Count)
            {
                currentInventoryIndex = 0;
                isVisable = false;
                DisplayUI(false);
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I)) isVisable = true;
        }
    }

    void DisplayUI(bool toggle)
    {
        inventoryText.SetActive(toggle);
        inventoryPanel.SetActive(toggle);
        inventoryImage.SetActive(toggle);
        inventoryDescription.SetActive(toggle);
    }

    public bool UpdateItem(Item.ItemType type, int nbItems)
    {
        bool foundSimilarItem = false;
        for (int i = 0; i < playerIventory.Count; i++)
        {
            if (playerIventory[i].type == type)
            {
                if (playerIventory[i].nb + nbItems <= playerIventory[i].maxNb)
                {
                    playerIventory[i].nb += nbItems;
                    foundSimilarItem = true;
                    break;
                }
                else return false;
            }
        }

        if (!foundSimilarItem)
        {
            playerIventory.Add(new Item(type));
            playerIventory[playerIventory.Count - 1].nb = nbItems;
        }

        return true;
    }
}
