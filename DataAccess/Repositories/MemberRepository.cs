using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly eStoreContext _context;

    public MemberRepository(eStoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member> GetMemberByIdAsync(int id)
    {
        return await _context.Members.FindAsync(id);
    }

    public async Task AddMemberAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMemberAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMemberAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member != null)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Member> GetMemberByEmailAsync(string email)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
    }

    public async Task UpdatePasswordAsync(int memberId, string hashedPassword)
    {
        var member = await _context.Members.FindAsync(memberId);
        if (member == null)
        {
            throw new Exception("Member not found");
        }

        member.Password = hashedPassword;
        await _context.SaveChangesAsync();
    }
}
