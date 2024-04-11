using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlInventoryPlayer : MonoBehaviour
{
    GameObject objectToPickUp;
    bool itemToPickUp = false;
    GameObject userMessage;

    // Start is called before the first frame update
    void Start()
    {
        userMessage = GameObject.Find("userMessage");
        userMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(itemToPickUp){
            if (Input.GetKeyDown(KeyCode.Y)) PickUpObject1();
            if (Input.GetKeyDown(KeyCode.N))
            {
                GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("itemToBeCollected"))
        {
            print("object to be collected");
            objectToPickUp = other.gameObject;
            itemToPickUp = true;
            PickUpObject2();
        }
    }

    void OnTriggerExit(Collider other)
    {
        itemToPickUp = false;
        if (userMessage.active)
        {
            userMessage.SetActive(false);
        }
    }

    void PickUpObject1()
    {
        if (GetComponent<InventorySystem>().UpdateItem(objectToPickUp.GetComponent<ObjectToCollect>().type, 1))
        {
            Destroy(objectToPickUp);
            itemToPickUp = false;
            userMessage.SetActive(false);
        }
        else
        {
            string message = "You can't collect this item, as you have reached maxium";

            GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = message;
        }
    }

    void PickUpObject2()
    {
        string article = objectToPickUp.GetComponent<ObjectToCollect>().item.article;
        string message = $"You just found {article} {objectToPickUp.GetComponent<ObjectToCollect>().item.name}\n Collect (Y/N)?";

        userMessage.SetActive(true);
        GameObject.Find("userMessage").GetComponent<TextMeshProUGUI>().text = message;
    }
}
