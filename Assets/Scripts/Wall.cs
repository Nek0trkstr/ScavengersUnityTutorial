using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite m_DmgSprite;
    public int m_HealthPoints = 4;

    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamdgeWall(int i_Loss)
    {
        m_SpriteRenderer.sprite = m_DmgSprite;
        m_HealthPoints = m_HealthPoints - i_Loss;
        if (m_HealthPoints <= 0){
            gameObject.SetActive(false);
        }
    }
}
