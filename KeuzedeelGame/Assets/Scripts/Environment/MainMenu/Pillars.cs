using UnityEngine;
using SimpleEasing;

public class Pillars : MonoBehaviour
{
    private float m_MoveTimer;
    private float m_AnimationLength;
    private float m_AnimationStartPositionOffset;
    private float m_AnimationEndPositionOffset;

    private Vector3 m_DefaultPosition;

    private bool m_MoveUp;

    private void Awake()
    {
        m_MoveTimer = 0.5f;
        m_DefaultPosition = transform.position;
        m_AnimationLength = Random.Range(8.0f, 12.0f);
        m_AnimationStartPositionOffset = Random.Range(-2.0f, -4.0f);
        m_AnimationEndPositionOffset = Random.Range(2.0f, 4.0f);

        if (Random.Range(0, 100) < 50)
        {
            m_MoveUp = false;
        }
        else
        {
            m_MoveUp = true;
        }
    }

    private void Update()
    {

        float y = 0.0f;
        //y = Easing.easeInQuint(m_buttonTimer, m_animationStart, m_animationChange, m_animationLength);
        //y = Easing.easeOutQuint(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);
        y = Easing.easeInOutQuad(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);

        if (m_MoveUp)
            m_MoveTimer += Time.deltaTime;
        else
            m_MoveTimer -= Time.deltaTime;

        m_MoveTimer = Mathf.Clamp(m_MoveTimer, 0, m_AnimationLength);
        if (m_MoveTimer <= 0.05f && !m_MoveUp)
        {
            m_MoveUp = true;
        }
        else if (m_MoveTimer >= m_AnimationLength - 0.05f && m_MoveUp)
        {
            m_MoveUp = false;
        }

        transform.position = m_DefaultPosition + new Vector3(0, y, 0);
    }

}
