using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHUD : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] UIManager mainAmmoImage;

    [SerializeField] Text ammoText;

    [Header("Reload")]
    [SerializeField] Text timeText;

    public void UpdateAmmoUI(int current, int max)
    {

        float rate = current / (float)max;

        mainAmmoImage.UpdateImage(rate);

        UpdateAmmoText(current);
    }

    public void ShowAmmoEmpty()
    { 
        
    }

    private void UpdateAmmoText(int value)
    {
        if (value > 0)
        {
            ammoText.text = value.ToString();
        }
        else
        {
            ammoText.text = null;
        }
    }
}
