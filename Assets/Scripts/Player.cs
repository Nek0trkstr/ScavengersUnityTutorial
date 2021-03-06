﻿using UnityEngine;

public class Player : MovingObject
{
    private const string k_ChopAnimationTrigger = "playerChop";
    private const string k_HitAnimationTrigger = "playerHit";
    public const int k_WallDamage = 1;
    public const int k_PointsPerFood = 10;
    public const int k_PointsPerSoda = 20;
    public const float k_RestartLevelDelay = 1f;

    private Animator m_Animator;
    private int m_Food;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Food = GameManager.m_Instance.m_PlayerFoodPoints;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.m_Instance.m_PlayerFoodPoints = m_Food;
    }

    void Update()
    {
       if(GameManager.m_Instance.m_PlayerTurn == false)
       {
           return;
       }

        int vertical = (int) Input.GetAxis("Vertical");
        int horizontal = (int) Input.GetAxis("Horizontal");

        if (horizontal == 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }

    protected override void AttemptMove<T>(int i_xDir, int i_yDir)
    {
        m_Food--;
        base.AttemptMove<T>(i_xDir, i_yDir);
        RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.m_Instance.m_PlayerTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D i_OtherObject)
    {
        switch(i_OtherObject.tag)
        {
            case "Exit":
                Invoke("Restart", k_RestartLevelDelay);
                enabled = false;
                break;
            case "Food":
                m_Food = m_Food + k_PointsPerFood;
                i_OtherObject.gameObject.SetActive(false);
                break;
            case "Soda":
                m_Food = m_Food + k_PointsPerFood;
                i_OtherObject.gameObject.SetActive(false);
                break;
        }
    }

    protected override void OnCantMove<T>(T i_Component)
    {
        Wall hitWall = i_Component as Wall;
        hitWall.DamdgeWall(k_WallDamage);
        m_Animator.SetTrigger(k_ChopAnimationTrigger);
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int i_Loss)
    {
        m_Animator.SetTrigger(k_HitAnimationTrigger);
        m_Food = m_Food - i_Loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if(m_Food <= 0)
        {
            GameManager.m_Instance.GameOver();
        }
    }
}
