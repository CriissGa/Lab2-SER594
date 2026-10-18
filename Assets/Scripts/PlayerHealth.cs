using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public bool IsDead => CurrentHealth <= 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);

        Debug.Log($"Vida: {CurrentHealth}/{maxHealth}");

        if (IsDead)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);

        Debug.Log($"Vida: {CurrentHealth}/{maxHealth}");
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        rb.angularVelocity = Vector3.zero;

        Debug.Log("Has muerto");
    }

    // Opciones temporales para probar desde el Inspector.
    [ContextMenu("Probar daño")]
    private void TestDamage()
    {
        TakeDamage(1);
    }

    [ContextMenu("Probar curación")]
    private void TestHeal()
    {
        Heal(1);
    }
}