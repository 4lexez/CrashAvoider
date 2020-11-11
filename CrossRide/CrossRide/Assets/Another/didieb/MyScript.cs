using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyScript : MonoBehaviour
{
	public NavMeshAgent agent;
	
	Animator anim;
    public float distance;
    public Vector3 point;
    Camera cam;
    private float rotationSpeed;

    private void Start()
	{
        cam = GameObject.Find("Camera").GetComponent<Camera>(); 
        agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
        point = transform.position;
        agent.updatePosition = false;
    }

    private void Update()
    {
        agent.nextPosition = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                agent.SetDestination(hit.point);
                point = hit.point;
            }

        }
        distance = Vector3.Distance(transform.position, point);
        anim.SetFloat("Speed", distance);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.updateRotation = false;
            transform.rotation = agent.transform.rotation;
        }
        else
        {
            agent.updateRotation = true;
        }
    }
}
