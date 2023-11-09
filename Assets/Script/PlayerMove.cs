using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMove : MonoBehaviour
{
    public float velocidadeNochao = 5.0f; // Velocidade no chão
    public float velocidadeNoAr = 100.0f; // Velocidade no ar
    public float forcaPulo = 10.0f; // Força do pulo
    public float desaceleracao = 15.0f;
    private bool nochao = false;
    private Rigidbody2D rb;
    public Animator animator;
 
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
 
    void Update()
 
    {
        // Captura as entradas do teclado
        float movimentoHorizontal = Input.GetAxis("Horizontal");
 
        // Calcula o vetor de movimento com base nas entradas
        Vector2 movimento = new Vector2(movimentoHorizontal, rb.velocity.y);
 
        // Verifica se o personagem está no chão
        if (nochao)
        {
            // Aplica a velocidade no chão
            rb.velocity = new Vector2(movimento.x * velocidadeNochao, rb.velocity.y);
        }
        else
        {
            // Aplica a velocidade no ar
            rb.velocity = new Vector2(movimento.x * velocidadeNoAr, rb.velocity.y);
        }
        if (movimentoHorizontal == 0 && nochao)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, Time.deltaTime * desaceleracao), rb.velocity.y);
        }
 
        // Verifica se o personagem está no chão e permite o pulo
        if (nochao && Input.GetButtonDown("Jump"))
        {
            // Aplica uma força vertical para o pulo
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            nochao = false;
        }
 
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Correndo", true);
            if (Input.GetKeyDown(KeyCode.D))
            {
                // Ativa a animação "new animation"
                animator.SetTrigger("New Animation");
            }
        }
        else
        {
            animator.SetBool("Correndo", false);
        }

        
    }
 
    // Detecta se o personagem tocou o chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            nochao = true;
        }
    }
 
    // Detecta se o personagem saiu do chão
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            nochao = false;
        }
    }
}