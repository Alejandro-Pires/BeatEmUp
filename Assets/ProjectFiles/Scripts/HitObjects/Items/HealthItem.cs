using System.Collections;
using System.Collections.Generic;
using ProjectFiles.Scripts;
using UnityEngine;

public class HealthItem : Item
{
    public int m_health;

    public void HitByPlayer   (float damage, CharacterBeatController player)
    {
        base.HitByPlayer(damage, player);
        m_player.AddHealth(m_health);
        StartCoroutine(HealFlash());
        SFXManager.Instance.Play(SFXType.ItemHeal);
    }

    public override void ExecuteAction ()
    {
        base.ExecuteAction();
    }

    public override void ExitAction    ()
    {
        base.ExitAction();
        Destroy(this.gameObject);
    }
    
    private IEnumerator HealFlash()
    {
        float time = 0f;
        float duration = 0.3f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            GlobalVolumeManager.Instance.SetSaturation(Mathf.Lerp(50, 0, t));
            yield return null;
        }
        GlobalVolumeManager.Instance.SetSaturation(0);
    }
}
