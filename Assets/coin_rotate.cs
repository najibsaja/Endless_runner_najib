using UnityEditor.Timeline.Actions;
using UnityEngine;

public class coin_rotate : MonoBehaviour
{
    public float rotate_spd = 0;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotate_spd * Time.deltaTime);
    }
}
