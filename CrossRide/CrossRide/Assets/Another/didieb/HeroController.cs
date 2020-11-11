using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public float speed;
    public float distance;

    public Transform target;

    //transform.rotation = agent.transform.rotation;
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
            anim.SetFloat("Speed", speed);
            speed = Mathf.Clamp(speed, 0, 1);

            if (distance > 0.5f)
            {
            
                agent.SetDestination(target.position);
                agent.isStopped = false;
                speed += 2 * Time.deltaTime;
            }
            else if (distance <= 0.5f)
            {
                speed += 2 * Time.deltaTime;

                if (speed <= 0.2f)
                {
                    target = null;
                }
            }
        }
    }
}