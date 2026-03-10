using UnityEngine;
using UnityEngine.UI;

public class progressBarUI : MonoBehaviour
{
    [SerializeField] Image BarImage;
    [SerializeField] GameObject HasProgressGameObject;

    IHasProgress HasProgress;

    private void Start()
    {
        HasProgress = HasProgressGameObject.GetComponent<IHasProgress>();
        HasProgress.onProgressChanged += HasProgress_onProgressChanged;
        BarImage.fillAmount = 1;
        Show();
    }

    private void HasProgress_onProgressChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        if (e.progressNormalized == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }

        BarImage.fillAmount = e.progressNormalized;
    }
    void Hide()
    {
       gameObject.SetActive(false);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
}
