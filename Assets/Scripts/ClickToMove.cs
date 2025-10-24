using System;
using System.Collections;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [Header ("Control de Movimiento")]
    [SerializeField] private float moveSpeed;
    
    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinationCapsule;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        destination = destinationCapsule.position;
        if (Input.GetMouseButtonDown(1))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        StartCoroutine(MoveToPosition(destination));
    }

    IEnumerator MoveToPosition (Vector3 _destination)
    {
        Vector3 moveDirection = _destination - transform.position;
        moveDirection = moveDirection.normalized;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
        yield return null;
    }
}
