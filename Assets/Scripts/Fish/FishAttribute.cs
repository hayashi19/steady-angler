using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "ScriptableObjects/Fish", order = 1)]
public class FishAttribute : ScriptableObject
{
    public new string name = "ikan1";
    public Sprite sprite;
    [Range(0f, 1f)] public float rarity = 1;
    public int point = 1;
    [Range(0f, 2f)] public float speed = 1;
    [Range(0f, 1f)] public float strenght = 1;
    public int max = 500;
    public int min = 500;
}
