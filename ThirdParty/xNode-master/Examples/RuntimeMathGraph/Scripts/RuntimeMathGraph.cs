using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XNode.Examples.MathNodes;

namespace XNode.Examples.RuntimeMathNodes
{
    public class RuntimeMathGraph : MonoBehaviour, IPointerClickHandler
    {
        [Header("Graph")] public MathGraph graph;

        [Header("Prefabs")] public UGUIMathNode runtimeMathNodePrefab;

        public UGUIVector runtimeVectorPrefab;
        public UGUIDisplayValue runtimeDisplayValuePrefab;
        public Connection runtimeConnectionPrefab;

        [Header("References")] public UGUIContextMenu graphContextMenu;

        public UGUIContextMenu nodeContextMenu;
        public UGUITooltip tooltip;
        private List<UGUIMathBaseNode> nodes;

        public ScrollRect scrollRect { get; private set; }

        private void Awake()
        {
            // Create a clone so we don't modify the original asset
            graph = graph.Copy() as MathGraph;
            scrollRect = GetComponentInChildren<ScrollRect>();
            graphContextMenu.onClickSpawn -= SpawnNode;
            graphContextMenu.onClickSpawn += SpawnNode;
        }

        private void Start()
        {
            SpawnGraph();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
                return;

            graphContextMenu.OpenAt(eventData.position);
        }

        public void Refresh()
        {
            Clear();
            SpawnGraph();
        }

        public void Clear()
        {
            for (var i = nodes.Count - 1; i >= 0; i--) Destroy(nodes[i].gameObject);
            nodes.Clear();
        }

        public void SpawnGraph()
        {
            if (nodes != null) nodes.Clear();
            else nodes = new List<UGUIMathBaseNode>();

            for (var i = 0; i < graph.nodes.Count; i++)
            {
                var node = graph.nodes[i];

                UGUIMathBaseNode runtimeNode = null;
                if (node is MathNode)
                    runtimeNode = Instantiate(runtimeMathNodePrefab);
                else if (node is Vector)
                    runtimeNode = Instantiate(runtimeVectorPrefab);
                else if (node is DisplayValue) runtimeNode = Instantiate(runtimeDisplayValuePrefab);
                runtimeNode.transform.SetParent(scrollRect.content);
                runtimeNode.node = node;
                runtimeNode.graph = this;
                nodes.Add(runtimeNode);
            }
        }

        public UGUIMathBaseNode GetRuntimeNode(Node node)
        {
            for (var i = 0; i < nodes.Count; i++)
                if (nodes[i].node == node)
                    return nodes[i];

            return null;
        }

        public void SpawnNode(Type type, Vector2 position)
        {
            var node = graph.AddNode(type);
            node.name = type.Name;
            node.position = position;
            Refresh();
        }
    }
}