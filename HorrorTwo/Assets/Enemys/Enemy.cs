using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("navMesh Settings")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private NavMeshAgent navMesh;

    [SerializeField]
    private Animator animation;

    private string state = "idle";

    private bool isAlive = true;

    [Header("Enemy Settings")]

    [SerializeField]
    private float searchRadius;

    [SerializeField]
    private float waitTime;

    private float wait;

    [SerializeField]
    private Transform triggerZone;

    public void CheckSight()
    {
        if (isAlive == false)
            return;

        RaycastHit hit;
        if (Physics.Linecast(triggerZone.position, player.position, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }

    private void Start()
    {
        navMesh.speed = 1;
        animation.speed = 1;
    }

    private void Update()
    {
        if (isAlive == false)
            return;

        if (state == "idle")
        {
            GoToRandomPoint();
        }
        if (state == "walk")
        {
            CheckDistance();
        }
        if (state == "search")
        {
            Search();
        }
        animation.SetFloat("speed", navMesh.velocity.magnitude);        
    }

    private void Search()
    {
        if (wait <= 0)
        {
            state = "idle";
            return;
        }        
            wait -= Time.deltaTime;
            transform.Rotate(0,120f * Time.deltaTime,0);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, searchRadius);
    }

    private void CheckDistance()
    {    

        if(navMesh.remainingDistance < navMesh.stoppingDistance && navMesh.pathPending == false)
        {
            state = "search";
            wait = waitTime;
        }
        

    }

    private void GoToRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * searchRadius;

        NavMeshHit navHit;

        NavMesh.SamplePosition(transform.position + randomPos, out navHit, searchRadius, NavMesh.AllAreas);

        navMesh.SetDestination(navHit.position);

        state = "walk";
    }
}
