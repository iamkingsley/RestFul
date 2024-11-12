using DemoApp.Application.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DemoApp.Identity.Utility;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private string SectionName = "JwtOptions";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
