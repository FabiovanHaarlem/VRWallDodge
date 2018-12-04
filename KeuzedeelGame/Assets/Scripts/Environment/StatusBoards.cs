using UnityEngine;

public class StatusBoards : MonoBehaviour
{
    private float m_RotateSpeed;

    private void Awake()
    {
        m_RotateSpeed = 12f;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f) * (m_RotateSpeed * Time.deltaTime));
    }

}
