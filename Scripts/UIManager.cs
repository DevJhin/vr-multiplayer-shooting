using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIManager : MonoBehaviour
{
    public enum ImageUpdateMode { Instant, Smooth }

    [Header("General")]
    public ImageUpdateMode updateMode;

    [Header("Time")]
    [SerializeField] float duration = 2f; // 체력이 다 떨어지는 데 걸리는 시간 (전체 시간)

    [Header("Color")]
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;



    const float interval = 0.05f; //코루틴 간격 (WaitForSeconds에 넣어줄 시간)
    WaitForSeconds waitForSeconds = new WaitForSeconds(interval);

    private float animInterval = 0.1f; //애니메이션 간격 (WaitForSeconds에 넣어줄 시간)

    public float currentAmountValue = 1;
    public float targetingAmountValue = 1;

    Image image;
    float velocity = 0;

    void Awake()
    {
        image = GetComponent<Image>();
        currentAmountValue = 1;
        targetingAmountValue = 1;

        StartCoroutine(SmoothDampRoutine());

    }

    public void UpdateImage(float value)
    {
        switch (updateMode)
        {
            case ImageUpdateMode.Instant:
                {
                    UpdateInstantImage(value);
                    break;
                }
            case ImageUpdateMode.Smooth:
                {
                    UpdateSmoothDampImage(value);
                    break;
                }
        }
    }

    private void UpdateInstantImage(float _value)
    {
        image.fillAmount = _value;
    }

    private void UpdateSmoothDampImage(float _value)
    {
        targetingAmountValue = _value;
    }

    IEnumerator SmoothDampRoutine()
    {
        while (true)
        {
            float velocity = 0;
            float difference = image.fillAmount - targetingAmountValue;
            if (Mathf.Abs(difference) > 0.01f)
            {
                currentAmountValue = Mathf.SmoothDamp(image.fillAmount, targetingAmountValue, ref velocity, duration);
                image.fillAmount = currentAmountValue;
            }

            yield return waitForSeconds;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }


}
