using Microsoft.EntityFrameworkCore;
using SaleManagementLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {
            private readonly DBContext _context;

            public MemberRepository(DBContext context)
            {
                _context = context;
            }

            public void Add(Member member)
            {
                try
                {
                    _context.Members.Add(member);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding member: " + ex.Message, ex);
                }
            }

            public void Delete(int id)
            {
                try
                {
                    var member = GetById(id);
                    if (member != null)
                    {
                        _context.Members.Remove(member);
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting member: " + ex.Message, ex);
                }
            }

            public IEnumerable<Member> GetAll()
            {
                return _context.Members.ToList();
            }

            public Member GetById(int id)
            {
                try
                {
                    return _context.Members.FirstOrDefault(m => m.MemberId == id);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving member: " + ex.Message, ex);
                }
            }

        public Member getUser(string email, string password)
        {
            try
            {
                return _context.Members.FirstOrDefault(m => m.Email == email && m.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving member: " + ex.Message, ex);
            }
        }

        public void Update(Member member)
            {
                try
                {
                    var existingMember = _context.Members.Find(member.MemberId);
                    if (existingMember != null)
                    {
                        _context.Entry(existingMember).State = EntityState.Detached;
                    }

                    _context.Update(member);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating member: " + ex.Message, ex);
                }
            }
        }
    }
