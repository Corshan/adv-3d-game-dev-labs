using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour
{
    private GameObject[] teamMembers;
    int nbTeamMembers, nbTargets;
    int wpIndex = 0;
    public GameObject[] wps;
    private float patrolTimer = 0.0f;
    private Animator anim;
    private AnimatorStateInfo info;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Player")
        {
            teamMembers = GameObject.FindGameObjectsWithTag("TeamMember");
        }
        else
        {
            teamMembers = GameObject.FindGameObjectsWithTag("Team2Member");
        }

        nbTeamMembers = teamMembers.Length;
        
        anim = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                Retreat();
            }
        }
        else
        {
            patrolTimer += Time.deltaTime;
            info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("Idle"))
            {
                if (patrolTimer >= 5)
                {
                    patrolTimer = 0;
                    anim.SetTrigger("startPatrol");
                }
            }

            if (info.IsName("Patrol"))
            {
                if (patrolTimer >= 4)
                {
                    DetectEnemies();
                    patrolTimer = 0;
                }

                if (Vector3.Distance(gameObject.transform.position, wps[wpIndex].transform.position) < 1.0f)
                {
                    wpIndex++;
                    if (wpIndex >= wps.Length)
                    {
                        wpIndex = 0;
                    }
                }

                agent.SetDestination(wps[wpIndex].transform.position);
                agent.isStopped = false;
            }
            
            if (info.IsName("Attack"))
            {
                agent.isStopped = true;
            }
        }
    }

    void DetectEnemies()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) < 10)
        {
            anim.SetTrigger("closeToEnemy");
        }
    }

    public void Attack()
    {
        GameObject[] allTargets;

        if (gameObject.name == "Leader")
        {
            allTargets = GameObject.FindGameObjectsWithTag("TeamMember");
        }
        else
        {
            allTargets = GameObject.FindGameObjectsWithTag("Team2Member");
        }

        nbTargets = allTargets.Length;
        for(int i = 0; i < nbTeamMembers; i++)
        {
            //NB this wll break if there are more team members than targets
            teamMembers[i].GetComponent<TeamMember>().Attack(allTargets[i]);
        }
    }
    
    void Retreat()
    {
        foreach (var teamMember in teamMembers)
        {
            teamMember.GetComponent<TeamMember>().Retreat();
        }
    }
}
