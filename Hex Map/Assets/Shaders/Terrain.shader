Shader "Custom/Terrain"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Terrain Texture Array", 2DArray) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Specular ("Specular", Color) = (0.2, 0.2, 0.2)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf StandardSpecular fullforwardshadows vertex:vert
        #pragma target 3.5

        #include "HexMetrics.cginc"

		UNITY_DECLARE_TEX2DARRAY(_MainTex);

        half _Glossiness;
		fixed3 _Specular;
		fixed4 _Color;

        struct Input
        {
			float4 color : COLOR;
			float3 worldPos;
			float3 terrain;
        };

		void vert(inout appdata_full v, out Input data) {
			UNITY_INITIALIZE_OUTPUT(Input, data);
			data.terrain = v.texcoord2.xyz;
		}

		float4 GetTerrainColor(Input IN, int index) {
			float3 uvw = float3(IN.worldPos.xz * (2 * TILING_SCALE), IN.terrain[index]);
			float4 c = UNITY_SAMPLE_TEX2DARRAY(_MainTex, uvw);
			return c * IN.color[index];
		}

        void surf (Input IN, inout SurfaceOutputStandardSpecular o)
        {
			fixed4 c =
				GetTerrainColor(IN, 0) +
				GetTerrainColor(IN, 1) +
				GetTerrainColor(IN, 2);
            o.Albedo = c.rgb * _Color;
			o.Specular = _Specular;
			o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
