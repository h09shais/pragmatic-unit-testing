using Shopping.Core.Models;

namespace Shopping.Core.Repositories
{
    public interface IMemberRepository
    {
        Member FindById(int memberId);
    }
}
