using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatComponent : MonoBehaviour
{
    [SerializeField] private GameObject boat;
    private Vector3 boatStartPost;
    [SerializeField] private float boatSpeed = 1.0f;
    [SerializeField] private float boatAmplitude = 0.5f;

    void Start()
    {
        boatStartPost = boat.transform.position;

        StartCoroutine(AnimateBoat());
    }

    private IEnumerator AnimateBoat()
    {
        while (true)
        {
            float boatMovement = Mathf.Sin(Time.time * boatSpeed) * boatAmplitude;
            boat.transform.position = boatStartPost + new Vector3(0, boatMovement, 0); yield return new WaitForEndOfFrame();
        }
    }
}