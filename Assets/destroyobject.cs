using UnityEngine;

public class destroyobject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("destroyer"))
        {
            Destroy(this.gameObject);
        }
    }
}
