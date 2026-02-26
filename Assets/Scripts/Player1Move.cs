using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Move : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public Rigidbody2D rb;

    [Header("Dash")]
    public float dashForca = 15f;
    public float dashDuracao = 0.15f;
    public float dashCooldown = 1f;

    [Header("Visual Dash")]
    public TrailRenderer dashTrail;

    [Header("Vida")]
    public int vida = 3;
    public Text vidaText;

    [Header("Invencibilidade")]
    public float tempoInvencivel = 0.8f;
    bool invencivel;

    public AudioClip Damage;
    public AudioClip DashSound;
    public GameObject attackHitbox;


    Vector2 movimento;
    Vector2 direcaoDash;

    bool estaDashando;
    float dashTempo;
    float dashCooldownTimer;

    int playerLayer;
    int enemyLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");

        dashTrail.emitting = false;

        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D attackCollider = attackHitbox.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, attackCollider);
    }

    void Update()
    {
        movimento = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (movimento != Vector2.zero)
            direcaoDash = movimento.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !estaDashando && dashCooldownTimer <= 0)
        {
            AudioSource.PlayClipAtPoint(DashSound, transform.position);
            IniciarDash();
        }

        mostrarVida();

        if (movimento != Vector2.zero && !estaDashando)
        {
            CameraShake.Instance.Shake(0.05f, 0.05f);
        }
    }
    void mostrarVida()
    {
        vidaText.text = vida.ToString();
    }

    void FixedUpdate()
    {
        if (estaDashando)
        {
            rb.velocity = direcaoDash * dashForca;
            dashTempo -= Time.fixedDeltaTime;

            if (dashTempo <= 0)
                FinalizarDash();
        }
        else
        {
            rb.velocity = movimento.normalized * velocidade;
        }

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.fixedDeltaTime;
    }

    void IniciarDash()
    {
        estaDashando = true;
        dashTempo = dashDuracao;
        dashCooldownTimer = dashCooldown;

        dashTrail.emitting = true;

        CameraShake.Instance.Shake(0.15f, 0.2f);

        //  Imortal apenas contra Enemy
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
    }

    void FinalizarDash()
    {
        estaDashando = false;
        dashTrail.emitting = false;

        if (!invencivel)
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }


    public void perderVida()
    {
        if (invencivel) return;

        invencivel = true; 

        vida--;

        if (vida <= 0)
        {
            Destroy(gameObject, 1f);
            return;
        }

        StartCoroutine(Invencibilidade());
    }


    IEnumerator Invencibilidade()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        float tempo = 0;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while (tempo < tempoInvencivel)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            tempo += 0.1f;
        }

        sr.enabled = true;

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        invencivel = false;
    }


}
