Shader "Toon/SurfaceToonWithOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color Tint", Color) = (1,1,1,1)
        _Detail ("Toon Step Detail", Range(0.01,1)) = 0.3
        _Brightness ("Ambient Brightness", Range(0,1)) = 0.2
        _Strength ("Light Strength", Range(0,1)) = 0.8
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0.001, 3)) = 0.03
        _ShadowColor ("Shadow Color", Color) = (0.1,0.1,0.2,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front
            Offset 1, 1

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;

                float3 viewNormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float4 viewPos = mul(UNITY_MATRIX_MV, v.vertex);

                viewPos.xyz += normalize(viewNormal) * _OutlineWidth;

                o.pos = mul(UNITY_MATRIX_P, viewPos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        CGPROGRAM
        #pragma surface surf ToonRamp fullforwardshadows

        sampler2D _MainTex;
        float4 _Color;
        float _Detail;
        float _Brightness;
        float _Strength;
        float4 _ShadowColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color.rgb;
            o.Alpha = tex.a;
        }

        half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {
            float ndotl = max(0, dot(s.Normal, lightDir));
            float step = ndotl > 0.5 ? 1.0 : 0.0;

            half3 lightColor = s.Albedo * _Color.rgb;
            half3 shadowColor = _ShadowColor.rgb;

            half3 finalColor = lerp(shadowColor, lightColor, step) * (_Brightness + _Strength) * atten;

            return half4(finalColor, s.Alpha);
        }
        ENDCG
    }

    FallBack "Diffuse"
}
