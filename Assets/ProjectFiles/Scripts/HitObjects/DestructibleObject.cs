using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleObject : MonoBehaviour, IHittableGameObjectByPlayer
{
    public GameObject[] m_item;
    public Transform  m_pivot;
    private SpriteRenderer m_spriteRenderer;
    public Sprite m_hitSprite;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HitByPlayer(float damage, CharacterBeatController player)
    {
        m_spriteRenderer.sprite = m_hitSprite;
        //droppeo un item aleatorio entre el primero y el último asignado en editor
        int randomItem = Random.Range(0, m_item.Length);
        Instantiate(m_item[randomItem], m_pivot.position, m_pivot.rotation);
        Destroy(gameObject);
    }
}
