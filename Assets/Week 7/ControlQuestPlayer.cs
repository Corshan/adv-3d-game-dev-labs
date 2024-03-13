using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlQuestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        print("collied with" + collision.gameObject.name);
        if(collision.collider.gameObject.tag == "collect"){
            Destroy(collision.collider.gameObject);
            GameObject.Find("Manager").GetComponent<QuestSystem>().Notify(QuestSystem.PossibleActions.aquire, collision.gameObject.name);
        }

        if(collision.collider.gameObject.tag == "dialogue"){
            // Destroy(collision.collider.gameObject);
            GameObject.Find("Manager").GetComponent<QuestSystem>().Notify(QuestSystem.PossibleActions.talk_to, collision.gameObject.name);
        }

        if(collision.collider.gameObject.tag == "gaurd"){
            Destroy(collision.collider.gameObject);
            GameObject.Find("Manager").GetComponent<QuestSystem>().Notify(QuestSystem.PossibleActions.destroy_one, collision.gameObject.name);
        }
    }
}
