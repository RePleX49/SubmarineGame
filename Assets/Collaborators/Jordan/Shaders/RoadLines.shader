// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/RoadLines"
{
	Properties
	{
		_Frequency("Frequency", Float) = 1
		_Offset("Offset", Float) = 0
		_Amplitude("Amplitude", Float) = 1
		_AmplitudeZ("AmplitudeZ", Float) = 1
		_OffsetVertical("OffsetVertical", Float) = 0
		_OffsetVerticalZ("OffsetVerticalZ", Float) = 0
		_OffsetLengthScaler("OffsetLengthScaler", Float) = 1
		_OffsetZ("OffsetZ", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
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
			float2 uv_texcoord;
		};

		uniform float _Frequency;
		uniform float _Offset;
		uniform float _OffsetLengthScaler;
		uniform float _Amplitude;
		uniform float _OffsetZ;
		uniform float _OffsetVertical;
		uniform float _AmplitudeZ;
		uniform float _OffsetVerticalZ;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 appendResult19 = (float3(( ( sin( ( ( _Time.y * _Frequency ) + _Offset + ( ase_vertex3Pos.z * _OffsetLengthScaler ) ) ) * _Amplitude * ( ase_vertex3Pos.z * _OffsetZ ) ) + _OffsetVertical ) , ( ( ( ( sin( ( ( _Time.y * 5.0 ) + 10.0 + ( ase_vertex3Pos.z * 0.1 ) ) ) + 1 ) / 2 ) * _AmplitudeZ * ( ase_vertex3Pos.z * 0.1 ) ) + _OffsetVerticalZ ) , 0.0));
			v.vertex.xyz += appendResult19;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
0;45;1440;834;1930.856;1040.958;2.544344;True;True
Node;AmplifyShaderEditor.RangedFloatNode;30;-945.658,794.0369;Inherit;False;Constant;_Float2Z;Float 2Z;9;0;Create;True;0;0;False;0;False;0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-918.637,459.1412;Inherit;False;Constant;_Float4Z;Float 4Z;1;0;Create;True;0;0;False;0;False;5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;29;-936.228,638.4376;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;34;-953.9999,258.0327;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-608.901,620.1465;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-744.1768,581.7347;Inherit;False;Constant;_Float1Z;Float 1Z;3;0;Create;True;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-732.3888,309.8993;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-852.3647,-320.9037;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-522.5651,374.2688;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-834.7736,-500.2003;Inherit;False;Property;_Frequency;Frequency;0;0;Create;True;0;0;False;0;False;1;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;1;-870.1365,-701.3087;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-861.7946,-165.3045;Inherit;False;Property;_OffsetLengthScaler;OffsetLengthScaler;6;0;Create;True;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-525.0381,-339.1947;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;37;-472.1049,164.1304;Inherit;False;Constant;_Int0;Int 0;10;0;Create;True;0;0;False;0;False;1;0;False;0;1;INT;0
Node;AmplifyShaderEditor.SinOpNode;24;-376.0991,258.0388;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-660.3135,-377.6066;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-648.5256,-649.4421;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-420.3143,-108.4149;Inherit;False;Property;_OffsetZ;OffsetZ;7;0;Create;True;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;39;-344.1049,349.1303;Inherit;False;Constant;_Int1;Int 1;10;0;Create;True;0;0;False;0;False;2;0;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-265.1048,118.1304;Inherit;False;2;2;0;FLOAT;0;False;1;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-438.7021,-585.0726;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-504.1771,850.9266;Inherit;False;Constant;_Float3Z;Float 3Z;11;0;Create;True;0;0;False;0;False;0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;38;-167.1049,219.1303;Inherit;False;2;0;FLOAT;0;False;1;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-368.8126,679.1027;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-279.1685,-487.1061;Inherit;False;Property;_Amplitude;Amplitude;2;0;Create;True;0;0;False;0;False;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-284.9496,-280.2385;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;11;-231.2361,-668.3026;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-363.0316,472.2354;Inherit;False;Property;_AmplitudeZ;AmplitudeZ;3;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-124.1363,584.0925;Inherit;False;Property;_OffsetVerticalZ;OffsetVerticalZ;5;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-40.27335,-375.2488;Inherit;False;Property;_OffsetVertical;OffsetVertical;4;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-23.77035,-589.7877;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-107.6333,369.5538;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;177.7586,661.8239;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;186.0532,-566.2119;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;102.1906,393.1295;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;246.7394,-46.34584;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;20;-68.05167,-897.3905;Inherit;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;False;-1;None;7a34f005c51804948af27941334ff9ed;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;693.0763,-231.0254;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/RoadLines;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;31;0;29;3
WireConnection;31;1;30;0
WireConnection;21;0;34;0
WireConnection;21;1;35;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;23;2;31;0
WireConnection;7;0;2;3
WireConnection;7;1;3;0
WireConnection;24;0;23;0
WireConnection;6;0;1;0
WireConnection;6;1;4;0
WireConnection;36;0;24;0
WireConnection;36;1;37;0
WireConnection;8;0;6;0
WireConnection;8;1;5;0
WireConnection;8;2;7;0
WireConnection;38;0;36;0
WireConnection;38;1;39;0
WireConnection;33;0;29;3
WireConnection;33;1;32;0
WireConnection;12;0;2;3
WireConnection;12;1;9;0
WireConnection;11;0;8;0
WireConnection;13;0;11;0
WireConnection;13;1;10;0
WireConnection;13;2;12;0
WireConnection;27;0;38;0
WireConnection;27;1;25;0
WireConnection;27;2;33;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;28;0;27;0
WireConnection;28;1;26;0
WireConnection;19;0;15;0
WireConnection;19;1;28;0
WireConnection;19;2;17;0
WireConnection;0;0;20;0
WireConnection;0;11;19;0
ASEEND*/
//CHKSM=6E83CBCAE78F6D78EB99805EB02DABF2B4CC125E