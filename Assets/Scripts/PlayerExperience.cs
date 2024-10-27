using System.Collections;

using TMPro;

using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public float experience = 0;
    [SerializeField]
    private TMP_Text expText, bonusExp;

    public void GainExperience(float exp, bool isWaveBonus)
    {
        experience += exp;
        int steps = isWaveBonus ? 40 : 20;
        string label = isWaveBonus ? "WAVE BONUS" : "EXP";
        StartCoroutine(ExpGained(exp, steps, label));
    }

    public void SpendExperience(float exp)
    {
        experience -= exp;
        StartCoroutine(ExpGained(-exp, 40, "UPGRADE BOUGHT"));
    }

    IEnumerator ExpGained(float exp, int steps, string label)
    {
        bonusExp.fontSize = 45;
        string sign = exp >= 0 ? "+" : "-";
        bonusExp.text = sign + Mathf.Abs(exp) + $" {label}!";
        for(int i = 0; i < steps; i++)
        {
            bonusExp.fontSize++;
            yield return new WaitForSeconds(0.02f);
        }
        bonusExp.text = "";
    }

    public float GetExperience()
    {
        return experience;
    }

    void Update()
    {
        expText.text = "Experience: " + experience;
    }
}
