Shader "Custom/RotatingVanishingLines" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _OrigineX ("PosX Origine", Range(0,1)) = 0.5
        _OrigineY ("PosY Origine", Range(0,1)) = 0.5
        _Speed ("Speed", Range(-100,100)) = 60.0
        _CircleNbr ("Circle quantity", Range(10,1000)) = 60.0
    }

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma target 3.0

            float4 _Color;
            float _OrigineX;
            float _OrigineY;
            float _Speed;
            float _CircleNbr;

            struct vertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            struct fragmentInput{
                float4 position : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
            };

            fragmentInput vert(vertexInput i){
                fragmentInput o;
                o.position = UnityObjectToClipPos (i.vertex);
                o.texcoord0 = i.texcoord0;
                return o;
            }

            fixed4 frag(fragmentInput i) : SV_Target {
                fixed4 color;
                float distanceToCenter;
                float time = _Time.x * _Speed;

                float xdist = _OrigineX - i.texcoord0.x;
                float ydist = _OrigineY - i.texcoord0.y;

                distanceToCenter = (xdist * xdist + ydist * ydist) * _CircleNbr;

                color = _Color + step(0.5, clamp(sin(atan2(xdist, ydist) * _CircleNbr + time), 0.0, 1.0));
                return color;
            }
            ENDCG
        }
    }
}