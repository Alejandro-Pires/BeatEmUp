using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject       m_player;
    public UIController     m_ui;
    public List<EnemyBeatController> m_enemies;
    public GameObject nextStageTrigger;

    public static GameManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance      = this;
        m_enemies = new List<EnemyBeatController>();
        nextStageTrigger.SetActive(false);
    }

    void Start()
    {
        m_ui.SetPlayerFace(m_player.GetComponent<CharacterBeatView>().m_face);
        m_ui.SetPlayerName(m_player.GetComponent<CharacterBeatView>().m_name);
        m_ui.SetPlayerLife(1f);
        GameManager.Instance.m_ui.SetEnableEnemyElements(false);
    }

    void Update() {}

    public void EnemyHitted (Sprite face, string name, float normalizedLife)
    {
        m_ui.SetEnemyFace(face);
        m_ui.SetEnemyName(name);
        m_ui.SetEnemyLife(normalizedLife);
    }

    public void PlayerHitted (float normalizedLife)
    {
        m_ui.SetPlayerLife(normalizedLife);
    }

    private void FinishStage ()
    {
        nextStageTrigger.SetActive(true);
    }

    public void GameOver    ()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].Stop();
        }
        m_ui.GameOver();
    }

    public void AddEnemy (EnemyBeatController enemy)
    {
        m_enemies.Add(enemy);
    }

    public void RemoveEnemy (EnemyBeatController enemy)
    {
        m_enemies.Remove(enemy);
        if (m_enemies.Count == 0) FinishStage();
    }
}
