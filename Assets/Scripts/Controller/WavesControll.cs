using UnityEngine;

public class WaveControll : MonoBehaviour
{
    [SerializeField] private GameObject backWave;
    [SerializeField] private GameObject frontWave;
    [SerializeField] private float backSpeed = 0.24f;
    [SerializeField] private float fronSpeed = 0.48f;

    void Update()
    {
        if (backWave != null && frontWave != null)
        {
            float backMovement = Mathf.Sin(Time.time) * backSpeed;
            float fronMovement = Mathf.Sin(Time.time) * fronSpeed;

            backWave.transform.position = new Vector3(backMovement, backWave.transform.position.y, backWave.transform.position.z);
            frontWave.transform.position = new Vector3(fronMovement, frontWave.transform.position.y, frontWave.transform.position.z);
        }
    }
}
