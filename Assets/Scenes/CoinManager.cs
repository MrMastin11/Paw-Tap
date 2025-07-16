using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static int Coins = 0; 
    public static int AllCoins = 0;
    public int Power = 1;

    public static double Cost = 100;
    public double AutoCost = 500;
    public static double TimeCost = 200;

    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI TextButton;
    public TextMeshProUGUI TextUpgrade;
    public TextMeshProUGUI AutoText;
    public TextMeshProUGUI AutoTime;
    public TextMeshProUGUI LevelText;

    public Image UpgradeButtonImage;
    public Image AutoClickButtonImage;
    public Image DoublerButtonImage;
    public Sprite NormalSprite;
    public Sprite ActiveSprite;
    public Image TrophyImage;

    public Slider progressSlider;
    public static int level = 1;
    private int coinsToNextLevel = 100;
    private int coinsAccumulated = 0;
    private bool isDoublerActive = false;

    void Start()
    {
        UpdateText(); 
        UpdateProgressBar();
        SetButtonVisibility();
        SetTextVisibility();
        TrophyImage.gameObject.SetActive(false);
    }

    public void AddCoin()
    { 
        int coinsToAdd = isDoublerActive ? Power * 2 : Power;
        Coins += coinsToAdd;
        AllCoins += coinsToAdd;
        coinsAccumulated += coinsToAdd;
        UpdateText();
        UpdateProgressBar();
    }

    void UpdateText()
    {
        CoinsText.text = "Coins: " + Coins;
        AutoClickButtonImage.sprite = (Coins >= AutoCost) ? ActiveSprite : NormalSprite;
        DoublerButtonImage.sprite = (Coins >= TimeCost) ? ActiveSprite : NormalSprite;
        UpdateButtonSprite();
        LevelText.text = "Level: " + level;
        if (level >= 10)
        {
            TrophyImage.gameObject.SetActive(true);
        }
    }

    public void PowerUp()
    {
        if (Coins >= (int)(Cost))
        {
            Power += 1;
            Coins -= (int)(Cost);
            Cost = (int)(Cost * 1.1);
            UpdateText();
            Upcoin();
            TextUpgrade.text = "Upgrade click\n" + (int)(Cost) + " coins";
        }
    }

    void Upcoin()
    {
        TextButton.text = "+" + Power + " coins";
    }
    
    public void buyAutoclick()
    {
        if (Coins >= AutoCost)
        {
            Coins -= (int)(AutoCost);
            AutoCost = (int)(AutoCost * 1.8);
            AutoText.text = "\nAutoclick\n" + AutoCost + " coins";
            if (AutoClickButtonImage != null)
            {
                AutoClickButtonImage.sprite = ActiveSprite;
            }
            AddCoin();
            InvokeRepeating("AddCoin", 1f, 1f);
            UpdateText();
        }
        else
        {
            if (AutoClickButtonImage != null)
            {
                AutoClickButtonImage.sprite = NormalSprite;
            }
        }
    }

    public void timeUP()
    {
        if (Coins >= (int)(TimeCost))
        {
            Coins -= (int)(TimeCost);
            TimeCost = (int)(TimeCost * 1.5);
            AutoTime.text = "\nDoubler time\n" + TimeCost + " coins";
            isDoublerActive = true;
        }
        else
        {
            isDoublerActive = false;
        }
        UpdateText();
    }

    void UpdateButtonSprite()
    {
        UpgradeButtonImage.sprite = (Coins >= Cost) ? ActiveSprite : NormalSprite;
    }

    void UpdateProgressBar()
    {
        progressSlider.value = (float)coinsAccumulated / coinsToNextLevel;
        if (coinsAccumulated >= coinsToNextLevel)
        {
            coinsAccumulated = 0;
            progressSlider.value = 0;
            level += 1;
            coinsToNextLevel = Mathf.FloorToInt(coinsToNextLevel * 2.5f);
            SetButtonVisibility();
            SetTextVisibility();
        }
    }

    void SetButtonVisibility()
    {
        UpgradeButtonImage.enabled = level >= 2;
        DoublerButtonImage.enabled = level >= 4;
        AutoClickButtonImage.enabled = level >= 5;
    }

    void SetTextVisibility()
    {
        TextUpgrade.enabled = level >= 2;
        AutoText.enabled = level >= 5;
        AutoTime.enabled = level >= 4;
    }
}
