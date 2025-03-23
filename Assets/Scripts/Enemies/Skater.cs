using UnityEngine;

public class Skater : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Velocidad del skater
    [SerializeField] private Vector2 direction = Vector2.right; // Dirección inicial del skater
    [SerializeField] private float pushDistance = 0.3f; // Distancia que la abuelita será empujada hacia arriba


    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Movimiento
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Si choca con una pared, invierte la dirección
        if (other.gameObject.CompareTag("grass"))
        {
            Debug.Log("Skater chocó con una pared (grass). Cambiando dirección.");
            direction *= new Vector2(-1, 0);
        }

        // Si choca con la abuelita, la empuja hacia arriba
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Skater detectó colisión con el Player (abuelita).");

            // Obtener la posición actual de la abuelita
            Vector3 grandmaPosition = other.gameObject.transform.position;

            // Mover la abuelita hacia arriba en el eje Y
            grandmaPosition.y += pushDistance;

            // Aplicar la nueva posición
            other.gameObject.transform.position = grandmaPosition;

            Debug.Log("Abuelita empujada hacia arriba. Nueva posición: " + grandmaPosition);
        }
    }
}