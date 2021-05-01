﻿using System;
using NoiseUltra.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;

namespace NoiseUltra
{
    /// <summary>
    ///  Class packs all nodes of a graph and serializes them into a single file.
    /// </summary>
    [CreateAssetMenu(fileName = "Ultra Noise Graph", menuName = "KillMobil/UltraNoise/Noise Graph")]
    public class NoiseNodeGraph : NodeGraph
    {
      
        [Button , ContextMenu("Update All Nodes")]
        public void UpdateAllNodes()
        {
            foreach (Node node in nodes)
            {
                if (node is NodeBase item)
                {
                    item.Update();
                }
            }
        }
    }
}