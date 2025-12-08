Shader "GameKit/Sample/ProceduralCube"
{
    Properties
    {
        _BaseColor("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalRenderPipeline"
        }

        Pass
        {
            Name "ForwardUnlit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
            CBUFFER_END

            static const float3 kPositions[36] = {
                float3(-0.5, -0.5, -0.5), float3( 0.5,  0.5, -0.5), float3( 0.5, -0.5, -0.5),
                float3(-0.5, -0.5, -0.5), float3(-0.5,  0.5, -0.5), float3( 0.5,  0.5, -0.5),

                float3(-0.5, -0.5,  0.5), float3( 0.5, -0.5,  0.5), float3( 0.5,  0.5,  0.5),
                float3(-0.5, -0.5,  0.5), float3( 0.5,  0.5,  0.5), float3(-0.5,  0.5,  0.5),

                float3(-0.5, -0.5,  0.5), float3(-0.5,  0.5,  0.5), float3(-0.5,  0.5, -0.5),
                float3(-0.5, -0.5,  0.5), float3(-0.5,  0.5, -0.5), float3(-0.5, -0.5, -0.5),

                float3( 0.5, -0.5, -0.5), float3( 0.5,  0.5, -0.5), float3( 0.5,  0.5,  0.5),
                float3( 0.5, -0.5, -0.5), float3( 0.5,  0.5,  0.5), float3( 0.5, -0.5,  0.5),

                float3(-0.5, -0.5, -0.5), float3( 0.5, -0.5, -0.5), float3( 0.5, -0.5,  0.5),
                float3(-0.5, -0.5, -0.5), float3( 0.5, -0.5,  0.5), float3(-0.5, -0.5,  0.5),

                float3(-0.5,  0.5, -0.5), float3(-0.5,  0.5,  0.5), float3( 0.5,  0.5,  0.5),
                float3(-0.5,  0.5, -0.5), float3( 0.5,  0.5,  0.5), float3( 0.5,  0.5, -0.5)
            };

            struct Attributes
            {
                uint vertexID : SV_VertexID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                float3 positionOS = kPositions[input.vertexID];
                float4 positionWS = mul(GetObjectToWorldMatrix(), float4(positionOS, 1.0));
                output.positionCS = mul(GetWorldToHClipMatrix(), positionWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                return _BaseColor;
            }
            ENDHLSL

            Blend One Zero
            ZWrite On

            Cull Back
        }
    }
}
