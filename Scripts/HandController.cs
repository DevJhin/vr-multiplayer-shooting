using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
public class HandController : MonoBehaviour
{
    public Gun gun;
    public Scanner scanner;

    public SteamVR_Input_Sources RightHandInputSource;
    public SteamVR_Input_Sources LeftHandInputSource;

    public SteamVR_Action_Boolean TriggerAction;
    public SteamVR_Action_Single TriggerPullAction;

    public bool isPCMode;

    void Start()
    {
        TriggerAction.AddOnStateDownListener(OnTriggerButtonDown, RightHandInputSource);
        TriggerPullAction.AddOnChangeListener(OnTriggerPull, RightHandInputSource);

        TriggerAction.AddOnStateDownListener(OnTriggerButtonDown, LeftHandInputSource);
        //TriggerAction.AddOnStateUpListener(OnTriggerButtonDown, LeftHandInputSource);
        TriggerPullAction.AddOnChangeListener(OnTriggerPull, LeftHandInputSource);
    }

    public void Update()
    {
        if (!isPCMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            gun.AttemptFire();
        }
        if (Input.GetMouseButtonDown(1))
        {
            scanner.AttemptFire();
        }

    }

    public void OnTriggerButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == RightHandInputSource)
        { 
            gun.AttemptFire();
        
        }
        else if (fromSource == LeftHandInputSource)
        {
            scanner.AttemptFire();

        }
    }

    public void OnTriggerPull(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        if (fromSource == RightHandInputSource)
        {
            gun.TriggerPullFeedback(newAxis);
        }
        else if (fromSource == LeftHandInputSource)
        {
            scanner.TriggerPullFeedback(newAxis);

        }
    }

    public void OnRefillRequest()
    {
        gun.RefillFeedback();
        
    }


}
