using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] public static bool _invulnerable;
    [SerializeField] private float _timer;
    [SerializeField] public static bool Death;
    [SerializeField] private TextMeshProUGUI healthRemaining;
    [SerializeField] private Image DefaultIcon;
    [SerializeField] private Sprite Hundred;
    [SerializeField] private Sprite Eighty;
    [SerializeField] private Sprite Sixty;
    [SerializeField] private Sprite Forty;
    [SerializeField] private Sprite Twenty;
    [SerializeField] private Sprite Zero;

    // Start is called before the first frame update
    void Start()
    {
        _invulnerable = false;
        Death = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthRemaining.text = _health.ToString();
        if (_invulnerable)
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                _invulnerable = false;
                _timer = 0f;
            }
        }
        ChangeHealth();
    }
    public void DoDamage(float damage)
    {
        if (_invulnerable == false)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Debug.Log("Mori");
                Death = true;
            }
            _invulnerable = true;
            _timer = 0;
        }
    }
    public void ChangeHealth()
    {
        if (DefaultIcon == null)
        {
            Debug.LogError("DefaultIcon is NULL!");
            return;
        }
        if (_health <= 250 && _health > 201)
        {
            DefaultIcon.sprite = Hundred;
        }
        if (_health <= 200 && _health > 151)
        {
            DefaultIcon.sprite = Eighty;
        }
        if(_health <= 150 &&  _health > 101)
        {
            DefaultIcon.sprite = Sixty;
        }
        if(_health <= 100 && _health > 51)
        {
            DefaultIcon.sprite = Forty;
        }
        if(_health <= 50 && _health > 1)
        {
            DefaultIcon.sprite = Twenty;
        }
        if(_health <= 0)
        {
            DefaultIcon.sprite = Zero;
        }
    }
}
