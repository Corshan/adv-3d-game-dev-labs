using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShopItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void IncreaseQuanity()
    {
        transform.parent.GetComponent<ShopItem>().IncreaseQuanity();
    }

    public void DecreseQuanity()
    {
        transform.parent.GetComponent<ShopItem>().DecreseQuanity();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
