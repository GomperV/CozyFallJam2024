using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameInfoPanel;
    [SerializeField]
    private TMP_Text infoText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void GameLost()
    {
        gameInfoPanel.SetActive(true);
        infoText.text = "YOU'VE LOST!";
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
