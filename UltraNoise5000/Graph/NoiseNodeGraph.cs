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
        [Button]
        public void UpdateAllValues()
        {
            foreach (var node in nodes)
                if (node is NodeBase item)
                    item.Update();
        }
    }
}