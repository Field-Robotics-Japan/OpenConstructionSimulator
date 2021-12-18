Shader "Unlit/CrawlerTextureShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ScrollX("ScrollX", float) = 0
		_ScrollY("ScrollY", float) = 0
		[KeywordEnum(Vertex, Fragment)] _Target("Calc Target", Float) = 0
	}

		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _TARGET_VERTEX _TARGET_FRAGMENT

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed _ScrollX, _ScrollY;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					#ifdef _TARGET_VERTEX
					o.uv = o.uv + fixed2(frac(_ScrollX * _Time.y), frac(_ScrollY * _Time.y));
					#endif

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					#ifdef _TARGET_FRAGMENT
					i.uv = fixed2(frac(i.uv.x + _ScrollX * _Time.y), frac(i.uv.y + _ScrollY * _Time.y));
					#endif

					return tex2D(_MainTex, i.uv);
				}

				ENDCG
			}
		}
}
