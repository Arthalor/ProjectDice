using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diceMenu = default;
    [SerializeField] private List<InventoryUI> diceInterfaces = default;

    [SerializeField] public Sprite[] dSprites = default;

    [SerializeField] private HealthUI hUI = default;
    [SerializeField] private ReloadUI rUI = default;
    [SerializeField] private HoverUI hoverUI = default;

    public static bool IsPaused = false;

    public void TogglePause() 
    {
        IsPaused = !IsPaused;
    }

    private void Start()
    {
        IsPaused = false;
        LoadAllDice();
    }

    private void Update()
    {
        if (IsPaused) Time.timeScale = 0;
        else Time.timeScale = 1;

        rUI.UpdateTimer();
        hUI.UpdateHealthUI();
        UploadDice();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
            diceMenu.SetActive(!diceMenu.activeInHierarchy);
            RefreshUI();
        }
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void RefreshUI()
    {
        foreach (InventoryUI ui in diceInterfaces)
        {
            ui.LoadImage(dSprites);
        }
    }

    public void LoadAllDice()
    {
        foreach (InventoryUI ui in diceInterfaces)
        {
            ui.LoadDice();
        }
    }

    public void UploadDice()
    {
        foreach (InventoryUI ui in diceInterfaces)
        {
            ui.UploadDice();
        }
    }

    public void UpdateReloadIcons() 
    {
        rUI.UpdateMagIcon();
    }

    public void UpdateReloadingIcon() 
    {
        rUI.UpdateReloadIcon();
    }

    public void HoverUI(Dice dice) 
    {
        hoverUI.gameObject.SetActive(true);
        hoverUI.UpdateSprites(dice);
    }

    public void HoverUIEnd() 
    {
        hoverUI.gameObject.SetActive(false);
    }
}