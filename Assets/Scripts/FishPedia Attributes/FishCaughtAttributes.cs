using UnityEngine.UI;

public class FishCaughtAttributes
{
    private int ClownFish = 0;
    private int Marlin = 0;
    private int TigerShark = 0;


    public FishCaughtAttributes(int clownFish, int Marlin, int TigerShark)
    {
        this.ClownFish = clownFish;
        this.Marlin = Marlin;
        this.TigerShark = TigerShark;
    }
    public string IkanBadutTeks()
    {
        return ClownFish.ToString();
    }
    public string IkanMarlinTeks()
    {
        return Marlin.ToString();
    }
    public string IkanTigerText()
    {
        return TigerShark.ToString();
    }
    public void checkNangkap(bool badut, bool marlin, bool tiger)
    {
        if (badut) { ClownFish++; }
        if (marlin) { Marlin++; }
        if (tiger) { TigerShark++; }
    }
}
