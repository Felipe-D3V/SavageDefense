using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;
    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    Collider2D col;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }
    }
    void Start()
    {
        col = attackHitbox.GetComponent<Collider2D>();
        col.enabled = false;
    }

    IEnumerator Attack()
    {
        canAttack = false;

        col.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        col.enabled = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}