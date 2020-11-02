using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvatarStyle
{ 
    UnrealCharacter=0,
    UnrealHuman=1,
    RealHuman=2
}

public enum PlayerGender
{ 
    Man=0,
    Woman=1
}


[CreateAssetMenu( menuName = "TreasureHunter/Create ExperimentSettings")]
public class ExperimentSettings : ScriptableObject
{
    public AvatarStyle AvatarStyle;

    public PlayerGender Gender;

    public bool UseVoiceDistortion;

    public int AvatarIndex;

    
}
