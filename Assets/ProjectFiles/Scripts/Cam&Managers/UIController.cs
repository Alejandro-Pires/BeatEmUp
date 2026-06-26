using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("UI del Jugador")]
    [SerializeField] private Image m_playerFace;    
    [SerializeField] private Image m_playerLife;

    [Header("UI del Enemigo (Opcional)")]
    [SerializeField] private Image m_enemyLife;

    [Header("Pantallas")]
    [SerializeField] private GameObject m_gameOver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        if (m_gameOver != null) m_gameOver.SetActive(false);
        //if (m_enemyLife != null) m_enemyLife.gameObject.SetActive(false);
    }
    
    public void SetPlayerFace(Sprite playerFace)
    {
        if (m_playerFace != null)
        {
            m_playerFace.sprite = playerFace;
        }
    }

    public void SetPlayerLife(float lifeNormalized)
    {
        if (m_playerLife != null)
        {
            m_playerLife.fillAmount = Mathf.Clamp01(lifeNormalized);
        }
    }

    public void SetEnemyLife(float lifeNormalized)
    {
        if (m_enemyLife != null)
        {
            if (lifeNormalized > 0 && !m_enemyLife.gameObject.activeSelf)
            {
                m_enemyLife.gameObject.SetActive(true);
            }

            m_enemyLife.fillAmount = Mathf.Clamp01(lifeNormalized);

            if (lifeNormalized <= 0)
            {
                m_enemyLife.gameObject.SetActive(false);
            }
        }
    }

    public void GameOver()
    {
        if (m_gameOver != null)
        {
            m_gameOver.SetActive(true);
        }
    }
}