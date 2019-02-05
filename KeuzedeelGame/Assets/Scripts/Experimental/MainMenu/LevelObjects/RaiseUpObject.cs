using UnityEngine;
using SimpleEasing;

public class RaiseUpObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerHead;

    [SerializeField]
    private LevelSelectManager m_LevelSelectManager;
    [SerializeField]
    private Calibrate m_Calibration;

    [SerializeField]
    private float m_DetectionRange;
    private float m_MoveTimer;
    [SerializeField]
    private float m_AnimationLength;
    [SerializeField]
    private float m_AnimationStartPositionOffset;
    [SerializeField]
    private float m_AnimationEndPositionOffset;

    private Vector3 m_DefaultPosition;

    private bool m_MoveUp;

    private void Start()
    {
        //m_PlayerHead = GameObject.Find("Camera (head)");
        m_MoveUp = false;

        m_MoveTimer = 0.0f;
        m_DefaultPosition = transform.position;
    }

    private void Update()
    {
        Vector3 wallPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 PlayerPosition = new Vector3(m_PlayerHead.transform.position.x, 0.0f, m_PlayerHead.transform.position.z);
        float distanceToPlayer = Vector3.Distance(wallPosition, PlayerPosition);

        if (distanceToPlayer <= m_DetectionRange)
        {
            Raise();
        }
        else
        {
            Lower();
        }

        float y = 0.0f;
        //y = Easing.easeInQuint(m_buttonTimer, m_animationStart, m_animationChange, m_animationLength);
        //y = Easing.easeOutQuint(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);
        y = Easing.easeInOutExpo(m_MoveTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);

        if (m_MoveUp)
        m_MoveTimer += Time.deltaTime;
        else
        m_MoveTimer -= Time.deltaTime;

        m_MoveTimer = Mathf.Clamp(m_MoveTimer, 0, m_AnimationLength);

        transform.position = m_DefaultPosition + new Vector3(0, y, 0);
    }

    private void Raise()
    {
        m_MoveUp = true;
    }

    private void Lower()
    {
        m_MoveUp = false;
        if (m_LevelSelectManager != null)
        {
            m_LevelSelectManager.MoveDown();
        }
        
        if (m_Calibration != null)
        {
            m_Calibration.Cancle();
        }
    }
}
