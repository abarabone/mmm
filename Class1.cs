using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace mmm
{

	public interface IMmObject
	{
		//TMm AddAttribute<TMm>( IMmObject obj );

		//IMmObject				Parent { get; set; }
		//IEnumerable<IMmObject>	Children { get; set; }
	}

	public interface IMmLocatable
	{
		MmPosition Center { get; }
	}

	public interface IMmAnchor
	{

	}


	public class MmNode : IMmObject
	{
		private MmNode	parent;
		private MmNode	child;
		private MmNode	sibling;
		
		public IMmAnchor		AnchorHead;
		public IMmAnchor		AnchorTail;
		public List<IMmAnchor>	AnchorMiddles;

		public MmNode( IMmObject data )
		{

		}

	}

	public class MmImage : IMmObject
	{

	}

	public class MmAnchorPoint : IMmObject
	{
		public MmPosition	position;
	}

	public struct MmPosition : IMmObject
	{
		public float	X;
		public float	Y;
	}
}

