using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI; 

public class ClickToMove : MonoBehaviour
{
    [Header ("Control de Movimiento")]
    [SerializeField] private float moveSpeed;
    
    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinationCapsule;
    NavMeshAgent agent;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = destinationCapsule.position;
    }

    
    void Update()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            
            HandleClick();
        }

    }

    private void HandleClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            destinationCapsule.position = hit.point;
            agent.destination = destinationCapsule.position;
        }
        
    }

}
