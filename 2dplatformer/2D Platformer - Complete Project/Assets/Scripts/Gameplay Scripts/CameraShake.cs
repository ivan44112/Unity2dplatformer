using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration, shakeAmpliutde, shakeFrequency;

    private float shakeElapsedTime;

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    void Awake()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if(shakeElapsedTime > 0)
        {
            virtualCameraNoise.m_AmplitudeGain = shakeAmpliutde;
            virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = 0;
            shakeElapsedTime = 0;
        }
    }

    public void ShakeItLow()
    {
        shakeAmpliutde = .3f;

        shakeElapsedTime = shakeDuration;
    }

    public void ShakeItMedium()
    {
        shakeAmpliutde = .6f;

        shakeElapsedTime = shakeDuration;
    }
}
