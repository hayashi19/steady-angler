using UnityEngine;

[System.Serializable]
public class Fish
{
    // nama ikan (unique)
    public string name = "ikan1";

    // gambar ikannya
    public Sprite sprite;

    // harga ikan
    public int price = 1;

    // menandakan seberapa langkah ikan
    public int rarity = 1;

    // ability `strength` akan memengaruhi seberapa sering bar stabilitas dapat berpindah-pindah
    // semakin tinggi akan membuat bar stabilitas sering berpindah-pindah
    // nantinya ini akan saling berbenturan dengan ability `sensitivity` fishing rod (@SEE_AT sensitivity class FishingRod)
    public int strength = 1;

    // ability `attractiveness` nilai treshold untuk ikan tertarik dengan umpan (@SEE_AT `attractiveness` class FishingBait)
    public int attractiveness = 1;
}
