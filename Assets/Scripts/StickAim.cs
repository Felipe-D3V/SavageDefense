using UnityEngine;

public class StickMeleeAim : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer stickSprite;

    [Header("Offsets")]
    [SerializeField] Vector3 rightOffset = new Vector3(0.5f, 0f, 0f);
    [SerializeField] Vector3 leftOffset = new Vector3(-0.5f, 0f, 0f);
    [SerializeField] float flipDeadzone = 0.2f;

    float lastDirX = 1f;
    Transform player;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        player = transform.parent;
    }

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2 dir = mouseWorldPos - player.position;

        // Atualiza o lado só se sair da deadzone
        if (Mathf.Abs(dir.x) > flipDeadzone)
            lastDirX = Mathf.Sign(dir.x);

        stickSprite.flipY = lastDirX < 0;

        Vector3 targetOffset = lastDirX > 0 ? rightOffset : leftOffset;
        transform.localPosition = targetOffset;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}