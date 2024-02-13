using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }
    
    public int GetHealth()
    {
        return health;
    }
    
    public void SetHealth(int newHealth)
    {
        health = newHealth;
        
        if (health <= 0)
        {
            DestroyNPC();
        }
    }
    
    public void DecreaseHealth(int damage)
    {
        SetHealth(health - damage);
    }

    public void HitByOpponent(GameObject obj, int damage)
    {
        SetHealth(health - damage);
        GetComponent<TeamMember>().Attack(obj);
    }
    
    public void DestroyNPC()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
