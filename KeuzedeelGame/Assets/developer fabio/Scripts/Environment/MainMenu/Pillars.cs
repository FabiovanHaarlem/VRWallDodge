using UnityEngine;

public class Pillars : MonoBehaviour
{
    private Vector3 m_StartPosition;
    private float m_HoverSpeed;
    private bool m_MovingUp;

    private void Awake()
    {
        m_StartPosition = transform.position;
        m_HoverSpeed = 0.2f;
        if (Random.Range(0, 100) < 50)
        {
            m_MovingUp = true;
        }
        else
        {
            m_MovingUp = false;
        }
    }

    private void Update()
    {
        Vector3 goToPostion = new Vector3();
        if (m_MovingUp)
        {
            goToPostion = new Vector3(m_StartPosition.x, m_StartPosition.y + 1f, m_StartPosition.z);
        }
        else
        {
            goToPostion = new Vector3(m_StartPosition.x, m_StartPosition.y - 1f, m_StartPosition.z);
        }

        if (Vector3.Distance(transform.position, goToPostion) <= 0.4f)
        {
            m_MovingUp = !m_MovingUp;
        }

        transform.position = Vector3.MoveTowards(transform.position, goToPostion, m_HoverSpeed * Time.deltaTime);
    }

}
