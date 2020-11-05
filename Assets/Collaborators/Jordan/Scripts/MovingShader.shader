// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RealisticMovingTexture"
{
	Properties
	{
		_Frequency("Frequency", Range(0 , 20)) = 1
		_Amplitude("Amplitude", Range(0 , 100)) = 2
		[Header(Refraction)]
		_ChromaticAberration("Chromatic Aberration", Range(0 , 0.3)) = 0.1
		[Header(Translucency)]
		_Translucency("Strength", Range(0 , 50)) = 1
		_TransNormalDistortion("Normal Distortion", Range(0 , 1)) = 0.1
		_TransScattering("Scaterring Falloff", Range(1 , 50)) = 2
		_TransDirect("Direct", Range(0 , 1)) = 1
		_TransAmbient("Ambient", Range(0 , 1)) = 0.2
		_TransShadow("Shadow", Range(0 , 1)) = 0.9
		_OffsetTime("OffsetTime", Range(0 , 100)) = 2
		_OffsetLengthScalar("OffsetLengthScalar", Range(0 , 100)) = 2
		_OffsetMagnitudeScalar("OffsetMagnitudeScalar", Range(0 , 100)) = 2
		_OffsetAmplitude("OffsetAmplitude", Range(0 , 100)) = 2
		_OffsetX("OffsetX", Range(0 , 100)) = 2
		_OffsetZ("OffsetZ", Range(0 , 100)) = 2
		[HDR]_EmissiveColor("Emissive Color", Color) = (0,1.541687,1.624505,0)
		[HDR]_FresnelColor("Fresnel Color", Color) = (3.865819,0.6004477,0,0)
		_AlbedoColor("Albedo Color", Color) = (1,1,1,0)
		_TranslucencyParam("TranslucencyParam", Color) = (1,1,1,0)
		_Metallic("Metallic", Float) = 0
		_Power("Power", Float) = 2.22
		_Opacity("Opacity", Float) = 0.5
		_Refraction("Refraction", Float) = 0
		_Smoothness("Smoothness", Float) = 0
		[HideInInspector] __dirty("", Int) = 1
	}

		SubShader
		{
			Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
			Cull Back
			GrabPass{ }
			CGINCLUDE
			#include "UnityShaderVariables.cginc"
			#include "UnityPBSLighting.cginc"
			#include "Lighting.cginc"
			#pragma target 3.0
			#pragma multi_compile _ALPHAPREMULTIPLY_ON
			#ifdef UNITY_PASS_SHADOWCASTER
				#undef INTERNAL_DATA
				#undef WorldReflectionVector
				#undef WorldNormalVector
				#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
				#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
				#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
			#endif
			struct Input
			{
				float3 worldPos;
				float3 worldNormal;
				INTERNAL_DATA
				float4 screenPos;
			};

			struct SurfaceOutputStandardCustom
			{
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half Metallic;
				half Smoothness;
				half Occlusion;
				half Alpha;
				half3 Translucency;
			};

			uniform float _OffsetX;
			uniform float _Frequency;
			uniform float _OffsetTime;
			uniform float _OffsetLengthScalar;
			uniform float _Amplitude;
			uniform float _OffsetMagnitudeScalar;
			uniform float _OffsetAmplitude;
			uniform float _OffsetZ;
			uniform float4 _AlbedoColor;
			uniform float4 _EmissiveColor;
			uniform float4 _FresnelColor;
			uniform float _Power;
			uniform float _Metallic;
			uniform float _Smoothness;
			uniform half _Translucency;
			uniform half _TransNormalDistortion;
			uniform half _TransScattering;
			uniform half _TransDirect;
			uniform half _TransAmbient;
			uniform half _TransShadow;
			uniform float4 _TranslucencyParam;
			uniform float _Opacity;
			uniform sampler2D _GrabTexture;
			uniform float _ChromaticAberration;
			uniform float _Refraction;

			void vertexDataFunc(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				float3 ase_vertex3Pos = v.vertex.xyz;
				float temp_output_33_0 = ((_Time.y * _Frequency) + _OffsetTime + (ase_vertex3Pos.y * _OffsetLengthScalar));
				float3 appendResult42 = (float3(_OffsetX , ((sin(temp_output_33_0) * _Amplitude * (ase_vertex3Pos.y * _OffsetMagnitudeScalar)) + _OffsetAmplitude) , _OffsetZ));
				v.vertex.xyz += appendResult42;
				v.vertex.w = 1;
			}

			inline half4 LightingStandardCustom(SurfaceOutputStandardCustom s, half3 viewDir, UnityGI gi)
			{
				#if !DIRECTIONAL
				float3 lightAtten = gi.light.color;
				#else
				float3 lightAtten = lerp(_LightColor0.rgb, gi.light.color, _TransShadow);
				#endif
				half3 lightDir = gi.light.dir + s.Normal * _TransNormalDistortion;
				half transVdotL = pow(saturate(dot(viewDir, -lightDir)), _TransScattering);
				half3 translucency = lightAtten * (transVdotL * _TransDirect + gi.indirect.diffuse * _TransAmbient) * s.Translucency;
				half4 c = half4(s.Albedo * translucency * _Translucency, 0);

				SurfaceOutputStandard r;
				r.Albedo = s.Albedo;
				r.Normal = s.Normal;
				r.Emission = s.Emission;
				r.Metallic = s.Metallic;
				r.Smoothness = s.Smoothness;
				r.Occlusion = s.Occlusion;
				r.Alpha = s.Alpha;
				return LightingStandard(r, viewDir, gi) + c;
			}

			inline void LightingStandardCustom_GI(SurfaceOutputStandardCustom s, UnityGIInput data, inout UnityGI gi)
			{
				#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
					gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
				#else
					UNITY_GLOSSY_ENV_FROM_SURFACE(g, s, data);
					gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal, g);
				#endif
			}

			inline float4 Refraction(Input i, SurfaceOutputStandardCustom o, float indexOfRefraction, float chomaticAberration) {
				float3 worldNormal = o.Normal;
				float4 screenPos = i.screenPos;
				#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
				#else
					float scale = 1.0;
				#endif
				float halfPosW = screenPos.w * 0.5;
				screenPos.y = (screenPos.y - halfPosW) * _ProjectionParams.x * scale + halfPosW;
				#if SHADER_API_D3D9 || SHADER_API_D3D11
					screenPos.w += 0.00000000001;
				#endif
				float2 projScreenPos = (screenPos / screenPos.w).xy;
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				float3 refractionOffset = (indexOfRefraction - 1.0) * mul(UNITY_MATRIX_V, float4(worldNormal, 0.0)) * (1.0 - dot(worldNormal, worldViewDir));
				float2 cameraRefraction = float2(refractionOffset.x, refractionOffset.y);
				float4 redAlpha = tex2D(_GrabTexture, (projScreenPos + cameraRefraction));
				float green = tex2D(_GrabTexture, (projScreenPos + (cameraRefraction * (1.0 - chomaticAberration)))).g;
				float blue = tex2D(_GrabTexture, (projScreenPos + (cameraRefraction * (1.0 + chomaticAberration)))).b;
				return float4(redAlpha.r, green, blue, redAlpha.a);
			}

			void RefractionF(Input i, SurfaceOutputStandardCustom o, inout half4 color)
			{
				#ifdef UNITY_PASS_FORWARDBASE
				color.rgb = color.rgb + Refraction(i, o, _Refraction, _ChromaticAberration) * (1 - color.a);
				color.a = 1;
				#endif
			}

			void surf(Input i , inout SurfaceOutputStandardCustom o)
			{
				o.Normal = float3(0,0,1);
				o.Albedo = _AlbedoColor.rgb;
				float3 ase_worldPos = i.worldPos;
				float3 ase_worldViewDir = normalize(UnityWorldSpaceViewDir(ase_worldPos));
				float3 ase_worldNormal = WorldNormalVector(i, float3(0, 0, 1));
				float fresnelNdotV9 = dot(ase_worldNormal, ase_worldViewDir);
				float fresnelNode9 = (0.0 + 1.0 * pow(1.0 - fresnelNdotV9, _Power));
				float4 lerpResult16 = lerp(_EmissiveColor , _FresnelColor , fresnelNode9);
				o.Emission = lerpResult16.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Smoothness;
				o.Translucency = _TranslucencyParam.rgb;
				float lerpResult18 = lerp(_Opacity , 1.0 , fresnelNode9);
				o.Alpha = lerpResult18;
				o.Normal = o.Normal + 0.00001 * i.screenPos * i.worldPos;
			}

			ENDCG
			CGPROGRAM
			#pragma surface surf StandardCustom keepalpha finalcolor:RefractionF fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

			ENDCG
			Pass
			{
				Name "ShadowCaster"
				Tags{ "LightMode" = "ShadowCaster" }
				ZWrite On
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_shadowcaster
				#pragma multi_compile UNITY_PASS_SHADOWCASTER
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#include "HLSLSupport.cginc"
				#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
					#define CAN_SKIP_VPOS
				#endif
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				sampler3D _DitherMaskLOD;
				struct v2f
				{
					V2F_SHADOW_CASTER;
					float4 screenPos : TEXCOORD1;
					float4 tSpace0 : TEXCOORD2;
					float4 tSpace1 : TEXCOORD3;
					float4 tSpace2 : TEXCOORD4;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};
				v2f vert(appdata_full v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					Input customInputData;
					vertexDataFunc(v, customInputData);
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					half3 worldNormal = UnityObjectToWorldNormal(v.normal);
					half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
					half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
					half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
					o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
					o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
					o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					o.screenPos = ComputeScreenPos(o.pos);
					return o;
				}
				half4 frag(v2f IN
				#if !defined( CAN_SKIP_VPOS )
				, UNITY_VPOS_TYPE vpos : VPOS
				#endif
				) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);
					Input surfIN;
					UNITY_INITIALIZE_OUTPUT(Input, surfIN);
					float3 worldPos = float3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
					half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
					surfIN.worldPos = worldPos;
					surfIN.worldNormal = float3(IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z);
					surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
					surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
					surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
					surfIN.screenPos = IN.screenPos;
					SurfaceOutputStandardCustom o;
					UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandardCustom, o)
					surf(surfIN, o);
					#if defined( CAN_SKIP_VPOS )
					float2 vpos = IN.pos;
					#endif
					half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy * 0.25, o.Alpha * 0.9375)).a;
					clip(alphaRef - 0.01);
					SHADOW_CASTER_FRAGMENT(IN)
				}
				ENDCG
			}
		}
			Fallback "Diffuse"
						CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
200.8;73.6;891;439;1075.234;-1334.926;1;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;21;-1834.852,1288.144;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1908.631,1455.061;Inherit;False;Property;_OffsetLengthScalar;OffsetLengthScalar;15;0;Create;True;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1529.631,1321.061;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1868.361,1182.412;Inherit;False;Property;_Frequency;Frequency;1;0;Create;True;0;0;False;0;False;1;2.49;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;22;-1781.014,1084.496;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;30;-1233.631,1299.061;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1503.595,1183.87;Inherit;False;Property;_OffsetTime;OffsetTime;14;0;Create;True;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1498.97,1084.558;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-1906.168,1560.577;Inherit;False;Property;_OffsetMagnitudeScalar;OffsetMagnitudeScalar;16;0;Create;True;0;0;False;0;False;2;0.2;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-1177.595,1092.87;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1544.631,1505.061;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1178.539,1397.543;Inherit;False;Property;_Amplitude;Amplitude;2;0;Create;True;0;0;False;0;False;2;1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;44;-989.5892,1210.589;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-868.1785,1390.838;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1103.402,1585.447;Inherit;False;Property;_OffsetAmplitude;OffsetAmplitude;17;0;Create;True;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1375.084,241.4589;Inherit;False;Property;_Power;Power;26;0;Create;True;0;0;False;0;False;2.22;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-647.6465,1649.727;Inherit;False;Property;_OffsetZ;OffsetZ;19;0;Create;True;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-642.8785,1414.611;Inherit;False;Property;_OffsetX;OffsetX;18;0;Create;True;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-663.4164,1527.91;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-704.7258,590.7407;Inherit;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-766.517,430.882;Inherit;False;Property;_Opacity;Opacity;27;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;9;-1289.824,-11.4771;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-1135.271,371.5029;Inherit;False;Property;_FresnelColor;Fresnel Color;22;1;[HDR];Create;True;0;0;False;0;False;3.865819,0.6004477,0,0;0.0007314645,0.0007314645,0.0007314645,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-945.538,-77.1054;Inherit;False;Property;_EmissiveColor;Emissive Color;20;1;[HDR];Create;True;0;0;False;0;False;0,1.541687,1.624505,0;0.0007314645,0.0007314645,0.0007314645,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;42;-285.4274,1491.785;Inherit;True;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-883.1184,174.3875;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;18;-528.4848,495.3304;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-503.2506,286.1431;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-656.663,-247.2119;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-262.1522,272.3845;Inherit;False;Property;_Metallic;Metallic;25;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-608.7717,43.13596;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1711.984,723.5641;Inherit;False;Property;_Scale;Scale;5;0;Create;True;0;0;False;0;False;2;5.267218;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1488.805,916.2561;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-315.1522,-94.61548;Inherit;False;Property;_AlbedoColor;Albedo Color;23;0;Create;True;0;0;False;0;False;1,1,1,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;45;-200.696,989.4365;Inherit;False;Property;_Color;Color;21;0;Create;True;0;0;False;0;False;0.5371177,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-255.4318,598.0873;Inherit;False;Property;_Refraction;Refraction;28;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-312.1562,429.426;Inherit;False;Property;_TranslucencyParam;TranslucencyParam;24;0;Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;34;-757.2095,874.0845;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-1282.617,885.933;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1729.55,812.4932;Inherit;False;Property;_Float1;Float 1;6;0;Create;True;0;0;False;0;False;5;5.267218;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-260.1522,350.3845;Inherit;False;Property;_Smoothness;Smoothness;29;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;26;-1648.843,922.1212;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;RealisticMovingTexture;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;7;3;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;21;2
WireConnection;24;1;20;0
WireConnection;30;0;24;0
WireConnection;31;0;22;0
WireConnection;31;1;23;0
WireConnection;33;0;31;0
WireConnection;33;1;28;0
WireConnection;33;2;30;0
WireConnection;39;0;21;2
WireConnection;39;1;41;0
WireConnection;44;0;33;0
WireConnection;43;0;44;0
WireConnection;43;1;37;0
WireConnection;43;2;39;0
WireConnection;38;0;43;0
WireConnection;38;1;40;0
WireConnection;9;3;12;0
WireConnection;42;0;36;0
WireConnection;42;1;38;0
WireConnection;42;2;35;0
WireConnection;10;0;9;0
WireConnection;10;1;13;0
WireConnection;18;0;7;0
WireConnection;18;1;19;0
WireConnection;18;2;9;0
WireConnection;15;0;9;0
WireConnection;15;1;7;0
WireConnection;16;0;1;0
WireConnection;16;1;13;0
WireConnection;16;2;9;0
WireConnection;14;0;10;0
WireConnection;14;1;1;0
WireConnection;29;0;25;0
WireConnection;29;1;26;0
WireConnection;34;1;33;0
WireConnection;34;2;32;0
WireConnection;32;0;29;0
WireConnection;32;1;27;0
WireConnection;26;0;22;0
WireConnection;0;0;2;0
WireConnection;0;2;16;0
WireConnection;0;3;3;0
WireConnection;0;4;4;0
WireConnection;0;7;6;0
WireConnection;0;8;8;0
WireConnection;0;9;18;0
WireConnection;0;11;42;0
ASEEND*/
//CHKSM=91D1D53BE05FA734F9EE092A5EAA547E200ACC16