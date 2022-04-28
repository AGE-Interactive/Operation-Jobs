using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Rigidbody rb;

    float mouseX, mouseY;

    [SerializeField] float minX, maxX;

    Vector3 mousePosition;

    [SerializeField] float mouseSensibility;

    CharacterController player;

    [SerializeField] Vector3 focusPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInputs();
        RotateCamera();
        MoveCamera();
    }

    void TakeInputs()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    void RotateCamera()
    {
        mousePosition += (mouseY * Vector3.right + mouseX * Vector3.up) * mouseSensibility * Time.deltaTime;
        mousePosition.x = Mathf.Clamp(mousePosition.x, minX, maxX);

        transform.eulerAngles = mousePosition;
    }

    void MoveCamera()
    {
        Vector3 target;
        if (!player.isInteracting)
        {
            target = player.transform.position + focusPoint;
        }
        else
        {
            target = player.interactableObject.transform.position + focusPoint + Vector3.up * 2f ;
        }

        transform.position = target;
    }
}
