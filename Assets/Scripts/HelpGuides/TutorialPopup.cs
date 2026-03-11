using TMPro;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{

    [Header("UI Elements")]

    [SerializeField] TextMeshProUGUI _HeadingText;
    [SerializeField] TextMeshProUGUI _contentText;

    private void Start()
    {
        tutorialTrigger.OnTutorialEnter += TutorialTrigger_OnTutorialEnter; ;
        tutorialTrigger.OnTutorialExit += TutorialTrigger_OnTutorialExit1; ;
        Hide();
    }

    private void TutorialTrigger_OnTutorialExit1(object sender, System.EventArgs e)
    {
        _HeadingText.text = "";
        _contentText.text = "";
        Hide();
    }

    private void TutorialTrigger_OnTutorialEnter(object sender, tutorialTrigger.TutorialEventArgs e)
    {
        _HeadingText.text = e.tutorialSO.Heading;
        _contentText.text = e.tutorialSO.Content;
        Show();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
