using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] AudioClip hitClip;

    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        if (collision.CompareTag("Bullet")) return;

        EnemyLogic enemy = collision.GetComponent<EnemyLogic>();

        if (enemy != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
            enemy.TomarDano(1);

            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

