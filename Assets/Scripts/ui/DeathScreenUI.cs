using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] Image _deathImage;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _mainMenuButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        Hide();
        ThirdPersonShooterController.OnPlayerDeath += ThirdPersonShooterController_OnPlayerDeath;
    }

    private void ThirdPersonShooterController_OnPlayerDeath(object sender, System.EventArgs e)
    {
        Show();
        StartCoroutine(fadeImage());
    }

    IEnumerator fadeImage()
    {
        float duration = 2;
        float timer =0;

        Color tempColor = _deathImage.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;    
            float alpha = Mathf.Lerp(0,1, timer);
            tempColor.a = alpha;
            _deathImage.color = tempColor;
            yield return null;
        }

        tempColor.a = 1;
        _deathImage.color= tempColor;
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
