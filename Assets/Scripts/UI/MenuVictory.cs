using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictory : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextScore;
    private void OnEnable()
    {
        //Debug.Log("Menu-Pause Enable");
        MenuManager.Instance.HUD.gameObject.SetActive(false);

    }
    public void DisplayScore(int a_Money)
    {
        m_TextScore.text = ("Votre Score : " + (a_Money).ToString());
    }
    public void Reload()
    {
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
        MenuManager.Instance.HUD.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MenuStart", LoadSceneMode.Single);
    }
}
