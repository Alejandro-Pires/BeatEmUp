using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject       m_player;
    public UIController     m_ui;
    public List<EnemyBeatController> m_enemies;
    
    [Header("Transición de Nivel")]
    [Tooltip("Vacio durante el Boss")]
    public GameObject nextStageTrigger;
    
    [Tooltip("Inútil en niveles normales")]
    public string sceneToLoadOnClear;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        m_enemies = new List<EnemyBeatController>();
        
        if (nextStageTrigger != null) nextStageTrigger.SetActive(false);
    }

    void Start()
    {
        if (m_player != null)
        {
            CharacterBeatView playerView = m_player.GetComponent<CharacterBeatView>();
            if (playerView != null)
            {
                m_ui.SetPlayerFace(playerView.m_face);
            }
        }
        
        m_ui.SetPlayerLife(1f);
    }

    public void EnemyHitted(Sprite face, string name, float normalizedLife)
    {
        m_ui.SetEnemyLife(normalizedLife);
    }

    public void PlayerHitted(float normalizedLife)
    {
        m_ui.SetPlayerLife(normalizedLife);
    }

    private void FinishStage()
    {
        // Opción A: Niveles Normales (trigger para pasar de nivel)
        if (nextStageTrigger != null) 
        {
            nextStageTrigger.SetActive(true);
        }

        // Opción B: Nivel del Boss (Transición automática tras acabar con el boss)
        if (!string.IsNullOrEmpty(sceneToLoadOnClear))
        {
            StartCoroutine(WaitAndLoadScene(sceneToLoadOnClear, 2.5f));
        }
    }
    
    private IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }

    public void GameOver()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_enemies[i] != null) 
            {
                m_enemies[i].Stop();
            }
        }
        m_ui.GameOver();
    }

    public void AddEnemy(EnemyBeatController enemy)
    {
        if (!m_enemies.Contains(enemy)) 
        {
            m_enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(EnemyBeatController enemy)
    {
        m_enemies.Remove(enemy);
        
        if (m_enemies.Count == 0) 
        {
            FinishStage();
        }
    }

    public void LoadScene(string scene)
    { 
        SceneManager.LoadScene(scene); 
    }
}