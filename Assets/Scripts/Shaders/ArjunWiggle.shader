Shader "Custom/FishWiggle"
{
    Properties
    {
        _Color("MainColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _SpeedX("SpeedX", Range(0, 100)) = 1
        _FrequencyX("FrequencyX", Range(0, 10)) = 1
        _AmplitudeX("AmplitudeX", Range(0, 0.2)) = 1
        _SpeedY("SpeedY", Range(0, 100)) = 1
        _FrequencyY("FrequencyY", Range(0, 10)) = 1
        _AmplitudeY("AmplitudeY", Range(0, 0.2)) = 1
        _SpeedZ("SpeedZ", Range(0, 100)) = 1
        _FrequencyZ("FrequencyZ", Range(0, 10)) = 1
        _AmplitudeZ("AmplitudeZ", Range(0,  2)) = 1
        _HeadLimit("HeadLimit", Range(-2,  2)) = 0.05
    }
    SubShader
        {
            Pass{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            float _SpeedX;
            float _FrequencyX;
            float _AmplitudeX;

            float _SpeedY;
            float _FrequencyY;
            float _AmplitudeY;

            float _SpeedZ;
            float _FrequencyZ;
            float _AmplitudeZ;

            // Mask value to reduce influence on head
            float _HeadLimit;

            v2f vert(appdata_base v)
            {
                v2f o;

                v.vertex.z += sin((v.vertex.z + _Time.y * _SpeedX) * _FrequencyX) * _AmplitudeX;

                v.vertex.y += sin((v.vertex.y + _Time.y * _SpeedY) * _FrequencyY) * _AmplitudeY;

                if (v.vertex.z > _HeadLimit)
                {
                    v.vertex.x += sin((0.05 + _Time.y * _SpeedZ) * _FrequencyZ) * _AmplitudeZ * _HeadLimit;
                }
                else
                {
                    v.vertex.x += sin((v.vertex.z + _Time.y * _SpeedZ) * _FrequencyZ) * _AmplitudeZ * v.vertex.z;
                }

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv) * _Color;
            }
            ENDCG
            }
        }
    FallBack "Diffuse"
}
