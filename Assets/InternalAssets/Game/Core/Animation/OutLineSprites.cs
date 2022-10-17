using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OutLineSprites : MonoBehaviour
{
    [SerializeField] private Material m_Outline;
    private Material m_MaterialDefault;
    private SpriteRenderer m_Sprite;

    private void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_MaterialDefault = m_Sprite.material;
    }

    private void OnMouseEnter() => m_Sprite.material = m_Outline;

    private void OnMouseExit() => m_Sprite.material = m_MaterialDefault;
}
