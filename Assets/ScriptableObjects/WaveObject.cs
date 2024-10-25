using System;

using UnityEngine;

[CreateAssetMenu]
public class WaveObject : ScriptableObject
{
    public Wave[] waves;
}


[Serializable]
public struct Wave
{
    public int ActiveBases;
    public float SpawnDelay;
    public float SkipDelayChance;
    public int MinGroupSize;
    public int MaxGroupSize;
    public float GroupChance;
    public int Defenders;
    public int Attackers;
    public int Guards;
    [Multiline] public string TutorialText;
}
