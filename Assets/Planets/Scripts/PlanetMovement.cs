using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlanetObject currentPlanet;

    bool isGrounded = false;

    Vector2 normal;
    Vector2 velocity;
    float verticalVelo = 0;
    float horizontalVelo = 0;
    Vector2 input;

    float rotationStart;

    [SerializeField] float jumpForce = 1f;
    [SerializeField] float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity = rb.velocity;

        if (currentPlanet != null)
            planetMovement();

        rb.velocity = velocity;
    }

    void planetMovement()
    {
        normal = (transform.position - currentPlanet.transform.position).normalized;

        horizontalVelo = input.x * moveSpeed;
        verticalVelo = isGrounded ? -0.5f : verticalVelo + currentPlanet.gravity * Time.deltaTime;

        if (normal != (Vector2)transform.up)
            rotation();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelo = Mathf.Sqrt(jumpForce * -2 * currentPlanet.gravity);
            isGrounded = false;
        }

        velocity = (Vector3)normal * verticalVelo + transform.right * horizontalVelo;
    }

    void rotation()
    {
        float fracComplete = (Time.time - rotationStart) / 3f;
        transform.up = Vector3.Slerp(transform.up, normal, fracComplete);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = true;
        else
        {
            IPlanet i = collision.GetComponent<IPlanet>();
            if (i != null)
            {
                currentPlanet = i.OnPlanet();
                rotationStart = Time.time;
                if (verticalVelo > 0)
                    verticalVelo = -verticalVelo;
            }    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlanet i = collision.GetComponent<IPlanet>();
        if (i != null)
            currentPlanet = null;
    }
}
