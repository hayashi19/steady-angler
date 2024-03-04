using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class StabilizationComponent : MonoBehaviour
{
    public GameObject stabilizationBar, stabilizationArea, stabilizationPoint;
    public Fish fish = null;
    //public bool isCaught = false;

    public float areaSpeed = 1f;
    public float areaStrength = 1f;
    public float pointSpeed = 0.5f;
    public float pointStrength = 1.2f;
    public float bound = 0.86f;

    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        if (fish != null)
        {
            MoveStabilizationArea();
            MoveStabilizationPoint();
        }
    }

    private void MoveStabilizationArea()
    {
        float changes = Mathf.PerlinNoise1D(Time.time);
        changes = Utils.Remap(changes, 0, 1, -(areaStrength * 0.5f), areaStrength);

        stabilizationArea.transform.Translate(Vector3.right * areaSpeed * changes * Time.deltaTime);

        float x = stabilizationArea.transform.position.x;
        if (x < -bound) x = -bound;
        if (x > bound) x = bound;
        stabilizationArea.transform.position = new Vector3(x, stabilizationArea.transform.position.y, stabilizationArea.transform.position.z);
    }

    private void MoveStabilizationPoint()
    {
        stabilizationPoint.GetComponent<Rigidbody2D>().AddForce(Vector2.left * pointSpeed, ForceMode2D.Force);

        float x = stabilizationPoint.transform.position.x;
        if (x < -bound) x = -bound;
        if (x > bound) x = bound;

        stabilizationPoint.transform.position = new Vector3(x, stabilizationPoint.transform.position.y, stabilizationPoint.transform.position.z);
    }

    public void StopStabilization()
    {
        Rigidbody2D rb = stabilizationPoint.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        fish = null;
    }


    public void AddForceStabilizationPoint()
    {
        //print("Add force");
        stabilizationPoint.GetComponent<Rigidbody2D>().AddForce(Vector2.right * pointStrength, ForceMode2D.Impulse);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0001f);
            if (stabilizationArea.transform.position.x <= (bound * -1) && areaSpeed < 0 || stabilizationArea.transform.position.x >= bound && areaSpeed > 0)
            {
                areaSpeed *= -1;
            }
        }
    }
}
