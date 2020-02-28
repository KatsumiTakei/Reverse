// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/EdgeGradient"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0

		[Space]
		[Header(Gradient)]
		_GradientAlpha1("Alpha 1", Range(0, 1)) = 0
		_GradientAlpha2("Alpha 2", Range(0, 1)) = 1
		[PowerSlider(2.0)] _GradientExponent("Exponent", Range(0.125, 8)) = 2
		[Header(Edges)]
		[Toggle] _GradientEdgeTop("Top", Float) = 0
		[Toggle] _GradientEdgeBottom("Bottom", Float) = 0
		[Toggle] _GradientEdgeLeft("Left", Float) = 0
		[Toggle] _GradientEdgeRight("Right", Float) = 0
		[Header(Corners)]
		[Toggle] _GradientCornerTopLeft("Top Left", Float) = 0
		[Toggle] _GradientCornerBottomLeft("Bottom Left", Float) = 0
		[Toggle] _GradientCornerTopRight("Top Right", Float) = 0
		[Toggle] _GradientCornerBottomRight("Bottom Right", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnitySprites.cginc"

				float _GradientAlpha1;
				float _GradientAlpha2;
				float _GradientExponent;
				float _GradientEdgeTop;
				float _GradientEdgeBottom;
				float _GradientEdgeLeft;
				float _GradientEdgeRight;
				float _GradientCornerTopLeft;
				float _GradientCornerBottomLeft;
				float _GradientCornerTopRight;
				float _GradientCornerBottomRight;

				struct v2fCustom
				{
					float4 vertex    : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float2 position : TEXCOORD1; // グラデーション用の位置
					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2fCustom vert(appdata_t IN)
				{
					v2fCustom OUT;

					UNITY_SETUP_INSTANCE_ID(IN);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

					OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
					OUT.vertex = UnityObjectToClipPos(OUT.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color * _RendererColor;
					OUT.position = IN.vertex.xy * 2;

					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				fixed4 frag(v2fCustom IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

					float4 edgeFactor = float4(_GradientEdgeTop, _GradientEdgeBottom, _GradientEdgeLeft, _GradientEdgeRight);
					float4 cornerFactor = float4(_GradientCornerTopLeft, _GradientCornerTopRight, _GradientCornerBottomLeft, _GradientCornerBottomRight);
					float4 edge = pow(saturate(IN.position.yyxx * float4(1, -1, -1, 1)), _GradientExponent);
					float4 corner = edge.xxyy * edge.zwzw * cornerFactor;
					float alpha = saturate(max(max(max(max(dot(edge, edgeFactor), corner.x), corner.y), corner.z), corner.w));
					c.r *= saturate(lerp(_GradientAlpha1, _GradientAlpha2, alpha));

					c.rgb *= c.a;

					return c;
				}
			ENDCG
			}
		}
}