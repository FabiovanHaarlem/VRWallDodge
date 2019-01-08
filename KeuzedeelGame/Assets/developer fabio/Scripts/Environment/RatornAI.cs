using System.Collections.Generic;
using UnityEngine;

public class RatornAI : MonoBehaviour
{
    [SerializeField]
    private Transform m_Face;
    [SerializeField]
    private List<Transform> m_LookToPositions;
    [SerializeField]
    private List<Transform> m_MoveToPositions;
    private Transform m_FocusOnPosition;
    private Transform m_MoveToPosition;
    private float m_LookAtNewObjectTimer;
    private float m_MoveToNextPositionTimer;

    private void Awake()
    {
        m_LookAtNewObjectTimer = GetRandomValue();
        m_MoveToNextPositionTimer = GetRandomValue();
        m_MoveToPosition = m_MoveToPositions[GetRandomValue(0, m_MoveToPositions.Count)];
    }

    private void Update()
    {
        m_MoveToNextPositionTimer -= Time.deltaTime;
        if (m_MoveToNextPositionTimer <= 0f)
        {
            m_MoveToPosition = m_MoveToPositions[GetRandomValue(0, m_MoveToPositions.Count)];
            m_MoveToNextPositionTimer = GetRandomValue();
        }

        transform.position = Vector3.Lerp(transform.position, m_MoveToPosition.position, 0.9f * Time.deltaTime);
        transform.LookAt(m_LookToPositions[0].position);
        m_Face.Rotate(new Vector3(0f, 0f, 1f) * (40f * Time.deltaTime));
    }

    private float GetRandomValue()
    {
        return Random.Range(3f, 8f);
    }

    private int GetRandomValue(int min, int max)
    {
        return Random.Range(min, max);
    }
}
