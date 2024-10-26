using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    private bool changePauseMode = true;
    [SerializeField] private GameObject gameInfoPanel;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private GameObject upgradeMenu;

    [Header("Upgrades")]
    public UpgradeItemUI upgradeItemPrefab;
    public Transform upgradeContainer;
    public UpgradeDisplay upgradeDisplay;

    private PlayerController player;
    private PlayerExperience exp;
    private float requiredExperience;

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
        gameInfoPanel.SetActive(true);
        infoText.text = "YOU'VE LOST!";
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
