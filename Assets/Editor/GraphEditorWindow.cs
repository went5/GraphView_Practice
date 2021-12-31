using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// GraphViewを子にもつEditorWindow
public class GraphEditorWindow : EditorWindow
{
    [MenuItem("WentTool/Open GraphView")]
    public static void Open()
    {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();
        graphEditorWindow.titleContent = new GUIContent("GraphEditor");
    }

    private void OnEnable()
    {
        var graphView = new MyGraphView();
        rootVisualElement.Add(graphView);

        // 削除予定
        rootVisualElement.Add(new Button(graphView.ExecuteAllNode) {text = "Execute"});
    }
}