using UnityEngine;

public class GameStateComponent : MonoBehaviour
{
    //// audio
    //[SerializeField] private AudioController audioBait;

    //// spawn fish

    //// catching

    //// bait
    //private bool isFishing = false;
    //private FishComponent caughtFish = null;
    //private Rigidbody2D rb;
    //private SpringJoint2D sj;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    sj = GetComponent<SpringJoint2D>();

    //    Respawn();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    caughtFish = collision.GetComponent<FishComponent>();
    //    Debug.Log(collision.tag + " " + caughtFish.fish.name + " " + caughtFish.fish.isCaught);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    caughtFish = null;
    //    Debug.Log(collision.tag + " " + (caughtFish == null));
    //}

    //private void Respawn()
    //{
    //    sj.distance = 0.56f;
    //    sj.frequency = 0;
    //}

    //public void ThrowBait()
    //{
    //    if (isFishing && caughtFish == null)
    //    {
    //        isFishing = !isFishing;

    //        Respawn();
    //    }
    //    else if (isFishing && caughtFish != null)
    //    {
    //        Debug.Log("Catching");
    //    }
    //    else
    //    {
    //        isFishing = !isFishing;

    //        sj.distance = 2.4f;
    //        sj.frequency = 1f;

    //        rb.AddForce(new Vector2(2.4f, 16f), ForceMode2D.Impulse);

    //        audioBait.PlayThrowBaitEffect();
    //    }
    //}
}
