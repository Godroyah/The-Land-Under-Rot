Shader "Custom/GlowShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		

		[Header(Glow Settings)]
		[Toggle]_DistanceGlow("Glow By Distance to Player", float) = 1
		[Toggle]_GlowEnabled("Glow", float) = 1
		_GlowColor("Glow Color", color) = (1,1,1,1)
		_GlowRadius("Glow Radius", float) = 5
		_GlowPower("Glow Power", float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _GlowColor;
		float _GlowEnabled;
		float _DistanceGlow;
		float4 _PlayerPosition;
		float _GlowRadius;
		float _GlowPower;
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

			float4 worldPivot = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
			float4 glow = _GlowColor;
			float d = distance(_PlayerPosition.xyz, worldPivot.xyz)/ _GlowRadius;
			d = 1 - saturate(d);
			if (_DistanceGlow < .5)
				d = 1;
			o.Emission = glow * _GlowEnabled * _GlowPower * d;

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
