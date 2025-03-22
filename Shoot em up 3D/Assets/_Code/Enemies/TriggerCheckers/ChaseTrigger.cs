using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public static bool Chase = false;
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Chase = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            Chase = false;
        }
    }
}
