using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphEditor : EditorWindow
{
    static Graph graph;

    [MenuItem("Window/Graph Editor")]
    public static void ShowWindow()
    {
        GetWindow<GraphEditor>();
        graph = new Graph();
    }

    private void OnGUI()
    {
        if (graph == null)
            graph = new Graph();
        graph.Render();
        graph.Input();
    }
}
