using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIScaler : MonoBehaviour
{
    [SerializeField] Vector3 additionalScale;
    [SerializeField] float duration;
    [SerializeField] int vibrato;

    Tween scaleTween;

    public bool loop;
    public bool onEnable;

    private void OnEnable()
    {
        if (onEnable) Play();
    }

    public void Play()
    {
        if(scaleTween != null) scaleTween.Complete();

        scaleTween = GetComponent<RectTransform>().DOPunchScale(additionalScale, duration, vibrato).SetLoops(loop ? -1 : 1);
    }

    private void OnDisable()
    {
        if(onEnable && scaleTween != null) scaleTween.Complete();        
    }
}
