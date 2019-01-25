using UnityEngine.Events;
using UnityEngine;
using SimpleEasing;

public class UIButton : MonoBehaviour
{
    private float m_buttonTimer = 0;
    private float m_buttonPressDelay = 0.4f;

    private float m_animationLength = 1;

    private float m_animationStart = 0.05f;
    private float m_animationChange = -0.05f;

    private Vector3 m_defaultPosition;

    [SerializeField]
    private UnityEvent m_FunctionToCall;

    private void Start()
    {
        m_buttonTimer = m_animationLength;
        m_defaultPosition = transform.position;
    }

    private void Update()
    {
        float z;

        z = Easing.easeOutBounce(m_buttonTimer, m_animationStart, m_animationChange, m_animationLength);

        m_buttonTimer += Time.deltaTime;
        m_buttonTimer = Mathf.Clamp(m_buttonTimer, 0, m_animationLength);

        transform.position = m_defaultPosition + new Vector3(0, 0, z);
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
        if (m_buttonTimer < m_buttonPressDelay)
            return;

        m_FunctionToCall.Invoke();
        m_buttonTimer = 0;
    }
}
