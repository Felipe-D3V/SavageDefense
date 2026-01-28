using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    public Vector2 respawnPosition = Vector2.zero; // meio do mapa

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Player1Move player = collision.GetComponent<Player1Move>();

        if (player != null)
        {
            player.perderVida();
        }

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.position = respawnPosition;
    }
}
