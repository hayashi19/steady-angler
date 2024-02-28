using UnityEngine;

[System.Serializable]
public class FishingBait
{
    // nama umpan (unique)
    public string name = "umpan1";

    // gambar umpannya
    public Sprite sprite;

    // harga umpan
    public int price = 1;

    // menandakan seberapa langkah umpan
    public int rarity = 1;

    // jumlah umpan yang dipunya
    public int quantity = 0;

    // seberapa cepat nantinya akan mendapatkan ikan
    // setiap ikan nantinya punya treshold untuk nilai `attractiveness` dengan sebuah umpan,
    // jika nilai `attractiveness` ikan > `attractiveness` umpan, makan tidak akan pernah bisa dapat ikan itu (@SEE_AT attractiveness class Fish)
    public int attractiveness = 1;
}
