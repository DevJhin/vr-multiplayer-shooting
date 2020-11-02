using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class PlayerAvatarController : MonoBehaviour
{
    [Header("Avatar Transforms")]
    public Transform HeadTransform;
    public Transform LeftHandTransform;
    public Transform RightHandTransform;

    [Header("VR Transforms")]
    public Transform HMDTransform;
    public Transform LeftControllerTransform;
    public Transform RightControllerTransform;

    [SerializeField] PlayerAvatar avatar;

    void Awake()
    {
        //avatar = GetComponent<PlayerAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        HeadTransform.position = HMDTransform.position;
        HeadTransform.rotation = HMDTransform.rotation;

        LeftHandTransform.position = LeftControllerTransform.position;
        LeftHandTransform.rotation = LeftControllerTransform.rotation;

        RightHandTransform.position = RightControllerTransform.position;
        RightHandTransform.rotation = RightControllerTransform.rotation;
    }

}
