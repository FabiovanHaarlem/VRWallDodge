using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelName", menuName = "LevelDataContainers")]
public class SOLevelValue : ScriptableObject
{
    public string m_LevelName;
    public List<GameObject> m_MovingBlocks;
    public float m_TimerResetValue;
    public float m_BlockSpeed;
}
