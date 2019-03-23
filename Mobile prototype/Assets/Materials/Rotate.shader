Shader "Unlit/Rotate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RotationSpeed("Rotation Speed", Float) = 2.0
			_TintColor("Tint Color", Color) = (1,1,1,1)
    }
    SubShader
    {
		 Tags { "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
		 Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
		
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _RotationSpeed;
			float4 _TintColor;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv - 0.5f;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

				float s = sin(_RotationSpeed * _Time.y);
				float c = cos(_RotationSpeed * _Time.y);
				float2x2 rotationMatrix = float2x2(c, -s, s, c);
				i.uv.xy= mul(i.uv.xy, rotationMatrix);
				i.uv.xy += 0.5;
                // sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
