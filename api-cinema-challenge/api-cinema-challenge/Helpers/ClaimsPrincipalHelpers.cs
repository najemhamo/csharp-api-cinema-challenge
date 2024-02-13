using System.Security.Claims;

namespace api_cinema_challenge.Helpers
{
  public static class ClaimsPrincipalHelpers
  {
    public static string? UserId(this ClaimsPrincipal user)
    {
      Claim? claim = user.FindFirst(ClaimTypes.NameIdentifier);
      return claim?.Value;
    }
    
  }

}