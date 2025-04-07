using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{
    public GameObject ActivePanel;
    public GameObject Panel1;
    public GameObject Panel2;
    private AudioSource audioSrc;

    void Start()
    {
        // Intenta obtener el AudioSource
        audioSrc = GetComponent<AudioSource>();

        // Si no hay AudioSource, lo agrega dinámicamente
        if (audioSrc == null)
        {
            audioSrc = gameObject.AddComponent<AudioSource>();
        }

        // Asegura que haya un botón y le asigna la función ChangePanel
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ChangePanel);
        }
        else
        {
            Debug.LogError("El script PanelTransition debe estar en un GameObject con un botón.");
        }
    }

    private void ChangePanel()
    {
        StartCoroutine(SoundButton());
    }

    private IEnumerator SoundButton()
    {
        if (audioSrc.clip != null) // Asegura que hay un audio para reproducir
        {
            audioSrc.Play();
            yield return new WaitForSeconds(audioSrc.clip.length); // Espera hasta que termine el audio
        }
        else
        {
            Debug.LogWarning("Audio1.");
        }

        // Activar y desactivar los paneles
        ActivePanel.SetActive(true);
        Panel1.SetActive(false);
        Panel2.SetActive(false);
    }
}