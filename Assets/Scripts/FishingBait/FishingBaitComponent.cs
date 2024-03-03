using UnityEngine;

enum FishingState
{
    // saat fisherman diam
    Idle,
    // saat fisherman telah melempar umpan, dan umpan belum dimanakan ikan
    Waiting,
    // saat umpan telah dimanakan ikan, tapi player masih diam
    Baited,
    // setelah umpan dimakan ikan, dan player akan tap tap mancing
    Catching
}

public class FishingBaitComponent : MonoBehaviour
{
    // data sifat dari bait itu
    public FishingBait bait;

    // audio
    [SerializeField] private AudioController audioBait;

    private FishingState fishingState = FishingState.Idle;

    // ikan yang tertangkap
    private FishComponent caughtFish = null;

    private void Start()
    {
        // pastingkan posisi umpan berada di tempat spawnnya
        TakeBait();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // tidak ada ikan yang ditangkap? dan ikan datang
        // ya: masukan ikan yang collide jadi target ikan
        if (caughtFish == null) caughtFish = collision.GetComponent<FishComponent>();
        //Debug.Log(caughtFish);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ada ikan yang akan ditangkap? dan ikannya pergi
        // ya: lepas ikan tersebut
        if (caughtFish != null) caughtFish = null;
        //Debug.Log(caughtFish);
    }

    public void Fishing()
    {
        Debug.Log(fishingState + " " + caughtFish);

        // ketika fisherman dari diam
        // lalu user tekan tombol fishing
        // ummpan akan dilemparkan
        if (fishingState == FishingState.Idle)
        {
            ThrowBait();

            Debug.Log("Throw Bait");
        }

        // ketika umpan sudah dilempar
        // user menekan tombol fishing
        // dan ikan tidak ada
        // maka umpan ditarik lagi
        else if (fishingState == FishingState.Waiting && caughtFish == null)
        {
            TakeBait();

            Debug.Log("Take Bait No Fish");
        }

        // ketika umpan sudah dilempar
        // user menekan tombol fishing
        // dan ikan ada
        // maka ikan akan ditangkap
        else if (fishingState == FishingState.Waiting && caughtFish != null)
        {
            Baited();
            Debug.Log("Take Bait With Fish");
        }

        // ketika umpan sudah dimakan
        // user menekan tombol fishing
        // maka akan terjadi process tarik menarik dengan ikan
        else if(fishingState == FishingState.Baited)
        {
            Catching();
            Debug.Log("PULL PULL PULL!!!!");
        }
    }

    private void ThrowBait()
    {
        fishingState = FishingState.Waiting;

        // melempar umpan
        Rigidbody2D baitRb = this.GetComponent<Rigidbody2D>();
        baitRb.AddForce(new Vector2(2.4f, 16f), ForceMode2D.Impulse);

        // mengubah ukuran panjang maksimal joint
        SpringJoint2D fishermanBaitJoint = this.GetComponent<SpringJoint2D>();
        fishermanBaitJoint.distance = 3.2f;
        fishermanBaitJoint.frequency = 1f;

        audioBait.PlayThrowBaitEffect();
    }

    private void TakeBait()
    {
        fishingState = FishingState.Idle;
     
        // menarik lagi umpan dengan cara reset panjang maksimal joint
        SpringJoint2D fishermanBaitJoint = this.GetComponent<SpringJoint2D>();
        fishermanBaitJoint.distance = 0.56f;
        fishermanBaitJoint.frequency = 0;
    }

    private void Baited()
    {
        fishingState = FishingState.Baited;

        // menggabungkan ikan yang akan ditangkap dengan umpan
        SpringJoint2D baitFishJoint = caughtFish.GetComponent<SpringJoint2D>();
        baitFishJoint.connectedBody = this.GetComponent<Rigidbody2D>();
        baitFishJoint.autoConfigureDistance = false;
        baitFishJoint.distance = 0.005f;
        baitFishJoint.enabled = true;
    }

    private void Catching()
    {
        //fishingState = FishingState.Waiting;
    }
}
