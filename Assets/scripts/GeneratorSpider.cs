using UnityEngine;

public class GeneratorSpider : MonoBehaviour
{
    [Header("Prefab del objeto a instanciar")]
    public GameObject prefab;

    [Header("Tiempo entre cada aparición (segundos)")]
    public float tiempoEntreInstancias = 2f;

    [Header("HUD superior reservado (en porcentaje de altura)")]
    [Range(0f, 1f)] public float hudHeightPercent = 0.2f; // 20% de la pantalla superior libre

    private float tiempoTranscurrido = 0f;

    void Update()
    {
        if (GameController.juegoPausado) return;

        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= tiempoEntreInstancias)
        {
            InstanciarObjeto();
            tiempoTranscurrido = 0f;
        }
    }

    void InstanciarObjeto()
    {
        Camera cam = Camera.main;
        if (cam == null || prefab == null) return;

        // Dimensiones visibles de la cámara
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float minX = cam.transform.position.x - halfWidth;
        float maxX = cam.transform.position.x + halfWidth;
        float minY = cam.transform.position.y - halfHeight;
        float maxY = cam.transform.position.y + halfHeight;

        // Ajustar para dejar libre la parte superior (HUD)
        float spawnMaxY = Mathf.Lerp(minY, maxY, 1f - hudHeightPercent);

        // Posición aleatoria dentro del área visible (sin el HUD)
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, spawnMaxY);

        Vector3 posicion = new Vector3(x, y, 0f);
        Instantiate(prefab, posicion, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float minX = cam.transform.position.x - halfWidth;
        float maxX = cam.transform.position.x + halfWidth;
        float minY = cam.transform.position.y - halfHeight;
        float maxY = cam.transform.position.y + halfHeight;

        float spawnMaxY = Mathf.Lerp(minY, maxY, 1f - hudHeightPercent);

        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + spawnMaxY) / 2f, 0f);
        Vector3 size = new Vector3(maxX - minX, spawnMaxY - minY, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}

