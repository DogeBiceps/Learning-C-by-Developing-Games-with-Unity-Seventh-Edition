using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddEnergy(int amount) => GameState.AddEnergy(amount);
    public void AddStress(int amount) => GameState.AddStress(amount);
    public void SubtractEnergy(int amount) => GameState.SubtractEnergy(amount);
    public void SubtractStress(int amount) => GameState.SubtractStress(amount);
    public void PrintStatus() => GameState.PrintStatus();
}
