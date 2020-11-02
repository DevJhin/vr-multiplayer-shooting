using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LoginUIManager : MonoBehaviour
{
    LoginManager loginManager;

    [SerializeField] TMP_InputField PlayerNameInputField;

    [SerializeField] WidgetSwitcher PanelSwitcher;

    [SerializeField] Button ConnectWithNameButton;
    [SerializeField] Button ConnectButton;


    // Start is called before the first frame update
    void Start()
    {
        loginManager = FindObjectOfType<LoginManager>();

        PanelSwitcher.SetActiveWidget(0);

        ConnectWithNameButton.onClick.AddListener(OnConnectWithNameButtonClick);
        ConnectButton.onClick.AddListener(OnConnectButtonClick);

    }

    void OnConnectWithNameButtonClick()
    {
        PanelSwitcher.SetActiveWidget(1);
    }


    void OnConnectButtonClick()
    {
        string playerName = GetPlayerNameFromInput();
        loginManager.ConnectToPhotonServer(playerName);
    }


    public string GetPlayerNameFromInput()
    {
        return PlayerNameInputField.text;
    }

    public void OnDisableVRButtonClick()
    {
        UnityEngine.XR.XRSettings.enabled = false;
    }
}
