using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

[RequireComponent(typeof(HunterPlayer))]
public class PlayerController : MonoBehaviour
{
    public SteamVR_Input_Sources Source;
    public SteamVR_Action_Vector2 TrackPadAction;
    public SteamVR_Action_Boolean PlayerSwitchAction;

    public Transform CamTransform;

    private HunterPlayer player;

    [SerializeField] PlayerHUD mainHUD;
    [SerializeField] PlayerHUD gunHUD;
    [SerializeField] GameObject HandTrigger;


    private void Awake()
    {
        player = GetComponent<HunterPlayer>();

    }

    private void OnEnable()
    {
        TrackPadAction.AddOnAxisListener(MoveTrackPad, Source);
        PlayerSwitchAction.AddOnStateDownListener(OnSwitchButtonPressAction, Source);


    }

    private void OnDisable()
    {
        TrackPadAction.RemoveOnAxisListener(MoveTrackPad, Source);
        PlayerSwitchAction.RemoveOnStateDownListener(OnSwitchButtonPressAction, Source);
    }

    void OnSwitchButtonPressAction(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        player.PlayerID = (player.PlayerID + 1) % 3;
        OnPlayerIDUpdate(player.PlayerID);
    }

    public void OnPlayerIDUpdate(int playerID)
    {
        mainHUD.SetText(player.PlayerID);
        gunHUD.SetText(player.PlayerID);
        HandTrigger.tag = $"Player{player.PlayerID + 1}";
    }


    void MoveTrackPad(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        Quaternion camRot = CamTransform.rotation;

        Vector3 moveDir = new Vector3(axis.x, 0, axis.y);
        moveDir = camRot * moveDir;
        moveDir.y = 0;

        moveDir.Normalize();

        Vector3 velocity = moveDir * (player.Speed * Time.deltaTime);
        transform.Translate(velocity, Space.World);
    }
}
