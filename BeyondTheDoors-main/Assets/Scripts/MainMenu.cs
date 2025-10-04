using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button PlayBtton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button CreditsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayBtton.onClick.AddListener(OnPlayButtonClick);
    }

    void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Additive);
    }
}
