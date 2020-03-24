using UnityEngine;

public class Enemy : MovingObject
{
    public int m_PlayerDamage;
    private Transform m_Target;
    private Animator m_Animator;
    private bool m_SkipMove;

    // Start is called before the first frame update
    protected override void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Target = GameObject.FindGameObjectWithTag("player").transform;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void AttemptMove<T>(int i_xDir, int i_yDir)
    {
        if (m_SkipMove)
        {
            m_SkipMove = false;
            return;
        }

        base.AttemptMove<Player>(i_xDir, i_yDir);
        m_SkipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Mathf.Abs(m_Target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = m_Target.position.y - transform.position.y > 0 ? 1 : -1;
        }
        else
        {
            xDir = m_Target.position.x - transform.position.x > 0 ? 1 : -1;
        }
    }

    protected override void OnCantMove<Player>(Player i_Component)
    {
        Player hitPlayer = i_Component as Player;
        
    }
}
