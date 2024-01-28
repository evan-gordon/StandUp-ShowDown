public class NPC
{
    public string name { get; private set; }
    public int prefabNum { get; private set; }
    public int[] acceptableEmoji { get; private set; }

    public NPC(string name, int prefabNum, int[] acceptableEmoji)
    {
        this.name = name;
        this.prefabNum = prefabNum;
        this.acceptableEmoji = acceptableEmoji;
    }
}


