using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureHunter/SpawnSchedule", fileName = "New SpawnSchedule")]
public class SpawnSchedule : ScriptableObject
{
    public List<SpawnSettings> SpawnSettingsList;

    public void Reset()
    {
        foreach (var settings in SpawnSettingsList)
        {
            settings.SpawnedCount = 0;
        }
    }

}
