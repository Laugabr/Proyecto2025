using UnityEngine;

public class Skater : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right; // Empieza moviéndose a la derecha

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Movimiento
    }

    private void OnTriggerEnter2D(Collider2D other) // Detecta si hubo colisión
    {
        // Si choca con una pared, invierte la dirección
        if (other.CompareTag("Wall"))
        {
            direction *= -1;
        }
    }
}
