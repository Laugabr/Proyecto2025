using UnityEngine;
using StatePattern; // Agrega el namespace del EnemyAI

public class AttackCollision : MonoBehaviour
{

        public GameObject hitImpactPrefab; // Prefab del efecto de impacto

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) // Asegúrate de que los enemigos tengan esta tag
            {
                // Encuentra el punto exacto del impacto dentro del collider del enemigo
                Vector2 hitPoint = other.ClosestPoint(transform.position);

                // Instancia el efecto en ese punto
                Instantiate(hitImpactPrefab, hitPoint, Quaternion.identity);

            
            EnemyAI enemy = other.GetComponent<EnemyAI>(); // Obtiene el script EnemyAI
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(); // Llama a la función de daño sin parámetros
            }            

                Debug.Log("Impacto en: " + hitPoint);
            }
        }
}
