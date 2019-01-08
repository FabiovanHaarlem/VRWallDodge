using UnityEngine;

public class FlyingLight : MonoBehaviour
{
    private Vector3 m_StartPosition;
    private float m_HoverSpeed;
    private bool m_MovingUp;

    private void Awake()
    {
        m_StartPosition = transform.position;
        m_HoverSpeed = 0.2f;
        m_MovingUp = true;
    }

    private void Update()
    {
        Vector3 goToPostion = new Vector3();
        if (m_MovingUp)
        {
            goToPostion = new Vector3(m_StartPosition.x, m_StartPosition.y + 0.5f, m_StartPosition.z);
        }
        else
        {
            goToPostion = new Vector3(m_StartPosition.x, m_StartPosition.y - 0.5f, m_StartPosition.z);
        }

        if (Vector3.Distance(transform.position, goToPostion) <= 0.2f)
        {
            m_MovingUp = !m_MovingUp;
        }

        transform.position = Vector3.MoveTowards(transform.position, goToPostion, m_HoverSpeed * Time.deltaTime);
    }
}
