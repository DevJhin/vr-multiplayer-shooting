using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HealthCellWidget : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] Image backgroundImage;

    public int targetPlayerID;

    public void Init(int playerID)
    {
        targetPlayerID = playerID;
        if (playerID != -1)
        {
            SetFillImageColor(HunterGameSettings.GetPlayerColor(playerID));
        }
        else
        {
            SetFillImageColor(Color.white);
        }
    }

    public void SetFillImageColor(Color color)
    {
        fillImage.color = color;
    }

    public void SetFillAmount(float value)
    {
        fillImage.fillAmount = value;
    }

    public void SetBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }
}
