Shader "Hidden/GrabBeforePixelation"
{
	Properties
	{}

	SubShader{
		Tags{"Queue"="Geometry-1"}
		GrabPass{"_BeforePixelGrab"}

		Pass {
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            fixed4 frag () : SV_Target
            {
                return float4(0, 0, 0, 0);
            }
            ENDCG
		}
	}

	Fallback "Standard"
}
