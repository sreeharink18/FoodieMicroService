namespace WebMVC.Service.IService
{
    public interface ITokenProvider
    {
        string? GetToken();
        void ClearToken();
        void SetToken(string token);
    }
}
