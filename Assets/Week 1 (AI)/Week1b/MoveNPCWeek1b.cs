using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNPCWeek1b : MonoBehaviour
{
    Animator anim;
    AnimatorStateInfo info;
    public GameObject player;
    float distance;
    GameObject[] allBCs;
    Ray ray;
    RaycastHit hit;
    float castingDistance;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Follow the player")){
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
        if(info.IsName("idle")){
            GetComponent<NavMeshAgent>().isStopped = true;
            // GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }

        // Listen();
        // SmellBreadCrumb();
        // Look();
        Look2();
    }

    void Listen(){
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 3) anim.SetBool("canHearPlayer", true);
        else anim.SetBool("canHearPlayer", false);
    }

    void SmellBreadCrumb(){

        allBCs = GameObject.FindGameObjectsWithTag("BC");
        float minDistance = 2;
        bool detectedBC = false;

        for(int i = 0; i < allBCs.Length-1; i++){
            if(Vector3.Distance(transform.position, allBCs[i].transform.position) < minDistance){
                detectedBC = true;
                break;
            }
        }

        if(detectedBC) anim.SetBool("canSmellPlayer", true);
        else anim.SetBool("canSmellPlayer", false);
    }

    void Look(){

        ray = new Ray();
        ray.origin = transform.position + Vector3.up * 0.7f;
        string objectInSight = "";
        castingDistance = 20;
        ray.direction = transform.forward * castingDistance;

        Debug.DrawRay(ray.origin, ray.direction* castingDistance, Color.red);
   
        if(Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance)){
            objectInSight = hit.collider.gameObject.name;
            if(objectInSight == "Player") anim.SetBool("canSeePlayer", true);
            else anim.SetBool("canSeePlayer", false);
        }
    }

    void Look2(){
        direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
        bool isInFieldOfView = (Vector3.Dot(transform.forward.normalized, direction) > 0.7f);

        Debug.DrawRay(transform.position, direction* 100, Color.green);
        Debug.DrawRay(transform.position, transform.forward* 100, Color.blue);
        
        Debug.DrawRay(transform.position, (transform.forward - transform.right)* 100, Color.red);
        Debug.DrawRay(transform.position, (transform.forward + transform.right)* 100, Color.red);

        Ray ray = new Ray();
        RaycastHit hit;

        ray.origin = transform.position + Vector3.up * 0.7f;
        string objectInSight = "";

        castingDistance = 20;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);

        if(Physics.Raycast(ray.origin, direction, out hit, castingDistance)){
            if(objectInSight == "player" || isInFieldOfView) anim.SetBool("canSeePlayer", true);
            else anim.SetBool("canSeePlayer", false);
        }
    }
}
