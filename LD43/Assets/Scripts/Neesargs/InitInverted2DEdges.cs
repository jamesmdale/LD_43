using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInverted2DEdges : MonoBehaviour {

    public int NumEdges;
    public float Radius;

    // Use this for initialization
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = NumEdges + 1;
        lineRenderer.useWorldSpace = false;

        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[NumEdges + 1];

        for (int i = 0; i < NumEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / NumEdges;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);

            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0.0f));
        }

        points[NumEdges] = points[0];
        lineRenderer.SetPosition(NumEdges, new Vector3(points[0].x, points[0].y, 0.0f));

        edgeCollider.points = points;
    }
}
