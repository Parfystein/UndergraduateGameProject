using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class StatsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TMPro.TMP_Text slimeCountText;
    [SerializeField] private TMPro.TMP_Text skeletonCountText;


    private void Start()
    {
        statsPanel.SetActive(false);
    }

    public void ShowStats()
    {
        var data = StatsManager.Instance.GetAllTimeStats();
        slimeCountText.text    = $"Slimes killed: {data.totalSlimeKills}";
        skeletonCountText.text = $"Skeletons killed: {data.totalSkeletonKills}";
        statsPanel.SetActive(true);
    }

    public void HideStats()
    {
        statsPanel.SetActive(false);
    }
}