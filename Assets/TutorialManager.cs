using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text movementTip, attackTip;
    private WavesManager wavesManager;
    private bool increase = true;
    private bool decrease = false;
    // Start is called before the first frame update
    void Start()
    {
        wavesManager = GameObject.Find("WavesManager").GetComponent<WavesManager>();
        StartCoroutine(TutorialTips());
    }
    IEnumerator TutorialTips()
    {
        movementTip.color = new Color(0f, 0.6933506f, 1f, 0f);
        attackTip.color = new Color(0f, 0.6933506f, 1f, 0f);
        StartCoroutine(ChangeOpacity(movementTip, increase));
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChangeOpacity(attackTip, increase));
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (wavesManager.waitTime < 3)
            {
                StartCoroutine(ChangeOpacity(movementTip, decrease));
                StartCoroutine(ChangeOpacity(attackTip, decrease));
                break;
            }
        }

    }

    IEnumerator ChangeOpacity(TMP_Text text, bool doIncrease)
    {
        if(doIncrease)
        {
            for (float i = 0; i < 100; i++)
            {
                text.color = new Color(0f, 0.6933506f, 1f, i / 100f);
                yield return new WaitForSeconds(0.01f);
            }
        } else
        {
            for (float i = 100; i > 0; i--)
            {
                text.color = new Color(0f, 0.6933506f, 1f, i / 100f);
                yield return new WaitForSeconds(0.01f);
            }
            text.color = new Color(0f, 0.6933506f, 1f, 0f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
