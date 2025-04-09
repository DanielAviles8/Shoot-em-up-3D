using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject DefeatPanel;
    // Start is called before the first frame update
    void Start()
    {
        TutorialPanel.SetActive(false);
        GamePanel.SetActive(false);
        VictoryPanel.SetActive(false);
        DefeatPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    private void Update()
    {
        if(CameraBehaviour._inMenu == false && CameraBehaviour._inCinematic == false)
        {
            GamePanel.SetActive(true);
        }

        if (WaveSpawner.Win == true || TakeDamage.Death == true)
        {
            GamePanel.SetActive(false);
            StartCoroutine(WinScreen());
        }
        if (WaveSpawner.Win == false && TakeDamage.Death == false)
        {
            StopAllCoroutines();
        }
        
    }
    public void Tutorial()
    {
        MenuPanel.SetActive(false);
        TutorialPanel.SetActive(true);
    }
    public void Options()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void Credits()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackButton()
    {
        TutorialPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    public IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(3);
        if (WaveSpawner.Win)
        {
            VictoryPanel?.SetActive(true);
        }
        if(TakeDamage.Death == true)
        {
            DefeatPanel?.SetActive(true);
        }
    }
}
