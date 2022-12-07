using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Deadplayer")
        {
            SceneManager.LoadScene(0);
        }

        if (other.tag == "fin")
        {
            SceneManager.LoadScene(2);
        }
        if (other.tag == "fin2")
        {
            SceneManager.LoadScene(0);
        }
    }
}
