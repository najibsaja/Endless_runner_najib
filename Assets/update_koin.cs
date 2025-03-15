using UnityEngine;
using TMPro;

public class update_koin : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textKoinAkhir;

    // Update is called once per frame
    void Update()
    {
        text.text = "Koin: " + player.GetComponent<PlayerMovements>(). coin_counter;
        textKoinAkhir.text = "Koin: " + player.GetComponent<PlayerMovements>(). coin_counter;
    }
}
