using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays upgrades in the bottom right of the screen so the player knows what they have
/// </summary>
public class UpgradeDisplay : MonoBehaviour
{
    public Transform uiRoot;
    public GameObject speedPrefab;
    public GameObject healthPrefab;

    void Start()
    {
        AddSpeedUpgrade();
        AddHealthUpgrade();
    }

    void AddSpeedUpgrade()
    {
        Instantiate(speedPrefab, uiRoot);
    }

    void AddHealthUpgrade()
    {
        Instantiate(healthPrefab, uiRoot);
    }
}
