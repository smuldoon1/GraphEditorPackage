using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphEditor : EditorWindow
{
    static Graph graph;

    static GraphEditor window;

    // Adds the graph editor to the window menu and allows it to be opened
    [MenuItem("Window/Graph Editor")]
    public static void ShowWindow()
    {
        window = GetWindow<GraphEditor>();
        graph = new Graph();

        window.minSize = new Vector2(800, 500);
    }

    // Called every frame that the editor window is in focus
    private void OnGUI()
    {
        if (graph == null)
            graph = new Graph();
        if (window == null)
            window = GetWindow<GraphEditor>();

        Rect graphView = new Rect(window.position.width * 0.2f, 0f, window.position.width * 0.8f, window.position.height);
        GUI.BeginGroup(graphView);
        Texture backgroundTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/grid.png");
        GUI.DrawTextureWithTexCoords(new Rect(0, graphView.height, graphView.width, -graphView.height), backgroundTexture, new Rect(0, 0, graphView.width / backgroundTexture.width, graphView.height / backgroundTexture.height));
        graph.Render();
        if (new Rect(0, 0, window.position.width * 0.8f, window.position.height).Contains(Event.current.mousePosition))
            graph.Input();
        GUI.EndGroup();

        Rect settingsView = new Rect(0f, 0f, window.position.width * 0.2f, window.position.height);
        GUI.BeginGroup(settingsView);
        EditorGUILayout.TextField("");
        GUI.EndGroup();
    }
}
