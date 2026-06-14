using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoBehaviour
{
    public static GlobalVolumeManager Instance;
    
    private Volume m_volume;
    private ColorAdjustments m_colAdjust;
    private ChromaticAberration m_aberration;

    private void Awake()
    {
        Instance = this;
        m_volume = GetComponent<Volume>();
        m_volume.profile.TryGet<ColorAdjustments>(out m_colAdjust);
        m_volume.profile.TryGet<ChromaticAberration>(out m_aberration);
    }

    public void SetSaturation(float saturation)
    {
        m_colAdjust.saturation.value = saturation;
    }

    public void SetAberration(float aberration)
    {
        m_aberration.intensity.value = aberration;
    }
    
}
