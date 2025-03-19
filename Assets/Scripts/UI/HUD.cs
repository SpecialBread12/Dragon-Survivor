using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextHP;
    [SerializeField]
    private TextMeshProUGUI m_TextPoint;
    [SerializeField]
    private TextMeshProUGUI m_TextLevel;

    public void DisplayHP(float a_HP)
    {
        m_TextHP.text = a_HP.ToString();
    }

    public void DisplayPoint(int a_Point)
    {
        m_TextPoint.text = a_Point.ToString();
    }

    public void DisplayLevel(int a_Level)
    {
        m_TextLevel.text = "Level : " + a_Level.ToString();
    }
}
