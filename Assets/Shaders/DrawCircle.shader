﻿Shader "Custom/DrawCircle"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Center("Center", Vector) = (0, 0, 0, 0)
		_Radius("Radius", Float) = 10
		_BandColor("Band color", Color) = (1, 0, 0, 1)
		_BandWidth("Band width", Float) = 2
		_BandFade("Band fade", Float) = 0.2
		_SideLength("Side length", Float) = 50
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float3 _Center;
		float _Radius;
		fixed4 _BandColor;
		float _BandWidth;
		float _BandFade;
		float _SideLength;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			// Draw circle
			// Compute distance from object center to current fragment
			float dist = distance(_Center, IN.worldPos);
			// Check the distance against the circle's band parameters

			//if (dist > (_Radius - _BandWidth) && dist < (_Radius + _BandWidth)) {
				// We are inside the solid region of the circle
			//	o.Albedo = _BandColor;
			//}

			//top
			if ((IN.worldPos.x >= (_Center.x - 20 / 2) && IN.worldPos.x <= (_Center.x - 10 / 2)) ||
				(IN.worldPos.x >= (_Center.x + 10 / 2) && IN.worldPos.x <= (_Center.x + 20 / 2)))
			{
				if (IN.worldPos.z >= (_Center.z - 20 / 2) && IN.worldPos.z <= (_Center.z + 20 / 2))
				{
					o.Albedo = _BandColor;
				}
			}
			if ((IN.worldPos.z >= (_Center.z - 20 / 2) && IN.worldPos.z <= (_Center.z - 10 / 2)) ||
				(IN.worldPos.z >= (_Center.z + 10 / 2) && IN.worldPos.z <= (_Center.z + 20 / 2)))
			{
				if (IN.worldPos.x >= (_Center.x - 20 / 2) && IN.worldPos.x <= (_Center.x + 20 / 2))
				{
					o.Albedo = _BandColor;
				}
			}
        }
        ENDCG
    }
    FallBack "Diffuse"
}
