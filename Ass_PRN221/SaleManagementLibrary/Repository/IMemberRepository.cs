using SaleManagementLibrary.DataAccess;

namespace SaleManagementLibrary.Repository
{
    public interface IMemberRepository
    {
        // Create
        void Add(Member member);

        // Read
        Member GetById(int id);

        Member getUser(string email,  string password);

        IEnumerable<Member> GetAll();

        // Update
        void Update(Member member);

        // Delete
        void Delete(int id);
    }

}
