using System.Collections.Generic;
using NoiseUltra.Nodes;
using UnityEngine;
using XNode;

namespace NoiseUltra.Output
{
    public class TerrainLayerExportGroup : ExportNode
    {
        [SerializeField]
        private List<PaintLayer> paintLayers;
        public PaintLayer[] GetPaintLayers() => paintLayers.ToArray();

        public override void OnCreateConnection(NodePort source, NodePort target)
        {
            base.OnCreateConnection(source, target);
            paintLayers ??= new List<PaintLayer>();
            var node = source.node as PaintLayer;
            if (node == null) 
                return;
            
            if (!paintLayers.Contains(node))
            {
                paintLayers.Add(node);
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            var count = paintLayers.Count;
            for (var index = 0; index < count; ++index)
            {
                var layer = paintLayers[index];
                if (!IsConnected(layer))
                    paintLayers.Remove(layer);
            }
        }

        public TerrainLayer[] GetTerrainLayers()
        {
            var amount = paintLayers.Count;
            var terrainLayers = new TerrainLayer[amount];
            var count = paintLayers.Count;
            for (var index = 0; index < count; ++index)
            {
                var layer = paintLayers[index];
                terrainLayers[index] = layer.TerrainLayer;
            }

            return terrainLayers;
        }
    }
}