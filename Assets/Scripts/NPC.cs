public class NPC
{
    public string name { get; private set; }
    public int prefabNum { get; private set; }
    public int[] acceptableEmoji { get; private set; }

    public Request activeRequest { get; private set; }

    public NPC(string name, int prefabNum, int[] acceptableEmoji)
    {
        this.name = name;
        this.prefabNum = prefabNum;
        this.acceptableEmoji = acceptableEmoji;
        this.activeRequest = null;
    }

    // Returns true if request is successfully set, false otherwise.
    public bool SetRequest(Request request)
    {
        if (activeRequest == null)
        {
            activeRequest = request;
            return true;
        }
        return false;
    }
}


