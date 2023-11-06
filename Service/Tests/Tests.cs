using Microsoft.Extensions.Configuration;
using Service.Domain;
using Service.Helpers;
using Service.Services;

namespace Tests;

public class Tests
{
    [Fact]
    public void Login_LoginProvided_Login()
    {
        var jwtToken = new JwtToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibG9naW4iOiJKb2hubnkgU2lsdmVyaGFuZCIsImlhdCI6MTUxNjIzOTAyMn0.LZGaTy5mb35Vr3e1-9WAWrRgeAkBz8F-OO00joUMFjY");

        Assert.Equal("Johnny Silverhand", jwtToken.Login);
    }
    
    [Fact]
    public void Login_LoginNotProvided_Throw()
    {
        var jwtToken = new JwtToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiaWF0IjoxNTE2MjM5MDIyfQ.L8i6g3PfcHlioHCCPURC9pmXT7gdJpx3kOoyAfNUwCc");
        
        Assert.Throws<KeyNotFoundException>(() => jwtToken.Login);
    }

    [Fact]
    public void OptionsProvided_ParseValues()
    {
        var config = new ConfigurationManager();
        config.AddInMemoryCollection(new Dictionary<string, string?>()
        {
            ["JwtValidation:Secret"] = "asd",
            ["JwtValidation:ExpireIn"] = "00:15:00"
        });

        var options = config.GetSection("JwtValidation").Get<JwtValidationOptions>();
        
        Assert.Equal(new JwtValidationOptions("asd", TimeSpan.FromMinutes(15)), options);
    }

    [Fact]
    public void GenerateToken()
    {
        var currentTime = new DateTimeOffset(2020, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var factory = new JwtPairFactory(new MockSystemClock(currentTime), new JwtValidationOptions("asdasdasdasdasdasdasdasdasdasdasdasd", TimeSpan.FromMinutes(15)));

        var jwtToken = factory.CreateJwtToken("John");
    }
}