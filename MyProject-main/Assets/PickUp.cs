using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    public float coin = 0f;
    public AudioSource collectSound;
    public TextMeshProUGUI textCoins;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Coin")
        {
            coin++;
            textCoins.text = coin.ToString();

            Destroy(collider.gameObject);
            
        }
    }


    public void Update()
    {
        if (coin == 20f)
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
        }
    }



}
