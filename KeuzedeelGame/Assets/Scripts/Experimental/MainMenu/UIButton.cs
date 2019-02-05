using UnityEngine.Events;
using UnityEngine;
using SimpleEasing;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_FunctionToCall;
    [SerializeField]
    private GameObject m_ParentObject;

    private Vector3 m_DefaultPosition;

    private float m_ButtonTimer = 1.0f;
    [SerializeField]
    private float m_AnimationLength;
    [SerializeField]
    private float m_AnimationStartPositionOffset;
    [SerializeField]
    private float m_AnimationEndPositionOffset;
    [SerializeField]
    private float m_ButtonPressDelay = 0.4f;
    [SerializeField]
    private bool m_UseAnimation;

    private void Awake()
    {
        m_DefaultPosition = transform.position;
    }

    private void Start()
    {
        m_ButtonTimer = m_AnimationLength;
    }

    private void Update()
    {
        if (m_UseAnimation)
        {
            float z;

            z = Easing.easeOutBounce(m_ButtonTimer, m_AnimationStartPositionOffset, m_AnimationEndPositionOffset, m_AnimationLength);

            m_ButtonTimer += Time.deltaTime;
            m_ButtonTimer = Mathf.Clamp(m_ButtonTimer, 0, m_AnimationLength);

            transform.position = m_DefaultPosition + new Vector3(0.0f, 0.0f, z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_FunctionToCall != null)
            {
                ButtonClick();
            }
            else
            {
                Debug.Log("Nothing to call");
            }
        }
    }

    private void ButtonClick()
    {
        if (m_UseAnimation)
        {
            if (m_ButtonTimer < m_ButtonPressDelay)
                return;
        }

        m_FunctionToCall.Invoke();
        m_ButtonTimer = 0;
    }
}
