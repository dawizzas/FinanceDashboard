using Bunit;
using Blazored.LocalStorage;
using Bunit.TestDoubles;
using FinanceDashboard.Client.Shared;

namespace FinanceDashboard.Tests;

public class LogoutButtonTest
{
    [Fact]
    public void ButtonShouldSayLogoutWhenNotAuthorized()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoredLocalStorage();
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("user", AuthorizationState.Unauthorized);

        // Act
        var cut = ctx.RenderComponent<Logout>();

        // Assert
        cut.MarkupMatches("<button  class=\"btn btn-primary\">Login</button>");
    }

    [Fact]
    public void ButtonShouldSayLoginWhenAuthorized()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoredLocalStorage();
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("user", AuthorizationState.Authorized);

        // Act
        var cut = ctx.RenderComponent<Logout>();

        // Assert
        cut.MarkupMatches("<button  class=\"btn btn-danger\">Logout</button>");
    }
}