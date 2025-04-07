using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject DefeatPanel;
    // Start is called before the first frame update
    void Start()
    {
        GamePanel.SetActive(false);
        VictoryPanel.SetActive(false);
        DefeatPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    private void Update()
    {
        if(CameraBehaviour._inCinematic == false)
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
    public void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
