using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    private bool changePauseMode = true;
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

    public void activate_pausemenu(bool isPaused)
    {
        if (isPaused)
        {
            infoText.text = "GAME PAUSED";
            gameInfoPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            infoText.text = "";
            gameInfoPanel.SetActive(false);
            Time.timeScale = 1;
        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            activate_pausemenu(changePauseMode);
            changePauseMode = !changePauseMode;
        }
    }
}
