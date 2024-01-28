using System;
using System.Collections.Generic;
using System.Linq;

static class NPCConstants
{
    private static readonly List<NPC> npcs = new List<NPC>();

    static NPCConstants()
    {
        // Wants: Soda, Snowflake, Meat, Poop
        npcs.Add(new NPC("polar bear", 0, new int[] { 1, 2, 3, 0 }));

        // Wants: Meat, Person, Fire, Poop
        npcs.Add(new NPC("evil polar bear", 1, new int[] { 3, 4, 5, 0}));
    }

    public static NPC GetNPC(int id)
    {
        // not safe, maybe fix later
        return npcs[id];
    }
}