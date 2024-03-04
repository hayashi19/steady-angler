using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishes;

    void Start()
    {
        StartCoroutine(SpawnFishes());
    }

    IEnumerator SpawnFishes()
    {
        while (true)
        {
            foreach (GameObject fishObject in fishes)
            {
                FishComponent fishComponent = fishObject.GetComponent<FishComponent>();
                if (fishComponent != null)
                {
                    float probability = 1.0f - (fishComponent.fish.rarity - 1f) / 5.0f;
                    float spawnChance = Random.value;
                    if (spawnChance < probability)
                    {
                        GameObject spawnedFish = Instantiate(fishObject, transform.position, Quaternion.identity);
                        spawnedFish.transform.position = new Vector3(spawnedFish.transform.position.x, Random.Range(-4f, -1f), spawnedFish.transform.position.z);
                        spawnedFish.GetComponent<Rigidbody2D>().velocity = new Vector3(fishComponent.fish.speed * 0.32f, 0, 0);
                        spawnedFish.transform.SetParent(this.transform);
                    }
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            float screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            if (child.position.x > screenWidth + 4 || child.position.y > screenHeight + 4 || child.position.y < -screenHeight - 4)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
