using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Trap Settings")]
    public int damage = 25; // Damage per trap

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, transform.position);
            }
        }
    }
}
