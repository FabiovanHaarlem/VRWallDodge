using UnityEngine.Events;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_FunctionToCall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (m_FunctionToCall != null)
            {
                m_FunctionToCall.Invoke();
            }
            else
            {
                Debug.Log("Nothing to call");
            }
        }
    }
}
