using UnityEngine;
using UnityEngine.UI;

public class lifeManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private int coins = 0;
    
    private Vector3 checkpoint;

    void Start()
    {
        currentHealth = maxHealth;
        checkpoint = transform.position;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateUI();
        }
    }

    public void Heal()
    {
        if (coins >= 10)
        {
            coins -= 10;
            currentHealth = maxHealth;
            UpdateUI();
        }
    }

    private void Die()
    {
        transform.position = checkpoint;
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void SetCheckpoint(Vector3 newPosition)
    {
        checkpoint = newPosition;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
    
    public void AddCoin()
    {
        coins++;
    }
}