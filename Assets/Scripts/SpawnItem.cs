using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public enum ItemType
    {
        Hazard,
        HealthPack
    }

    [SerializeField] private ItemType itemType;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player == null || player.IsDead || collected)
        {
            return;
        }

        collected = true;

        if (itemType == ItemType.Hazard)
        {
            player.TakeDamage(1);
        }
        else
        {
            player.Heal(1);
        }

        Destroy(gameObject);
    }
}