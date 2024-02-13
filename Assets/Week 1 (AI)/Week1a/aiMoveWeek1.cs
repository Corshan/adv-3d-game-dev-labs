using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiMoveWeek1 : MonoBehaviour
{
    public GameObject target;
    public GameObject[] waypoints;
    public enum Type {follow, path, randomPath, wandering};
    GameObject wanderingTarget;
    public Type npcType;
    int WpCount = 0;
    float timer;
    Animator anim;
    AnimatorStateInfo info;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        for(int i = 0; i < waypoints.Length; i++){
            waypoints[i].GetComponent<Renderer>().enabled = false;
        }

        wanderingTarget = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        if(GetComponent<Animator>() != null){
            info = anim.GetCurrentAnimatorStateInfo(0);
        }
        switch (npcType){
            case Type.follow:{
                target = GameObject.Find("Target");
                if(GetComponent<NavMeshAgent>() != null) GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

                if(Vector3.Distance(transform.position, target.transform.position) < 1) anim.SetTrigger("stopWalking");
                else anim.SetTrigger("startWalking");
                break;
            }
            case Type.path:{
                target = waypoints[WpCount];
                if(Vector3.Distance(transform.position, target.transform.position) < 1){
                    WpCount++;
                    if (WpCount > waypoints.Length-1) WpCount = 0;
                }
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            case Type.randomPath: {
                if(Vector3.Distance(transform.position, target.transform.position) < 1){
                    int prev = WpCount;
                    int random = 0;
                    do{
                        random = Random.Range(0, waypoints.Length-1);
                    }while(random == prev);

                    WpCount = random;
                    target = waypoints[WpCount];
                }
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            case Type.wandering: {
                timer += Time.deltaTime;
                if(timer > 4) {
                    timer = 0;
                    RaycastHit hit;
                    Ray ray = new Ray();
                    ray.origin = transform.position + Vector3.up*0.7f;
                    float distanceToOb = 0;
                    float castingDistance = 20;
                    do{
                        float randomdirX = Random.Range(-0.5f, 0.5f);
                        float randomdirZ = Random.Range(-0.5f, 0.5f);

                        ray.direction = transform.forward*randomdirZ + transform.right*randomdirX;
                        Debug.DrawRay(ray.origin, ray.direction, Color.red);
                        
                        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance)){
                            distanceToOb = hit.distance;   
                        }else distanceToOb = castingDistance;
                        wanderingTarget.transform.position = ray.origin + ray.direction * (distanceToOb -4);
                        target = wanderingTarget;
                    }while(distanceToOb < 1.0f);
                }

                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                break;
            }
            default:break;
        }
    }
}
