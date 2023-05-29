using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class BranchModel : IBranchModel
    {

        private readonly OdinContext _context;

        public BranchModel(OdinContext context)
        {
            _context = context;
        }
        public Branch GetBranchById(int id)
        {
            try
            {
                Branch branch = _context.Branch.Find(id);
                if(branch.id == 0)
                {
                    return branch;
                }
                else
                {
                    return new Branch();
                }
            }
            catch (Exception)
            {
                return new Branch();
            }
        }

        public List<Branch> GetBranches()
        {
            try
            {
                return _context.Branch.ToList();
            }
            catch (Exception)
            {
                return new List<Branch>();
            }
        }

        public Branch PostBranch(Branch branch)
        {
            try
            {
                _context.Branch.Add(branch);
                _context.SaveChanges();
                return branch;
            }
            catch (Exception)
            {
                return new Branch();
            }
        }

        public Branch DeleteBranch(int id)
        {
            try
            {
                Branch branch = _context.Branch.Find(id);
                if(branch != null)
                {
                    _context.Remove(branch);
                    _context.SaveChanges();
                    return branch;
                }
                else
                {
                    return new Branch();
                }
            }
            catch (Exception)
            {
                return new Branch();
            }
        }

        public Branch PutBranch(Branch branch)
        {
            try
            {
                _context.Update(branch);
                _context.SaveChanges();
                return branch;
            }
            catch (Exception)
            {
                return new Branch();
            }
        }
    }
}
