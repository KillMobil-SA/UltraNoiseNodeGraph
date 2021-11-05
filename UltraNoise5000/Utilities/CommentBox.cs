using Sirenix.OdinInspector;
using UnityEngine;
using XNode;
using NoiseUltra.Nodes;

namespace NoiseUltra.Utilities 
{
	[NodeTint(NodeColor.UTILITY)]
	public class CommentBox : Node
	{
		[TextArea (20 , 100) , HideLabel]
		public string commentBox;

	}
}