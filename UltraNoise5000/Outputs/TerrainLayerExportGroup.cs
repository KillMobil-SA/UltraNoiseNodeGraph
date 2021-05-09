using System.Collections.Generic;
using NoiseUltra.Nodes;
using UnityEngine;
using XNode;

namespace NoiseUltra.Output
{
    public class TerrainLayerExportGroup : ExportNode
    {
        [SerializeField] private List<PaintLayer> paintLayers;

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            paintLayers ??= new List<PaintLayer>();
            var node = @from.node as PaintLayer;
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

            for (var index = 0; index < paintLayers.Count; index++)
            {
                var layer = paintLayers[index];
                if (!IsConnected(layer))
                    paintLayers.Remove(layer);
            }
        }
        
        public PaintLayer[] GetPaintLayers() => paintLayers.ToArray();

        public TerrainLayer[] GetTerrainLayers()
        {
            var amount = paintLayers.Count;
            var terrainLayers = new TerrainLayer[amount];
            for (var index = 0; index < paintLayers.Count; index++)
            {
                var layer = paintLayers[index];
                terrainLayers[index] = layer.TerrainLayer;
            }

            return terrainLayers;
        }
    }
}