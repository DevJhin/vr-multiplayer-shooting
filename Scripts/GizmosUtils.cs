using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosUtils
{
    public static void DrawArrow2D(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);

        float arrowBodyLength = distance * 0.3f;
        Vector3 arrowBodyDir = (end - start).normalized;

        Vector3 arrowTipPos = start + arrowBodyDir * arrowBodyLength;

        Gizmos.DrawLine(start, arrowTipPos);

        Vector3 arrowLeftDir = Quaternion.Euler(0, 30, 0) * -arrowBodyDir;
        Vector3 arrowRightDir = Quaternion.Euler(0, -30, 0) * -arrowBodyDir;

        Gizmos.DrawLine(arrowTipPos, arrowTipPos + arrowLeftDir * 0.2f);
        Gizmos.DrawLine(arrowTipPos, arrowTipPos + arrowRightDir * 0.2f);

    }

    public static void DrawArrow3D(Vector3 start, Vector3 end, float thickness = 0.1f)
    {
        float distance = Vector3.Distance(start, end);

        float arrowBodyLength = distance * 0.3f;
        Vector3 arrowBodyDir = (end - start).normalized;
        Vector3 arrowTipPos = start + arrowBodyDir * arrowBodyLength;

        DrawLineWithCube(start, arrowTipPos, thickness);

        Vector3 arrowLeftDir = Quaternion.Euler(0, 30, 0) * -arrowBodyDir;
        Vector3 arrowRightDir = Quaternion.Euler(0, -30, 0) * -arrowBodyDir;

        Gizmos.DrawLine(arrowTipPos, arrowTipPos + arrowLeftDir * 0.1f);
        Gizmos.DrawLine(arrowTipPos, arrowTipPos + arrowRightDir * 0.1f);

    }

    public static void DrawLineWithCube(Vector3 start, Vector3 end,  float thickness)
    {
        float length = Vector3.Distance(start, end);
        Vector3 cubePos = (start + end) * 0.5f;
        Vector3 cubeScale = new Vector3(thickness, 0.01f, length);

        Quaternion rotation = Quaternion.LookRotation(end - start, Vector3.up);
        var tempMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, rotation,Vector3.one);
        Gizmos.DrawCube(cubePos, cubeScale);
        Gizmos.matrix = tempMatrix;
    }
}
