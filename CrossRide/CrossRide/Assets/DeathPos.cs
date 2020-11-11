using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Vector3 startPos;
    public LayerMask whatCanbeClickedOn;

    private NavMeshAgent myAgent;

    Camera cam;


    private void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanbeClickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DeathPos"))
        {
            transform.position = startPos;
            myAgent.enabled = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DeathPos"))
        {
            myAgent.enabled = true;
        }
    }
}
/*public class DeathPos : MonoBehaviour
{
    public Vector3 tr;
    public LayerMask whatCanbeClickedOn;

    private UnityEngine.AI.NavMeshAgent myAgent;

    Camera cam;


    private void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanbeClickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            transform.position = tr;
            myAgent.enabled = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            myAgent.enabled = true;
        }
    }
}*/
