using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using DataAccess.DBContext;

namespace DataAccess.Repositories;
public class MemberRepository : IMemberRepository
{

    private readonly eStoreContext _context;
    public MemberRepository(eStoreContext context)
    {
        _context = context;
    }

    public IEnumerable<Member> GetMembers() => _context.Members.ToList();

    public Member GetMemberById(int id) => _context.Members.Find(id);

    public void AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public void UpdateMember(Member member)
    {
        _context.Members.Update(member);
        _context.SaveChanges();
    }

    public void DeleteMember(int id)
    {
        var member = _context.Members.Find(id);
        if (member != null)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }
    }
}