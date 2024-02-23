using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool isGrounded;
    [SerializeField] private float empuje = 2f;
    public GameObject marioPeque, MarioGrande;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Cogemos el input
        float movX = Input.GetAxis("Horizontal");
        //float movY = Input.GetAxis("Vertical");

        // Aplicamos la velocidad
        Vector2 velocidadFinal = new Vector2(movX * velocidad, rbody.velocity.y);
        //rb.velocity.x = 
        rbody.velocity = velocidadFinal;

        // Si nos movemos hacia la izquierda, espejo el sprite.
        if (movX < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Lo pongo normal si miramos hacia la derecha
        else if (movX > 0)
        {
            spriteRenderer.flipX = false;
        }

        // TODO: ESTABLECER LAS ANIMACIONES
        //animator.SetFloat("MovY", velocidadFinal.y);
        animator.SetBool("MovX", velocidadFinal.x != 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jumping");
            rbody.AddForce(transform.up * empuje);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("grounded", isGrounded);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            animator.SetTrigger("Death");

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PowerUp"))
        {
            marioPeque.gameObject.SetActive(false);
            MarioGrande.gameObject.SetActive(true);
        }
    }
}
