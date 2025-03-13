using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangePanel);
    }
    private void ChangePanel()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }
}
