using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalParameters
{
    public static int trashPenalty = 1;         // For each trash
    public static int stainPenalty = 5;        // For each stain
    public static int furniturePenalty = 10;     // For each furniture out of place
    public static int furnitureReward = 5;      // For each furniture in place
    public static int peopleReward = 5;        // For each person in the room
    public static int glassesReward = 10;       // For each glass in a table
    public static int glassesPenalty = 5;      // If the amount of glasses is too low
    public static int foodReward = 10;          // For each food in a table
    public static int foodPenalty = 10;          // If the amount of food is too low
    public static float timePenalty = 0.5f;     // For each second that passes
}
