using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button StartAndStopButton;
    public Button ClearButton;
    public FractalController FractCtrl;
    public GameObject InfoPanel;
    public Text Iteration;

    [SerializeField] private Color startColor;
    [SerializeField] private Color stopColor;

    private Text startStopButText;

    private const string startString = "Start";
    private const string stopString = "Stop";

    private bool showAgain = true;
    private bool IsGoing = false;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("showAgain"))
        {
            if (PlayerPrefs.GetInt("showAgain") == 0)
            {
                showAgain = false;
            }
            else
            {
                showAgain = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("showAgain", 1);
        }
    }

    private void Start()
    {
        startStopButText = StartAndStopButton.GetComponentInChildren<Text>();
        InfoPanel.SetActive(showAgain);
    }

    public void UpdateIteration(int i)
    {
        Iteration.text = $"iteration: {i}";
    }

    public void CloseInfoPanel()
    {
        InfoPanel.SetActive(false);
    }

    public void DontShowInfoPanel(bool value)
    {
        showAgain = value;
        if (value)
        {
            PlayerPrefs.SetInt("showAgain", 0);
        }
        else
        {
            PlayerPrefs.SetInt("showAgain", 1);
        }
    }

    public void PressStartStopButton()
    {
        if (IsGoing)
        {
            StopButton();
        }
        else
        {
            StartButton();
        }

        ChangeButtonText();

        IsGoing = !IsGoing;
    }

    public void PressClearButton()
    {
        FractCtrl.ClearField();
    }

    private void StartButton()
    {
        FractCtrl.BeginIterations();
    }

    private void StopButton()
    {
        FractCtrl.StopIterations();
    }

    public void PressSpeedButton(int value)
    {
        FractCtrl.SetSpeed(value);
    }

    private void ChangeButtonText()
    {
        if (IsGoing)
        {
            startStopButText.text = startString;
            startStopButText.color = startColor;
        }
        else
        {
            startStopButText.text = stopString;
            startStopButText.color = stopColor;
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
