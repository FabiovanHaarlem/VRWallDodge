using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnController : MonoBehaviour
{
    private SOLevelValue m_LevelData;
    [SerializeField]
    private List<WaveSpawner> m_WaveSpawners;
    private List<MovingBlock> m_Waves;
    private DirectionArrow m_DirectionArrow;

    private int m_Wave;
    private float m_NextWaveTimer;
    private float m_NextWaveTimerReset;
    private float m_BlockSpeed;

    //Sets Variables
    public void SetVariables()
    {
        GetLevelData();
        m_NextWaveTimer = 4f;
    }

    public void GetRefs()
    {
        GameManager.m_Instance.SetAmountOfBlocks(m_Waves.Count);
        GameManager.m_Instance.m_EventSystem.E_HitBlockEvent += DisableAllBlocks;
    }

    private void GetLevelData()
    {
        m_LevelData = SelectedLevelData.GetLevelData();
        m_Waves = new List<MovingBlock>(SetWaveObjects(m_LevelData.m_MovingBlocks));
        m_NextWaveTimerReset = m_LevelData.m_TimerResetValue;
        m_BlockSpeed = m_LevelData.m_BlockSpeed;
    }

    private void Update()
    {
        if (GameManager.m_Instance.GetGameState() == GameState.Running)
        {
            m_NextWaveTimer -= GameManager.m_Instance.GetGameSpeed() * Time.deltaTime;

            if (m_Wave < m_Waves.Count && m_NextWaveTimer <= 0)
            {
                StartNextWave();
            }

            if (m_DirectionArrow != null)
            {
                SendNextBlockToArrow();
            }
        }
    }

    private void StartNextWave()
    {
        m_WaveSpawners[Random.Range(0, m_WaveSpawners.Count)].SpawnNextWave(m_Waves[m_Wave], m_Wave, m_BlockSpeed);
        m_Wave++;
        m_NextWaveTimer = m_NextWaveTimerReset;
    }

    private void DisableAllBlocks()
    {
        for (int i = 0; i < m_Waves.Count; i++)
        {
            m_Waves[i].gameObject.SetActive(false);
        }
    }

    private List<MovingBlock> SetWaveObjects(List<GameObject> blocks)
    {
        List<MovingBlock> waves = new List<MovingBlock>();
        for (int i = 0; i < blocks.Count; i++)
        {
            GameObject obstacle = Instantiate(blocks[i].gameObject);
            obstacle.SetActive(false);
            waves.Add(obstacle.GetComponent<MovingBlock>());
        }

        return waves;
    }

    public void ArrowActive(DirectionArrow arrow)
    {
        m_DirectionArrow = arrow;
    }

    private void SendNextBlockToArrow()
    {
        for (int i = 0; i < m_Waves.Count; i++)
        {
            if (m_Waves[i].gameObject.activeInHierarchy)
            {
                m_DirectionArrow.FollowNextBlock(m_Waves[i]);
            }
        }
    }
}
