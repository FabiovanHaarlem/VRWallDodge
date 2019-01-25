using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private MoveDirection m_Direction;

    public void SpawnNextWave(MovingBlock block, int wave, float speed)
    {
        LayerMask layer = 9;
        Vector3 rotation = new Vector3();
        switch (m_Direction)
        {
            case MoveDirection.MoveNorth:
                layer = 9;
                rotation = new Vector3(0, 180, 0);
                break;
            case MoveDirection.MoveEast:
                layer = 10;
                rotation = new Vector3(0, 270, 0);
                break;
            case MoveDirection.MoveSouth:
                rotation = new Vector3(0, 0, 0);
                layer = 11;
                break;
            case MoveDirection.MoveWest:
                rotation = new Vector3(0, 90, 0);
                layer = 12;
                break;
        }
        Vector3 startPos = transform.position;
        if (block.GetNeedToDuck())
        {
            startPos.y = startPos.y + PlayerData.GetPlayerDuckHeightMax();
        }
        else
        {
            startPos.y = startPos.y + PlayerData.GetPlayerHeightDiffrence();
        }

        block.SetMoveDirection(startPos, m_Direction, speed, layer, rotation);
    }
}
