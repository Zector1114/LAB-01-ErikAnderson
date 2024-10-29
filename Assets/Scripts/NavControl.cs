using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    bool isWalking = true;
    public GameObject targetFacing;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            agent.destination = target.transform.position;
            FaceTarget();
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (targetFacing.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ATTACK")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ATTACK")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }
}
