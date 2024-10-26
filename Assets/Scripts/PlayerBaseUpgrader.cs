using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseUpgrader : MonoBehaviour
{
    public UpgradesObject upgrades;
    public float requiredExperience = 100f;

    private PlayerExperience exp;
    private UIManager ui;

    private void Start()
    {
        exp = FindObjectOfType<PlayerExperience>();
        ui = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && exp.GetExperience() >= requiredExperience)
        {
            Debug.Log("Enter upgrade menu");
            ui.ActivateUpgradeMenu(GetRandomUpgrades());
            exp.SpendExperience(requiredExperience);
        }
    }

    private UpgradeData[] GetRandomUpgrades()
    {
        UpgradeData[] result = new UpgradeData[3];

        List<int> bucket = new();
        for(int i = 0; i < upgrades.upgrades.Length; i++) bucket.Add(i);

        for(int i = 0; i < 3; i++)
        {
            int index = bucket[Random.Range(0, bucket.Count)];
            bucket.Remove(index);
            result[i] = upgrades.upgrades[index];
        }

        return result;
    }
}
