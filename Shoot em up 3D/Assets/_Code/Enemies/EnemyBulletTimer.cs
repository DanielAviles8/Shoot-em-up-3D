using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTimer : MonoBehaviour
{

    [SerializeField] private float _timer;
    [SerializeField] private bool _active;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _active = true;
        if (_active)
        {
            _timer += Time.deltaTime;
            if (_timer > 5)
            {
                gameObject.SetActive(false);
                _active = false;
            }
        }
    }
}
