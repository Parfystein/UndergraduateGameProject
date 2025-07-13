using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameVictoryManager : MonoBehaviour
{
    public static GameVictoryManager Instance { get; private set; }

    [SerializeField] private GameObject victoryUI;
    public int totalEnemies;
    public int defeatedEnemies;
    private bool victoryShown = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
{
    yield return null;

    totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    Debug.Log(totalEnemies);

    }
    public void RegisterEnemyDefeat()
    {
        defeatedEnemies++;

        if (!victoryShown && defeatedEnemies >= totalEnemies)
        {
            ShowVictoryScreen();
        }
    }

    public void ShowVictoryScreen()
    {

        StatsManager.Instance.SaveSessionStats();
        Time.timeScale = 0f;
        victoryUI.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
