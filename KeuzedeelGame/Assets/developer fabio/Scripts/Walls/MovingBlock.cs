using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField]
    private List<ControleArea> m_ControleAreas;
    private MoveDirection m_Direction;

    [SerializeField]
    private bool m_NeedToDuck;

    private float m_Speed;

    private int m_CheckpointsHitDisableArea;

    public void SetMoveDirection(Vector3 startPosition, MoveDirection direction, float speed, LayerMask layer, Vector3 rotation)
    {
        transform.position = startPosition;
        m_Speed = speed;
        m_Direction = direction;
        this.gameObject.layer = layer;
        for (int i = 0; i < m_ControleAreas.Count; i++)
        {
            m_ControleAreas[i].SetCollisionLayer(layer, this);
        }
        transform.eulerAngles = rotation;
        gameObject.SetActive(true);
    }

    public void DisableBlock()
    {
        m_CheckpointsHitDisableArea++;
        if (m_CheckpointsHitDisableArea == m_ControleAreas.Count)
        {
            gameObject.SetActive(false);
            GameManager.m_Instance.BlockCompleted();   
        }
    }

    public bool GetNeedToDuck()
    {
        return m_NeedToDuck;
    }

    private void Update()
    {
        if (GameManager.m_Instance.GetGameState() == GameState.Running)
        {
            switch (m_Direction)
            {
                case MoveDirection.MoveNorth:
                    Move(new Vector3(0, 0, 1));
                    break;
                case MoveDirection.MoveEast:
                    Move(new Vector3(1, 0, 0));
                    break;
                case MoveDirection.MoveSouth:
                    Move(new Vector3(0, 0, -1));
                    break;
                case MoveDirection.MoveWest:
                    Move(new Vector3(-1, 0, 0));
                    break;
                default:
                    Move(new Vector3(0, 0, 1));
                    break;
            }
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;
        transform.position += direction * m_Speed * Time.deltaTime;
    }
}
