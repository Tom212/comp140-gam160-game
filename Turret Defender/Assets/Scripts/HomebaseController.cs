using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomebaseController : MonoBehaviour {

    public int homebaseHealth;
    public int tankDamage;

    public Slider healthUI;

    void Start()
    {

        healthUI.maxValue = homebaseHealth;
        healthUI.value = homebaseHealth;

    }
	
    void OnTriggerEnter (Collider entrant)
    {
        
        if (entrant.gameObject.CompareTag("Tank"))
        {
          
            TankReceived(entrant.gameObject);

        }

    }

    void TankReceived (GameObject enemy)
    {

        TakeDamage();
        Destroy(enemy, 4f);

    }

    void TakeDamage ()
    {

        homebaseHealth -= tankDamage;
        //Debug.Log("Taking damage!");

        healthUI.value = homebaseHealth;

        if (homebaseHealth <= 0)
        {
            Debug.Log("Game over!");
            SceneManager.LoadScene(0);
        }

    }

}
