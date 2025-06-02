using UnityEngine;

public class level_generator : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject spawnlevel;
    public float offset_spawn_z;
    public float offset_spawn_y;
    public float offset_spawn_x;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int nextObstacle = Random.Range(0, levels.Length); 
            Instantiate(levels[nextObstacle], new Vector3(offset_spawn_x, offset_spawn_y, offset_spawn_z), Quaternion.identity);
        }
    }
}
