Shader "Custom/WaterTurbulence" {
    Properties {
        _ShowTiling ("Show Tiling", Int) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _ SHOW_TILING
            
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float3 _iResolution;
            float _iTime;
            int _ShowTiling;
            
            #define TAU 6.28318530718
            #define MAX_ITER 5

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float time = _iTime * 0.5 + 23.0;
                float2 uv = i.uv;
                
            #if SHOW_TILING
                float2 p = fmod(uv * TAU * 2.0, TAU) - 250.0;
            #else
                float2 p = fmod(uv * TAU, TAU) - 250.0;
            #endif
                
                float2 iter = p;
                float c = 1.0;
                float inten = 0.005;

                [loop] for (int n = 0; n < MAX_ITER; n++) {
                    float t = time * (1.0 - (3.5 / (float(n) + 1.0)));
                    iter = p + float2(cos(t - iter.x) + sin(t + iter.y), 
                                  sin(t - iter.y) + cos(t + iter.x));
                    c += 1.0 / length(float2(
                        p.x / (sin(iter.x + t) / inten),
                        p.y / (cos(iter.y + t) / inten)
                    ));
                }
                
                c /= float(MAX_ITER);
                c = 1.17 - pow(c, 1.4);
                float3 colour = pow(abs(c), 8.0);
                colour = clamp(colour + float3(0.0, 0.35, 0.5), 0.0, 1.0);
                
            #if SHOW_TILING
                float2 pixel = 2.0 / _iResolution.xy;
                uv *= 2.0;
                float f = floor(fmod(_iTime * 0.5, 2.0));
                float2 first = step(pixel, uv) * f;
                uv = step(frac(uv), pixel);
                colour = lerp(colour, float3(1.0, 1.0, 0.0), 
                              (uv.x + uv.y) * first.x * first.y);
            #endif
                
                return fixed4(colour, 1.0);
            }
            ENDCG
        }
    }
    // CustomEditor "WaterTurbulenceEditor"  // Optional
}