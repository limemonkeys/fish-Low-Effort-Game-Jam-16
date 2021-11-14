Shader "Hidden/DitherShader"
{
    Properties
    {
    	_MainTex ("DitherPattern", 2D) = "white" {}
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

            uniform sampler2D _MainTex;
            float4 _DitherPattern_TexelSize;

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
              float2 screenPos = i.screenPos.xy / i.screenPos.w;
              float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize;
              float ditherValue = tex2D(_MainTex, ditherCoordinate).r;
              return col;
            }
            ENDCG
        }
    }
}
