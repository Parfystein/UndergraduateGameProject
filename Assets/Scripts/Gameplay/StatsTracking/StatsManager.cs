using System.IO;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    private int sessionSlimeKills = 0;
    private int sessionSkeletonKills = 0;

    private string SavePath => Path.Combine(Application.persistentDataPath, "stats.json");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (!File.Exists(SavePath))
        {
            var data = new StatsData();
            File.WriteAllText(SavePath, JsonUtility.ToJson(data, prettyPrint: true));
        }
        Debug.Log($"Stats file path: {SavePath}");
    }

    public void AddKill(EnemyType type)
    {
        if (type == EnemyType.Slime)
            sessionSlimeKills++;
        else if (type == EnemyType.Skeleton)
            sessionSkeletonKills++;
    }


    public void SaveSessionStats()
    {

        string jsonOld = File.ReadAllText(SavePath);
        var data = JsonUtility.FromJson<StatsData>(jsonOld);

        data.totalSlimeKills    += sessionSlimeKills;
        data.totalSkeletonKills += sessionSkeletonKills;

        File.WriteAllText(SavePath, JsonUtility.ToJson(data, prettyPrint: true));
    }

    public StatsData GetAllTimeStats()
    {
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<StatsData>(json);
    }
}