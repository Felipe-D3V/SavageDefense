using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer gunSprite;
    public GameObject Bullet;
    public Transform SpawnBullet;
    public AudioSource BulletSound;
    public AudioClip ReloadSound;
    public Text balaText;
     


    [Header("Offsets")]
    [SerializeField] Vector3 rightOffset = new Vector3(0.5f, 0f, 0f);
    [SerializeField] Vector3 leftOffset = new Vector3(-0.5f, 0f, 0f);
    [SerializeField] float flipDeadzone = 0.2f;


    [Header("Gun Stats")]
    public int maxBalas = 15;
    public float fireCooldown = 0.2f;
    public float reloadTime = 1.2f;

    int balasAtuais;
    bool recarregando = false;
    float cooldownTimer;

    float lastDirX = 1;

    void Start()
    {
        balasAtuais = maxBalas;
    }

    void Update()
    {
        Aim();
        CooldownTick();
        Atirar();
        ReloadInput();
    }

    void LateUpdate()
    {
        if (recarregando)
        {
            balaText.text = "Reloading...";
        }
        else
        {
            balaText.text = balasAtuais + "/" + maxBalas;
        }
    }

    private void CooldownTick()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    private void Aim()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2 dir = mouseWorldPos - transform.parent.position;

        // Atualiza o lado só se sair da deadzone
        if (Mathf.Abs(dir.x) > flipDeadzone)
            lastDirX = Mathf.Sign(dir.x);

        gunSprite.flipY = lastDirX < 0;

        Vector3 targetOffset = lastDirX > 0 ? rightOffset : leftOffset;
        transform.localPosition = targetOffset;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void Atirar()
    {
        if (recarregando) return;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0 && balasAtuais > 0)
        {
            Instantiate(Bullet, SpawnBullet.position, transform.rotation);
            BulletSound.Play();

            balasAtuais--;
            cooldownTimer = fireCooldown;
        }
    }

    private void ReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && !recarregando && balasAtuais < maxBalas)
        {
            AudioSource.PlayClipAtPoint(ReloadSound, transform.position);
            StartCoroutine(Recarregar());
        }
    }

    private System.Collections.IEnumerator Recarregar()
    {
        recarregando = true;
        recarregando = true;
        yield return new WaitForSeconds(reloadTime);
        balasAtuais = maxBalas;
        recarregando = false;
    }
}
