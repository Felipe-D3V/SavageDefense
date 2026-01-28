using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] int life = 1;

    Rigidbody2D rb;
    Transform player;

    bool isTank => CompareTag("TankEnemy");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (isTank)
            life = 3;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    public void TomarDano(int dano = 1)
    {
        life -= dano;

        if (life <= 0)
            Morrer();
    }

    void Morrer()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.1f);

        SpawnManager sm = FindObjectOfType<SpawnManager>();
        if (sm != null)
            sm.InimigoMorreu();
    }
}
