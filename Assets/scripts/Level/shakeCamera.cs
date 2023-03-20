using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class shakeCamera : MonoBehaviour {
    public static shakeCamera Instance;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin multiChanel;
    
    float moventTime;
    float miventTimeTotal;
    float initialIntensity;

    void Awake() {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        multiChanel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoveCamera(float intensity, float frequency, float time) {
        multiChanel.m_AmplitudeGain = intensity;
        multiChanel.m_FrequencyGain = frequency;
        initialIntensity = intensity;
        miventTimeTotal = time;
        moventTime = time;
    }

    void Update() {
        if (moventTime > 0) {
            moventTime -= Time.deltaTime;
            multiChanel.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (moventTime / miventTimeTotal));
        }
    }
}