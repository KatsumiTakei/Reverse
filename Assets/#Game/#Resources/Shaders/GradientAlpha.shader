﻿Shader "Sprites/Gradient Alpha"
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

			// マテリアルにグラデーション用プロパティを追加
			[Space]
			[Header(Gradient)]
			_GradientAlpha1("Alpha 1", Range(0, 1)) = 0
			_GradientAlpha2("Alpha 2", Range(0, 1)) = 1

			_GradientRed1("Red 1", Range(0, 1)) = 0
			_GradientRed2("Red 2", Range(0, 1)) = 1

			_GradientGreen1("Green 1", Range(0, 1)) = 0
			_GradientGreen2("Green 2", Range(0, 1)) = 1

			_GradientBlue1("Blue 1", Range(0, 1)) = 0
			_GradientBlue2("Blue 2", Range(0, 1)) = 1


			_GradientScale("Scale", Range(0, 2)) = 1
			_GradientAngle("Angle", Range(0, 360)) = 0
			_GradientOffset("Offset", Range(-1, 1)) = 0

			_GradientRedValue("GradientRedValue", Range(0, 1)) = 0
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
				// バーテックスシェーダー、フラグメントシェーダーを独自仕様に差し替え
				#pragma vertex vert
				#pragma fragment frag

				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnitySprites.cginc"

				// 追加したプロパティのための変数を追加
				float _GradientAlpha1;
				float _GradientAlpha2;
				float _GradientRed1;
				float _GradientRed2;
				float _GradientGreen1;
				float _GradientGreen2;
				float _GradientBlue1;
				float _GradientBlue2;
				float _GradientScale;
				float _GradientAngle;
				float _GradientOffset;

				float _GradientRedValue;


				// v2fもカスタマイズする
				struct v2fCustom
				{
					float4 vertex    : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float  alpha : TEXCOORD1; // グラデーション描画のためのアルファ情報を追加
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
					OUT.color = IN.color /** _Color * _RendererColor*/;

					// グラデーションの角度に合わせてスプライト頂点を回転、オフセット分だけ移動、
					// スケールで割った際のX座標を求め、さらに範囲を-1～1 → 0～1に変える
					float theta = _GradientAngle * UNITY_PI / 180;
					OUT.alpha = ((dot(float2(cos(theta), -sin(theta)), IN.vertex.xy)/* + _GradientOffset * 2*/) /*/ _GradientScale*/) + 0.5;

					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				fixed4 frag(v2fCustom IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

				//// プロパティから設定したアルファ1とアルファ2をバーテックスシェーダーで求めた比率で
				//// 混合し、0～1におさめてアルファに乗算する
				c.r *= saturate(lerp(_GradientRed1, _GradientRed2, IN.alpha));
				c.g *= saturate(lerp(_GradientGreen1, _GradientGreen2, IN.alpha));
				c.b *= saturate(lerp(_GradientBlue1, _GradientBlue2, IN.alpha));
				//c.a *= saturate(lerp(_GradientAlpha1, _GradientAlpha2, IN.alpha));

				//c.rgb = saturate(IN.alpha);
				c.rgb *= c.a;

				return c;
			}
		ENDCG
		}
			}
}