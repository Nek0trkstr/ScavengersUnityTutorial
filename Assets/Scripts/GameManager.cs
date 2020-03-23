using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager m_BoardScript;
    public static GameManager m_Instance = null;
    private int m_Level = 3;
    
    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        m_BoardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        m_BoardScript.SetupScene(m_Level);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
