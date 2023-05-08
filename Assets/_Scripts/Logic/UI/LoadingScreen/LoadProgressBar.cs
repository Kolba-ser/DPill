using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI progressText;

    public void UpdateProgress(float value)
    {
        fillImage.fillAmount = value;
        progressText.text = (int)(value * 100) + "%";
    }
}
