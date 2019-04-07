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
		public 

		protected override void OnRender( DrawingContext drawingContext )
		{
			Pen pen = new Pen(Brushes.Black, 10);
			Point pt1 = new Point(left, top);
			Point pt2 = new Point(200, 100);
			drawingContext.DrawLine( pen, pt1, pt2 );
		}

	//	static class Utility
	//{
		
	//	public static float4 Interpolate( float4 v0, float4 v1, float4 v2, float4 v3, float t )
	//	{
	//		//return Quaternion.Slerp( new quaternion(v1), new quaternion(v2), Mathf.Clamp01(t) ).ToFloat4();
	//		return v1 + 0.5f * t * ( ( v2 - v0 ) + ( ( 2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3 ) + ( -v0 + 3.0f * v1 - 3.0f * v2 + v3 ) * t ) * t );
	//	}
	//}
		
	}
}
