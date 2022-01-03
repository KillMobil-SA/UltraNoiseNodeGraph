using System;
using NoiseUltra.Output;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public abstract class PlacementVectorRange
    {
        #region Constants

        private const string AMOUNT_NAME = "Amount";
        private const string RIGHT_GROUP = "Right";
        private const string LEFT_GROUP = "Left";

        private const string UNIFIED_GROUP = "Unified";
        private const string SEPARATED_GROUP = "Separated";

        private const string RANGE_SETTINGS_GROUP = "RangeSettings";
        private const string RANGE_SETTINGS_VERTICAL_GROUP = RANGE_SETTINGS_GROUP + "/" + RIGHT_GROUP;
        private const string RANGE_SETTINGS_BOX_GROUP_UNIFIED = RANGE_SETTINGS_VERTICAL_GROUP + "/" + UNIFIED_GROUP;
        private const string RANGE_SETTINGS_BOX_GROUP_SEPARATED = RANGE_SETTINGS_VERTICAL_GROUP + "/" + SEPARATED_GROUP;

        private const string ROUND_GROUP = "Range Round";
        private const string NOISE_GROUP = "NoiseGroup";

        private const string OTHER_SETTINGS_GROUP = "OtherSetingsGroup";
        private const string OTHER_SETTINGS_VERTICAL_GROUP = OTHER_SETTINGS_GROUP + "/" + LEFT_GROUP;
        private const string OTHER_SETTINGS_BOX_GROUP = OTHER_SETTINGS_VERTICAL_GROUP + "/" + ROUND_GROUP;

        private const string OTHER_SETTINGS_VERTICAL_GROUP_RIGHT = OTHER_SETTINGS_GROUP + "/" + RIGHT_GROUP;
        private const string OTHER_SETTINGS_BOX_GROUP_RIGHT = OTHER_SETTINGS_VERTICAL_GROUP_RIGHT + "/" + NOISE_GROUP;

        #endregion

        #region EditorShowConditions

        private bool showMinRangeField => (axisType == AxisType.Unified && rangeType == RangeType.MinMax);
        private bool showMinRangeV3Field => (axisType == AxisType.Separated && rangeType == RangeType.MinMax);
        private bool showRangeField => (axisType == AxisType.Unified);
        private bool showRangeV3Field => (axisType == AxisType.Separated);

        #endregion

        #region Members

        private UltraPlacementTool _mUltraPlacementTool;

        [Space] [EnumToggleButtons] [HideLabel]
        public AxisType axisType = AxisType.Unified;

        [EnumToggleButtons]
        [HideLabel]
        public RangeType rangeType = RangeType.MinMax;

        [HorizontalGroup(RANGE_SETTINGS_GROUP)]
        [VerticalGroup(RANGE_SETTINGS_VERTICAL_GROUP)]
        [BoxGroup(RANGE_SETTINGS_BOX_GROUP_UNIFIED)]
        [HideLabel]
        [ShowIf(nameof(showMinRangeField))]
        public float minRange = 0;

        [BoxGroup(RANGE_SETTINGS_BOX_GROUP_UNIFIED)] [ShowIf(nameof(showRangeField))] [HideLabel]
        public float range = 0;

        [BoxGroup(RANGE_SETTINGS_BOX_GROUP_SEPARATED)] [ShowIf(nameof(showMinRangeV3Field))] [HideLabel]
        public Vector3 minRangeV3 = Vector3.zero;

        [BoxGroup(RANGE_SETTINGS_BOX_GROUP_SEPARATED)] [ShowIf(nameof(showRangeV3Field))] [HideLabel]
        public Vector3 rangeV3 = Vector3.zero;

        [HorizontalGroup(OTHER_SETTINGS_GROUP, 110, PaddingRight = 15)]
        [VerticalGroup(OTHER_SETTINGS_VERTICAL_GROUP, PaddingTop = 15)]
        [BoxGroup(OTHER_SETTINGS_BOX_GROUP)]
        [LabelText(AMOUNT_NAME)]
        [LabelWidth(50)]
        [Min(0)]
        public float roundAmount;

        [VerticalGroup(OTHER_SETTINGS_VERTICAL_GROUP_RIGHT, PaddingTop = 15), BoxGroup(OTHER_SETTINGS_BOX_GROUP_RIGHT)]
        public bool useExternalNoise;

        [BoxGroup(OTHER_SETTINGS_BOX_GROUP_RIGHT), ShowIf(nameof(useExternalNoise))]
        public ExportNode externalSource;

        #endregion

        #region Public

        public virtual void Initialize()
        {
        }

        public abstract Vector3 GetVectorRange(Vector3 pos, float threshold);

        public float RoundValue(float value)
        {
            return (roundAmount != 0) ? Mathf.Round(value / roundAmount) * roundAmount : value;
        }

        public Vector3 RoundValue(Vector3 vector3)
        {
            return (roundAmount != 0)
                ? new Vector3(RoundValue(vector3.x), RoundValue(vector3.y), RoundValue(vector3.z))
                : vector3;
        }

        #endregion
    }
}