// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/CoralShader"
{
	Properties
	{
		_ColorAlbedo2("ColorAlbedo1", Color) = (0.8773585,0.343733,0.2690015,1)
		_ColorAlbedo3("ColorAlbedo2", Color) = (1,0.6392157,0.4117647,1)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half filler;
		};

		uniform float4 _ColorAlbedo2;
		uniform float4 _ColorAlbedo3;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 transform1 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float dotResult4_g1 = dot( transform1.xy , float2( 12.9898,78.233 ) );
			float lerpResult10_g1 = lerp( 0.0 , 1.0 , frac( ( sin( dotResult4_g1 ) * 43758.55 ) ));
			float4 ifLocalVar6 = 0;
			if( 0.5 >= lerpResult10_g1 )
				ifLocalVar6 = _ColorAlbedo2;
			else
				ifLocalVar6 = _ColorAlbedo3;
			o.Albedo = ifLocalVar6.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
301;137;959;636;1318.739;497.9611;1.882292;True;False
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;1;-850.3774,126.8181;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-309.1528,-355.0366;Inherit;False;Property;_ColorAlbedo2;ColorAlbedo1;0;0;Create;True;0;0;False;0;False;0.8773585,0.343733,0.2690015,1;0.01891242,0.66,0.397,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-568.296,-218.1176;Inherit;False;Property;_ColorAlbedo3;ColorAlbedo2;1;0;Create;True;0;0;False;0;False;1,0.6392157,0.4117647,1;0.06341223,0.5377358,0.4738846,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-372.3184,39.20978;Inherit;False;Constant;_Float3;Float 2;8;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;5;-536.687,148.8221;Inherit;False;Random Range;-1;;1;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;6;-103.0616,37.50586;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;180.7,31.99896;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/CoralShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;1;1;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;6;2;2;0
WireConnection;6;3;2;0
WireConnection;6;4;3;0
WireConnection;0;0;6;0
ASEEND*/
//CHKSM=2EA4F0B8BBB4E20F1D167AC7A01AF5CF1273C30D