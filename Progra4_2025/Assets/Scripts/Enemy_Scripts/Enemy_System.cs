using UnityEngine;

public class Enemy_System : MonoBehaviour, IHealth
{
    [Header("Life")]
    public int maxHealth = 50;
    int currentHealth;

    [Header("Death Points")]
    public int minPoints;
    public int maxPoints;
    public void Die()
    {
        if (CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            int points = Random.Range(minPoints, maxPoints + 1);
            Level_Manager.Instance.AddPoints(points);
        }
        Destroy(gameObject);
        Analytic_Manager.Instance.EnemyDefeated("Enemy");
        
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
}
