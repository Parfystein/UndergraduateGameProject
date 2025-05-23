using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI floorText;

    public void UpdateHealth(int current, int max)
    {
        healthFill.fillAmount = (float)current / max;
    }

    public void UpdateFloor(int floor)
    {
        floorText.text = $"Floor: {floor}";
    }
}
