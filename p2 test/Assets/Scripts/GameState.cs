using UnityEngine;

public static class GameState
{
    public static int energy { get; private set; } = 50;
    public static int stress { get; private set; } = 50;

    public static void AddEnergy(int amount)
    {
        energy += amount;
        Debug.Log("Energy: " + energy);
    }

    public static void AddStress(int amount)
    {
        stress += amount;
        Debug.Log("Stress: " + stress);
    }

    public static void SubtractEnergy(int amount)
    {
        energy -= amount;
        Debug.Log("Energy: " + energy);
    }

    public static void SubtractStress(int amount)
    {
        stress -= amount;
        Debug.Log("Stress: " + stress);
    }

    public static void PrintStatus()
    {
        Debug.Log($"[STATUS] Energy: {energy}, Stress: {stress}");
    }
}
