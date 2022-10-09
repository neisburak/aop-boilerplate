using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    //Otorasyon iş ihtiyaçlarına dayalıdır. Mesela proje için veritabanına bağlanılacaksa; (o projeye özgü olacaksa) bu business içerisine yazılmalıdır.
    //Veya özel bir veritabanına bağlanılmayacak veya sistemin temel bir Identity altyapısı vardır, o zaman Core'a yazılır.
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claims(ClaimTypes.Role);
}