using System;
using System.Collections.Generic;
using System.Linq;

static class NPCConstants
{
    // Wants: Soda, Snowflake, Meat(?)
    public static readonly NPC polarbear;

    // Wants: Meat, Person, Fire
    public static readonly NPC evilPolarbear;

    static NPCConstants()
    {
        polarbear = new NPC("polar bear", 0, new int[] {1, 2, 3, 0});
        evilPolarbear = new NPC("evil polar bear", 1, new int[] { 3, 4, 5, 0});
    }
}