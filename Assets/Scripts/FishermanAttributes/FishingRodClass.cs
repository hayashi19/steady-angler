using UnityEngine;

[CreateAssetMenu(fileName = "New Rod", menuName = "Rod")]
public class FishingRod : ScriptableObject
{
    // nama pancingannya (unique)
    public new string name = "pancingan1";
    
    // gambar pancingannya
    public Sprite sprite;

    // harga pancingan
    public int price = 1;

    // menandakan seberapa langkah pancingan
    public int rarity = 1;

    // level dari pancingan yang nantinya dapat dipakai untuk meningkatkan ability pancingan
    public int level = 1;

    // ability `strength` digunakan seberapa besar nantinya kenaikan bar stabilitas saat user tap-tap
    // semakin tinggi nilai `strength` semakin besar kenaikan bar stabilitas
    public int strength = 1;

    // ability `sensitivity` dapat mengontrol seberapa sering bar stabilitas dapat berpindah-pindah
    // semakin tinggi nilai `stabilitas` semakin jarang ber stabilitas berpindah-pindah
    // nantinya ini akan saling berbenturan dengan ability `strength` fish (@SEE_AT strength class Fish)
    public int sensitivity = 1;
}
