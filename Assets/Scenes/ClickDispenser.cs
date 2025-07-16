using UnityEngine;
using TMPro;

public class ClickDispenser : MonoBehaviour
{
    public int clicksToOpen = 200;
    private int clickCount = 0;
    public static bool isActive = false;
    public TextMeshProUGUI Progres;
    private float timeRemaining = 5f;
    private float time = 5f;
    private bool timerActive = false;

    void Start()
    {
        UpdateVisibility();
    }

    public void OnClick()
    {
        if (isActive || CoinManager.level < 3) return;
        clickCount++;
        UpdateText();
        if (clickCount >= clicksToOpen)
        {
            ActivateDispenser();
        }
    }

    void ActivateDispenser()
    {
        isActive = true;
        timerActive = true;
        timeRemaining = time;
    }

    void CloseDispenser()
    {
        isActive = false;
        timerActive = false;
        clickCount = 0;
        Progres.text = "Clicks to x2 bonus\n0/" + clicksToOpen;
    }

    void Update()
    {
        UpdateVisibility();

        if (timerActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Progres.text = "x2 BONUS!\n" + Mathf.Ceil(timeRemaining);
            }
            else
            {
                CloseDispenser();
            }
        }
    }

    void UpdateText()
    {
        Progres.text = "Clicks to x2 bonus\n" + clickCount + "/" + clicksToOpen;
    }

    void UpdateVisibility()
    {
        if (CoinManager.level < 3)
        {
            Progres.gameObject.SetActive(false);
        }
        else
        {
            Progres.gameObject.SetActive(true);
        }
    }
    public void timeUP()
{
    if (CoinManager.Coins > CoinManager.TimeCost)
    {
        time += 1f;
    }
}

}
