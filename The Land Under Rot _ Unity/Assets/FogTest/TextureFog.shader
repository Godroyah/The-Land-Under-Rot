Shader "Hidden/Custom/TextureFog"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _DistanceBlend;
        float4 _FogColor;
        float _NearPush;
        float _FogPower;
		float _DebugDepth;
      
        sampler2D _CameraDepthTexture;

        

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            half depth = tex2D(_CameraDepthTexture, i.texcoord).r;
            float4 screenCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            
            
            float linearDepth = Linear01Depth(depth);
            float4 depthCol = _FogColor;
            float depthAlpha = saturate((linearDepth - _NearPush*.01) * _DistanceBlend);
			if (_DebugDepth > .1)
				return depthAlpha;
			
			if (depthAlpha >= _DistanceBlend)
				depthAlpha = 0;
				
			depthCol = lerp(screenCol, depthCol ,  depthAlpha );
            
            return lerp(screenCol,  depthCol , saturate(_FogPower));
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}