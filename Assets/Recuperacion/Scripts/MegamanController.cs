using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
    private Rigidbody2D rBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private AudioSource shotingSound;
    [SerializeField] private float impulseForce = 2f;
    public float movementVel = 5f;
    public bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detectar imput
        float movementX = Input.GetAxis("Horizontal");

        // Aplicar velocidad
        Vector2 finalVelocity = new Vector2(movementX * movementVel, rBody.velocity.y);
        rBody.velocity = finalVelocity;

        //Girar sprite si se mueve a la izquierda
        if (movementX < 0)
        {
            spriteRenderer.flipX = true;
        }

        //Sprite normal si gira a la derecha
        else if (movementX > 0)
        {
            spriteRenderer.flipX = false;
        }

        //Animaciones
        animator.SetBool("Moving", finalVelocity.x != 0f);

        if (movementX != 0f && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("RunningShooter");
            shotingSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isGrounded = false;
            animator.SetTrigger("Jump");
            rBody.AddForce(transform.up * impulseForce);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Shooting");
            shotingSound.Play();
        }
    }
}
