// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SeagrassWave"
{
	Properties
	{
		_Frequency1("Frequency", Float) = 1
		_Offset1("Offset", Float) = 0
		_Amplitude1("Amplitude", Float) = 1
		_OffsetVertical1("OffsetVertical", Float) = 0
		_OffsetLengthScaler1("OffsetLengthScaler", Float) = 1
		_OffsetZ1("OffsetZ", Float) = 1
		_ColorAlbedo1("ColorAlbedo", Color) = (0,0,0,0)
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

		uniform float _Frequency1;
		uniform float _Offset1;
		uniform float _OffsetLengthScaler1;
		uniform float _Amplitude1;
		uniform float _OffsetZ1;
		uniform float _OffsetVertical1;
		uniform float4 _ColorAlbedo1;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 appendResult18 = (float3(( ( sin( ( ( _Time.y * _Frequency1 ) + _Offset1 + ( ase_vertex3Pos.z * _OffsetLengthScaler1 ) ) ) * _Amplitude1 * ( ase_vertex3Pos.z * _OffsetZ1 ) ) + _OffsetVertical1 ) , 0.0 , 0.0));
			v.vertex.xyz += appendResult18;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _ColorAlbedo1.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
0;243;959;636;1435.59;747.1307;1.410069;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;1;-760.7993,-652.8987;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-699.5033,-92.72455;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-708.9332,62.87494;Inherit;False;Property;_OffsetLengthScaler1;OffsetLengthScaler;4;0;Create;True;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-725.4363,-451.7902;Inherit;False;Property;_Frequency1;Frequency;0;0;Create;True;0;0;False;0;False;1;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-550.9762,-329.1967;Inherit;False;Property;_Offset1;Offset;1;0;Create;True;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-539.1882,-601.0322;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-473.1772,-121.0155;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-329.3647,-536.6627;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-267.4534,119.7646;Inherit;False;Property;_OffsetZ1;OffsetZ;5;0;Create;True;0;0;False;0;False;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;12;-121.8987,-619.8927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-204.4575,-161.2249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-119.5411,-411.7113;Inherit;False;Property;_Amplitude1;Amplitude;2;0;Create;True;0;0;False;0;False;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;85.56735,-541.3777;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;69.06433,-326.8388;Inherit;False;Property;_OffsetVertical1;OffsetVertical;3;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;295.3911,-517.8019;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;150.8123,-78.40585;Inherit;False;Constant;_OffsetY1;OffsetY;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;113.1878,27.30115;Inherit;False;Constant;_Float1;Float 0;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;333.2964,-987.4194;Inherit;False;Property;_ColorAlbedo1;ColorAlbedo;6;0;Create;True;0;0;False;0;False;0,0,0,0;1,0.4764151,0.4764151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;18;526.433,-498.2265;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;762.5952,-782.9486;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SeagrassWave;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;1;0
WireConnection;6;1;4;0
WireConnection;7;0;2;3
WireConnection;7;1;3;0
WireConnection;8;0;6;0
WireConnection;8;1;5;0
WireConnection;8;2;7;0
WireConnection;12;0;8;0
WireConnection;10;0;2;3
WireConnection;10;1;9;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;13;2;10;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;18;0;15;0
WireConnection;18;1;16;0
WireConnection;18;2;17;0
WireConnection;0;0;19;0
WireConnection;0;11;18;0
ASEEND*/
//CHKSM=5671A4378AE9DE778AA302C0ADCAC1A98504A517