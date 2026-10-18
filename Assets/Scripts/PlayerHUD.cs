using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TMP_Text hudText;

    private void Update()
    {
        Transform player = playerHealth.transform;

        Vector3 position = player.position;
        float rotationY = Mathf.Repeat(player.eulerAngles.y, 360f);

        hudText.text =
            $"Health: {playerHealth.CurrentHealth} / {playerHealth.MaxHealth}\n" +
            $"Position X: {position.x:F1}   Z: {position.z:F1}\n" +
            $"Rotation Y: {rotationY:F1}°";
    }
}