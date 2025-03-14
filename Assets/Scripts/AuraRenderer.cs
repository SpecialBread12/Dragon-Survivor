using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraRenderer : MonoBehaviour
{
    public int segments = 50; // Nombre de points pour le cercle
    public float radius = 2f; // Rayon de la zone
    private LineRenderer line;

    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0));
            angle += 2 * Mathf.PI / segments;
        }
    }

    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        DrawCircle();
    }
}