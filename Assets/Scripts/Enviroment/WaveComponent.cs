using System.Collections;
using UnityEngine;

public class WaveComponent : MonoBehaviour
{
    [SerializeField] private GameObject frontWave;
    private Vector3 frontWaveStartPost;
    [SerializeField] private float frontWaveSpeed = 1.0f;
    [SerializeField] private float frontWaveAmplitude = 0.5f;

    [SerializeField] private GameObject backWave;
    private Vector3 backWaveStartPost;
    [SerializeField] private float backWaveSpeed = 1.0f;
    [SerializeField] private float backWaveAmplitude = 0.5f;

    void Start()
    {
        frontWaveStartPost = frontWave.transform.position;
        backWaveStartPost = backWave.transform.position;

        StartCoroutine(AnimateWave());
    }

    private IEnumerator AnimateWave()
    {
        while (true)
        {
            float frontWaveMovement = Mathf.Sin(Time.time * frontWaveSpeed) * frontWaveAmplitude;
            frontWave.transform.position = frontWaveStartPost + new Vector3(frontWaveMovement, 0, 0);

            float backWaveMovement = Mathf.Sin(Time.time * backWaveSpeed) * backWaveAmplitude;
            backWave.transform.position = backWaveStartPost + new Vector3(backWaveMovement, 0, 0); yield return new WaitForEndOfFrame();
        }
    }
}