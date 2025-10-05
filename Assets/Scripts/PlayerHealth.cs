using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("Maximum health of the player.")]
    public int maxHealth = 100;
    [HideInInspector]
    public int currentHealth;

    [Tooltip("Number of traps the player can trigger before game over.")]
    public int maxTrapDeaths = 3;
    [HideInInspector]
    public int trapDeaths = 0;

    [Header("UI References")]
    [Tooltip("Assign your Health TextMeshPro UI element here.")]
    public TMP_Text healthText;
    [Tooltip("Assign your Trap Alert TextMeshPro UI element here.")]
    public TMP_Text trapAlertText;

    [Header("Trap Alert Settings")]
    [Tooltip("Duration (in seconds) to show trap location on screen.")]
    public float trapAlertDuration = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (trapAlertText != null)
            trapAlertText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Call this method when the player steps on a trap
    /// </summary>
    /// <param name="damage">Amount of health to reduce</param>
    /// <param name="trapPosition">Position of the trap in world space</param>
    public void TakeDamage(int damage, Vector3 trapPosition)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        ShowTrapAlert(trapPosition);

        trapDeaths++;
        if (trapDeaths >= maxTrapDeaths)
        {
            GameOver();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth;
    }

    private void ShowTrapAlert(Vector3 trapPosition)
    {
        if (trapAlertText == null) return;

        trapAlertText.gameObject.SetActive(true);
        trapAlertText.text = $"Trap nearby at: {trapPosition}";

        CancelInvoke(nameof(HideTrapAlert));
        Invoke(nameof(HideTrapAlert), trapAlertDuration);
    }

    private void HideTrapAlert()
    {
        if (trapAlertText != null)
            trapAlertText.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("Game Over! You stepped on 3 traps!");
        // Optional: add UI game over screen or reload scene
        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
