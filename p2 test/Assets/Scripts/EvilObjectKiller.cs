using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Evil"))
        {
            Destroy(other.gameObject);

        }
        else
        {
            return;
        }
    }
}