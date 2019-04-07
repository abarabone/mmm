using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Numerics;

namespace mmm
{
	public class MmNodeControl : ContentControl
	{
		static MmNodeControl()
		{
			init_override_metadata_();
			init_dependency_properties_( "top", (MmNodeControl me, int value) => me.top = value);
			init_dependency_properties_( "left", (MmNodeControl me, int value) => me.left = value);

			return;


			void init_override_metadata_()
			{
				DefaultStyleKeyProperty.OverrideMetadata(
					typeof( MmNodeControl ),
					new FrameworkPropertyMetadata( typeof( MmNodeControl ) )
				);
			}
			
			void init_dependency_properties_
				( string property_name, Action<MmNodeControl,int> property_chanded_proc )
			{
				var prop_metadata	= new FrameworkPropertyMetadata(
					defaultValue:
						0, 
					flags:
						FrameworkPropertyMetadataOptions.AffectsRender, 
					propertyChangedCallback:
						(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
							property_chanded_proc((d as MmNodeControl), (int)e.NewValue )
				);

				DependencyProperty.Register(
					name:			property_name,
					propertyType:	typeof(int),
					ownerType:		typeof(MmNodeControl),
					typeMetadata:	prop_metadata
				);
			}
			
		}

		public int	top { get; set; }
		public int	left { get; set; }

		public string Text { get; set; }
		//public 

		protected override void OnRender( DrawingContext drawingContext )
		{
			var v0	= new Vector2( 0, 0 );
			var v1	= new Vector2( top, left );
			var v2	= new Vector2( 200, 100 );
			var v3	= new Vector2( 200, 110 );
			Pen pen = new Pen(Brushes.Green, 1.74f);

			var iu	= new InterPolationUnit( v0, v1, v2, v3 );
			for( var t=0.0f; t<1.0f; t+=0.1f )
			{
				var vt	= iu.Interpolate( t );
				Point pt1 = new Point(left, top);
				Point pt2 = new Point(200, 100);
				drawingContext.DrawLine( pen, pt1, pt2 );
			}
		}

		public struct InterPolationUnit
		{
			Vector2 vt0;
			Vector2 vt1;
			Vector2 vt2;
			Vector2 vt3;

			Vector2 prev;

			public InterPolationUnit( Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3 )
			{
				vt0	= v1;
				vt1	= 0.5f * (v2 - v0);
				vt2	= 0.5f * (2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3);
				vt3	= 0.5f * (-v0 + 3.0f * v1 - 3.0f * v2 + v3);

				prev_vector = vt1;
			}
			
			public (Vector2 prev, Vector2 curr) Interpolate( float t )
			{
				var tt	= t * t;
				var ttt	= tt * t;
				var res	= (this.prev, curr:(vt0 + vt1 * t + vt2 * tt + vt3 * ttt));
			}
		}
		
		//public static Vector<float> Interpolate( Vector<float> v0, Vector<float> v1, Vector<float> v2, Vector<float> v3, float t )
		//{
		//	//return Quaternion.Slerp( new quaternion(v1), new quaternion(v2), Mathf.Clamp01(t) ).ToFloat4();
		//	return v1 + 0.5f * t * ( ( v2 - v0 ) + ( ( 2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3 ) + ( -v0 + 3.0f * v1 - 3.0f * v2 + v3 ) * t ) * t );
		//}
		//public static Vector2 Interpolate( Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3, float t )
		//{
		//	//return Quaternion.Slerp( new quaternion(v1), new quaternion(v2), Mathf.Clamp01(t) ).ToFloat4();
		//	return v1 + 0.5f * t * ( ( v2 - v0 ) + ( ( 2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3 ) + ( -v0 + 3.0f * v1 - 3.0f * v2 + v3 ) * t ) * t );
		//}

		//public struct InterPolationUnit
		//{
		//	Vector2 vt0;
		//	Vector2 vt1;
		//	Vector2 vt2;
		//	Vector2 vt3;

		//	public InterPolationUnit( Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3 )
		//	{
		//		vt0	= v1;
		//		vt1	= v2 - v0;
		//		vt2	= 2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3;
		//		vt3	= -v0 + 3.0f * v1 - 3.0f * v2 + v3;
		//	}
			
		//	public Vector2 Interpolate(  float t )
		//	{
		//		return vt0 + 0.5f * t * ( vt1 + ( vt2 + vt3 * t ) * t );
		//		//return v1 + 0.5f * t * 
		//		//	(
		//		//		( v2 - v0 ) +
		//		//		(
		//		//			( 2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3 ) +
		//		//			( -v0 + 3.0f * v1 - 3.0f * v2 + v3 ) * t
		//		//		) * t
		//		//	);
		//	}
		//}
	}
}
