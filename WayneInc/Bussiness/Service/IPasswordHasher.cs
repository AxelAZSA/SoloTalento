namespace WayneInc.Bussiness.Service
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHasher);
    }
}
