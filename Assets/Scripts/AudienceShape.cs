public class AudienceShape
{
    public string name { get; private set; }
    public int width {  get; private set; }
    public int height { get; private set; }
    public bool[,] layout { get; private set; }

    public AudienceShape(string name, int width, int height, bool[,] layout)
    {
        this.name = name;
        this.width = width;
        this.height = height;
        this.layout = layout;
    }
}
