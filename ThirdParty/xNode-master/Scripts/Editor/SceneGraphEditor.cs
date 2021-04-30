using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace XNodeEditor
{
    [CustomEditor(typeof(SceneGraph), true)]
    public class SceneGraphEditor : Editor
    {
        private Type graphType;
        private bool removeSafely;
        private SceneGraph sceneGraph;

        private void OnEnable()
        {
            sceneGraph = target as SceneGraph;
            var sceneGraphType = sceneGraph.GetType();
            if (sceneGraphType == typeof(SceneGraph))
            {
                graphType = null;
            }
            else
            {
                var baseType = sceneGraphType.BaseType;
                if (baseType.IsGenericType) graphType = sceneGraphType = baseType.GetGenericArguments()[0];
            }
        }

        public override void OnInspectorGUI()
        {
            if (sceneGraph.graph == null)
            {
                if (GUILayout.Button("New graph", GUILayout.Height(40)))
                {
                    if (graphType == null)
                    {
                        var graphTypes = typeof(NodeGraph).GetDerivedTypes();
                        var menu = new GenericMenu();
                        for (var i = 0; i < graphTypes.Length; i++)
                        {
                            var graphType = graphTypes[i];
                            menu.AddItem(new GUIContent(graphType.Name), false, () => CreateGraph(graphType));
                        }

                        menu.ShowAsContext();
                    }
                    else
                    {
                        CreateGraph(graphType);
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Open graph", GUILayout.Height(40))) NodeEditorWindow.Open(sceneGraph.graph);
                if (removeSafely)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Really remove graph?");
                    GUI.color = new Color(1, 0.8f, 0.8f);
                    if (GUILayout.Button("Remove"))
                    {
                        removeSafely = false;
                        Undo.RecordObject(sceneGraph, "Removed graph");
                        sceneGraph.graph = null;
                    }

                    GUI.color = Color.white;
                    if (GUILayout.Button("Cancel")) removeSafely = false;
                    GUILayout.EndHorizontal();
                }
                else
                {
                    GUI.color = new Color(1, 0.8f, 0.8f);
                    if (GUILayout.Button("Remove graph")) removeSafely = true;
                    GUI.color = Color.white;
                }
            }

            DrawDefaultInspector();
        }

        public void CreateGraph(Type type)
        {
            Undo.RecordObject(sceneGraph, "Create graph");
            sceneGraph.graph = CreateInstance(type) as NodeGraph;
            sceneGraph.graph.name = sceneGraph.name + "-graph";
        }
    }
}