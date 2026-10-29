namespace SmartDeApi.Common.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapFeatureEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointTypes = typeof(EndpointExtensions).Assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false }
                && type.GetInterfaces().Contains(typeof(IEndpoint)))
            .OrderBy(type => type.FullName, StringComparer.Ordinal);

        foreach (var type in endpointTypes)
            ((IEndpoint)Activator.CreateInstance(type)!).MapEndpoint(app);

        return app;
    }
}
