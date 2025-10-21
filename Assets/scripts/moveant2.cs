using UnityEngine;

public class moveant2 : MonoBehaviour
{
    [Header("Velocidad del objeto")]
    public float velocidad = 2f;

    [Header("Tiempo entre cambios aleatorios (segundos)")]
    public float tiempoCambioDireccion = 3f;

    [Header("Margen en metros (0.01 = 1 cm)")]
    public float margen = 0.01f;

    private Rigidbody2D rb;
    private Vector2 direccionActual;
    private float temporizador;

    // 👉 Referencias para ocultar/mostrar
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        CambiarDireccion();
    }

    void Update()
    {
        if (GameController.juegoPausado)
        {
            // 🔹 Detener movimiento y rotación
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // 🔹 Desaparecer de la pantalla
            if (spriteRenderer != null) spriteRenderer.enabled = false;
            if (col != null) col.enabled = false;

            return;
        }
        else
        {
            // 🔹 Reaparecer cuando se reanuda
            if (spriteRenderer != null && !spriteRenderer.enabled) spriteRenderer.enabled = true;
            if (col != null && !col.enabled) col.enabled = true;
        }

        // Si está en animación de destrucción, detener movimiento y rotación
        if (animator != null)
        {
            AnimatorStateInfo estadoActual = animator.GetCurrentAnimatorStateInfo(0);
            if (estadoActual.IsName("Hormiga aplastada")) // Cambia al nombre exacto de tu animación
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                return;
            }
        }

        temporizador += Time.deltaTime;
        if (temporizador >= tiempoCambioDireccion)
        {
            CambiarDireccion();
            temporizador = 0f;
        }

        rb.linearVelocity = direccionActual * velocidad;

        // 👉 Rotación adaptada (sprite apunta hacia arriba)
        if (direccionActual != Vector2.zero)
        {
            float angulo = Mathf.Atan2(direccionActual.y, direccionActual.x) * Mathf.Rad2Deg;
            rb.rotation = angulo + 90f;
        }

        // 👉 Limitar dentro de la cámara
        LimitarDentroDeCamara();
    }

    void CambiarDireccion()
    {
        direccionActual = Random.insideUnitCircle.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CambiarDireccion();
        temporizador = 0f;
    }

    void LimitarDentroDeCamara()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // 📏 Aplicamos margen de seguridad
        float minX = cam.transform.position.x - halfWidth + margen;
        float maxX = cam.transform.position.x + halfWidth - margen;
        float minY = cam.transform.position.y - halfHeight + margen;
        float maxY = cam.transform.position.y + halfHeight - margen;

        Vector3 pos = transform.position;

        // Si se sale, corrige la posición y cambia dirección
        bool fuera = false;

        if (pos.x < minX) { pos.x = minX; fuera = true; }
        if (pos.x > maxX) { pos.x = maxX; fuera = true; }
        if (pos.y < minY) { pos.y = minY; fuera = true; }
        if (pos.y > maxY) { pos.y = maxY; fuera = true; }

        if (fuera)
        {
            transform.position = pos;
            CambiarDireccion();
            temporizador = 0f;
        }
    }
}
