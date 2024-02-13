using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamMember : MonoBehaviour
{
    private GameObject leader;
    private Animator anim;
    private AnimatorStateInfo info; 
    private float distanceToLeader, distanceToTarget;
    private NavMeshAgent agent;
    private GameObject target;
    private int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // leader = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        if (gameObject.tag == "TeamMember")
        {
            leader = GameObject.Find("Player");
        }
        else
        {
            leader = GameObject.Find("Leader");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        print(GetComponent<NavMeshAgent>().destination);
        
        info = anim.GetCurrentAnimatorStateInfo(0);
        
        distanceToLeader = Vector3.Distance(transform.position, leader.transform.position);
        if (distanceToLeader < 5)
        {
            anim.SetBool("closeToLeader", true);
        }
        else
        {
            anim.SetBool("closeToLeader", false);
        }
        
        if (info.IsName("Idle"))
        {
            agent.isStopped = true;
        }
        
        if(info.IsName("MoveTowardsLeader"))
        {
            agent.isStopped = false;
            agent.SetDestination(leader.transform.position);
        }
        if(info.IsName("GoToTarget"))
        {
            if (target != null)
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget < 2.25f)
                {
                    anim.SetBool("closeToTarget", true);
                    agent.isStopped = true;
                }
                else
                {
                    anim.SetBool("closeToTarget", false);
                }
            }
            else
            {
                anim.SetBool("targetDestroyed", true);
            }
        }
        
        if(info.IsName("AttackTarget"))
        {
            if (target != null)
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                if(info.normalizedTime %1f >= .98f)
                {
                    if (gameObject.CompareTag("Team2Member"))
                    {
                        damage = 10;
                    }
                    else
                    {
                        damage = 20;
                    }
                    
                    target.GetComponent<NPC>().HitByOpponent(gameObject, damage);
                }
            }
            else
            {
                anim.SetBool("targetDestroyed", true);
            }
        }
    }
    
    public void Attack(GameObject t)
    {
        target = t;
        // anim.SetTrigger("attackOneToOne");
        anim.SetTrigger("respondToAttack");
    }
    
    public void Retreat()
    {
        anim.SetTrigger("retreat");
    }
}
