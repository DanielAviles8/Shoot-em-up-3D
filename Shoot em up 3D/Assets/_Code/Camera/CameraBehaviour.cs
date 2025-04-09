using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using FirstGearGames.SmoothCameraShaker;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Cinematic")]
    [SerializeField] private Transform _firstTransform;
    [SerializeField] private Transform _secondTransform;
    [SerializeField] private Transform _thirdTransform;
    [SerializeField] private float _cinematicDuration = 3f;
    public GameObject Player;
    private Quaternion _firstPlayerRotation;
    public static bool _inCinematic;
    public static bool _inMenu;

    [Header("Gameplay")]
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private float _targetDistanceZ = -15f;
    [SerializeField] private float _targetDistanceY = 20f;

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] GameObject Title;

    [SerializeField] private ShakeData shakeData;
    public CameraShaker Shaker;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _firstTransform.position;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Shaker = GetComponent<CameraShaker>();
        Time.timeScale = 0f;
        _inMenu = true;
        _inCinematic = false;
        Title.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {    
        if(_inCinematic == false && _inMenu == false) 
        {
            GameplayCamera();
        }   
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        _inMenu = false;
        _inCinematic = true;
        Title.SetActive(false);
        StartCoroutine(Cinematic());
        
        MainPanel.SetActive(false);
    }

    public void GameplayCamera()
    {
        Vector3 targetPosition = _targetTransform.position + new Vector3(0f, _targetDistanceY, _targetDistanceZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed * Time.deltaTime);
        if (TakeDamage._invulnerable)
        {
            CameraShakerHandler.Shake(shakeData);
        }
    }
    public IEnumerator Cinematic()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = _firstTransform.position;
        Quaternion startRotation = _firstTransform.rotation;

        Vector3 targetPosition = _thirdTransform.position;
        Quaternion targetRotation = _thirdTransform.rotation;

        while (elapsedTime < _cinematicDuration)
        {
            Player.transform.rotation = _firstPlayerRotation;
            float t = elapsedTime / _cinematicDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        _inCinematic = false;
    }
}
