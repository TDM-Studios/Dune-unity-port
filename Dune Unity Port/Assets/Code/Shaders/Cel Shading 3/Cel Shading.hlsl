void CelShading_float(float diff, float4 csp, out float diffuse)
{
	if (diff < csp.x) diffuse = 0.05f;
	else if (diff < csp.y) diffuse = csp.y;
	else if (diff < csp.z) diffuse = csp.z;
	else diffuse = csp.w;
}

void CelShading_half(half diff, half4 csp, out half diffuse)
{
	if (diff < csp.x) diffuse = 0.05f;
	else if (diff < csp.y) diffuse = csp.y;
	else if (diff < csp.z) diffuse = csp.z;
	else diffuse = csp.w;
}