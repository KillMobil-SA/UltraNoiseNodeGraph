using System;
using System.Collections;
using System.Collections.Generic;
using NoiseUltra.Nodes;
using NoiseUltra.Output;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
    public class PlacementVectorRange
    {
        
        private const string RightGroup = "Right";
        private const string LeftGroup = "Left";

        private const string UnifiedGroup = "Unified";
        private const string SeparatedGroup = "Separated";
        
        private const string RangeSettingsGroup = "RangeSettings";
        private const string RangeSettingsVerticalGroup =  RangeSettingsGroup + "/" + RightGroup;
        private const string RangeSettingsBoxGroupUnified = RangeSettingsVerticalGroup+ "/" + UnifiedGroup;
        private const string RangeSettingsBoxGroupSeparated = RangeSettingsVerticalGroup + "/" + SeparatedGroup;
        
        private const string RoundGroup = "Range Round";
        private const string NoiseGroup = "NoiseGroup";
        
        private const string OtherSetingsGroup = "OtherSetingsGroup";
        private const string OtherSetingsVerticalGroup = OtherSetingsGroup + "/" + LeftGroup;
        private const string OtherSetingsBoxGroup = OtherSetingsVerticalGroup + "/" + RoundGroup;
        
        private const string OtherSetingsVerticalGroupRight = OtherSetingsGroup + "/" + RightGroup;
        private const string OtherSetingsBoxGroupRight = OtherSetingsVerticalGroupRight + "/" + NoiseGroup;
        

        

        private bool showMinRangeField => (axisType == AxisType.Unified && rangeType == RangeType.MinMax);
        private bool showMinRangeV3Field => (axisType == AxisType.Separated && rangeType == RangeType.MinMax);
        private bool showRangeField => (axisType == AxisType.Unified);
        private bool showRangeV3Field => (axisType == AxisType.Separated);
      
        [Space]
        [EnumToggleButtons , HideLabel] public AxisType axisType = AxisType.Unified;
        [EnumToggleButtons, HideLabel] public RangeType rangeType = RangeType.MinusPlus;

        
        
        [HorizontalGroup(RangeSettingsGroup) , VerticalGroup( RangeSettingsVerticalGroup) , BoxGroup(RangeSettingsBoxGroupUnified)]
        [HideLabel ,ShowIf(nameof(showMinRangeField))]
        public float minRange = 0;
        
        [BoxGroup(RangeSettingsBoxGroupUnified)] 
        [HideLabel , ShowIf(nameof(showRangeField))]
        public float range = 0;

        [BoxGroup(RangeSettingsBoxGroupSeparated) ]
        [HideLabel ,ShowIf(nameof(showMinRangeV3Field))] 
        public Vector3 minRangeV3 = Vector3.zero;
        
        [BoxGroup(RangeSettingsBoxGroupSeparated)]
        [HideLabel ,ShowIf(nameof(showRangeV3Field))] 
        public Vector3 rangeV3 = Vector3.zero;
       
        
        [HorizontalGroup(OtherSetingsGroup , 110 , PaddingRight = 15), VerticalGroup(OtherSetingsVerticalGroup, PaddingTop  = 15 ),BoxGroup(OtherSetingsBoxGroup)]
        [LabelText("Amount") , LabelWidth(50) , Min(0)]
        public float roundAmount;
        
     
        [VerticalGroup(OtherSetingsVerticalGroupRight ,  PaddingTop  = 15) , BoxGroup(OtherSetingsBoxGroupRight)]
        public bool useExternalNoise;
        [BoxGroup(OtherSetingsBoxGroupRight) , ShowIf(nameof(useExternalNoise))]
        public ExportNode externalSource;




        public virtual void Init()
        {
            
        }        
        
        public virtual Vector3 GetVectorRange (Vector3 pos, float thresHold)
        {
            return Vector3.zero;
        }
        
        
        public float RoundValue(float value)
        {
            return (roundAmount!= 0) ? Mathf.Round(value / roundAmount) * roundAmount : value;
        }

        public Vector3 RoundValue(Vector3 vector3)
        {
            return (roundAmount!= 0) ? new Vector3(RoundValue(vector3.x), RoundValue(vector3.y), RoundValue(vector3.z)) : vector3;
        }
        
        
        
    
        
        
    }
}