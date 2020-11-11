// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/FishSwim"
{
	Properties
	{
		_Frequency("Frequency", Float) = 1
		_Offset("Offset", Float) = 0
		_Amplitude("Amplitude", Float) = 1
		_OffsetVertical("OffsetVertical", Float) = 0
		_OffsetLengthScaler("OffsetLengthScaler", Float) = 1
		_OffsetZ("OffsetZ", Float) = 1
		_ColorAlbedo("ColorAlbedo", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float _Frequency;
		uniform float _Offset;
		uniform float _OffsetLengthScaler;
		uniform float _Amplitude;
		uniform float _OffsetZ;
		uniform float _OffsetVertical;
		uniform float4 _ColorAlbedo;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 appendResult16 = (float3(( ( sin( ( ( _Time.y * _Frequency ) + _Offset + ( ase_vertex3Pos.z * _OffsetLengthScaler ) ) ) * _Amplitude * ( ase_vertex3Pos.z * _OffsetZ ) ) + _OffsetVertical ) , 0.0 , 0.0));
			v.vertex.xyz += appendResult16;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _ColorAlbedo.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
0;130;1440;834;1375.963;-174.7675;1;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;1;-1326.237,-82.03683;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;3;-1264.941,478.1374;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-1274.371,633.7369;Inherit;False;Property;_OffsetLengthScaler;OffsetLengthScaler;4;0;Create;True;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1290.874,119.0717;Inherit;False;Property;_Frequency;Frequency;0;0;Create;True;0;0;False;0;False;1;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1116.414,241.6653;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1104.626,-30.17023;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1038.615,449.8464;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-894.8024,34.19925;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-832.8911,690.6265;Inherit;False;Property;_OffsetZ;OffsetZ;5;0;Create;True;0;0;False;0;False;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-769.8952,409.6371;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-684.9789,159.1506;Inherit;False;Property;_Amplitude;Amplitude;2;0;Create;True;0;0;False;0;False;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;12;-687.3364,-49.03073;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-479.8704,29.48425;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-496.3734,244.0231;Inherit;False;Property;_OffsetVertical;OffsetVertical;3;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-270.0467,53.05997;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-414.6254,492.4561;Inherit;False;Constant;_OffsetY;OffsetY;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-452.2499,598.1631;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-39.00476,72.63547;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;21;-232.1413,-416.5575;Inherit;False;Property;_ColorAlbedo;ColorAlbedo;6;0;Create;True;0;0;False;0;False;0,0,0,0;1,0.4764151,0.4764151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;366.326,-332.09;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/FishSwim;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;1;0
WireConnection;6;1;2;0
WireConnection;7;0;3;3
WireConnection;7;1;4;0
WireConnection;8;0;6;0
WireConnection;8;1;5;0
WireConnection;8;2;7;0
WireConnection;18;0;3;3
WireConnection;18;1;17;0
WireConnection;12;0;8;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;13;2;18;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;16;0;15;0
WireConnection;16;1;19;0
WireConnection;16;2;20;0
WireConnection;0;0;21;0
WireConnection;0;11;16;0
ASEEND*/
//CHKSM=FBC1627BC5E12FCBF1E1639B9E61B38BD825C7C5