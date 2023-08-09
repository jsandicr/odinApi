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
                if(branch.id != 0)
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
                var query = (from b in _context.Branch
                             where b.active == true
                             select b).ToList();

                if (query != null)
                {
                    List<Branch> branches = new List<Branch>();
                    foreach (var q in query)
                    {
                        branches.Add(q);
                    }
                    return branches;
                }
                else
                {
                    return new List<Branch>();
                }
                return query;
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
                    if (branch.active)
                    {
                        branch.active = false;
                    }
                    else
                    {
                        branch.active = true;
                    }
                    _context.Update(branch);
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

        public List<Branch> GetBranchesAll()
        {

            var branches = _context.Branch.ToList();

            if (branches != null)
            {
                
                return branches;
            }
            else
            {
                return new List<Branch>();
            }
            
        }
    }
}
