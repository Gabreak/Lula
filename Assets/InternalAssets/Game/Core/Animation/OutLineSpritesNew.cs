using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OutLineSpritesNew : MonoBehaviour
{
    [SerializeField] private Material m_Outline;
    [SerializeField] private Material m_MaterialDefault;
    private SpriteRenderer m_Sprite;

    private void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (enabled)
            m_Sprite.material = m_Outline;
    }

    private void OnMouseExit()
    {
        if (enabled)
            m_Sprite.material = m_MaterialDefault;
    }
}
