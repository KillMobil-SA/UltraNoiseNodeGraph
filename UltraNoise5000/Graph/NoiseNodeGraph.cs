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
        private const string UpdateAllNodesName = "Update All Nodes";
        private const string UpdateAllZoomsName = "Update All Zooms";

        [SerializeField] 
        private int globalZoom = 1024;
        public int GlobalZoom => globalZoom;
        
        [Button]
        [ContextMenu(UpdateAllNodesName)]
        public void UpdateAllNodes()
        {
            for (int index = 0; index < nodes.Count; index++)
            {
                var node = nodes[index];
                if (node is NodeBase item)
                    item.Draw();
            }
        }

        [Button]
        [ContextMenu(UpdateAllZoomsName)]
        public void UpdateAllZooms()
        {
            for (int index = 0; index < nodes.Count; index++)
            {
                var node = nodes[index];
                if (node is NodeBase item)
                    item.SetZoom(globalZoom);
            }
        }
    }
}