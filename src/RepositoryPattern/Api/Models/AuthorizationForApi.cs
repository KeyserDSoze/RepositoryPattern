namespace Microsoft.Extensions.DependencyInjection
{
    public class AuthorizationForApi
    {
        public bool UseAuthorization { get; set; }
        public string[]? Policies { get; set; }
    }
}
