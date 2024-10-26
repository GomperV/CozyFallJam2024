using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Displays upgrades in the bottom right of the screen so the player knows what they have
/// </summary>
public class UpgradeDisplay : MonoBehaviour
{
    public Transform uiRoot;
    public UpgradeDisplayItem upgradeItemPrefab;

    public void AddUpgrade(UpgradeData data)
    {
        var ui = Instantiate(upgradeItemPrefab, uiRoot);
        ui.image.sprite = data.sprite;
    }
}
