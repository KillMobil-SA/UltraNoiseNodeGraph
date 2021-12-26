using System.Collections.Generic;
using UnityEngine;
using XNode;
using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;

namespace NoiseUltra.Output
{
    [Serializable,NodeTint(NodeColor.PAINTEXPORT)]
    public class PaintLayerExportGroup : ExportNode
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

    [Button]
        private void UpdateList()
        {
            int count = paintLayers.Count;
         
            //Check the connection and remove any remaining Items
            for (int index = count - 1; index >= 0; index--)
            {
                PaintLayer layer = paintLayers[index];
                if (!isInputConnected(layer))
                    paintLayers.Remove(layer);
            }
            
            NodePort inputPort = GetPort("input");
            List<NodePort> connectionList = inputPort.GetConnections();
            for (int i = 0; i < connectionList.Count; i++)
            {
                PaintLayer node = connectionList[i].node as PaintLayer;
                if (node == null)
                    continue;

                if (!paintLayers.Contains(node))
                    paintLayers.Add(node);
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