using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSettings
{
    public string SpawnID;

    public string AssetPath;

    public float MinSpawnDelay;
    public float MaxSpawnDelay;

    public int MaxAllowedCount;

    public int SpawnedCount;

    public float GetDelayTime()
    {
        return Random.Range(MinSpawnDelay, MaxSpawnDelay);
    }

    public bool CanSpawn()
    {
        return SpawnedCount < MaxAllowedCount;
    }
}
