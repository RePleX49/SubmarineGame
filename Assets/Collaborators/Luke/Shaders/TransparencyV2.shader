// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TransparencyV2"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.6
		_FillValue("FillValue", Range( 0 , 2)) = 0
		_EdgePower("EdgePower", Float) = 2.55
		_WaterFoam("WaterFoam", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_FlowEndNoiseSpeed("FlowEndNoiseSpeed", Vector) = (0.1,-0.1,0,0)
		[HDR]_WaterColor("WaterColor", Color) = (0,1.521569,4,0)
		[HDR]_BaseWater("BaseWater", Color) = (0,1.521569,4,0)
		_BaseWater2("BaseWater2", Color) = (0,0.3555381,0.9346617,0)
		_Specular("Specular", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _BaseWater2;
		uniform float4 _BaseWater;
		uniform sampler2D _WaterFoam;
		SamplerState sampler_WaterFoam;
		uniform float4 _WaterColor;
		uniform float _Specular;
		uniform sampler2D _TextureSample0;
		SamplerState sampler_TextureSample0;
		uniform float2 _FlowEndNoiseSpeed;
		uniform float _FillValue;
		uniform float _EdgePower;
		uniform float _Cutoff = 0.6;


		//https://www.shadertoy.com/view/XdXGW8
		float2 GradientNoiseDir( float2 x )
		{
			const float2 k = float2( 0.3183099, 0.3678794 );
			x = x * k + k.yx;
			return -1.0 + 2.0 * frac( 16.0 * k * frac( x.x * x.y * ( x.x + x.y ) ) );
		}
		
		float GradientNoise( float2 UV, float Scale )
		{
			float2 p = UV * Scale;
			float2 i = floor( p );
			float2 f = frac( p );
			float2 u = f * f * ( 3.0 - 2.0 * f );
			return lerp( lerp( dot( GradientNoiseDir( i + float2( 0.0, 0.0 ) ), f - float2( 0.0, 0.0 ) ),
					dot( GradientNoiseDir( i + float2( 1.0, 0.0 ) ), f - float2( 1.0, 0.0 ) ), u.x ),
					lerp( dot( GradientNoiseDir( i + float2( 0.0, 1.0 ) ), f - float2( 0.0, 1.0 ) ),
					dot( GradientNoiseDir( i + float2( 1.0, 1.0 ) ), f - float2( 1.0, 1.0 ) ), u.x ), u.y );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float gradientNoise17 = GradientNoise(( i.uv_texcoord + ( float2( 0,-0.5 ) * _Time.y ) ),6.0);
			gradientNoise17 = gradientNoise17*0.5 + 0.5;
			float4 lerpResult32 = lerp( _BaseWater2 , _BaseWater , pow( gradientNoise17 , 3.0 ));
			float4 tex2DNode22 = tex2D( _WaterFoam, ( i.uv_texcoord + ( float2( -0.2,-0.35 ) * _Time.y ) ) );
			o.Emission = ( lerpResult32 + ( tex2DNode22.r * _WaterColor ) ).rgb;
			o.Smoothness = _Specular;
			o.Alpha = 1;
			float temp_output_18_0 = ( _FillValue - i.uv_texcoord.y );
			float clampResult23 = clamp( temp_output_18_0 , 0.0 , 1.0 );
			clip( ( ( tex2D( _TextureSample0, ( i.uv_texcoord + ( _FlowEndNoiseSpeed * _Time.y ) ) ).b + temp_output_18_0 ) * pow( clampResult23 , _EdgePower ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
-1920;0;1920;1019;2113.778;1395.793;1.3;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;1;-2218.597,-506.0638;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;2;-2208.598,-636.7632;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;False;0,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2110.463,-760.7139;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;4;-2335.19,-121.5259;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;False;0;False;-0.2,-0.35;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;5;-2324.387,10.47397;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-2011.299,-632.563;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;7;-2006.665,62.93753;Inherit;False;Property;_FlowEndNoiseSpeed;FlowEndNoiseSpeed;5;0;Create;True;0;0;False;0;False;0.1,-0.1;0.1,-0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;8;-1962.065,200.1368;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1599.671,308.9348;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-2255.252,-242.8756;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1769.066,119.1372;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1769.434,-12.91282;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1858.322,-701.1003;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-2131.388,-70.52614;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1652.361,216.4748;Inherit;False;Property;_FillValue;FillValue;1;0;Create;True;0;0;False;0;False;0;1.375;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;18;-1339.184,222.817;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;17;-1667.014,-706.6135;Inherit;True;Gradient;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1492.59,49.30112;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1978.41,-196.262;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;21;-1338.015,-993.6135;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;34;-994.9106,-1195.732;Inherit;False;Property;_BaseWater2;BaseWater2;8;0;Create;True;0;0;False;0;False;0,0.3555381,0.9346617,0;0,0.1714666,0.5377358,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;27;-1315.009,-201.1142;Inherit;False;Property;_WaterColor;WaterColor;6;1;[HDR];Create;True;0;0;False;0;False;0,1.521569,4,0;0,1.528796,4,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;33;-994.9106,-1006.732;Inherit;False;Property;_BaseWater;BaseWater;7;1;[HDR];Create;True;0;0;False;0;False;0,1.521569,4,0;0,4.643137,11.98431,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-1318.562,-24.73783;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;False;-1;ed7ba18ac1b0b5b409d784d22399347a;ed7ba18ac1b0b5b409d784d22399347a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-1023.485,498.4174;Inherit;False;Property;_EdgePower;EdgePower;2;0;Create;True;0;0;False;0;False;2.55;8.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;-1096.402,254.7128;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;22;-1676.006,-443.3403;Inherit;True;Property;_WaterFoam;WaterFoam;3;0;Create;True;0;0;False;0;False;-1;ed7ba18ac1b0b5b409d784d22399347a;ed7ba18ac1b0b5b409d784d22399347a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;28;-824.8021,412.9128;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-725.9106,-1080.732;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1023.841,-366.315;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-828.3022,64.41293;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-321.3824,73.24615;Inherit;False;Property;_Specular;Specular;9;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-967.9106,-618.7317;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1268.015,-446.6135;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-550.2013,222.4128;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-618.9106,-529.7317;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;37;-1372.911,-711.7317;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TransparencyV2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.6;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;2;0
WireConnection;6;1;1;0
WireConnection;13;0;7;0
WireConnection;13;1;8;0
WireConnection;11;0;3;0
WireConnection;11;1;6;0
WireConnection;10;0;4;0
WireConnection;10;1;5;0
WireConnection;18;0;15;0
WireConnection;18;1;9;2
WireConnection;17;0;11;0
WireConnection;16;0;14;0
WireConnection;16;1;13;0
WireConnection;19;0;12;0
WireConnection;19;1;10;0
WireConnection;21;0;17;0
WireConnection;20;1;16;0
WireConnection;23;0;18;0
WireConnection;22;1;19;0
WireConnection;28;0;23;0
WireConnection;28;1;24;0
WireConnection;32;0;34;0
WireConnection;32;1;33;0
WireConnection;32;2;21;0
WireConnection;29;0;22;1
WireConnection;29;1;27;0
WireConnection;25;0;20;3
WireConnection;25;1;18;0
WireConnection;36;0;26;0
WireConnection;36;1;27;0
WireConnection;26;0;22;1
WireConnection;26;1;37;0
WireConnection;30;0;25;0
WireConnection;30;1;28;0
WireConnection;35;0;32;0
WireConnection;35;1;29;0
WireConnection;37;0;17;0
WireConnection;0;2;35;0
WireConnection;0;4;31;0
WireConnection;0;10;30;0
ASEEND*/
//CHKSM=94AC6682F0F1265BAFB659E7636E2A083DABD6B1