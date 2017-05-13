// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OverlapAble"
{
	Properties
	{
		_StandardColor("StandardColor", Color) = (0, 0, 1, 1)
	}
		
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			Stencil{
				Ref 0
				Comp Equal
				Pass IncrSat
				Fail IncrSat
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 _StandardColor;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _StandardColor;
				return col;
			}
			ENDCG
		}
	}
}