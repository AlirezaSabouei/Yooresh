using Yooresh.Domain.Entities;
namespace Yooresh.Application.Common.Tools;

public interface IPasswordEncryption<TBaseEntity> where TBaseEntity : BaseEntity
{
    string HashPassword(BaseEntity baseEntity, string password);

    bool VerifyPassword(BaseEntity baseEntity, string hashedPassword, string password);
}
