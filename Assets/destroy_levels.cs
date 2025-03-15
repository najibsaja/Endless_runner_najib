using UnityEngine;

public class destroy_levels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("levels"))
        {
            Destroy(other.gameObject);
        }
    }
}
