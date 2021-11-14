Shader "Hidden/DitherShader"
{
    Properties
    {
      _MainTex ("MainTex", 2D) = "white" {}     
    	_DitherPattern ("DitherPattern", 2D) = "white" {}
        _DitherPattern_TexelSize ("Size", Float) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _DitherPattern;
            uniform sampler2D _MainTex;
            uniform float4 _DitherPattern_TexelSize;

            struct v2f
            {
              float2 uv : TEXCOORD0;
              float4 vertex : SV_POSITION;
              float4 screenPos : TEXCOORD1;
            };

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            
            fixed4 frag (v2f i) : COLOR
            {
              fixed4 col = tex2D(_MainTex, i.uv);
                
                // just invert the colors
                //col.rgb = 1 - col.rgb;
              float2 ditherCoordinate = i.screenPos * _ScreenParams.xy * 0.05;
              float ditherValue = tex2D(_DitherPattern, ditherCoordinate);
              fixed4 outCol = LinearRgbToLuminance(col) < ditherValue ? 0.0 : 1.0;
              return outCol;
            }
            ENDCG
        }
    }
}
