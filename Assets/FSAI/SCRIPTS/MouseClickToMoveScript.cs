using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseClickToMoveScript : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Camera _mainCamera;

    public GameObject soundProduced;

    private float productionRate = 0;
    
    public Vector3 currentPos;
    public Vector3 ancientPos;

    public Rigidbody rb;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, int.MaxValue))
                _navMeshAgent.SetDestination(hit.point);
        }


        ProduceNoise();
    }

    private void LateUpdate()
    { 
        ancientPos = gameObject.transform.localPosition;
    }

    void ProduceNoise()
    {
        currentPos = gameObject.transform.localPosition;

        Vector3 moveDirection = currentPos -  ancientPos;
        
        productionRate += Time.deltaTime;
        if (productionRate > 1)
        {
            if (moveDirection.magnitude > 0)
            {
                Instantiate(soundProduced, currentPos, gameObject.transform.rotation );
                productionRate = 0;
            }
        }
    }
}
