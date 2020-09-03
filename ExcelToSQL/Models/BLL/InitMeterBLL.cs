using ExcelToSQL.Models.DAL;
using System.Collections.Generic;

namespace ExcelToSQL.Models.BLL
{
    public class InitMeterBLL
    {
        public static void Create(int PID)
        {
            var result = getBranchesAndBranchMeter(PID);
            BranchMeterCreate(PID, result.branchMeters);
            BuildMeterCreate(PID, result.branches, result.branchMeters);
            EnergyItemMeterCreate(PID, result.branches, result.branchMeters);
        }

        private static (List<VM_Branch> branches, List<BranchMeter> branchMeters) getBranchesAndBranchMeter(int PID)
        {
            //读取所有仪表
            var meters = MeterDAL.GetViewListByPID(PID);
            //读取所有支路
            var branches = BranchDAL.GetViewListByPID(PID);
            var branchMeters = ModelLink.BranchMeterLink(branches, meters);
            return (branches, branchMeters);
        }

        public static void BranchMeterCreate(int PID)
        {
            var result = getBranchesAndBranchMeter(PID);
            //清空BranchMeter表
            BranchMeterDAL.DeleteByPID(PID);
            CommonDAL.CreateMultiple(result.branchMeters);
        }

        private static void BranchMeterCreate(int PID, List<BranchMeter> branchMeters)
        {
            //清空BranchMeter表
            BranchMeterDAL.DeleteByPID(PID);
            CommonDAL.CreateMultiple(branchMeters);
        }

        public static void BuildMeterCreate(int PID)
        {
            var result = getBranchesAndBranchMeter(PID);
            //清空BuildMeter表
            BuildMeterDAL.DeleteByPID(PID);
            var buildMeters = ModelLink.BuildMeterLink(result.branches, result.branchMeters);
            CommonDAL.CreateMultiple(buildMeters);
        }

        private static void BuildMeterCreate(int PID, List<VM_Branch> branches, List<BranchMeter> branchMeters)
        {
            //清空BuildMeter表
            BuildMeterDAL.DeleteByPID(PID);
            var buildMeters = ModelLink.BuildMeterLink(branches, branchMeters);
            CommonDAL.CreateMultiple(buildMeters);
        }

        private static void EnergyItemMeterCreate(int PID, List<VM_Branch> branches, List<BranchMeter> branchMeters)
        {
            //清空EnergyItemMeter表
            EnergyItemMeterDAL.DeleteByPID(PID);
            var item_meters = ModelLink.EnergyItemMeterLink(branches, branchMeters);
            CommonDAL.CreateMultiple(item_meters);
        }

    }


}