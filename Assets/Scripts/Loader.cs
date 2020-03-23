using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
   public GameManager m_GameManager;

    void Awake()
    {
        if ( GameManager.m_Instance == null )
        {
            Instantiate(m_GameManager);
        }
    }
}
