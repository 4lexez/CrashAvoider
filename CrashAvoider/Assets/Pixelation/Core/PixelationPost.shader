Shader "Hidden/PixelationPost"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_CellSize("Cell Size", Float) = 0.0054
		_ColorBits("Color Bits", Int) = 8
	}
	SubShader
	{
		Pass
		{
			Name "PIXELATION"
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			struct appdata_img {
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};

			float2 MultiplyUV(float4x4 mat, float2 inUV) {
				float4 temp = float4 (inUV.x, inUV.y, 0, 0);
				temp = mul(mat, temp);
				return temp.xy;
			}

			v2f vert_img(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
				return o;
			}

			sampler2D _MainTex;
			sampler2D _BeforePixelGrab;
			float _CellSize;
			int _ColorBits;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 steppedUV = i.uv.xy;
				float2 cells = _CellSize;
				cells.y = _CellSize / _ScreenParams.y * _ScreenParams.x;
				steppedUV /= cells.xy;
				steppedUV = round(steppedUV);
				steppedUV *= cells.xy;

				half4 beforeGrabStepped = tex2D(_BeforePixelGrab, steppedUV);
				half4 afterGrabStepped = tex2D(_MainTex, steppedUV);
				if (beforeGrabStepped.r == afterGrabStepped.r && beforeGrabStepped.g == afterGrabStepped.g && beforeGrabStepped.b == afterGrabStepped.b) {
					return tex2D(_BeforePixelGrab, i.uv);
				}

				if (_ColorBits != 8)
				{
					afterGrabStepped *= pow(2, _ColorBits);
					afterGrabStepped = round(afterGrabStepped);
					afterGrabStepped /= pow(2, _ColorBits);
				}
				return afterGrabStepped;
			}
			ENDCG
		}
	}
	
	Fallback Off
}
