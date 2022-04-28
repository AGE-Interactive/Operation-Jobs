using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isInteracting;
    bool isNearTruck;

    [System.NonSerialized] public GameObject interactableObject;

    CapsuleCollider collider;

    Transform interactingStandingPosition;
    Transform interactingExitPosition;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInputs();

        if(isInteracting)
        {
            collider.enabled = false;
            transform.position = interactableObject.GetComponent<TruckController>().playerPos.position;
        }
        else
        {
            collider.enabled = true;
        }
    }

    void TakeInputs()
    {
        if(isNearTruck && Input.GetKeyDown(KeyCode.E))
        {
            if(isInteracting)
            {
                transform.position = interactableObject.GetComponent<TruckController>().playerExit.position;
            }
            isInteracting = !isInteracting;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Truck")
        {
            interactableObject = other.gameObject;
            isNearTruck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Truck")
        {
            isNearTruck = false;
        }
    }
}
