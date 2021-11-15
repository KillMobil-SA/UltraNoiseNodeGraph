using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public sealed class PlacementItem
    {
        [SerializeField] [TableColumnWidth(40, false)]
        public bool active;

        [SerializeField] [TableColumnWidth(60, false)] [Min(0.1f)]
        public float spacing = 10;

        public PlacementSettings settings;
    }
}