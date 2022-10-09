void MainLight_float(float3 worldPos, out float3 direction, out float3 color, 
	out float distanceAttenuation, out float shadowAttenuation)
{
#ifdef SHADERGRAPH_PREVIEW

	direction = normalize(float3(.5f, .5f, .25f));
	color = float3(1.f, 1.f, 1.f);
	distanceAttenuation = 1.f;
	shadowAttenuation = 1.f;

#else
	
	float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
	Light mainLight = GetMainLight(shadowCoord);

	direction = mainLight.direction;
	color = mainLight.color;
	distanceAttenuation = mainLight.distanceAttenuation;
	shadowAttenuation = mainLight.shadowAttenuation;

#endif
}

void AdditionalLight_float(float3 worldPos, int index, out float3 direction, out float3 color,
	out float distanceAttenuation, out float shadowAttenuation)
{
	direction = normalize(float3(.5f, .5f, .25f));
	color = float3(0.f, 0.f, 0.f);
	distanceAttenuation = 0.f;
	shadowAttenuation = 0.f;

#ifndef SHADERGRAPH_PREVIEW
	
	int pixelLightCount = GetAdditionalLightsCount();
	if (index < pixelLightCount)
	{
		Light light = GetAdditionalLight(index, worldPos);
		direction = light.direction;
		color = light.color;
		distanceAttenuation = light.distanceAttenuation;
		shadowAttenuation = light.shadowAttenuation;
	}
#endif

}