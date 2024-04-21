using System.Collections;
using UnityEngine;

public class FishSpawnerComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] fishes;

    private CameraController cameraController;

    void Start()
    {
        cameraController = new CameraController();

        StartCoroutine(SpawnFishes());
        StartCoroutine(DestroyFishes());
    }

    private IEnumerator SpawnFishes()
    {
        while (true)
        {
            // Calculate total rarity
            float totalRarity = 0f;
            foreach (GameObject fishObject in fishes)
            {
                FishComponent fishComponent = fishObject.GetComponent<FishComponent>();
                totalRarity += fishComponent.fish.rarity;
            }

            // Generate a random number
            float randomNumber = Random.Range(0f, totalRarity);

            // Determine which fish to spawn
            foreach (GameObject fishObject in fishes)
            {
                FishComponent fishComponent = fishObject.GetComponent<FishComponent>();
                randomNumber -= fishComponent.fish.rarity;
                if (randomNumber <= 0)
                {
                    GameObject spawnedFish = Instantiate(fishObject, transform.position, Quaternion.identity);
                    spawnedFish.transform.position = new Vector3(cameraController.camLeftEdge.x - 2f, Random.Range(-4f, 1f), spawnedFish.transform.position.z);
                    spawnedFish.GetComponent<Rigidbody2D>().velocity = new Vector3(fishComponent.fish.speed, 0, 0);
                    spawnedFish.transform.SetParent(this.transform);

                    break;
                }
            }

            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    private IEnumerator DestroyFishes()
    {
        while (true)
        {
            foreach (Transform child in this.transform)
            {
                if (child.position.x > cameraController.camRightEdge.x + 2 || child.position.y > cameraController.camTopEdge.y * 0.4 || child.position.y < -cameraController.camTopEdge.y - 2)
                {
                    Destroy(child.gameObject);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
