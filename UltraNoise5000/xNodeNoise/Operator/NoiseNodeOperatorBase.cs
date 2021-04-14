using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra.Operation
{
	public class NoiseNodeOperatorBase : NoiseNodeBase
	{

		[Input] public NoiseNodeBase noiseA;
		[Input] public NoiseNodeBase noiseB;


		
		// ReSharper disable Unity.PerformanceAnalysis
		public override void OnCreateConnection(NodePort from, NodePort to) {
			_noiseAInput = null;
			_noiseBInput = null;
			UpdateValues();
		}
		
		
		[Output] public NoiseNodeBase noiseResult;
		public override object GetValue(NodePort port)
		{
			return this; 
		}



		public bool isNoiseNullCheck()  /// Delete?
		{
			if (noiseAInput == null || noiseBInput == null) return true;
			else return false;
		}

		
		private NoiseNodeBase _noiseAInput;
		public NoiseNodeBase noiseAInput
		{
			get {
				if (_noiseAInput == null)
					_noiseAInput = GetInputValue<NoiseNodeBase>("noiseA", this.noiseA);
				return _noiseAInput;
			}
			set { _noiseAInput = value; }
		}
		
		private NoiseNodeBase _noiseBInput;
		public NoiseNodeBase noiseBInput
		{
			get {
				if (_noiseBInput == null)
					_noiseBInput = GetInputValue<NoiseNodeBase>("noiseB", this.noiseB);
				return _noiseBInput;
			}
			set { _noiseBInput = value; }
		}
		
		
	}
}