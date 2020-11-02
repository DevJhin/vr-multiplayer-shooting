using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{
    public float duration;
    public Vector3 targetAngles;
    public float delay;
    [SerializeField] AnimationCurve curve;
    void Start()
    {
        transform.localEulerAngles = Vector3.LerpUnclamped(Vector3.zero, targetAngles, 0);
    }

    public void Play()
    {
        StartCoroutine(MoveAnimation());
    }

    IEnumerator MoveAnimation()
    {
        yield return new WaitForSeconds(delay);
        float playTime = 0f;
        while (playTime < duration)
        {
            float t = curve.Evaluate(playTime/duration);

            transform.localEulerAngles = Vector3.LerpUnclamped(Vector3.zero,targetAngles,t);

            playTime += Time.deltaTime;
            yield return null;
        }
    }

}
