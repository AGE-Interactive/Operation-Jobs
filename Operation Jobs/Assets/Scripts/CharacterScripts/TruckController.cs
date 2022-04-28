using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    Rigidbody rb;

    CharacterController player;

    float xPos, zPos;
    [SerializeField] float speed;
    [SerializeField] float turningSpeed;

    Vector3 rotationTarget;
    [SerializeField] float rotationMin, rotationMax;
    [SerializeField] Transform steering;

    Vector3 direction;

    public Transform playerPos;
    public Transform playerExit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isInteracting && player.interactableObject == gameObject)
        {
            TakeInputs();
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (player.isInteracting && player.interactableObject == gameObject)
        {
            if (zPos != 0)
            {
                Move();
            }
        }
    }

    void TakeInputs()
    {
        xPos = Input.GetAxis("Horizontal");
        zPos = Input.GetAxis("Vertical");
    }

    void Move()
    {
        if (zPos >= 0)
        {
            transform.eulerAngles += rotationTarget * Time.fixedDeltaTime;
        }
        else
        {
            transform.eulerAngles -= rotationTarget * Time.fixedDeltaTime;
        }

        direction = zPos * speed * transform.forward;

        rb.velocity = direction * Time.fixedDeltaTime;

    }

    void Rotate()
    {
        rotationTarget += xPos * transform.up * turningSpeed * Time.fixedDeltaTime;
        rotationTarget.y = Mathf.Clamp(rotationTarget.y, rotationMin, rotationMax);

        steering.localEulerAngles = rotationTarget;
    }
}
