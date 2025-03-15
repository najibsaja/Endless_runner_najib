using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class tengahpos : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        //player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
    }
}
