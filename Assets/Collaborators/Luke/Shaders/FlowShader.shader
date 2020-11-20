// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlowShader"
{
	Properties
	{
		_FillValue("FillValue", Range( 0 , 2)) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.6
		_EdgePower("EdgePower", Float) = 2.55
		_WaterFoam("WaterFoam", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_FlowEndNoiseSpeed("FlowEndNoiseSpeed", Vector) = (0.1,-0.1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _WaterFoam;
		SamplerState sampler_WaterFoam;
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
			float gradientNoise22 = GradientNoise(( i.uv_texcoord + ( float2( 0,-0.5 ) * _Time.y ) ),6.0);
			gradientNoise22 = gradientNoise22*0.5 + 0.5;
			float4 color20 = IsGammaSpace() ? float4(0,1.521569,4,0) : float4(0,2.517918,21.11213,0);
			o.Emission = ( ( tex2D( _WaterFoam, ( i.uv_texcoord + ( float2( -0.2,-0.35 ) * _Time.y ) ) ).r + pow( gradientNoise22 , 1.25 ) ) * color20 ).rgb;
			o.Alpha = 1;
			float temp_output_4_0 = ( _FillValue - i.uv_texcoord.y );
			float clampResult33 = clamp( temp_output_4_0 , 0.0 , 1.0 );
			clip( ( ( tex2D( _TextureSample0, ( i.uv_texcoord + ( _FlowEndNoiseSpeed * _Time.y ) ) ).b + temp_output_4_0 ) * pow( clampResult33 , _EdgePower ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
-1920;0;1920;1019;1220.658;417.5696;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;15;-1326.215,-672.9807;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;16;-1316.216,-803.6801;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;False;0,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1218.08,-927.6308;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;25;-1442.809,-288.443;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;False;0;False;-0.2,-0.35;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;26;-1432.006,-156.4431;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1118.915,-799.4799;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;41;-1114.281,-103.9796;Inherit;False;Property;_FlowEndNoiseSpeed;FlowEndNoiseSpeed;5;0;Create;True;0;0;False;0;False;0.1,-0.1;0.1,-0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;40;-1069.681,33.2197;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-707.2877,142.0179;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1239.005,-237.4432;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-965.9384,-868.0172;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-1362.87,-409.7927;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-876.6824,-47.77991;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-877.0508,-179.8299;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-759.9776,49.55774;Inherit;False;Property;_FillValue;FillValue;0;0;Create;True;0;0;False;0;False;0;1.127;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;22;-774.6305,-873.5304;Inherit;True;Gradient;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-600.2063,-117.616;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1086.026,-363.1791;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-446.8,55.89999;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;32;-426.1774,-191.6549;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;False;-1;ed7ba18ac1b0b5b409d784d22399347a;ed7ba18ac1b0b5b409d784d22399347a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;24;-480.6305,-868.5304;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-783.6224,-610.2572;Inherit;True;Property;_WaterFoam;WaterFoam;3;0;Create;True;0;0;False;0;False;-1;ed7ba18ac1b0b5b409d784d22399347a;ed7ba18ac1b0b5b409d784d22399347a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;33;-204.017,87.79588;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-131.0999,331.5004;Inherit;False;Property;_EdgePower;EdgePower;2;0;Create;True;0;0;False;0;False;2.55;5.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;38;67.58292,245.9959;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-669.6251,-302.0316;Inherit;False;Constant;_Color1;Color 1;5;1;[HDR];Create;True;0;0;False;0;False;0,1.521569,4,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-436.6305,-580.5304;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;64.08282,-102.5042;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;10;179.7116,869.706;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;342.183,55.49579;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-202.1636,869.706;Inherit;False;1;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;9;-13.15877,869.706;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;7;365.3618,868.3533;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-131.4555,-533.2319;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;562,-266;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;FlowShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.6;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;0
WireConnection;17;1;15;0
WireConnection;27;0;25;0
WireConnection;27;1;26;0
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;43;0;41;0
WireConnection;43;1;40;0
WireConnection;22;0;19;0
WireConnection;44;0;42;0
WireConnection;44;1;43;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;4;0;3;0
WireConnection;4;1;1;2
WireConnection;32;1;44;0
WireConnection;24;0;22;0
WireConnection;11;1;29;0
WireConnection;33;0;4;0
WireConnection;38;0;33;0
WireConnection;38;1;6;0
WireConnection;21;0;11;1
WireConnection;21;1;24;0
WireConnection;37;0;32;3
WireConnection;37;1;4;0
WireConnection;10;0;9;0
WireConnection;39;0;37;0
WireConnection;39;1;38;0
WireConnection;9;0;8;0
WireConnection;7;0;10;0
WireConnection;12;0;21;0
WireConnection;12;1;20;0
WireConnection;0;2;12;0
WireConnection;0;10;39;0
ASEEND*/
//CHKSM=272E7011EA0916877763CB0895DEEA0500A9BD76