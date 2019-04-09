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

		public string	Text { get; set; }

		public readonly float[]	t_seeds = Enumerable.Range( 1, 128 ).Cast<float>().ToArray();

		protected override void OnRender( DrawingContext drawingContext )
		{
			
			var string_length		= 10;

			var segmentLength		= 10;
			var totalElementLength	= (segmentLength - 1) * 2 * 2;// 中点 * ＸＹ要素 * 二本分

			var elementLength		= Vector<float>.Count;
			
			var t_vectors	= new Vector<float> [ (segmentLength-1)*2 + string_length ];
			
			var outline_param_length	= (segmentLength-1) * 2;	// 中点数 * ＸＹ要素 * 二本分
			var	griph_param_length		= string_length * 2;		// 文字数 * ＸＹ要素
			var spacer					= Vector<float>.Count % (outline_param_length * 2 + griph_param_length);
			var iu_params	= new float [ outline_param_length * 2 + griph_param_length +  ];

			var i	= 0;
			make_outline_parameters_( iu_params, segmentLength-1, 1.0f / segmentLength, ref i );
			make_gryph_parameters_( iu_params, string_length, 1.0f / (string_length+1), ref i );

			return;


			void make_outline_parameters_( float[] params_, int point_length_, float inclement_, ref int i_ )
			{
				var t_	= inclement_;
				for( ; i_ < point_length_; i_++ )
				{
					params_[i_++]	= t_;	// 1st x
					params_[i_++]	= t_;	// 1st y
					params_[i_++]	= t_;	// 2nd x
					params_[i_++]	= t_;	// 2nd y
					t_ += inclement_;
				}
			}
			void make_gryph_parameters_( float[] params_, int string_length_, float inclement_, ref int i_ )
			{
				var t_	= inclement_;
				for( ; i_ < string_length_; i_++ )
				{
					params_[i_++]	= t_;	// x
					params_[i_++]	= t_;	// y
					t_ += inclement_;
				}
			}
			
			void interpolate_vectors_()
			{

			}
			
			void build_outline_from_vectors_()
			{

			}
			void build_gryph_from_vectors_()
			{

			}
		}

		public struct InterPolationUnit
		{
			Vector<float>	vt0;
			Vector<float>	vt1;
			Vector<float>	vt2;
			Vector<float>	vt3;

			public InterPolationUnit( Vector<float> v0, Vector<float> v1, Vector<float> v2, Vector<float> v3 )
			{
				this.vt0	= v1;
				this.vt1	= 0.5f * (v2 - v0);
				this.vt2	= 0.5f * (2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3);
				this.vt3	= 0.5f * (-v0 + 3.0f * v1 - 3.0f * v2 + v3);
			}
			
			public Vector<float> Interpolate( Vector<float> t )
			{
				var tt	= t * t;
				var ttt	= tt * t;
				return ( vt0 + vt1 * t + vt2 * tt + vt3 * ttt );
			}
		}


		//protected override void OnRender( DrawingContext drawingContext )
		//{
		//	var pen = new Pen( Brushes.Green, 1.74f );

		//	//var iu	= new InterPolationUnit( v0, v1, v2, v3 );
		//	//var prev = v1;
		//	//for( var t=0.0f; t<=1.001f; t+=0.1f )
		//	//{
		//	//	var vt = iu.calc_line_segment( t );
		//	//	Point pt1 = new Point( vt.start.X, vt.start.Y );
		//	//	Point pt2 = new Point( vt.end.X, vt.end.Y );
		//	//	drawingContext.DrawLine( pen, pt1, pt2 );
		//	//	prev = vt.end;
		//	//}
			
		//	var start	= new Vector2( left, top );
		//	var end		= new Vector2( 200, 100 );

		//	var dir	= end - start;
		//	var len	= dir.Length();
		//	var orth_width	= ( new Vector2( dir.Y, dir.X ) / len ) * 5.5f;

		//	var end0	= end - orth_width;
		//	var end1	= end + orth_width;

		//	var v0	= new Vector4( start.X, start.Y-100, start.X, start.Y-100 );
		//	var v1	= new Vector4( start.X, start.Y, start.X, start.Y );
		//	var v2	= new Vector4( end0.X, end0.Y, end1.X, end1.Y );
		//	var v3	= new Vector4( end0.X+100, end0.Y, end1.X+100, end1.Y );

		//	var stream0	= new StreamGeometry();
		//	var stream1	= new StreamGeometry();
		//	//stream.FillRule = FillRule.EvenOdd;
		//	using( var context0 = stream0.Open() )
		//	using( var context1 = stream1.Open() )
		//	{
		//		var line_start	= new Point( start.X, start.Y );
		//		context0.BeginFigure( line_start, isFilled:false, isClosed:false );
		//		context1.BeginFigure( line_start, isFilled:false, isClosed:false );
				
		//		var iu	= new InterPolationUnit( v0, v1, v2, v3 );
		//		for( var t = 0.1f; t < 1.0f; t += 0.1f )
		//		{
		//			var vt	= iu.Interpolate( t );
		//			var pt0	= new Point( vt.X, vt.Y );
		//			context0.LineTo( pt0, isStroked:true, isSmoothJoin:false );
		//			var pt1	= new Point( vt.Z, vt.W );
		//			context1.LineTo( pt1, isStroked:true, isSmoothJoin:false );
		//		}
				
		//		var line_end0	= new Point( end0.X, end0.Y );
		//		context0.LineTo( line_end0, isStroked:true, isSmoothJoin:false );
		//		var line_end1	= new Point( end1.X, end1.Y );
		//		context1.LineTo( line_end1, isStroked:true, isSmoothJoin:false );
		//	}
		//	//var combind = new CombinedGeometry( GeometryCombineMode.Union, stream0, stream1 );
		//	//combind.Freeze();
		//	//drawingContext.DrawGeometry( null, pen, combind );
		//	//stream0.Freeze();
		//	//stream1.Freeze();
		//	//drawingContext.DrawGeometry( null, pen, stream0 );
		//	//drawingContext.DrawGeometry( null, pen, stream1 );
		//	var group = new GeometryGroup();
		//	group.Children.Add( stream0 );
		//	group.Children.Add( stream1 );
		//	var line_end0_ = new Point( end0.X, end0.Y );
		//	var line_end1_ = new Point( end1.X, end1.Y );
		//	var line_close = new LineGeometry( line_end0_, line_end1_ );
		//	group.Children.Add( line_close );
		//	group.FillRule = FillRule.Nonzero;
		//	var drawing = new GeometryDrawing( pen.Brush, pen, group );
		//	drawingContext.DrawDrawing( drawing );
		//}

		//public struct InterPolationUnit
		//{
		//	Vector4 vt0;
		//	Vector4 vt1;
		//	Vector4 vt2;
		//	Vector4 vt3;

		//	public InterPolationUnit( Vector4 v0, Vector4 v1, Vector4 v2, Vector4 v3 )
		//	{
		//		this.vt0	= v1;
		//		this.vt1	= 0.5f * (v2 - v0);
		//		this.vt2	= 0.5f * (2.0f * v0 - 5.0f * v1 + 4.0f * v2 - v3);
		//		this.vt3	= 0.5f * (-v0 + 3.0f * v1 - 3.0f * v2 + v3);
		//	}
			
		//	public Vector4 Interpolate( float t )
		//	{
		//		var tt	= t * t;
		//		var ttt	= tt * t;
		//		return ( vt0 + vt1 * t + vt2 * tt + vt3 * ttt );
		//	}
		//}

	}
}
