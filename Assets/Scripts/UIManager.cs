using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameInfoPanel, losePanel, winPanel;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text xpCostText;
    [SerializeField] private GameObject upgradeMenu;

    [Header("Upgrades")]
    public UpgradeItemUI upgradeItemPrefab;
    public Transform upgradeContainer;
    public UpgradeDisplay upgradeDisplay;

    private PlayerBaseUpgrader playerBase;
    private PlayerController player;
    private PlayerExperience exp;
    private bool gameOver;

    void Start()
    {
        Time.timeScale = 1f;

        player = FindObjectOfType<PlayerController>();
        exp = FindObjectOfType<PlayerExperience>();
        playerBase = FindObjectOfType<PlayerBaseUpgrader>();
    }

    private void UpdateXPCostDisplay()
    {
        string color = playerBase.requiredExperience > exp.experience ? "red" : "white";
        xpCostText.text = $"Cost: {playerBase.requiredExperience}/<color={color}>{exp.experience}</color> EXP";
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void GameLost()
    {
        gameOver = true;
        losePanel.SetActive(true);
        //infoText.text = "YOU'VE LOST!";
        Time.timeScale = 0f;
    }
    public void GameWon()
    {
        gameOver = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ActivateUpgradeMenu(UpgradeData[] upgrades)
    {
        Time.timeScale = 0;
        upgradeMenu.SetActive(true);
        UpdateXPCostDisplay();

        foreach(Transform t in upgradeContainer)
        {
            Destroy(t.gameObject);
        }

        for(int i = 0; i < upgrades.Length; i++)
        {
            UpgradeItemUI ui = Instantiate(upgradeItemPrefab, upgradeContainer);
            ui.image.sprite = upgrades[i].sprite;
            ui.title.text = upgrades[i].title;
            ui.description.text = upgrades[i].description;
            ui.selectionButton.onClick.AddListener(OnClickedUpgrade(upgrades[i], ui));
        }
    }

    public void CloseUpgradeMenu()
    {
        if(!upgradeMenu.activeSelf) return;

        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private UnityAction OnClickedUpgrade(UpgradeData data, UpgradeItemUI ui)
    {
        return () =>
        {
            if(exp.experience > playerBase.requiredExperience)
            {
                exp.SpendExperience(playerBase.requiredExperience);
                upgradeDisplay.AddUpgrade(data);
                player.ApplyUpgrade(data);
                playerBase.requiredExperience += 100f;
                UpdateXPCostDisplay();

                if(data.id != "healing")
                {
                    Destroy(ui.gameObject);
                }
            }
        };
    }

    public void activate_pausemenu()
    {
        if(upgradeMenu.activeSelf || gameOver) return;

        if (gameInfoPanel.activeSelf)
        {
            infoText.text = "";
            gameInfoPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            infoText.text = "GAME PAUSED";
            gameInfoPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            activate_pausemenu();
        }
    }
}
