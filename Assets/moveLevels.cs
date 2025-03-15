using UnityEngine;

public class moveLevels : MonoBehaviour
{
    public float Speed_levels;
    public GameObject player;

    void Update()
    {
        transform.position += new Vector3(0, 0, -Speed_levels * Time.deltaTime);
    }
}
