using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;

public class TimeManager : MonoBehaviour
{
    double startTime;
    double duration = 300;

    int leftTime;
    
    [SerializeField] TMP_Text textField;

    [SerializeField] AudioClip countdownBeepSFX;
    [SerializeField] AudioClip gameFinishBeepSFX;

    AudioSource audioSource;

    bool isPlaying;

    bool needUpdate;
    
    public event System.Action OnTimerEnd;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        isPlaying = true;
        startTime = PhotonNetwork.Time;
        needUpdate = true;
    }

    private void Update()
    {
        if (!isPlaying) return;
        
        UpdateTimer();

        if (needUpdate)
        {
            UpdateText();
        }
    }


    void UpdateTimer()
    {
        int newLeftTime = (int)(duration - GetPlayTime());
        if (leftTime == newLeftTime) return;
        
        leftTime = newLeftTime;
        needUpdate = true;

        //audioSource.PlayOneShot(countdownBeepSFX);
        if (leftTime <= 0)
        {
            leftTime = 0;
            isPlaying = false;
            OnTimerEnd?.Invoke();
            return;
        }

    }

    void UpdateText()
    {
        

        var span = new System.TimeSpan(0, 0, leftTime);

        var timeText = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);

        textField.text = timeText;

    }



    public double GetPlayTime()
    {
        return PhotonNetwork.Time - startTime;
    }
}
