using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controlmove : MonoBehaviour
{
    CharacterController controller;
    public Transform goal;
    private NavMeshAgent agent;

    void Start()
    {
        // controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

    }

    void Update()
    {
        //Move();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(-h, 0, -v);
        controller.SimpleMove(dir);
    }
}

