void Crash_float(UnityTexture2D MainTex, UnityTexture2D CrashTex, float2 UV, float4 color, float value, out float4 RGBA)
{
//    value = 0.8;
    // 원본 텍스처 색상
    float4 basecolor = MainTex.Sample(MainTex.samplerstate, UV);

    // CrashTex 알파
    float crashMask = CrashTex.Sample(CrashTex.samplerstate, UV).a;

    // 중심 거리 (0=중심, 0.7~1.0=가장자리)
    float2 center = float2(0.5, 0.5);
    float dist = distance(UV, center) * 2;

    // 중심에서 value 반경만큼 퍼지도록 dissolveMask 생성
    float dissolveMask = (value > 0.0) ? 1.0 - smoothstep(value - 0.05, value + 0.05, dist) : 0;

    
    // crashMask가 있는 부분만 파괴
    dissolveMask *= crashMask;

    // basecolor → color 로 보간
    RGBA = lerp(basecolor, color, dissolveMask);

    // 알파도 같이 줄어듦
    RGBA.a = basecolor.a * (1.0 - dissolveMask);
}