using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBaseHealth : MonoBehaviour
{
    public float baseHealth;
    [SerializeField]
    private TMP_Text baseHealthText;
    [SerializeField]
    private UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        //UI = GameObject.Find("UI");
        baseHealth = 100;
    }

    public void TakeDamage(float dmg)
    {
        baseHealth -= dmg;
        if(baseHealth < 1)
        {
            ui.GameLost();
        }
    }
    // Update is called once per frame
    void Update()
    {
        baseHealthText.text = "" + baseHealth + "%";
        if (baseHealth < 30)
        {
            baseHealthText.color = Color.yellow;
            //UI.GetComponent<UIManager>().GameEnded(false);
        } else
        {
            baseHealthText.color = Color.white;
        }
    }

}
