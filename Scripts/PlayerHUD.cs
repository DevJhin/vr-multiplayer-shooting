using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    int playerIndex;

    [SerializeField]
    TMP_Text NameTag;

    public bool followCamera;

    Camera cam;

    public void Start()
    {
        //SetText(HunterGameMode.Instance.GetCurrentPlayerID());
        cam = HunterGameMode.Instance.LocalHunterPlayer.PlayerCamera;
    }

    public void SetText(int playerIndex)
    {
        NameTag.text = $"Player {(playerIndex+1).ToString()}";
        NameTag.outlineColor = HunterGameSettings.GetPlayerColor(playerIndex);
    }

    private void Update()
    {
        if (followCamera)
        { 
            LookCamera();
        }
    }

    public void LookCamera()
    {
        Vector3 camPos = cam.transform.position;
        camPos.y = 0;
        Vector3 hudPos = transform.position;
        hudPos.y = 0;

        Vector3 forward = (hudPos - camPos).normalized;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }




}
