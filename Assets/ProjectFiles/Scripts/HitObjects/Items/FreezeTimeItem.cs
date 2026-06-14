using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTimeItem : Item
{
    public void HitByPlayer   (float damage, CharacterBeatController player)
    {
        base.HitByPlayer(damage, player);
    }

    public override void ExecuteAction ()
    {
        base.ExecuteAction();
        
        foreach (EnemyBeatController enemy in GameManager.Instance.m_enemies)
        {
            enemy.SetFrozen(true);
        }
        GlobalVolumeManager.Instance.SetAberration(0.8f);
    }

    public override void ExitAction    ()
    {
        base.ExitAction();
        
        foreach (EnemyBeatController enemy in GameManager.Instance.m_enemies)
        {
            enemy.SetFrozen(false);
        }
        GlobalVolumeManager.Instance.SetAberration(0f);
        Destroy(this.gameObject);
    }
}
