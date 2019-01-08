using UnityEngine;

public class TubeRotation : MonoBehaviour
{
    private Vector3 m_RotateRotation;

    private float m_RotateSpeed;
    private float m_RotateTimer;
    private float m_RotationLeft;
    private bool m_Rotate;

    private void Awake()
    {
        m_RotateSpeed = 12f;
        m_RotateTimer = 2f;
        m_RotationLeft = 45f;
        m_RotateRotation = new Vector3(0f, 0f, transform.eulerAngles.z - 45f);
    }

    private void Update()
    {
        if (!m_Rotate)
        {
            m_RotateTimer -= Time.deltaTime;
        }

        if (m_RotateTimer <= 0)
        {
            m_Rotate = true;
        }

        if (m_Rotate)
        {
            m_RotationLeft -= (m_RotateSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, -1f) * (m_RotateSpeed * Time.deltaTime));

            if (m_RotationLeft <= 0f)
            {
                m_Rotate = false;
                m_RotateTimer = 2f;
                m_RotationLeft = 45f;
                m_RotateRotation = new Vector3(0f, 0f, transform.eulerAngles.z - 45f);
            }
        }
    }
}
