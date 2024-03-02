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

                        //if (fishComponent.fish.isCaught)
                        //{
                            spawnedFish.GetComponent<Rigidbody2D>().velocity = new Vector3(fishComponent.fish.speed * 0.32f, 0, 0);
                        //}
                        //else
                        //{
                        //    spawnedFish.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        //}

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
            if (child.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 512, 0, 0)).x)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
