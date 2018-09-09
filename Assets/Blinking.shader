Shader "Unlit/Blinking"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1)
		_BlinkSpeed("Blink Speed", Float) = 1.0
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

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			float _BlinkSpeed;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{ 
				fixed4 col = fixed4(_Color.rgb * cos(_Time.x * _BlinkSpeed), 1);
				return col; 
			}
			ENDCG
		}
	}
}
