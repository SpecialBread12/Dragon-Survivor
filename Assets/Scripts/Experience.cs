using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    private int m_ExperiencePoint;
    private const int m_MaxExp = 100;
    public PlayerControler PlayerControler;
    private void FixedUpdate()
    {
        if (m_ExperiencePoint > m_MaxExp)
        {
            m_ExperiencePoint = 0;
            PlayerControler.LevelUp();
        }
    }
    public void GainExperience(int point)
    {
        m_ExperiencePoint += point;
    }
}
