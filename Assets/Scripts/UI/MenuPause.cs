using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{


    private void OnEnable()
    {
        Debug.Log("Menu-Pause Enable");

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Debug.Log("Menu-Pause Crippled");

        Time.timeScale = 1;
    }

    public void Resume()
    {
        this.gameObject.SetActive(false);
    }
}
