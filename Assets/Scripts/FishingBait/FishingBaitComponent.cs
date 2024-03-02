using UnityEngine;

public class FishingBaitComponent : MonoBehaviour
{
    [SerializeField] private AudioController audioBait;

    private bool isFishing = false;
    private FishComponent caughtFish = null;
    private Rigidbody2D baitRb;
    private SpringJoint2D fishermanBaitJoint;

    private void Start()
    {
        baitRb = GetComponent<Rigidbody2D>();
        fishermanBaitJoint = GetComponent<SpringJoint2D>();

        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        caughtFish =  collision.GetComponent<FishComponent>();
        Debug.Log(collision.tag + " " + caughtFish.fish.name + " " + caughtFish.fish.isCaught);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        caughtFish = null;
        Debug.Log(collision.tag + " " + (caughtFish == null));
    }

    private void Respawn()
    {
        fishermanBaitJoint.distance = 0.56f;
        fishermanBaitJoint.frequency = 0;
    }

    public void ThrowBait()
    {
        if (isFishing && caughtFish == null)
        {
            isFishing = !isFishing;
         
            Respawn();
        }
        else if (isFishing && caughtFish != null)
        {
            caughtFish.fish.isCaught = true;
            caughtFish.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //fishermanBaitJoint = caughtFish.GetComponent<SpringJoint2D>();
            //fishermanBaitJoint.connectedBody = this.GetComponent<Rigidbody2D>();
            //fishermanBaitJoint.autoConfigureDistance = false;
            //fishermanBaitJoint.distance = 2f;

            //fishermanBaitJoint.enabled = true;

            Debug.Log("Catching");
        }
        else
        {
            isFishing = !isFishing;

            fishermanBaitJoint.distance = 3.2f;
            fishermanBaitJoint.frequency = 1f;

            baitRb.AddForce(new Vector2(2.4f, 16f), ForceMode2D.Impulse);

            audioBait.PlayThrowBaitEffect();
        }
    }
}
