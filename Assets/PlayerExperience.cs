using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerExperience : MonoBehaviour
{
    private float experience = 0;
    [SerializeField]
    private TMP_Text expText, bonusExp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GainExperience(float exp, bool isWaveBonus)
    {
        experience += exp;
        StartCoroutine(ExpGained(exp, isWaveBonus));
    }
    IEnumerator ExpGained(float exp, bool isWaveBonus)
    {
        bonusExp.fontSize = 45;
        if (!isWaveBonus)
        {
            bonusExp.text = "+" + exp + " EXP!";
            for(int i = 0; i < 20; i++)
            {
                bonusExp.fontSize++;
                yield return new WaitForSeconds(0.02f);
            }
        } else
        {
            bonusExp.text = "+" + exp + " WAVE BONUS!";
            for (int i = 0; i < 40; i++)
            {
                bonusExp.fontSize++;
                yield return new WaitForSeconds(0.02f);
            }
        }
        bonusExp.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        expText.text = "Experience: " + experience;
    }
}
