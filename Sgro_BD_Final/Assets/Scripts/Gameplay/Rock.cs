using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float turnSpeed = 25f;
    [SerializeField] float accelerationSpeed = 25f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float groundCheckDistance = 6f;
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] float timeForParticles = .25f;
    [SerializeField] ParticleSystem jumpParticles = null;

    Rigidbody rb;
    float horizontal = 0;
    float vertical = 0;
    bool inputLocked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (inputLocked) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
            {
                rb.AddForce(Vector3.up * jumpForce);
                StartCoroutine(ParticlesAnimation());
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Camera.main.transform.right * horizontal * turnSpeed);
        rb.AddForce(Camera.main.transform.forward * vertical * accelerationSpeed);
    }

    IEnumerator ParticlesAnimation()
    {
        yield return new WaitForSeconds(timeForParticles);
        jumpParticles.Play();
    }

    public void StopMovement()
    {
        inputLocked = true;
        rb.velocity = Vector3.zero;
        horizontal = 0;
        vertical = 0;
    }

    public void StartMovement()
    {
        inputLocked = false;
        rb.velocity = Vector3.zero;
    }

}
