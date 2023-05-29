using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IBranchModel
    {
        public List<Branch> GetBranches();
        public Branch GetBranchById(int id);
        public Branch PostBranch(Branch branch);
        public Branch PutBranch(Branch branch);
        public Branch DeleteBranch(int id);
    }
}
