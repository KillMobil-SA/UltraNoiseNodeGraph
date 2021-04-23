using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra.Modifiers
{
	public class NoiseNodeModifierBase : NoiseNodeBase
	{

		[Input] public NoiseNodeBase noiseA;
		
		public override float Sample1D (float x)
		{
			if (noiseAInput == null ) return -1;
			
			float v = ModifyValue (noiseAInput.Sample1D (x));
			
			return v;
		}

		public override float Sample2D (float x , float y) {
			if (noiseAInput == null) return -1;
			
			float v =  ModifyValue (noiseAInput.Sample2D (x , y));
	
			return v;
		}

		public override float Sample3D (float x , float y , float z) {
			if (noiseAInput == null ) return -1;
			
			float v =  ModifyValue (noiseAInput.Sample3D (x , y , z));
			
			return v;
		}

		public virtual float ModifyValue(float v)
		{
			return -1;
		}
		

		
		
		public override void OnCreateConnection(NodePort from, NodePort to) {
			ResetConnectionReferences();
			Update();
			UpdatePreview ();
			
		}

		public void ResetConnectionReferences()
		{
			Debug.Log(string.Format("ResetConnectionReferences ()"));
			_noiseAInput = null;
		}

		
		
		[Output] public NoiseNodeBase noiseResult;
		public override object GetValue(NodePort port)
		{
			return this; 
		}
		
		private NoiseNodeBase _noiseAInput;
		private NoiseNodeBase noiseAInput
		{
			get {
				if (_noiseAInput == null)
					_noiseAInput = GetInputValue<NoiseNodeBase>("noiseA", this.noiseA);
				return _noiseAInput;
			}
			set { _noiseAInput = value; }
		}
		

		
	}
}