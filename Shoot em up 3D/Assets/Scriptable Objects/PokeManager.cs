using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeManager : MonoBehaviour
{
    [SerializeField] private PokeTest[] pokeTest = new PokeTest[21];
    [SerializeField] private int _currentPokebuelo;
    [SerializeField] private TMPro.TextMeshPro name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCurrentPokebuelo()
    {
        name.text = pokeTest[_currentPokebuelo].name;
        //un prefab que acceda a poketest y al ser clickeado currentpokebuelo se actualiza
        //boton con el index de cada pokemon para actualizar currentPokebuelo;
    }
}
