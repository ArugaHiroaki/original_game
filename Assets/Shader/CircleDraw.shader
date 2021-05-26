Shader "Custom/CircleDraw"
{
    /*Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    */
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard

        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float dist = distance(fixed3(0,3,0), IN.worldPos);

            float val = abs(sin(dist*3.0+_Time*50));
            if(val > 0.98)
            {
                o.Albedo = fixed4(0,0,0,0);
            }
            else
            {
                o.Albedo = fixed4(255.0/255.0, 255/255.0, 255/255.0, 1);
            }

            //円一つの描画
            /*float radius = 1;
            if(radius < dist && dist < radius + 0.1)
            {
                o.Albedo = fixed4(0,0,0,0);
            }
            else
            {
                o.Albedo = fixed4(255.0/255.0, 255/255.0, 255/255.0, 1);
            }
            */
        }
        ENDCG
    }
    FallBack "Diffuse"
}
