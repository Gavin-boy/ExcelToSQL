using System.Collections.Generic;
using System.Linq;

namespace ExcelToSQL.Models
{
    public class ModelLink
    {
        public static List<BuildMeter> BuildMeterLink(List<VM_Branch> branches, List<BranchMeter> BranchMeterLink)
        {
            return branches
               .Where(b => b.Level == 1 && b.State == StateConsts.Normal)
               .Join(BranchMeterLink, b => b.ID, bm => bm.BranchID, (b, bm) => new BuildMeter { BuildID = b.BuildID, MeterSN = bm.MeterSN, PID = b.PID }).ToList();
        }

        public static List<BranchMeter> BranchMeterLink(List<VM_Branch> branches, List<VM_Meter> meters)
        {
            // 支路组装出层级，但列表依然是全支路，而不是定级支路
            var v = branches.Select(b =>
            {
                VM_Branch_Child c = b.MapTo<VM_Branch, VM_Branch_Child>();
                c.Childs = new List<VM_Branch_Child>();
                return c;
            })
                            .ToDictionary(b => b.ID, b => b);

            foreach (VM_Branch_Child branch in v.Values)
            {
                if (branch.ParentID != null)
                {
                    v[branch.ParentID.Value].Childs.Add(branch);
                }
            }

            // 支路和仪表是一对一的
            Dictionary<int, VM_Meter> branch_meter_dict = meters.ToDictionary(m => m.BranchID, m => m);

            List<BranchMeter> branchMeters = new List<BranchMeter>();

            foreach (VM_Branch_Child branch in v.Values)
            {
                branchMeters.AddRange(GetBranchMeters(branch.ID, branch, branch_meter_dict));
            }

            return branchMeters;
        }

        private static IEnumerable<BranchMeter> GetBranchMeters(int branch_id, VM_Branch_Child branch, Dictionary<int, VM_Meter> branch_meter_dict)
        {
            if (branch_meter_dict.ContainsKey(branch.ID))
            {
                // 支路有表，则以当前支路的表计为准
                return new BranchMeter[]
                {
                    new BranchMeter { BranchID = branch_id, MeterSN = branch_meter_dict[branch.ID].SN,PID = branch.PID }
                };
            }
            else
            {
                // 支路没有表，则统计子支路之和
                List<BranchMeter> branchMeters = new List<BranchMeter>();
                foreach (VM_Branch_Child child in branch.Childs)
                {
                    branchMeters.AddRange(GetBranchMeters(branch_id, child, branch_meter_dict));
                }
                return branchMeters;
            }
        }

        public static List<EnergyItemMeter> EnergyItemMeterLink(List<VM_Branch> branches, List<BranchMeter> BranchMeterLink)
        {
            return branches
                .Where(b => b.Level == 1 && b.State == StateConsts.Normal && b.EnergyItemCode != null)
                .Join(BranchMeterLink, b => b.ID, bm => bm.BranchID, (b, bm) => new EnergyItemMeter { EnergyItemCode = b.EnergyItemCode, BuildID = b.BuildID, MeterSN = bm.MeterSN, PID = b.PID }).ToList();
        }
    }
}
