using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSettings : MonoBehaviour
{
    public bool disableVR;
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
        UnityEngine.XR.XRSettings.enabled = !disableVR;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
