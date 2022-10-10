Shader "Unlit/cs3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gloss ("Gloss", Float) = 10
        _Csp ("Cel Shading Parameters", Vector) = (0.1, 0.3, 0.6, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags{ "LightMode" = "UniversalForward" }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RealtimeLights.hlsl"
            //#include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"

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
            float4 _Csp;

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
                float diff = max(dot(normal, lightDir), 0.0f);

                if (diff < _Csp.x) diff = 0.05f;
                else if (diff < _Csp.y) diff = _Csp.y;
                else if (diff < _Csp.z) diff = _Csp.z;
                else diff = _Csp.w;

                float3 diffuse = diff * _LightColor0.rgb;

                // ===============================================

                // Specular Lightning ============================
                float3 camPos = _WorldSpaceCameraPos;
                float3 viewDir = normalize(camPos - worldPos);
                float3 reflectDir = reflect(-viewDir, normal);
                float specular = pow(max(dot(reflectDir, lightDir), 0.0f), _Gloss);
                // ===============================================

                return diffuse + specular;
            }

            float3 CalculatePointLight(float3 worldPos, float3 normal, float3 lightDir, float3 lightColor, float lightAttenuation)
            {
                //float3 lightPos = unity_LightPosition[i].xyz;
                //float3 lightPos = _WorldSpaceLightPos0.xyz;
                //float3 lightColor = unity_LightColor[i].xyz;
                //float lightAttenuation = unity_LightAtten[i].xyz;


                //float3 lightPos = unity_4LightPosX0.xyz;
                //float3 lightColor = unity_LightColor[i].rgb;
                //float3 lightAttenuation = unity_4LightAtten0.xyz;

                //float3 lightDir = normalize(lightPos - worldPos);
                float diff = max(dot(normal, lightDir), 0.0);

                if (diff < _Csp.x) diff = 0.05f;
                else if (diff < _Csp.y) diff = _Csp.y;
                else if (diff < _Csp.z) diff = _Csp.z;
                else diff = _Csp.w;

                float3 diffuse = diff * lightColor;

                float3 camPos = _WorldSpaceCameraPos;
                float3 viewDir = normalize(camPos - worldPos);
                float3 reflectDir = reflect(-viewDir, normal);
                float specular = pow(max(dot(reflectDir, lightDir), 0.0f), _Gloss);

                return (diffuse /*+ specular*/)*lightAttenuation;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                
                fixed4 col = fixed4(0, 0, 0, 0);
                float3 worldPos = i.worldPos;
                float3 normal = normalize(i.normal);

                float3 color = float3(0, 0, 0);
                color += CalculateDirLight(worldPos, normal);

                //int lightsCount = GetAdditionalLightsCount();
                for (int i = 0; i < 8; i++)
                {
                    //Light light = GetAdditionalLight(i, worldPos);
                    //color += CalculatePointLight(worldPos, normal, light.direction, light.color, light.attenuation);
                }
                
                col.rgb = color * tex.rgb;
                return col;
            }
            ENDCG
        }

        //Pass
        //{
        //    Name "Additional Lights Pass"
        //    Tags{ "LightMode" = "UniversalForward" }
        //    CGPROGRAM

        //    #pragma vertex vert
        //    #pragma fragment frag

        //    #include "UnityCG.cginc"
        //    #include "Lighting.cginc"
        //    #include "AutoLight.cginc"

        //    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        //    struct appdata
        //    {
        //        float4 position : POSITION;
        //        float3 normal : NORMAL;
        //        float3 worldPos : TEXCOORD0;
        //        float2 uv : TEXCOORD1;
        //    };

        //    struct v2f
        //    {
        //        float4 position : SV_POSITION;
        //        float3 normal : TEXCOORD3;
        //        float3 worldPos : TEXCOORD0;
        //        float2 uv : TEXCOORD1;
        //    };

        //    sampler2D _MainTex;
        //    float4 _MainTex_ST;
        //    float _Gloss;
        //    float4 _Csp;


        //    v2f vert(appdata v)
        //    {
        //        v2f o;
        //        o.position = UnityObjectToClipPos(v.position);
        //        o.normal = UnityObjectToWorldNormal(v.normal);
        //        o.worldPos = mul(unity_ObjectToWorld, v.position);
        //        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //        return o;
        //    }


        //    float3 CalculatePointLight(float3 worldPos, float3 normal, float3 lightDir, float3 lightColor, float lightAttenuation)
        //    {
        //        //float3 lightPos = unity_LightPosition[i].xyz;
        //        //float3 lightPos = _WorldSpaceLightPos0.xyz;
        //        //float3 lightColor = unity_LightColor[i].xyz;
        //        //float lightAttenuation = unity_LightAtten[i].xyz;


        //        //float3 lightPos = unity_4LightPosX0.xyz;
        //        //float3 lightColor = unity_LightColor[i].rgb;
        //        //float3 lightAttenuation = unity_4LightAtten0.xyz;

        //        //float3 lightDir = normalize(lightPos - worldPos);
        //        float diff = max(dot(normal, lightDir), 0.0);

        //        if (diff < _Csp.x) diff = 0.05f;
        //        else if (diff < _Csp.y) diff = _Csp.y;
        //        else if (diff < _Csp.z) diff = _Csp.z;
        //        else diff = _Csp.w;

        //        float3 diffuse = diff * lightColor;

        //        float3 camPos = _WorldSpaceCameraPos;
        //        float3 viewDir = normalize(camPos - worldPos);
        //        float3 reflectDir = reflect(-viewDir, normal);
        //        float specular = pow(max(dot(reflectDir, lightDir), 0.0f), _Gloss);

        //        return (diffuse /*+ specular*/)*lightAttenuation;
        //    }

        //    fixed4 frag(v2f i) : SV_Target
        //    {
        //        fixed4 col = tex2D(_MainTex, i.uv);

        //        float3 worldPos = i.worldPos;
        //        float3 normal = normalize(i.normal);

        //        float3 color = float3(1, 1, 1);

        //        int lightsCount = GetAdditionalLightsCount();
        //        color.r = lightsCount;
        //        for (int i = 0; i < lightsCount; i++)
        //        {
        //            Light light = GetAdditionalLight(i, worldPos);
        //            color += CalculatePointLight(worldPos, normal, light.direction, light.color, light.attenuation);
        //        }

        //        //col.rgb = color;
        //        col.rgb = float3(1, 1, 1);
        //        return col;
        //    }
        //        ENDCG

        //}

        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            // -------------------------------------
            // Universal Pipeline keywords

            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            ENDHLSL
        }


    }
}
