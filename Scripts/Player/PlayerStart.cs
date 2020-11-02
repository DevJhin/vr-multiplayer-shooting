using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerStart : MonoBehaviour
{
    [SerializeField] int playerIndex;
    [SerializeField] Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer.material.color = HunterGameSettings.GetPlayerColor(playerIndex);       
    }

}
