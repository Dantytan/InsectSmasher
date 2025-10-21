using UnityEngine;

public class MoveAnt : MonoBehaviour
{
    private float moveSpeed = 0.7f;
    private float changeDirectionInterval = 3f;

    private Vector2 movementDirection;
    private float timeSinceLastDirectionChange;

    void Start()
    {
        // Inicializa la dirección de movimiento aleatoria
        ChangeDirection();
    }

    void Update()
    {
        // Actualiza el temporizador
        timeSinceLastDirectionChange += Time.deltaTime;

        // Si ha pasado el intervalo de cambio de dirección, cambia la dirección
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
        }

        // Mueve el prefab en la dirección actual
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

    }

    void ChangeDirection()
    {
        // Reinicia el temporizador
        timeSinceLastDirectionChange = 0f;

        // Genera una nueva dirección aleatoria
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
        public void OnTouch()
    {
        // Aquí puedes agregar la lógica para la interacción con el objeto
        Debug.Log("Objeto " + gameObject.name + " tocado!");
        // Por ejemplo, puedes cambiar su color, reproducir una animación, etc.
        GetComponent<Renderer>().material.color = Color.yellow;
    }


}

