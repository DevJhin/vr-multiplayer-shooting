using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class RoomUIManager : MonoBehaviour
{
    RoomManager roomManager;

    [SerializeField] 
    TMP_Text OccupancyTextField;

    [SerializeField]
    Button enterRoomButton;

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();

        enterRoomButton.onClick.AddListener(OnEnterRandomRoomButtonClick);
    }

    public void OnEnterRandomRoomButtonClick()
    {
        roomManager.EnterRandomRoom();
    }

    public void SetOccupancyText(int currentPlayerCount, int maxPlayerCount)
    {
        string text = $"{currentPlayerCount}/{maxPlayerCount}";

        OccupancyTextField.text = text;
    }
}
