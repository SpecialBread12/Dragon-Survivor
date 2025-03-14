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

    public void DisplayHP(float a_HP)
    {
        m_TextHP.text = a_HP.ToString();
    }

    public void DisplayPoint(int a_Point)
    {
        m_TextPoint.text = a_Point.ToString();
    }
}
