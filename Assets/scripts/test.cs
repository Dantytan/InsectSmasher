using UnityEngine;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    private Camera camara;

    // Guardamos el número de toques por objeto Spider
    private Dictionary<GameObject, int> toquesPorObjeto = new Dictionary<GameObject, int>();

    [Header("🎵 Sonido al destruir objetos")]
    public AudioClip sonidoDestruccion;

    void Start()
    {
        camara = Camera.main;
    }

    void Update()
    {
        if (GameController.juegoPausado) 
            return;

        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                VerificarToque(toque.position);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            VerificarToque(Input.mousePosition);
        }
    }

    void VerificarToque(Vector2 tocoPantalla)
    {
        Vector2 posicionMundo = camara.ScreenToWorldPoint(tocoPantalla);
        RaycastHit2D hit = Physics2D.Raycast(posicionMundo, Vector2.zero);

        if (hit.collider == null) return;

        GameObject objetoTocado = hit.collider.gameObject;

        if (objetoTocado.CompareTag("Destructible"))
        {
            objetoTocado.tag = "Untagged"; // 🚫 evita volver a entrar
            Contador.objetosDestruidos++;
            StartCoroutine(ReproducirAnimacionYDestruir(objetoTocado, "Destruir"));
        }
        else if (objetoTocado.CompareTag("Spider"))
        {
            if (!toquesPorObjeto.ContainsKey(objetoTocado))
                toquesPorObjeto[objetoTocado] = 0;

            toquesPorObjeto[objetoTocado]++;

            if (toquesPorObjeto[objetoTocado] >= 2)
            {
                objetoTocado.tag = "Untagged"; // 🚫 evita volver a entrar
                Contador.objetosDestruidos += 5;
                StartCoroutine(ReproducirAnimacionYDestruir(objetoTocado, "SpiderOut"));
                toquesPorObjeto.Remove(objetoTocado);
            }
        }
    }

    System.Collections.IEnumerator ReproducirAnimacionYDestruir(GameObject objeto, string trigger)
    {
        Animator animator = objeto.GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger(trigger);
        }

        // 🎵 Reproducir sonido de destrucción usando AudioManager
        if (AudioManager.Instance != null && sonidoDestruccion != null)
        {
            AudioManager.Instance.PlayEfecto(sonidoDestruccion);
        }

        yield return new WaitForSeconds(1f);

        Destroy(objeto);
    }
}





