using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IHittableGameObjectByPlayer
{
    public GameObject m_item;
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
        Instantiate(m_item, m_pivot.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
