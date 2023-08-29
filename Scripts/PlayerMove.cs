using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    // Rigidbody 2D
    Rigidbody2D rb2D;

    // Velocidad de correr (horizontal)
    public float runSpeed = 2;

    // Velocidad de salto (vertical)
    public float jumpSpeed = 3;

    // Detecta si el salto debe depender de
    // cuánto presione el jugador la barra espaciadora
    public bool betterJump = false;

    // Cantidad de fuerza multiplicadora de la caída
    public float fallMultiplier = 0.5f;

    // Cantidad de fuerza multiplicadora del salto
    public float lowJumpMultiplier = 1f;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Si el jugador presiona las teclas establecidas, el personaje se mueve
    void FixedUpdate() {
        if ((Input.GetKey("d")) || Input.GetKey("right")) {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if ((Input.GetKey("a")) || Input.GetKey("left")) {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }

        // Si presiona espacio o la tecla arriba, el personaje salta
        if (Input.GetKey("space") && CheckGround.isGrounded) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }

        if (betterJump) {
            if (rb2D.velocity.y < 0) {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }

            if (rb2D.velocity.y > 0 && !Input.GetKey("space")) {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

        // Si no está en el suelo, mostrará animación de salto (Jump)
        if (CheckGround.isGrounded == false) {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        else {
            // De lo contrario, no se muestra
            animator.SetBool("Jump", false);
        }
    }
}
