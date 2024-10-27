using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameInfoPanel, losePanel, winPanel;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private GameObject upgradeMenu;

    [Header("Upgrades")]
    public UpgradeItemUI upgradeItemPrefab;
    public Transform upgradeContainer;
    public UpgradeDisplay upgradeDisplay;

    private PlayerController player;
    private PlayerExperience exp;
    private float requiredExperience;
    private bool gameOver;

    void Start()
    {
        Time.timeScale = 1f;
        player = FindObjectOfType<PlayerController>();
        exp = FindObjectOfType<PlayerExperience>();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
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

    public void ActivateUpgradeMenu(UpgradeData[] upgrades, float requiredExperience)
    {
        this.requiredExperience = requiredExperience;
        Time.timeScale = 0;
        upgradeMenu.SetActive(true);

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
            ui.selectionButton.onClick.AddListener(OnClickedUpgrade(upgrades[i]));
        }
    }

    public void CloseUpgradeMenu()
    {
        if(!upgradeMenu.activeSelf) return;

        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private UnityAction OnClickedUpgrade(UpgradeData data)
    {
        return () =>
        {
            CloseUpgradeMenu();
            upgradeDisplay.AddUpgrade(data);
            player.ApplyUpgrade(data);
            exp.SpendExperience(requiredExperience);
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
