// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Default"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"


					struct VertexInput {
				float4 pos:  POSITION;    // 3D��ԍ��W
				float2 uv:   TEXCOORD0;   // �e�N�X�`�����W
			};

			struct VertexOutput {
				float4 v:    SV_POSITION; // 2D���W
				float2 uv:   TEXCOORD0;   // �e�N�X�`�����W
			};

			// ���_ shader
			VertexOutput vert(VertexInput input)
			{
				VertexOutput output;
				output.v = UnityObjectToClipPos(input.pos);
				output.uv = input.uv;

				return output;
			}

			// �s�N�Z�� shader
			fixed4 frag(VertexOutput output) : SV_Target
			{
				float2 tex = output.uv;
				// ���F�����F�̃O���f�[�V����
				return fixed4(1.0, 1.0, 0.3 - tex.y, 1.0);
			}

        ENDCG
        }
    }
}
