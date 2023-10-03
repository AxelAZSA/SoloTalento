using System.Threading.Tasks;
using System;
using WayneInc.Entitys.Tokens;

namespace WayneInc.Bussiness.Repository
{
    public interface IRefreshTokenRepository
    {
        Task CreateRefreshToken(RefreshToken refresh);
        Task<RefreshToken> GetByToken(string token, string role);
        Task DeleteRefreshToken(int tokenId);
        Task DeleteAll(int UserId, string role);
        Task Save();

    }
}
