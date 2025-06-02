using UnityEngine;

public class barrier2 : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        //player.transform.position = new Vector3(2, player.transform.position.y, player.transform.position.z);

        if (other.gameObject == player)
        {
            player.GetComponent<PlayerMovements>().bataskanan = true;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerMovements>().bataskanan = false;
        }
    }
}
