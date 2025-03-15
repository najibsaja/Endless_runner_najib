using UnityEngine;

public class level_generator : MonoBehaviour
{
    public GameObject[] levels;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int nextObstacle = Random.Range(0, levels.Length); 
            Instantiate(levels[nextObstacle], new Vector3(0, 0, 48), Quaternion.identity);
        }
    }
}
