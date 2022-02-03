using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OutputConnector : GraphNodeConnector
{
    bool selected = false;

    public InputConnector connection;

    public bool Selected
    {
        set { selected = value; color = value ? Color.red : Color.white; }
    }

    public void Connect(InputConnector inputConnection)
    {
        connection = inputConnection;
        inputConnection.AddConnection(this);
    }

    new public void Draw()
    {
        base.Draw();
        if (connection != null || selected)
        {
            Vector3 endPosition = selected ? Event.current.mousePosition : connection.Rect.center;
            float distanceFraction = Vector3.Distance(Rect.center, endPosition) * .4f;
            if (distanceFraction > 0f)
                Handles.DrawBezier(Rect.center, endPosition, (Vector3)Rect.center + Vector3.right * distanceFraction, endPosition + Vector3.left * distanceFraction, color, null, 5f);
        }
    }
}
