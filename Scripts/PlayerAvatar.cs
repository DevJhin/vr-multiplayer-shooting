using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RootMotion.FinalIK;


public class PlayerAvatar : MonoBehaviour
{
    public List<AvatarGroupSettings> groupSettingsList;

    public VirtualCameraRig virtualRig;
    public GameObject avatarInstance;
    PlayerAvatarController controller;

    public string AvatarID;

    void Awake()
    {
        controller = GetComponent<PlayerAvatarController>();

        //SwitchAvatarModel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnAvatarModel();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnAvatarModel();
        }
    }

    public void SpawnAvatarModel()
    {
        var splits = AvatarID.Split('_');
        string groupName = splits[0];
        int avatarIndex = int.Parse(splits[1]);

        if (avatarInstance != null)
        {
            Destroy(avatarInstance);
        }

        LoadAvatarModel(groupName, avatarIndex);
        SetupVirtualRig();
    }

    public void LoadAvatarModel(string avatarGroupName, int avatarIndex)
    {
        GameObject avatarPrefab = LoadPrefabFromSettings(avatarGroupName, avatarIndex);
        avatarInstance = Instantiate(avatarPrefab, transform);
        virtualRig = avatarInstance.GetComponentInChildren<VirtualCameraRig>();
    }

    public GameObject LoadPrefabFromSettings(string groupName, int avatarIndex)
    {
        foreach (var groupSettings in groupSettingsList)
        {
            if (groupSettings.GroupName == groupName)
            {
                return groupSettings.Avatars[avatarIndex].Prefab;
            }
        }
        return null;
    }

    public void SetupVirtualRig()
    {
        virtualRig.HMD = controller.HeadTransform;
        virtualRig.LeftController = controller.LeftHandTransform;
        virtualRig.RightController = controller.RightHandTransform;
    }



}
