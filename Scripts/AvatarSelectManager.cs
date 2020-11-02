using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class AvatarSelectManager : MonoBehaviour
{
    public List<AvatarGroupSettings> AvatarGroups;

    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Boolean boolean;

    WidgetSwitcher groupSwitcher;
    WidgetShifter avatarShifter;


    void Start()
    {
        groupSwitcher = GetComponent<WidgetSwitcher>();

    }

    public void Show()
    {
        var avatarStyle = ExperimentManager.Instance.settings.AvatarStyle;
        var gender = ExperimentManager.Instance.settings.Gender;
        switch (avatarStyle)
        {
            case AvatarStyle.UnrealCharacter:
                groupSwitcher.SetActiveWidget(0);
                ExperimentManager.Instance.AvatarGroupName = "UnrealCharacter";
                break;
            case AvatarStyle.UnrealHuman:
                if (gender == PlayerGender.Man)
                {
                    groupSwitcher.SetActiveWidget(1);
                    ExperimentManager.Instance.AvatarGroupName = "UnrealMan";
                }
                else
                {
                    groupSwitcher.SetActiveWidget(2);
                    ExperimentManager.Instance.AvatarGroupName = "UnrealWoman";
                }
                break;
            case AvatarStyle.RealHuman:
                groupSwitcher.SetActiveWidget(3);
                ExperimentManager.Instance.AvatarGroupName = "RealHuman";
                break;
        }
        avatarShifter = groupSwitcher.ActiveWidget.GetComponent<WidgetShifter>();
        ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;
    }


    private void OnEnable()
    {
        boolean.AddOnStateDownListener(ShiftSelection, SteamVR_Input_Sources.LeftHand);
        boolean.AddOnStateDownListener(ShiftSelection, SteamVR_Input_Sources.RightHand);
    }

    private void OnDisable()
    {
        boolean.RemoveOnStateDownListener(ShiftSelection, SteamVR_Input_Sources.LeftHand);
        boolean.RemoveOnStateDownListener(ShiftSelection, SteamVR_Input_Sources.RightHand);
    }

    public void ShiftSelection(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == SteamVR_Input_Sources.RightHand)
        {
            avatarShifter.ShiftRight();
        }
        else
        {
            avatarShifter.ShiftLeft();
        }
        ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;
    }



    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            groupSwitcher.SetActiveWidget(0);
            avatarShifter = groupSwitcher.ActiveWidget.GetComponent<WidgetShifter>();
            ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            groupSwitcher.SetActiveWidget(1);
            avatarShifter = groupSwitcher.ActiveWidget.GetComponent<WidgetShifter>();
            ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            groupSwitcher.SetActiveWidget(2);
            avatarShifter = groupSwitcher.ActiveWidget.GetComponent<WidgetShifter>();
            ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;

        }
        

         if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            avatarShifter.ShiftRight();
            ExperimentManager.Instance.AvatarIndex = avatarShifter.ShiftIndex;
        }
        */
    }


}
