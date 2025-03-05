using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Titre;

    [SerializeField]
    private GameObject m_BtnStart;

    [SerializeField]
    private GameObject m_BtnInstruction;

    [SerializeField]
    private GameObject m_BtnExit;


    [SerializeField]
    private GameObject m_Panel;

    public void StartJeu()
    {
        Debug.Log("Menu- Start");

        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }

    public void Instruction()
    {
        Debug.Log("Menu- Intruction");
        m_Titre.gameObject.SetActive(false);
        m_BtnStart.gameObject.SetActive(false);
        m_BtnInstruction.gameObject.SetActive(false);
        m_BtnExit.gameObject.SetActive(false);
        m_Panel.gameObject.SetActive(true);

    }

    public void InstructionExit()
    {
        Debug.Log("Menu- IntructionExit");
        m_Titre.gameObject.SetActive(true);
        m_BtnStart.gameObject.SetActive(true);
        m_BtnInstruction.gameObject.SetActive(true);
        m_BtnExit.gameObject.SetActive(true);
        m_Panel.gameObject.SetActive(false);

    }

    public void Exit()
    {
        Debug.Log("Menu- Exit");

        Application.Quit();
    }
}
