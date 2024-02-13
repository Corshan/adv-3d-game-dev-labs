using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmbushTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player") GameObject.Find("NPC").GetComponent<ManageNPCWeek2>().PlayerEnteredAmbushArea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
