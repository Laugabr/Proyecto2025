using UnityEngine;

public class Skater : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction = Vector2.right; // Empieza movi�ndose a la derecha

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Movimiento
    }

     /*
   private void OnTriggerEnter2D(Collider2D other) // Detecta si hubo colisi�n
    {
        // Si choca con una pared, invierte la direcci�n
        if (other.CompareTag("Wall"))
        {
            direction *= new Vector2(-1,0);
        }
    }
    */

    void OnCollisionEnter2D(Collision2D other)
    {
        // Si choca con una pared, invierte la direcci�n
        if (other.gameObject.CompareTag("grass"))
        {
            direction *= new Vector2(-1,0);
        }
    }

}
