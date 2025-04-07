using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class CameraBehaviour : MonoBehaviour
{
    public TakeDamage takeDamage;
    [Header("Cinematic")]
    [SerializeField] private Transform _firstTransform;
    [SerializeField] private Transform _secondTransform;
    [SerializeField] private Transform _thirdTransform;
    [SerializeField] private float _cinematicDuration = 3f;
    public GameObject Player;
    private Quaternion _firstPlayerRotation;
    public static bool _inCinematic;
    public static bool _inMenu;

    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private float _targetDistanceZ = -15f;
    [SerializeField] private float _targetDistanceY = 20f;

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject MainPanel;


    //public Animation anim;
    private bool _wasInvulnerable = false;


    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animation>();
        //_inMenu = true;
        _inCinematic = true;
        PlayGame();
    }
    // Update is called once per frame
    void Update()
    {    
        if(/*_inMenu == false && */_inCinematic == false) 
        {
            GameplayCamera();
            //anim.Play("Shake");
        }   
    }
    public void PlayGame()
    {
        _inMenu = false;
        StartCoroutine(Cinematic());
        Title.SetActive(false);
        MainPanel.SetActive(false);
    }
    private void GameplayCamera()
    {
        Title.SetActive(false);
        Vector3 targetPosition = _targetTransform.position + new Vector3(0f, _targetDistanceY, _targetDistanceZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed * Time.deltaTime);
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
