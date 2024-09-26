Shader "Shader Graphs/S_UI_Base_FromCode"
{
    Properties
    {
        [Header(Main)]
        [NoScaleOffset]_MainTex("MainTex", 2D) = "white" {}
        _main_color("main_color", Color) = (1, 1, 1, 0)
        [NoScaleOffset]_main_alpha_tex("main_alpha_tex", 2D) = "white" {}
        [NoScaleOffset]_dissolve_tex("dissolve_tex", 2D) = "white" {}
        [Space(10)]

        [Header(Dissolve)]
        _dissolve_amount("dissolve_amount", Range(-1, 1)) = 1
        [ToggleUI]_is_up_direction("is_up_direction", Float) = 0
        _edge_width("edge_width", Range(0, 1)) = 0.01
        [HDR]_dissolve_color("dissolve_color", Color) = (2.858034, 2.367068, 0.6336207, 0)
        _noise_size("noise_size", Float) = 30
        _noise_str("noise_str", Float) = 30
        _multiplier("multiplier", Float) = 0
        [Space(10)]

        [Header(Line)]
        _rot("rot", Float) = 0
        [HDR]_line_color("line_color", Color) = (1, 1, 1, 0)
        _line_opacity("line_opacity", Float) = 0
        _line_amount("line_amount", Range(0, 1)) = 2.92
        [Space(10)]

        [Header(SubTexture)]
        [ToggleUI]_use_subtex("use_subtex", Float) = 0
        [HDR]_sub_color("sub_color", Color) = (1, 1, 1, 0)
        [NoScaleOffset]_sub_tex("sub_tex", 2D) = "white" {}
        [NoScaleOffset]_sub_alpha_tex("sub_alpha_tex", 2D) = "white" {}
        [HDR]_sub_sec_color("sub_sec_color", Color) = (1, 1, 1, 0)
        [NoScaleOffset]_sub_sec_tex("sub_sec_tex", 2D) = "white" {}

        [Header(Aura)]
        [ToggleUI]_use_aura("use_aura", Float) = 1
        [ToggleUI]_use_aura_mask("use_aura_mask", Float) = 1
        [ToggleUI]_use_aura_mask_1("use_aura_mask_-1", Float) = 0
        [NoScaleOffset]_aura_tex("aura_tex", 2D) = "white" {}
        [HDR]_aura_color("aura_color", Color) = (0.5676507, 0.5135309, 0.3186342, 0)
        [Space(5)]

        _aura_ins("aura_ins", Range(1, 10)) = 1
        _aura_power("aura_power", Float) = 0.72
        [Space(5)]

        _aura_tex_tiling("aura_tex_tiling", Vector) = (2, 2, 0, 0)
        [Space(5)]
        _aura_upanner("aura_upanner", Float) = 0
        _aura_vpanner("aura_vpanner", Float) = -1
        [Space(5)]
        [NoScaleOffset]_aura_normal_tex("aura_normal_tex", 2D) = "white" {}
        [Space(5)]
        _aura_normal_tiling("aura_normal_tiling", Vector) = (1, 1, 0, 0)
        [Space(5)]
        _aura_normal_upanner("aura_normal_upanner", Float) = 1
        _aura_normal_vpanner("aura_normal_vpanner", Float) = 1
        _aura_noraml_str("aura_noraml_str", Range(0, 1)) = 0.03
        _aura_mask_power("aura_mask_power", Float) = 6.25
        _aura_mask_intencity("aura_mask_intencity", Float) = 1


        [HideInInspector]_CastShadows("_CastShadows", Float) = 0
        [HideInInspector]_Surface("_Surface", Float) = 1
        [HideInInspector]_Blend("_Blend", Float) = 0
        [HideInInspector]_AlphaClip("_AlphaClip", Float) = 1
        [HideInInspector]_SrcBlend("_SrcBlend", Float) = 1
        [HideInInspector]_DstBlend("_DstBlend", Float) = 0
        [HideInInspector][ToggleUI]_ZWrite("_ZWrite", Float) = 0
        [HideInInspector]_ZWriteControl("_ZWriteControl", Float) = 0
        [HideInInspector]_ZTest("_ZTest", Float) = 4
        [HideInInspector]_Cull("_Cull", Float) = 2
        [HideInInspector]_AlphaToMask("_AlphaToMask", Float) = 0
        [HideInInspector]_QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
        [Space(5)]
        [Header(Stencil)]
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation",Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) =15
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend]
        ZTest [unity_GUIZTestMode]
        ZWrite [_ZWrite]
        AlphaToMask [_AlphaToMask]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass

        Stencil
        {
            Ref[_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask[_StencilWriteMask]

        }
        ColorMask[_ColorMask]
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma shader_feature _ _SAMPLE_GI
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        #pragma shader_feature_fragment _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _ALPHAMODULATE_ON
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
             float3 normalWS : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_ChannelMask_RedGreen_float4 (float4 In, out float4 Out)
        {
            Out = float4(In.r, In.g, 0, 0);
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_bf8c6aa7f31c456aa30496f5eceb5276_Out_0_Boolean = _use_aura;
            float4 _Property_3aa6a9f3d8e6402096a662cab75c6587_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_aura_color) : _aura_color;
            float _Property_96ae2675c9ce4619a2d109bc2ca88678_Out_0_Boolean = _use_aura_mask;
            float _Property_09a34853954e4858be642f41df2c42fc_Out_0_Boolean = _use_aura_mask_1;
            float4 _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4 = IN.uv0;
            float _Split_88540391b22f4dd7a649b653bfdd7731_R_1_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[0];
            float _Split_88540391b22f4dd7a649b653bfdd7731_G_2_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[1];
            float _Split_88540391b22f4dd7a649b653bfdd7731_B_3_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[2];
            float _Split_88540391b22f4dd7a649b653bfdd7731_A_4_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[3];
            float _Property_45e554c2e6dd4fe7a1c6babe151db9f3_Out_0_Float = _aura_mask_power;
            float _Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float;
            Unity_Power_float(_Split_88540391b22f4dd7a649b653bfdd7731_G_2_Float, _Property_45e554c2e6dd4fe7a1c6babe151db9f3_Out_0_Float, _Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float);
            float _Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float;
            Unity_Saturate_float(_Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float, _Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float);
            float _Property_c30c3926725e41bbbca9de8e64d820a2_Out_0_Float = _aura_mask_intencity;
            float _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float, _Property_c30c3926725e41bbbca9de8e64d820a2_Out_0_Float, _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float);
            float _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float;
            Unity_OneMinus_float(_Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float, _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float);
            float _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float;
            Unity_Branch_float(_Property_09a34853954e4858be642f41df2c42fc_Out_0_Boolean, _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float, _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float, _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float);
            float _Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float;
            Unity_Branch_float(_Property_96ae2675c9ce4619a2d109bc2ca88678_Out_0_Boolean, _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float, 1, _Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float);
            float _Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float;
            Unity_Saturate_float(_Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float, _Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float);
            float _Property_c5f85df2388e47439a88a093738eb2f1_Out_0_Float = _aura_ins;
            UnityTexture2D _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_aura_tex);
            UnityTexture2D _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_aura_normal_tex);
            float2 _Property_8e468c16cf4d49b8aac90a2e06b52edf_Out_0_Vector2 = _aura_normal_tiling;
            float _Property_ce53b97baa3a44269dcdda9638894569_Out_0_Float = _aura_normal_upanner;
            float _Property_e55276b5b9a3402cba1e21e1e1ae1b15_Out_0_Float = _aura_normal_vpanner;
            float4 _Combine_58fa82948b884a049486ab32ff1fc887_RGBA_4_Vector4;
            float3 _Combine_58fa82948b884a049486ab32ff1fc887_RGB_5_Vector3;
            float2 _Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2;
            Unity_Combine_float(_Property_ce53b97baa3a44269dcdda9638894569_Out_0_Float, _Property_e55276b5b9a3402cba1e21e1e1ae1b15_Out_0_Float, 0, 0, _Combine_58fa82948b884a049486ab32ff1fc887_RGBA_4_Vector4, _Combine_58fa82948b884a049486ab32ff1fc887_RGB_5_Vector3, _Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2);
            float2 _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2, (IN.TimeParameters.x.xx), _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2);
            float2 _TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_8e468c16cf4d49b8aac90a2e06b52edf_Out_0_Vector2, _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2, _TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2);
            float4 _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.tex, _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.samplerstate, _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2) );
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_R_4_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.r;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_G_5_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.g;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_B_6_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.b;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_A_7_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.a;
            float4 _ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4;
            Unity_ChannelMask_RedGreen_float4 (_SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4, _ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4);
            float _Property_d8ff5f8e77af43eaa4166a99fa39bb89_Out_0_Float = _aura_noraml_str;
            float4 _Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4, (_Property_d8ff5f8e77af43eaa4166a99fa39bb89_Out_0_Float.xxxx), _Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4);
            float2 _Property_17fa63dc5693405d9237b0f32c4ebdb4_Out_0_Vector2 = _aura_tex_tiling;
            float _Property_7892ee16bbb6427b8f7464e42b5278ac_Out_0_Float = _aura_upanner;
            float _Property_d3564925866f423790d8ee4253cd1407_Out_0_Float = _aura_vpanner;
            float4 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGBA_4_Vector4;
            float3 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGB_5_Vector3;
            float2 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2;
            Unity_Combine_float(_Property_7892ee16bbb6427b8f7464e42b5278ac_Out_0_Float, _Property_d3564925866f423790d8ee4253cd1407_Out_0_Float, 0, 0, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGBA_4_Vector4, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGB_5_Vector3, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2);
            float2 _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2, (IN.TimeParameters.x.xx), _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2);
            float2 _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_17fa63dc5693405d9237b0f32c4ebdb4_Out_0_Vector2, _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2, _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2);
            float2 _Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2;
            Unity_Add_float2((_Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4.xy), _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2, _Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2);
            float4 _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.tex, _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.samplerstate, _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.GetTransformedUV(_Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2) );
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_R_4_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.r;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_G_5_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.g;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_B_6_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.b;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_A_7_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.a;
            float _Property_17fd6a48ac284b298d68d3f6ca75b178_Out_0_Float = _aura_power;
            float _Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float;
            Unity_Power_float(_SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_R_4_Float, _Property_17fd6a48ac284b298d68d3f6ca75b178_Out_0_Float, _Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float);
            float _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float;
            Unity_Saturate_float(_Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float, _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float);
            float _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float;
            Unity_Multiply_float_float(_Property_c5f85df2388e47439a88a093738eb2f1_Out_0_Float, _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float, _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float);
            float _Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float, _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float, _Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float);
            float4 _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_3aa6a9f3d8e6402096a662cab75c6587_Out_0_Vector4, (_Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float.xxxx), _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4);
            float4 _Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4;
            Unity_Branch_float4(_Property_bf8c6aa7f31c456aa30496f5eceb5276_Out_0_Boolean, _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4, float4(0, 0, 0, 0), _Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4);
            float _Property_dc68cefccbb94c3cb1baafd25b46ba62_Out_0_Boolean = _use_subtex;
            float4 _Property_99e3ffd58f9e49068ef85806c99d01dc_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_sub_color) : _sub_color;
            float4 _Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_99e3ffd58f9e49068ef85806c99d01dc_Out_0_Vector4, float4(1, 1, 1, 1), _Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4);
            UnityTexture2D _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_tex);
            float4 _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.tex, _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.samplerstate, _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_R_4_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.r;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_G_5_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.g;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_B_6_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.b;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_A_7_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.a;
            float4 _Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4, _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4, _Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4);
            float4 _Property_c4b91c47dfc645e6ada02e7dc89126ec_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_sub_sec_color) : _sub_sec_color;
            UnityTexture2D _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_sec_tex);
            float4 _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.tex, _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.samplerstate, _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_R_4_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.r;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_G_5_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.g;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_B_6_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.b;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_A_7_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.a;
            float4 _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_c4b91c47dfc645e6ada02e7dc89126ec_Out_0_Vector4, _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4, _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4);
            float4 _Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4;
            Unity_Add_float4(_Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4, _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4, _Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4);
            UnityTexture2D _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_alpha_tex);
            float4 _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.tex, _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.samplerstate, _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_R_4_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.r;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_G_5_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.g;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_B_6_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.b;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_A_7_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.a;
            float _OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_R_4_Float, _OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float);
            float _Property_00d215c2468b4f7389789ef952e5c3d0_Out_0_Float = _line_opacity;
            float _Property_3bfc1c8f32124097bbd579a45e09d7a1_Out_0_Float = _rot;
            float2 _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Property_3bfc1c8f32124097bbd579a45e09d7a1_Out_0_Float, _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2);
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_R_1_Float = _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2[0];
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_G_2_Float = _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2[1];
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_B_3_Float = 0;
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_A_4_Float = 0;
            float _Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float;
            Unity_Add_float(_Split_37b28c9739344c66b5d21efc3bec2ce4_R_1_Float, _Split_37b28c9739344c66b5d21efc3bec2ce4_G_2_Float, _Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float);
            float _Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float;
            Unity_Multiply_float_float(_Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float, 2, _Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float);
            float _Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float, 0.55, _Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float);
            float _Property_9c30ee88cbee494392e07938bbd1aec7_Out_0_Float = _line_amount;
            float _Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float;
            Unity_Add_float(_Property_9c30ee88cbee494392e07938bbd1aec7_Out_0_Float, -0.5, _Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float);
            float _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float;
            Unity_Multiply_float_float(_Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float, 5.74, _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float);
            float _Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float;
            Unity_Add_float(_Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float, _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float, _Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float);
            float _Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float;
            Unity_Sine_float(_Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float, _Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float);
            float _Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float;
            Unity_Saturate_float(_Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float, _Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float);
            float _Float_d88d5ebf2e15431b8578b52cba0dfc42_Out_0_Float = 8.2;
            float _Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float;
            Unity_Power_float(_Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float, _Float_d88d5ebf2e15431b8578b52cba0dfc42_Out_0_Float, _Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float);
            float4 _Property_75a0d0446e854f9c9e4f3344170112ef_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_line_color) : _line_color;
            float4 _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float.xxxx), _Property_75a0d0446e854f9c9e4f3344170112ef_Out_0_Vector4, _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4);
            float4 _Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Property_00d215c2468b4f7389789ef952e5c3d0_Out_0_Float.xxxx), _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4, _Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4);
            float4 _Property_884e592925cd4542b162f0856759af55_Out_0_Vector4 = _main_color;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float4 _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_884e592925cd4542b162f0856759af55_Out_0_Vector4, _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4, _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4);
            float4 _Add_888d24facb73490487eae44562501a54_Out_2_Vector4;
            Unity_Add_float4(float4(0, 0, 0, 0), _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4, _Add_888d24facb73490487eae44562501a54_Out_2_Vector4);
            float4 _Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4;
            Unity_Add_float4(_Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4, _Add_888d24facb73490487eae44562501a54_Out_2_Vector4, _Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4);
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Property_68fa83bd3ff84da9acc92b2a8b125437_Out_0_Float = _edge_width;
            float _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float;
            Unity_Multiply_float_float(_Property_68fa83bd3ff84da9acc92b2a8b125437_Out_0_Float, 210, _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float);
            float _Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float;
            Unity_Subtract_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float, _Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float);
            float _Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float;
            Unity_Step_float(_Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float);
            float _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float;
            Unity_OneMinus_float(_Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float, _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float);
            float _Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float;
            Unity_Subtract_float(_OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float, _Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float);
            float4 _Property_34ff7996b61446ca9af2553e2a3e0c98_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_dissolve_color) : _dissolve_color;
            float4 _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float.xxxx), _Property_34ff7996b61446ca9af2553e2a3e0c98_Out_0_Vector4, _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4);
            float4 _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4;
            Unity_Add_float4(_Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4, _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4, _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4);
            float4 _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4;
            Unity_Multiply_float4_float4((_OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float.xxxx), _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4, _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4);
            float4 _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4;
            Unity_Add_float4(_Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4, _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4, _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4);
            float4 _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4;
            Unity_Branch_float4(_Property_dc68cefccbb94c3cb1baafd25b46ba62_Out_0_Boolean, _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4, _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4, _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4);
            float4 _Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4;
            Unity_Add_float4(_Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4, _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4, _Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.BaseColor = (_Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4.xyz);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        ColorMask R
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
        #pragma shader_feature_fragment _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _ALPHAMODULATE_ON
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
             float3 normalWS : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SHADOWCASTER
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
             float3 normalWS : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "GBuffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }
        
        // Render State
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend]
        ZTest [_ZTest]
        ZWrite [_ZWrite]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile _ LOD_FADE_CROSSFADE
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        #pragma shader_feature_fragment _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _ALPHAMODULATE_ON
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_GBUFFER
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if !defined(LIGHTMAP_ON)
             float3 sh : INTERP0;
            #endif
             float4 texCoord0 : INTERP1;
             float3 positionWS : INTERP2;
             float3 normalWS : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_ChannelMask_RedGreen_float4 (float4 In, out float4 Out)
        {
            Out = float4(In.r, In.g, 0, 0);
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Sine_float(float In, out float Out)
        {
            Out = sin(In);
        }
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_bf8c6aa7f31c456aa30496f5eceb5276_Out_0_Boolean = _use_aura;
            float4 _Property_3aa6a9f3d8e6402096a662cab75c6587_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_aura_color) : _aura_color;
            float _Property_96ae2675c9ce4619a2d109bc2ca88678_Out_0_Boolean = _use_aura_mask;
            float _Property_09a34853954e4858be642f41df2c42fc_Out_0_Boolean = _use_aura_mask_1;
            float4 _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4 = IN.uv0;
            float _Split_88540391b22f4dd7a649b653bfdd7731_R_1_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[0];
            float _Split_88540391b22f4dd7a649b653bfdd7731_G_2_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[1];
            float _Split_88540391b22f4dd7a649b653bfdd7731_B_3_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[2];
            float _Split_88540391b22f4dd7a649b653bfdd7731_A_4_Float = _UV_f050407a28d54c81925dd897a8d158cc_Out_0_Vector4[3];
            float _Property_45e554c2e6dd4fe7a1c6babe151db9f3_Out_0_Float = _aura_mask_power;
            float _Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float;
            Unity_Power_float(_Split_88540391b22f4dd7a649b653bfdd7731_G_2_Float, _Property_45e554c2e6dd4fe7a1c6babe151db9f3_Out_0_Float, _Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float);
            float _Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float;
            Unity_Saturate_float(_Power_f57ae36d05074e4994f02139e46188ab_Out_2_Float, _Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float);
            float _Property_c30c3926725e41bbbca9de8e64d820a2_Out_0_Float = _aura_mask_intencity;
            float _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_c6841d00ded14509b843ba510dd8d1c4_Out_1_Float, _Property_c30c3926725e41bbbca9de8e64d820a2_Out_0_Float, _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float);
            float _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float;
            Unity_OneMinus_float(_Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float, _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float);
            float _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float;
            Unity_Branch_float(_Property_09a34853954e4858be642f41df2c42fc_Out_0_Boolean, _OneMinus_69ec49bf62d14229989b0bf7c6ee885f_Out_1_Float, _Multiply_5d6d4533c78143a78840d33f11b29010_Out_2_Float, _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float);
            float _Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float;
            Unity_Branch_float(_Property_96ae2675c9ce4619a2d109bc2ca88678_Out_0_Boolean, _Branch_d200972c4eac48029d5efde10b4ce3b5_Out_3_Float, 1, _Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float);
            float _Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float;
            Unity_Saturate_float(_Branch_2b33bd1b784241e69e9ff1a4fea53aca_Out_3_Float, _Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float);
            float _Property_c5f85df2388e47439a88a093738eb2f1_Out_0_Float = _aura_ins;
            UnityTexture2D _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_aura_tex);
            UnityTexture2D _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_aura_normal_tex);
            float2 _Property_8e468c16cf4d49b8aac90a2e06b52edf_Out_0_Vector2 = _aura_normal_tiling;
            float _Property_ce53b97baa3a44269dcdda9638894569_Out_0_Float = _aura_normal_upanner;
            float _Property_e55276b5b9a3402cba1e21e1e1ae1b15_Out_0_Float = _aura_normal_vpanner;
            float4 _Combine_58fa82948b884a049486ab32ff1fc887_RGBA_4_Vector4;
            float3 _Combine_58fa82948b884a049486ab32ff1fc887_RGB_5_Vector3;
            float2 _Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2;
            Unity_Combine_float(_Property_ce53b97baa3a44269dcdda9638894569_Out_0_Float, _Property_e55276b5b9a3402cba1e21e1e1ae1b15_Out_0_Float, 0, 0, _Combine_58fa82948b884a049486ab32ff1fc887_RGBA_4_Vector4, _Combine_58fa82948b884a049486ab32ff1fc887_RGB_5_Vector3, _Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2);
            float2 _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_58fa82948b884a049486ab32ff1fc887_RG_6_Vector2, (IN.TimeParameters.x.xx), _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2);
            float2 _TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_8e468c16cf4d49b8aac90a2e06b52edf_Out_0_Vector2, _Multiply_3e093bb1ef2e420f8b6c0cd3411f7d38_Out_2_Vector2, _TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2);
            float4 _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.tex, _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.samplerstate, _Property_67d342c5692b481a87e3f675fa5b14f7_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_b84eb2ebc722438f9502d825b9ab6ab2_Out_3_Vector2) );
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_R_4_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.r;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_G_5_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.g;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_B_6_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.b;
            float _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_A_7_Float = _SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4.a;
            float4 _ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4;
            Unity_ChannelMask_RedGreen_float4 (_SampleTexture2D_08e4c44a8c484cc3b2d1aa0fa27ef862_RGBA_0_Vector4, _ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4);
            float _Property_d8ff5f8e77af43eaa4166a99fa39bb89_Out_0_Float = _aura_noraml_str;
            float4 _Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_ChannelMask_3b0ab047e6244896a305876756613806_Out_1_Vector4, (_Property_d8ff5f8e77af43eaa4166a99fa39bb89_Out_0_Float.xxxx), _Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4);
            float2 _Property_17fa63dc5693405d9237b0f32c4ebdb4_Out_0_Vector2 = _aura_tex_tiling;
            float _Property_7892ee16bbb6427b8f7464e42b5278ac_Out_0_Float = _aura_upanner;
            float _Property_d3564925866f423790d8ee4253cd1407_Out_0_Float = _aura_vpanner;
            float4 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGBA_4_Vector4;
            float3 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGB_5_Vector3;
            float2 _Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2;
            Unity_Combine_float(_Property_7892ee16bbb6427b8f7464e42b5278ac_Out_0_Float, _Property_d3564925866f423790d8ee4253cd1407_Out_0_Float, 0, 0, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGBA_4_Vector4, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RGB_5_Vector3, _Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2);
            float2 _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_8c2eadaee9ab4b208ce4470e875218d4_RG_6_Vector2, (IN.TimeParameters.x.xx), _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2);
            float2 _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_17fa63dc5693405d9237b0f32c4ebdb4_Out_0_Vector2, _Multiply_528623b87c904399a7c719756e53e5e3_Out_2_Vector2, _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2);
            float2 _Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2;
            Unity_Add_float2((_Multiply_c30f3049dd424bb780aeef23b89f51c6_Out_2_Vector4.xy), _TilingAndOffset_4eea74aea296445da518badd74fbe144_Out_3_Vector2, _Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2);
            float4 _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.tex, _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.samplerstate, _Property_59804be84a2f4d7cbf86172f4342c5e8_Out_0_Texture2D.GetTransformedUV(_Add_33883d0e39c2428dac15056d6b90ddb9_Out_2_Vector2) );
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_R_4_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.r;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_G_5_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.g;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_B_6_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.b;
            float _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_A_7_Float = _SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_RGBA_0_Vector4.a;
            float _Property_17fd6a48ac284b298d68d3f6ca75b178_Out_0_Float = _aura_power;
            float _Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float;
            Unity_Power_float(_SampleTexture2D_2100adbe79d948ff8c4fc099e5fa62de_R_4_Float, _Property_17fd6a48ac284b298d68d3f6ca75b178_Out_0_Float, _Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float);
            float _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float;
            Unity_Saturate_float(_Power_30ebf9b9ce164d73bebd330320b5aa7e_Out_2_Float, _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float);
            float _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float;
            Unity_Multiply_float_float(_Property_c5f85df2388e47439a88a093738eb2f1_Out_0_Float, _Saturate_cedbaa5722724053866f7cc5495dcdaa_Out_1_Float, _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float);
            float _Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_b6e09722f9464273bd70232a54e38c1c_Out_1_Float, _Multiply_d3ad87a66d714846b9dd3142fa09ee1e_Out_2_Float, _Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float);
            float4 _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_3aa6a9f3d8e6402096a662cab75c6587_Out_0_Vector4, (_Multiply_fad2ec646db74f37a99cf0ad2a59a30c_Out_2_Float.xxxx), _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4);
            float4 _Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4;
            Unity_Branch_float4(_Property_bf8c6aa7f31c456aa30496f5eceb5276_Out_0_Boolean, _Multiply_cbe79904734845ac835b53c1d1f8f3f6_Out_2_Vector4, float4(0, 0, 0, 0), _Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4);
            float _Property_dc68cefccbb94c3cb1baafd25b46ba62_Out_0_Boolean = _use_subtex;
            float4 _Property_99e3ffd58f9e49068ef85806c99d01dc_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_sub_color) : _sub_color;
            float4 _Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_99e3ffd58f9e49068ef85806c99d01dc_Out_0_Vector4, float4(1, 1, 1, 1), _Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4);
            UnityTexture2D _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_tex);
            float4 _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.tex, _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.samplerstate, _Property_f332927706dc4193b10b8cea9d1f4c6b_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_R_4_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.r;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_G_5_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.g;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_B_6_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.b;
            float _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_A_7_Float = _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4.a;
            float4 _Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Multiply_8d976f62a71d4c338181c14bd2d22a87_Out_2_Vector4, _SampleTexture2D_aa46ecae1c9943beab61bf559c3f4e4c_RGBA_0_Vector4, _Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4);
            float4 _Property_c4b91c47dfc645e6ada02e7dc89126ec_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_sub_sec_color) : _sub_sec_color;
            UnityTexture2D _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_sec_tex);
            float4 _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.tex, _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.samplerstate, _Property_216187a1c86e492086d180d8bd5359e5_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_R_4_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.r;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_G_5_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.g;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_B_6_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.b;
            float _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_A_7_Float = _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4.a;
            float4 _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_c4b91c47dfc645e6ada02e7dc89126ec_Out_0_Vector4, _SampleTexture2D_516e1c72291b43a1b332b736e6892eb8_RGBA_0_Vector4, _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4);
            float4 _Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4;
            Unity_Add_float4(_Multiply_cee43a94d4904f038fae9a9015ba58e9_Out_2_Vector4, _Multiply_89b9ea1fc1134946913686ee2ed785f6_Out_2_Vector4, _Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4);
            UnityTexture2D _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_sub_alpha_tex);
            float4 _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.tex, _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.samplerstate, _Property_737cce461dd8463a9080f36e0e737df2_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_R_4_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.r;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_G_5_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.g;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_B_6_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.b;
            float _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_A_7_Float = _SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_RGBA_0_Vector4.a;
            float _OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_c8f605645b1f48ff9926a67aef12b568_R_4_Float, _OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float);
            float _Property_00d215c2468b4f7389789ef952e5c3d0_Out_0_Float = _line_opacity;
            float _Property_3bfc1c8f32124097bbd579a45e09d7a1_Out_0_Float = _rot;
            float2 _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Property_3bfc1c8f32124097bbd579a45e09d7a1_Out_0_Float, _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2);
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_R_1_Float = _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2[0];
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_G_2_Float = _Rotate_4ee3e27cbd83453e918dbd877b173e18_Out_3_Vector2[1];
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_B_3_Float = 0;
            float _Split_37b28c9739344c66b5d21efc3bec2ce4_A_4_Float = 0;
            float _Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float;
            Unity_Add_float(_Split_37b28c9739344c66b5d21efc3bec2ce4_R_1_Float, _Split_37b28c9739344c66b5d21efc3bec2ce4_G_2_Float, _Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float);
            float _Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float;
            Unity_Multiply_float_float(_Add_b194028f5ea140faa923bb85ecf2d7c8_Out_2_Float, 2, _Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float);
            float _Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_96e41eaefd2642e39e7858efb674a9eb_Out_2_Float, 0.55, _Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float);
            float _Property_9c30ee88cbee494392e07938bbd1aec7_Out_0_Float = _line_amount;
            float _Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float;
            Unity_Add_float(_Property_9c30ee88cbee494392e07938bbd1aec7_Out_0_Float, -0.5, _Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float);
            float _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float;
            Unity_Multiply_float_float(_Add_e330336c801f4b58a5ef975dfe9fd644_Out_2_Float, 5.74, _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float);
            float _Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float;
            Unity_Add_float(_Multiply_c9fcd8f080774a91844816aad9159a60_Out_2_Float, _Multiply_7c5c80bea6bd42aa9956029807ab16bd_Out_2_Float, _Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float);
            float _Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float;
            Unity_Sine_float(_Add_e3882ebba6c24070b6c1eacad5a78c41_Out_2_Float, _Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float);
            float _Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float;
            Unity_Saturate_float(_Sine_d541a1aabd774755b723c1293f89851c_Out_1_Float, _Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float);
            float _Float_d88d5ebf2e15431b8578b52cba0dfc42_Out_0_Float = 8.2;
            float _Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float;
            Unity_Power_float(_Saturate_7e64ad347c9f426d9fe4cb077faf0a29_Out_1_Float, _Float_d88d5ebf2e15431b8578b52cba0dfc42_Out_0_Float, _Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float);
            float4 _Property_75a0d0446e854f9c9e4f3344170112ef_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_line_color) : _line_color;
            float4 _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Power_8cee047ec48546c79ff8f3f29480347d_Out_2_Float.xxxx), _Property_75a0d0446e854f9c9e4f3344170112ef_Out_0_Vector4, _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4);
            float4 _Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Property_00d215c2468b4f7389789ef952e5c3d0_Out_0_Float.xxxx), _Multiply_53c737ef99c64c4db0b44271e3c62efd_Out_2_Vector4, _Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4);
            float4 _Property_884e592925cd4542b162f0856759af55_Out_0_Vector4 = _main_color;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float4 _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4;
            Unity_Multiply_float4_float4(_Property_884e592925cd4542b162f0856759af55_Out_0_Vector4, _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4, _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4);
            float4 _Add_888d24facb73490487eae44562501a54_Out_2_Vector4;
            Unity_Add_float4(float4(0, 0, 0, 0), _Multiply_7dddad1e136f432b8de4ae8e87dc7ae3_Out_2_Vector4, _Add_888d24facb73490487eae44562501a54_Out_2_Vector4);
            float4 _Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4;
            Unity_Add_float4(_Multiply_0bb462190e234a57a266d13071881676_Out_2_Vector4, _Add_888d24facb73490487eae44562501a54_Out_2_Vector4, _Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4);
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Property_68fa83bd3ff84da9acc92b2a8b125437_Out_0_Float = _edge_width;
            float _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float;
            Unity_Multiply_float_float(_Property_68fa83bd3ff84da9acc92b2a8b125437_Out_0_Float, 210, _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float);
            float _Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float;
            Unity_Subtract_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Multiply_ee3ad75d789942a8996eacaaa4ac8bcc_Out_2_Float, _Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float);
            float _Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float;
            Unity_Step_float(_Subtract_ff761b6b8f404ca48966342ad308b664_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float);
            float _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float;
            Unity_OneMinus_float(_Step_07f9e2b96b8f4ff9a34c842d450cd3ab_Out_2_Float, _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float);
            float _Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float;
            Unity_Subtract_float(_OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _OneMinus_2cf80e282a5c4eb8972af03a8720deba_Out_1_Float, _Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float);
            float4 _Property_34ff7996b61446ca9af2553e2a3e0c98_Out_0_Vector4 = IsGammaSpace() ? LinearToSRGB(_dissolve_color) : _dissolve_color;
            float4 _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4;
            Unity_Multiply_float4_float4((_Subtract_24a4c19b16744c64a51c3d1cffcbccc9_Out_2_Float.xxxx), _Property_34ff7996b61446ca9af2553e2a3e0c98_Out_0_Vector4, _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4);
            float4 _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4;
            Unity_Add_float4(_Add_1399f640409346e4918426d5b5cb5ed1_Out_2_Vector4, _Multiply_7ac6e14bfde14ec78eced6ab5a51b8e0_Out_2_Vector4, _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4);
            float4 _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4;
            Unity_Multiply_float4_float4((_OneMinus_589d251d69ca4be3bf6126adeef8dfab_Out_1_Float.xxxx), _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4, _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4);
            float4 _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4;
            Unity_Add_float4(_Add_2153ff1686df45b5800d164d6621e5f1_Out_2_Vector4, _Multiply_4395e462a1904da893d8495158041aa2_Out_2_Vector4, _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4);
            float4 _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4;
            Unity_Branch_float4(_Property_dc68cefccbb94c3cb1baafd25b46ba62_Out_0_Boolean, _Add_0afc357dac2547d1b7fbd58afee0cfc6_Out_2_Vector4, _Add_9a6fc029372645b7b41a8639343b2b99_Out_2_Vector4, _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4);
            float4 _Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4;
            Unity_Add_float4(_Branch_8f4513bb65bf4518a13ecf29f86f951f_Out_3_Vector4, _Branch_a0e09211c321412493d509ca7c374b9b_Out_3_Vector4, _Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.BaseColor = (_Add_8aa7b4fbb7f642ceb8fa9d2e67426f1f_Out_2_Vector4.xyz);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitGBufferPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
        // Render State
        Cull [_Cull]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float _use_aura_mask_1;
        float4 _main_color;
        float _dissolve_amount;
        float4 _dissolve_tex_TexelSize;
        float _is_up_direction;
        float _edge_width;
        float4 _dissolve_color;
        float _noise_size;
        float _aura_normal_vpanner;
        float _noise_str;
        float _multiplier;
        float4 _outline_color;
        float _outline_thick;
        float _rot;
        float _aura_mask_intencity;
        float4 _line_color;
        float _line_opacity;
        float _line_amount;
        float4 _sub_color;
        float4 _sub_tex_TexelSize;
        float _aura_ins;
        float4 _aura_tex_TexelSize;
        float4 _aura_color;
        float _aura_upanner;
        float _aura_vpanner;
        float2 _aura_tex_tiling;
        float _aura_power;
        float4 _aura_normal_tex_TexelSize;
        float _aura_normal_upanner;
        float2 _aura_normal_tiling;
        float _aura_noraml_str;
        float4 _sub_alpha_tex_TexelSize;
        float _use_subtex;
        float _use_aura;
        float _use_aura_mask;
        float _aura_mask_power;
        float4 _sub_sec_color;
        float4 _sub_sec_tex_TexelSize;
        float4 _main_alpha_tex_TexelSize;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_dissolve_tex);
        SAMPLER(sampler_dissolve_tex);
        TEXTURE2D(_sub_tex);
        SAMPLER(sampler_sub_tex);
        TEXTURE2D(_aura_tex);
        SAMPLER(sampler_aura_tex);
        TEXTURE2D(_aura_normal_tex);
        SAMPLER(sampler_aura_normal_tex);
        TEXTURE2D(_sub_alpha_tex);
        SAMPLER(sampler_sub_alpha_tex);
        TEXTURE2D(_sub_sec_tex);
        SAMPLER(sampler_sub_sec_tex);
        TEXTURE2D(_main_alpha_tex);
        SAMPLER(sampler_main_alpha_tex);
        
        // Graph Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        float Unity_SimpleNoise_ValueNoise_Deterministic_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0; Hash_Tchou_2_1_float(c0, r0);
            float r1; Hash_Tchou_2_1_float(c1, r1);
            float r2; Hash_Tchou_2_1_float(c2, r2);
            float r3; Hash_Tchou_2_1_float(c3, r3);
            float bottomOfGrid = lerp(r0, r1, f.x);
            float topOfGrid = lerp(r2, r3, f.x);
            float t = lerp(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        
        void Unity_SimpleNoise_Deterministic_float(float2 UV, float Scale, out float Out)
        {
            float freq, amp;
            Out = 0.0f;
            freq = pow(2.0, float(0));
            amp = pow(0.5, float(3-0));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            Out += Unity_SimpleNoise_ValueNoise_Deterministic_float(float2(UV.xy*(Scale/freq)))*amp;
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Step_float(float Edge, float In, out float Out)
        {
            Out = step(Edge, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.tex, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.samplerstate, _Property_9bdeed3abe7f4d32b9243bf6e30b05e6_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_R_4_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.r;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_G_5_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.g;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_B_6_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.b;
            float _SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float = _SampleTexture2D_1504930aab0f4cb98da65443285758b2_RGBA_0_Vector4.a;
            float _Property_54eb17712c94487e82c094957ec32416_Out_0_Float = _noise_size;
            float _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float;
            Unity_SimpleNoise_Deterministic_float(IN.uv0.xy, _Property_54eb17712c94487e82c094957ec32416_Out_0_Float, _SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float);
            float _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float = _noise_str;
            float _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float;
            Unity_Negate_float(_Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float, _Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float);
            float2 _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2 = float2(_Negate_9d1f851fd5a3481d8c9a1990af37fe03_Out_1_Float, _Property_a16c22c8705846299a4a4aad8c9c39e9_Out_0_Float);
            float _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float;
            Unity_Remap_float(_SimpleNoise_558a36fc162746748a65a352227ce1e5_Out_2_Float, float2 (0, 1), _Vector2_b16b7e66c668473994ed5f4c7667bf61_Out_0_Vector2, _Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float);
            float _Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float = _dissolve_amount;
            float _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float = _multiplier;
            float _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float;
            Unity_Multiply_float_float(_Property_c098083fc933474eae61b3310d3c0aed_Out_0_Float, _Property_e83e12fbae67459e90a56be858377ed4_Out_0_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float);
            float _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float;
            Unity_Add_float(_Remap_3b3064479bda459c8f9e8ba3b60b7af9_Out_3_Float, _Multiply_b5fd05a655834fc686b7b3d9468cdb55_Out_2_Float, _Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float);
            float _Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean = _is_up_direction;
            float _Split_68ed751b8867488781d79bc9da72b657_R_1_Float = IN.WorldSpacePosition[0];
            float _Split_68ed751b8867488781d79bc9da72b657_G_2_Float = IN.WorldSpacePosition[1];
            float _Split_68ed751b8867488781d79bc9da72b657_B_3_Float = IN.WorldSpacePosition[2];
            float _Split_68ed751b8867488781d79bc9da72b657_A_4_Float = 0;
            float _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float;
            Unity_OneMinus_float(_Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float);
            float _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float;
            Unity_Branch_float(_Property_869ebac8a1954f7785735ef4cdf213f8_Out_0_Boolean, _Split_68ed751b8867488781d79bc9da72b657_G_2_Float, _OneMinus_81be35a99f1746b09648b3709cf0008d_Out_1_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float);
            float _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float;
            Unity_Step_float(_Add_d479e0cb9d98463197d001fd2930c928_Out_2_Float, _Branch_e3cfa7e80a4543c88a9b0438ff51fae9_Out_3_Float, _Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float);
            float _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float;
            Unity_OneMinus_float(_Step_970b45ee5ce54ae89f66004051a2ebe4_Out_2_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float);
            float _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_1504930aab0f4cb98da65443285758b2_A_7_Float, _OneMinus_f8d4d2eb4f2841508cb603ace0a7a677_Out_1_Float, _Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float);
            UnityTexture2D _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_main_alpha_tex);
            float4 _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.tex, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.samplerstate, _Property_db3cf1ee8fac4b20ae844baa04f23185_Out_0_Texture2D.GetTransformedUV(IN.uv0.xy) );
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.r;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_G_5_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.g;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_B_6_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.b;
            float _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_A_7_Float = _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_RGBA_0_Vector4.a;
            float _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_ed5bfe14560048aeb72385898eeb27bd_Out_2_Float, _SampleTexture2D_0fb29eb0365d47d1a7cf7aba9789d0cf_R_4_Float, _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float);
            surface.Alpha = _Multiply_e2c01d6209974f5e9a3af92ced765d1d_Out_2_Float;
            surface.AlphaClipThreshold = 0.5;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    CustomEditorForRenderPipeline "UnityEditor.ShaderGraphUnlitGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    FallBack "Hidden/Shader Graph/FallbackError"
}