using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public const float k_MoveTime = .1f;
    public LayerMask m_BlockingLayer;

    private BoxCollider2D m_BoxCollider2D;
    private Rigidbody2D m_RigidBody2D;
    private float m_InverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        m_InverseMoveTime = 1f / k_MoveTime;
    }

    protected bool Move(int i_xDir, int i_yDir, out RaycastHit2D io_Hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(i_xDir, i_yDir);

        m_BoxCollider2D.enabled = false;
        io_Hit = Physics2D.Linecast(start, end, m_BlockingLayer);
        m_BoxCollider2D.enabled = true;

        if(io_Hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 i_EndPoint)
    {
        float sqrRemainingDistance = (transform.position - i_EndPoint).sqrMagnitude;
        
        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(m_RigidBody2D.position, i_EndPoint, m_InverseMoveTime * Time.deltaTime);
            m_RigidBody2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - i_EndPoint).sqrMagnitude;
            yield return null;
        }
    }
    protected abstract void OnCantMove<T>(T i_Component) where T : Component;
}
