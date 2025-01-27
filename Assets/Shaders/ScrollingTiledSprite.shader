Shader "Custom/ScrollingTiledSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Vector) = (0, 0, 0, 0)
        _Color ("Color", Color) = (1, 1, 1, 1)
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

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _ScrollSpeed;
            float4 _Color; // The color passed from the SpriteRenderer

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply scrolling effect to UV coordinates
                o.uv = v.texcoord + _ScrollSpeed * _Time.y;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the texture with the scrolled UVs
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Multiply the texture color by the sprite's color
                return texColor * _Color;
            }
            ENDCG
        }
    }
}