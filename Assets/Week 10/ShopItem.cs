using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    string name;
    int price, quanity;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        quanity = 0;
        UpdateQuanityLabel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateQuanityLabel()
    {
        transform.Find("ItemQty").GetComponent<TextMeshProUGUI>().text = $"{quanity}";
    }

    public void IncreaseQuanity()
    {
        if (!CanClick()) return;
        quanity++;
        UpdateQuanityLabel();
    }

    public void DecreseQuanity()
    {
        quanity--;
        if (quanity <= 0) quanity = 0;
        UpdateQuanityLabel();
    }

    bool CanClick()
    {
        return true;
    }
}
