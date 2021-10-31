using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra
{
    /// <summary>
    ///     Class packs all nodes of a graph and serializes them into a single file.
    /// </summary>
    [CreateAssetMenu(fileName = "Ultra Noise Graph", menuName = "KillMobil/UltraNoise/Noise Graph")]
    public class NoiseNodeGraph : NodeGraph
    {
        private const string UPDATE_ALL_NODES_NAME = "Update All Nodes";
        private const string UPDATE_ALL_ZOOMS_NAME = "Update All Zooms";

        [SerializeField] 
        private int globalZoom = 1024;
        public int GlobalZoom => globalZoom;
        
        [Button]
        [ContextMenu(UPDATE_ALL_NODES_NAME)]
        public void UpdateAllNodes()
        {
            int count = nodes.Count;
            for (int index = 0; index < count; index++)
            {
                Node node = nodes[index];
                if (node is NodeBase item)
                {
                    item.DrawSync();
                }
            }
        }

        [Button]
        [ContextMenu(UPDATE_ALL_ZOOMS_NAME)]
        public void UpdateAllZooms()
        {
            int count = nodes.Count;
            for (int index = 0; index < count; index++)
            {
                var node = nodes[index];
                if (node is NodeBase item)
                {
                    item.SetZoom(globalZoom);
                }
            }
        }
    }
}