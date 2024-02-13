using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    float time;
    bool isThrown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2 && !isThrown)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            isThrown = true;
        }
        Destroy(gameObject, 3);
    }
}
