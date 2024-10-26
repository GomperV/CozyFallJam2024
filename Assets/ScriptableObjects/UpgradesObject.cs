using System;

using TMPro;

using UnityEngine;

[CreateAssetMenu]
public class UpgradesObject : ScriptableObject
{
    public UpgradeData[] upgrades;
}

[Serializable]
public struct UpgradeData
{
    public Sprite sprite;
    public string id;
    public string title;
    [Multiline]
    public string description;
}
