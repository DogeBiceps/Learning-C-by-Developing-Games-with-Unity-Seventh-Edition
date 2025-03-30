using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject button;
    public Player player;

    private void Start()
    {
        button.SetActive(false);
    }
    void Update()
    {
        if (player.isDead == true)
        {
            button.SetActive(true);
        }
    }
}
