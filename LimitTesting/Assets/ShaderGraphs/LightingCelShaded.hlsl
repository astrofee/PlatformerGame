void LightingCelShaded_float(in float3 Normal, in float Smoothness, in float3 ClipSpacePos, in float3 worldPos, in float4 Tinting, in float Offset, out float3 Color, out float3 Direction)
{
    #ifdef SHADERGRAPH_PREVIEW
    Color = float3(0.5f, 0.5f, 0);
    Direction = float3(0, 0, 0);
    #else
        #if SHADOWS_SCREEN
            half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(worldPos);
        #endif

        #if _MAIN_LIGHT_SHADOWS_CASECADE || _MAIN_LIGHT_SHADOWS
            Light light = GetMainLight(shadowCoord);
        #else
            Light light = GetMainLight();
        #endif

        half d = dot(Normal, light.direction) * 0.5 + 0.5;

        half toonRamp = smoothstep(Offset, Offset + Smoothness, d);
        
        toonRamp *= light.shadowAttenuation;

        Color = light.color * (toonRamp * Tinting);

        Direction = light.direction;
        #endif
}