using System;
using System.Collections.Generic;
using System.Linq;

static class ShapeConstants
{
    // X X
    // X X
    public static readonly AudienceShape square2x2;

    // X X X
    // X - X
    // X X X
    public static readonly AudienceShape circle3x3;

    // - X -
    // X X X
    // - X -
    public static readonly AudienceShape plus3x3;

    static ShapeConstants () {
        square2x2 = new AudienceShape("square2x2", 2, 2, new bool[,] { { true, true }, { true, true } });
        circle3x3 = new AudienceShape("circle3x3", 3, 3, new bool[,] { { true, true, true }, { true, false, true }, { true, true, true } });
        plus3x3 = new AudienceShape("plus3x3", 3, 3, new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } });
    }

    private static HashSet<AudienceShape> GetAllShapes()
    {
        HashSet<AudienceShape> shapes = new HashSet<AudienceShape>();
        shapes.Add(square2x2);
        shapes.Add(circle3x3);
        shapes.Add(plus3x3);
        
        return shapes;
    }

    public static AudienceShape[] ChooseRandomShapes(int count, bool uniqueShapesOnly) {
        AudienceShape[] result = new AudienceShape[count];
        HashSet<AudienceShape> possibleShapes = GetAllShapes();
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            if (uniqueShapesOnly) 
            {
                // If we've exhausted all the unique shapes but still need more, replenish the set.
                if (possibleShapes.Count == 0)
                {
                    possibleShapes = GetAllShapes();
                }
                AudienceShape uniqueShape = possibleShapes.ElementAt(random.Next(possibleShapes.Count));
                result[i] = uniqueShape;
                possibleShapes.Remove(uniqueShape);
            }
            else
            {
                result[i] = possibleShapes.ElementAt(random.Next(possibleShapes.Count));
            }
        }
        return result;
    }
}