using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TMP_Text hudText;
    [SerializeField] private GameObject deathPanel;

    private void Start()
    {
        deathPanel.SetActive(false);
    }

    private void Update()
    {
        Transform player = playerHealth.transform;

        Vector3 position = player.position;
        float rotationY = Mathf.Repeat(player.eulerAngles.y, 360f);

        hudText.text =
            $"Health: {playerHealth.CurrentHealth} / {playerHealth.MaxHealth}\n" +
            $"Position X: {position.x:F1}   Z: {position.z:F1}\n" +
            $"Rotation Y: {rotationY:F1}°";

        deathPanel.SetActive(playerHealth.IsDead);

        if (playerHealth.IsDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}