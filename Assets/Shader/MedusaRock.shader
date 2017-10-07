// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MedusaRock"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_AlbedoColor("Albedo Color", Color) = (0,0,0,0)
		_RockColor("Rock Color", Color) = (0,0,0,0)
		_RockEffect("RockEffect", Range( 0 , 1)) = 0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
		};

		uniform half4 _AlbedoColor;
		uniform half4 _RockColor;
		uniform half _RockEffect;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half blendOpSrc12 = ( 1.0 - i.texcoord_0.x );
			half blendOpDest12 = _RockEffect;
			float4 lerpResult3 = lerp( _AlbedoColor , _RockColor , ( saturate( ( 1.0 - ( ( 1.0 - blendOpDest12) / blendOpSrc12) ) )));
			o.Albedo = lerpResult3.rgb;
			float temp_output_5_0 = 0.0;
			o.Specular = temp_output_5_0;
			o.Gloss = temp_output_5_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=12001
1927;122;1368;686;1251.847;64.10477;1.19;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1056.311,256.1149;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;14;-778.2292,278.6151;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;-728.1047,477.9603;Float;False;Property;_RockEffect;RockEffect;2;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;2;-493.7504,104.7201;Float;False;Property;_RockColor;Rock Color;1;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;1;-495.7504,-72.27994;Float;False;Property;_AlbedoColor;Albedo Color;0;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.BlendOpsNode;12;-377.199,310.7451;Float;False;ColorBurn;True;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-66.87518,285.7201;Float;False;Constant;_Float0;Float 0;2;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;3;-166.7504,154.7201;Float;False;3;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;136,-1;Half;False;True;2;Half;ASEMaterialInspector;0;Lambert;MedusaRock;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;4;1
WireConnection;12;0;14;0
WireConnection;12;1;7;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;3;2;12;0
WireConnection;0;0;3;0
WireConnection;0;3;5;0
WireConnection;0;4;5;0
ASEEND*/
//CHKSM=CE4C3D7D2D235A4D48AAA53B91685EAD7BA1A733