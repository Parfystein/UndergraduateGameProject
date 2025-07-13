using System;

[Serializable]
public class StatsData
{
    public int totalSlimeKills;
    public int totalSkeletonKills;
}

public enum EnemyType
{
    Slime,
    Skeleton
}