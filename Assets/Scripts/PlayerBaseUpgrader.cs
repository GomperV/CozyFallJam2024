using System.Collections.Generic;

using UnityEngine;
using TMPro;
public class PlayerBaseUpgrader : MonoBehaviour
{
    public UpgradesObject upgrades;
    public float requiredExperience = 100f;
    public float menuOpenDelay = 5f;
    public GameObject upgradeParticle;

    private PlayerExperience exp;
    private UIManager ui;
    private PlayerController player;

    private float _lastOpenedMenu = -999f;

    private void Start()
    {
        exp = FindObjectOfType<PlayerExperience>();
        ui = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        upgradeParticle.SetActive(exp.experience > requiredExperience);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && exp.GetExperience() >= requiredExperience && Time.time > _lastOpenedMenu + menuOpenDelay)
        {
            _lastOpenedMenu = Time.time;
            ui.ActivateUpgradeMenu(GetRandomUpgrades());
        }
    }

    public UpgradeData[] GetRandomUpgrades()
    {
        var ownedUpgrades = player.upgradesOwned;

        List<int> bucket = new();
        for(int i = 0; i < upgrades.upgrades.Length; i++)
        {
            if(!ownedUpgrades.Contains(upgrades.upgrades[i].id) || upgrades.upgrades[i].repeatable)
            {
                bucket.Add(i);
            }
        }

        UpgradeData[] result = new UpgradeData[Mathf.Min(bucket.Count, 5)];

        for(int i = 0; i < result.Length; i++)
        {
            int index = bucket[Random.Range(0, bucket.Count)];
            bucket.Remove(index);
            result[i] = upgrades.upgrades[index];
        }

        return result;
    }
}
