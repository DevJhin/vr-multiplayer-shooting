using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField] SpawnSchedule spawnSchedule;
    
    Dictionary<string, SpawnSettings> spawnDict;

    // Start is called before the first frame update
    void Start()
    {
        spawnDict = new Dictionary<string, SpawnSettings>();
        if (PhotonNetwork.IsMasterClient)
        {
            spawnSchedule.Reset();
            var spawnSettingsList = spawnSchedule.SpawnSettingsList;
            foreach (var settings in spawnSettingsList)
            {
                spawnDict.Add(settings.SpawnID, settings);
                StartCoroutine(SpawnRoutine(settings));
            }
        }
    }


    //Should be only called by MasterClient.
    private IEnumerator SpawnRoutine(SpawnSettings spawnSettings)
    {
        yield return new WaitForSeconds(3);
        while (true)    
        {
            yield return new WaitForSeconds(spawnSettings.GetDelayTime());
            if (spawnSettings.CanSpawn())
            { 
                SpawnRobot(spawnSettings);
            }
        }
    }

    public void SpawnRobot(SpawnSettings spawnSettings)
    {
        spawnSettings.SpawnedCount++;

        var instance = PhotonNetwork.InstantiateRoomObject(spawnSettings.AssetPath, transform.position, transform.rotation);
        var enemy = instance.GetComponent<Enemy>();

        var startWaypointIndex = WaypointManager.Instance.GetRandomWaypointIndex();
        
        enemy.AttemptSpawn(startWaypointIndex);


    }

    public void NotifyDestroy(string spawnID)
    {
        spawnDict[spawnID].SpawnedCount -= 1;
    }

}
