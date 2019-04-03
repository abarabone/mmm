using System;
using System.Runtime.Serialization;

namespace mmm
{

	public interface IMmObject<TMm>
	{
		TMm AddAttribute( IMmObject obj );

		MmPosition Center { get; }
	}


	public class MmNode : IMmObject<MmNode>
	{
		public Node	parent;
		public Node	child;
		public Node	sibling;
		
		public Node( IMmObject data )
		{

		}

	}

	public class MmImage : IMmObject<MmImage>
	{

	}

	public struct MmPosition : IMmObject<MmPosition>
	{
		public float	x;
		public float	y;
	}
}

