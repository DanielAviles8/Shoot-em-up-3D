using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private float _targetDistanceZ = -6f;
    [SerializeField] private float _targetDistanceY = 15.75f;

    [SerializeField] private Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _inCinematic = true;
        StartCoroutine(Cinematic());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_inCinematic);
        if (_inCinematic)
        {
            Cinematic();
        }
        else
        {
            GameplayCamera();
        }
    }

    private void GameplayCamera()
    {
        Vector3 targetPosition = _targetTransform.position + new Vector3(0f, _targetDistanceY, _targetDistanceZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed * Time.deltaTime);
    }
    private IEnumerator Cinematic()
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
