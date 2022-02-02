using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphEditor : EditorWindow
{
    static Graph graph;

    // Adds the graph editor to the window menu and allows it to be opened
    [MenuItem("Window/Graph Editor")]
    public static void ShowWindow()
    {
        GetWindow<GraphEditor>();
        graph = new Graph();
    }

    // Called every frame that the editor window is in focus
    private void OnGUI()
    {
        if (graph == null)
            graph = new Graph();
        graph.Render();
        graph.Input();
    }
}
