using UnityEngine;
using System.Linq;

public class FishController : MonoBehaviour
{
    public Fish[] fishes;
    public float speed = 2.0f;
    public float minY = -5.0f;
    public float maxY = -2.0f;

    private float spawnRate = 0f;
    private float timer = 0;

    void Update()
    {
        if (timer > 0)
        {
            spawnRate = Mathf.PerlinNoise1D(Time.deltaTime) * 5;
        }

        if (timer > spawnRate)
        {
            timer = 0;

            int totalWeight = fishes.Sum(fish => 6 - fish.rarity);

            int randomValue = Random.Range(0, totalWeight);

            Fish fish = null;
            foreach (Fish f in fishes)
            {
                randomValue -= (6 - f.rarity);
                if (randomValue <= 0)
                {
                    fish = f;
                    break;
                }
            }

            GameObject fishObject = new GameObject(fish.name);

            SpriteRenderer sr = fishObject.AddComponent<SpriteRenderer>();

            sr.sprite = fish.sprite;

            sr.sortingOrder = 5;

            float spawnY = Random.Range(minY, maxY);
            fishObject.transform.position = new Vector3(transform.position.x, spawnY, 10);
            fishObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            fishObject.transform.parent = transform;
        }

        timer += Time.deltaTime;

        float addMove = Mathf.PerlinNoise1D(Time.deltaTime);

        foreach (Transform child in transform)
        {
            child.position += new Vector3(addMove, 0, 10) * speed * Time.deltaTime;

            if (child.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 512, 0, 0)).x)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
