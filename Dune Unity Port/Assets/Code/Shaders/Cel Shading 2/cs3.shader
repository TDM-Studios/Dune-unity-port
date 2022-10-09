Shader "Unlit/cs3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss ("Gloss", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 normal : TEXCOORD3;
                float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.position);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.position);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 CalculateDirLight(float3 worldPos, float3 normal)
            {
                // Diffuse Lightning =============================
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float3 diffuse = max(dot(normal, lightDir), 0.0f) * _LightColor0.rgb;
                // ===============================================

                // Specular Lightning ============================
                float3 camPos = _WorldSpaceCameraPos;
                float3 viewDir = normalize(camPos - worldPos);
                float3 reflectDir = reflect(-viewDir, normal);
                float specular = pow(max(dot(reflectDir, lightDir), 0.0f), _Gloss);
                // ===============================================

                return diffuse + specular;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                float3 normal = normalize(i.normal);
                col.rgb = CalculateDirLight(i.worldPos, normal);
                
                return col;
            }
            ENDCG
        }
    }
}
