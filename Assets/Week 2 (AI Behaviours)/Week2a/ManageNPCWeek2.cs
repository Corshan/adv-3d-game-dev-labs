using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManageNPCWeek2 : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo info;
    [Range(0, 100)]
    public int health;
    float healthTimer = 0;
    GameObject[] healthPacks;
    GameObject target;
    float distanceToClosestPack;
    int rankOfClosestPack;
    float distanceToCurrentPack;
    Vector3 startPos;
    NavMeshAgent navMeshAgent;
    public enum Type { Flee, Ambush }
    public Type npcType;
    public GameObject player;
    float minimumDistanceBetweenPlayerAndNpc = 20;
    Vector3 towardsPlayer;
    public GameObject ambushStart;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        SetHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);

        healthTimer += Time.deltaTime;

        if (healthTimer > 2)
        {
            healthTimer = 0;
            SetHealth(health - 2);
        }

        if (info.IsName("GoToAmbushSpot"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            target = ambushStart;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            GetComponent<NavMeshAgent>().speed = 5.5f;
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < 2.5f)
            {
                anim.SetTrigger("reachedAmbush");
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 3.5f;
        }

        if (info.IsName("backToStartingPoint"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().SetDestination(startPos);

            if (Vector3.Distance(transform.position, startPos) < 1) anim.SetTrigger("reachedStartingPoint");
        }

        if (info.IsName("Look For Health Pack"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            // GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("HealthPack").transform.position);

            healthPacks = GameObject.FindGameObjectsWithTag("healthPack");
            if (healthPacks.Length == 0)
            {
                anim.SetBool("healthPackAvailable", false);
            }
            else
            {
                anim.SetBool("healthPackAvailable", true);
                // SelectHealthPack();
                SelectHealthPackByPath();
            }

            if (target != null)
            {

                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
                {
                    SetHealth(100);
                    Destroy(target);
                }
            }
        }

        switch (npcType)
        {
            case Type.Flee:
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < minimumDistanceBetweenPlayerAndNpc)
                    {
                        anim.SetTrigger("startToFlee");
                        towardsPlayer = (player.transform.position - transform.position).normalized;
                        if (target == null) target = new GameObject();
                        target.transform.position = transform.position - towardsPlayer * minimumDistanceBetweenPlayerAndNpc;
                        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                        GetComponent<NavMeshAgent>().isStopped = false;
                    }
                    break;
                }
            case Type.Ambush:
                {
                    break;
                }
        }
    }

    void SetHealth(int newHealth)
    {
        health = newHealth;
        anim.SetInteger("health", health);
    }

    void SelectHealthPack()
    {
        distanceToClosestPack = float.MaxValue;
        for (int i = 0; i < healthPacks.Length - 1; i++)
        {
            distanceToCurrentPack = Vector3.Distance(transform.position, healthPacks[i].transform.position);
            if (distanceToCurrentPack < distanceToClosestPack)
            {
                distanceToClosestPack = distanceToCurrentPack;
                rankOfClosestPack = i;
            }
        }

        target = healthPacks[rankOfClosestPack];
    }

    void SelectHealthPackByPath()
    {
        distanceToClosestPack = 1000;
        for (int i = 0; i < healthPacks.Length; i++)
        {
            // GetComponent<NavMeshAgent>().SetDestination(healthPacks[i].transform.position);
            // distanceToCurrentPack = GetComponent<NavMeshAgent>().remainingDistance;

            NavMeshPath path = new NavMeshPath();
            navMeshAgent.CalculatePath(healthPacks[i].transform.position, path);
            distanceToCurrentPack = CalculatePathByCorners(path.corners);

            // Debug.Log(i + " " + distanceToCurrentPack);
            // distanceToCurrentPack = Vector3.Distance(transform.position, healthPacks[i].transform.position);

            if (distanceToCurrentPack < distanceToClosestPack)
            {
                distanceToClosestPack = distanceToCurrentPack;
                rankOfClosestPack = i;
            }
        }

        target = healthPacks[rankOfClosestPack];
    }

    float CalculatePathByCorners(Vector3[] corners)
    {
        float totalDistance = 0;

        for (int i = 1; i < corners.Length; i++)
        {
            totalDistance += Vector3.Distance(corners[i - 1], corners[i]);
        }

        return totalDistance;
    }

    public void PlayerEnteredAmbushArea()
    {
        anim.SetTrigger("goToAmbush");
    }
}
