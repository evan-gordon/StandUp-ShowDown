public class NPC
{
    public string name
    {
        get { return data.npcName; }
    }
    public int prefabNum
    {
        get { return data.id; }
    }
    public EmojiEnum[] acceptableEmoji
    {
        get { return data.acceptableEmoji; }
    }

    public Request activeRequest { get; private set; }
    public NPCData data { get; private set; }

    public NPC(NPCData data)
    {
        this.data = data; 
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

    public bool HasActiveRequest()
    {
        return activeRequest != null;
    }

    public void ClearRequest()
    {
        activeRequest = null;
    }
}


