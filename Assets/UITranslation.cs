using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITranslation : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] float duration;

    Tween scaleTween;

    public bool loop;
    public bool onEnable;

    public LoopType typeOfLoop;
    public Ease easeType;

    private void OnEnable()
    {
        if (onEnable) Play();
    }

    public void Play()
    {
        if (scaleTween != null) scaleTween.Complete();

        scaleTween = GetComponent<RectTransform>().DOLocalMove(target, duration).SetLoops(loop ? -1 : 1, typeOfLoop).SetEase(easeType);
    }

    private void OnDisable()
    {
        if (onEnable && scaleTween != null) scaleTween.Complete();
    }
}
