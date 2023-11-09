using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController2D : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;

            // Defina a posição Y do objeto para a altura desejada acima do chão
            float desiredHeight = transform.position.y + GetComponent<BoxCollider2D>().bounds.extents.y + rb.GetComponent<Collider2D>().bounds.extents.y;
            rb.transform.position = new Vector2(rb.transform.position.x, desiredHeight);
        }
    }
}