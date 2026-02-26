using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;
    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    Collider2D col;
    Player1Move playerMove;
    public Animator animator;

    void Start()
    {
        col = attackHitbox.GetComponent<Collider2D>();
        col.enabled = false;

        playerMove = GetComponent<Player1Move>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        animator.SetBool("atacando", true);

        col.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        col.enabled = false;

        animator.SetBool("atacando", false);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

}