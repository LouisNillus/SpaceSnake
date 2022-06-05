using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyboxHandler : MonoBehaviour
{
    float skyboxRotation;

    void Start()
    {
        DOTween.To(() => skyboxRotation, x => skyboxRotation = x, 360, 360).SetLoops(-1, LoopType.Yoyo).OnUpdate(() => RenderSettings.skybox.SetFloat("_Rotation", skyboxRotation));
    }
}
