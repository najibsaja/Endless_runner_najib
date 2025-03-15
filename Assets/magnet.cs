using UnityEngine;
using System.Collections;

public class MagnetObject : MonoBehaviour
{
    public float magnetRadius = 5f; 
    public float magnetSpeed = 5f;   
    public bool isActive = false;    

    void Update()
    {
        if (isActive)
        {
            AttractCoins();
        }
    }

    void AttractCoins()
    {
        Collider[] coins = Physics.OverlapSphere(transform.position, magnetRadius);

        foreach (Collider coin in coins)
        {
            if (coin.CompareTag("coins")) 
            {
                StartCoroutine(PullCoin(coin.transform));
            }
        }
    }

    IEnumerator PullCoin(Transform coin)
    {
        while (coin != null && Vector3.Distance(coin.position, transform.position) > 0.5f)
        {
            coin.position = Vector3.Lerp(coin.position, transform.position, magnetSpeed * Time.deltaTime);
            yield return null;
        }

        if (coin != null)
        {
            Destroy(coin.gameObject);  
        }
    }

    public void ActivateMagnet(float duration)
    {
        StartCoroutine(MagnetDuration(duration));
    }

    IEnumerator MagnetDuration(float duration)
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}

