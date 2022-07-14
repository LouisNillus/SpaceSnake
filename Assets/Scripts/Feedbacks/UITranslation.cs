using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITranslation : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] float duration;

    Tween translateTween;

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
        if (translateTween != null)
        {
            translateTween.Rewind();
            translateTween.Kill();
        }

        translateTween = GetComponent<RectTransform>().DOLocalMove(target, duration).SetLoops(loop ? -1 : 1, typeOfLoop).SetEase(easeType);
    }

    private void OnDisable()
    {
        if (onEnable && translateTween != null)
        {        
            translateTween.Rewind();
            translateTween.Kill();       
        }
    }
}
