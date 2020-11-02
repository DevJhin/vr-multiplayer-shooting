using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    TMP_Text scoreTextField;
    int TeamScore;

    [Header("SFX")]
    [SerializeField]AudioSource score5000SFX;

    private void Awake()
    {
        scoreTextField = GetComponentInChildren<TMP_Text>();
        TeamScore = 0;
        UpdateScoreFeedback();

    }

    public void AppendScore(int newScore)
    {
        TeamScore += newScore;
        UpdateScoreFeedback();
    }

    private void UpdateScoreFeedback()
    {
        scoreTextField.text = TeamScore.ToString();
    }
}
