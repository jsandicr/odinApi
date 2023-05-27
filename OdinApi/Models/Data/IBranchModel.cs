using OdinApi.Models.Obj;

namespace OdinApi.Models.Data
{
    public interface IBranchModel
    {
        public List<Branch> GetBranches();
        public Branch GetBranchById(int id);
        public Branch PostBranch(Branch branch);
        public Branch PutBranch(Branch branch);
        public Branch DeleteBranch(Branch);
    }
}
