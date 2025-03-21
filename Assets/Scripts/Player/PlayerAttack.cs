using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f; // Última vez que atacó
    public float basicAttackRange = 2f;
    public float specialAttackRange = 3f;
    public float ultimateAttackRange = 4f; 
    private Vector2 attackDirection;
    public GameObject attackEffectPrefab; // Prefab del efecto de ataque
    public Transform player; // Referencia al jugador
    public float attackAngle = 45f; // Ángulo del ataque triangular
    public LayerMask enemyLayer; // Capa del enemigo

    void Update()
    {
        // Comprueba el cooldown del ataque y si ya pasó el tiempo
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackBasic();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                AttackSpecial();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                AttackUltimate();
            }
        }
    }

    // Métodos de ataque (usa un raycast para el ataque triangular)
    void AttackBasic()
    {
        lastAttackTime = Time.time;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)player.position).normalized; // Dirección del ataque

            // Instancia el efecto en el jugador
        GameObject effect = Instantiate(attackEffectPrefab, player.position, Quaternion.identity);

            // Rota el efecto para que apunte al mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Destruye el efecto después de 0.3 segundos
        Destroy(effect, 0.3f);

        DealDamageInTriangle(basicAttackRange, attackAngle);
        Debug.Log("is attacking");
    }
    
    void AttackSpecial()
    {
        lastAttackTime = Time.time;
        
        DealDamageInTriangle(specialAttackRange, attackAngle);
    }
    
    void AttackUltimate()
    {
        lastAttackTime = Time.time;
        
        DealDamageInTriangle(ultimateAttackRange, attackAngle * 1.5f);
    }

    // Método para hacer daño a los enemigos dentro del área
    void DealDamageInTriangle(float range, float angle)
    {
        Vector2 attackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Vector2 attackOrigin = transform.position;

        // Calcula el área del ataque triangular
        float halfAngle = angle / 2f;
        Vector2 leftDirection = RotateVector(attackDirection, -halfAngle);
        Vector2 rightDirection = RotateVector(attackDirection, halfAngle);

        // La punta del triángulo está en la dirección del ataque
        Vector2 vertex1 = attackOrigin + (attackDirection * range);
    
        // La base del triángulo está más cerca del jugador
        Vector2 vertex2 = attackOrigin + (leftDirection * (range / 2));
        Vector2 vertex3 = attackOrigin + (rightDirection * (range / 2));

        // Detecta enemigos dentro del triángulo
        Collider2D[] enemiesInRange = Physics2D.OverlapAreaAll(vertex2, vertex3, enemyLayer);

        foreach (var enemy in enemiesInRange)
        {
            Vector2 enemyPosition = enemy.transform.position;

            // Comprobar si el enemigo está dentro del triángulo
            if (IsPointInTriangle(enemyPosition, vertex1, vertex2, vertex3))
            {
                // hacer daño al enemigo(está comentado porque hay que ponerle la variable de ataque)
                // enemy.GetComponent<EnemyHealth>().TakeDamage(10);
            }
        }

        // Visualiza el triángulo en el editor
        Debug.DrawLine(vertex1, vertex2, Color.red); // Lado izquierdo
        Debug.DrawLine(vertex1, vertex3, Color.red); // Lado derecho
        Debug.DrawLine(vertex2, vertex3, Color.red); // Base
    }


    // Función para verificar si un punto está dentro de un triángulo
    bool IsPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
    {
        float Area(Vector2 v1, Vector2 v2, Vector2 v3) =>
            Mathf.Abs((v1.x * (v2.y - v3.y) + v2.x * (v3.y - v1.y) + v3.x * (v1.y - v2.y)) / 2.0f);

        float totalArea = Area(a, b, c);
        float area1 = Area(p, b, c);
        float area2 = Area(a, p, c);
        float area3 = Area(a, b, p);

        return Mathf.Approximately(totalArea, area1 + area2 + area3);
    }


    // Esta función es para rotar el vector por un ángulo
    Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radian);
        float sin = Mathf.Sin(radian);
        return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
    }

    // Esto es para ver los ataques (solo en el editor)
    private void OnDrawGizmos()
    {
        if (attackDirection == Vector2.zero) return; // Asegúrate de que haya una dirección de ataque válida

        // Color del triángulo (para ataque básico, especial y ultimate)
        Gizmos.color = Color.red;

        // Calcula el área triangular
        float halfAngle = attackAngle / 2f;
        Vector2 leftDirection = RotateVector(attackDirection, -halfAngle);
        Vector2 rightDirection = RotateVector(attackDirection, halfAngle);

        // Crea los puntos del triángulo
        Vector2 vertex1 = transform.position;
        Vector2 vertex2 = (Vector2)transform.position + (leftDirection * basicAttackRange);
        Vector2 vertex3 = (Vector2)transform.position + (rightDirection * basicAttackRange);

        // Dibuja el triángulo
        Gizmos.DrawLine(vertex1, vertex2);
        Gizmos.DrawLine(vertex1, vertex3);
        Gizmos.DrawLine(vertex2, vertex3);
    }
}
