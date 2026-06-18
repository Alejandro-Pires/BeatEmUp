using System;
using System.Collections;
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

    //para el healitem
    public void SetSaturation(float saturation)
    {
        m_colAdjust.saturation.value = saturation;
    }

    //para el freezetimeitem
    public void SetAberration(float aberration)
    {
        m_aberration.intensity.value = aberration;
    }
    
    //método que se llama cuando el jugador recibe daño
    public void TriggerHitEffect()
    {
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        SetSaturation(-80f);

        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.5f;
            SetSaturation(Mathf.Lerp(-80, 0f, t));
            yield return null;
        }

        SetSaturation(0f);
    }
    
}
