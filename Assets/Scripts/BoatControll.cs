using UnityEngine;

public class BoatControll : MonoBehaviour
{
    [SerializeField] private GameObject boat;
    [SerializeField] private float boatSpeed = 0.08f;

    void Update()
    {
        if (boat != null)
        {
            float boatMovement = Mathf.Sin(Time.time) * boatSpeed;

            boat.transform.position = new Vector3(boat.transform.position.x, boatMovement - 0.36f, boat.transform.position.z);
        }
    }
}
