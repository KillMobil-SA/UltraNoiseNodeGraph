using System.Collections.Generic;
using UnityEngine;
using XNode;
using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Output
{
    [Serializable,NodeTint(NodeColor.PAINTEXPORT)]
    public class TerrainLayerExportGroup : ExportNode
    {
        [SerializeField]
        private List<PaintLayer> paintLayers;
        public PaintLayer[] GetPaintLayers() => paintLayers.ToArray();

        public override void OnCreateConnection(NodePort source, NodePort target)
        {
            base.OnCreateConnection(source, target);
            paintLayers ??= new List<PaintLayer>();
            PaintLayer node = source.node as PaintLayer;
            if (node == null)
            {
                return;
            }
            
            if (!paintLayers.Contains(node))
            {
                paintLayers.Add(node);
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            int count = paintLayers.Count;
            for (int index = 0; index < count; ++index)
            {
                PaintLayer layer = paintLayers[index];
                if (!IsConnected(layer))
                {
                    paintLayers.Remove(layer);
                }
            }
        }

        public TerrainLayer[] GetTerrainLayers()
        {
            int amount = paintLayers.Count;
            TerrainLayer[] terrainLayers = new TerrainLayer[amount];
            int count = paintLayers.Count;
            for (int index = 0; index < count; ++index)
            {
                PaintLayer layer = paintLayers[index];
                terrainLayers[index] = layer.TerrainLayer;
            }

            return terrainLayers;
        }
    }
}