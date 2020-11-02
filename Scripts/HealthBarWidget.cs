using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarWidget : MonoBehaviour
{
    [SerializeField]
    Color mainColor;

    [SerializeField]
    int maxValue;

    [SerializeField]
    int currentValue;

    [SerializeField]
    List<HealthCellWidget> cellWidgets;

    void InitWidget(HunterPlayer player)
    { 
        
    }

    public void SetMainColor(Color color)
    {
        mainColor = color;
    }

    public void SetCurrentValue(int value)
    {
        currentValue = value;
        for (int i = 0; i < maxValue; i++)
        {
            cellWidgets[i].SetFillAmount(i<currentValue?1f:0f);
        }
    }
}
