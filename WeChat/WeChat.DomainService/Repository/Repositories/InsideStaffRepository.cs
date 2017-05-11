using System.Collections.Generic;
using System.Linq;
using Dapper;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;

namespace WeChat.DomainService.Repository.Repositories
{
    public class InsideStaffRepository : Repository<InsideStaff, string>, IInsideStaffRepository
    {
        public InsideStaff CheckOperStaff(string operCardNo)
        {
            string sql =
                @"Select  STAFFNO, STAFFNAME, OPERCARDNO, OPERCARDPWD, DEPARTNO, DIMISSIONTAG, UPDATESTAFFNO, UPDATETIME, REMARK From TD_M_INSIDESTAFF Where OPERCARDNO = :OPERCARDNO And DIMISSIONTAG = '1' ";
            var checkStaff = Connection.Query<InsideStaff>(sql,
                    new
                    {
                        OPERCARDNO = operCardNo
                    }, transaction: Tx).FirstOrDefault();
            if (checkStaff == null)
            {
                throw new Utility.WeChatException("CHECK_STAFF", "查询审核员工失败" + operCardNo);
            }
            return checkStaff;
        }

        public IEnumerable<InsideStaff> GetAssignedStaff()
        {
            string sql = @"select stff.STAFFNO || ':' || stff.STAFFNAME AS STAFFNAME, stff.STAFFNO STAFFNO from  TD_M_INSIDESTAFF stff 
                          where  stff.DIMISSIONTAG = '1' and    stff.STAFFNO in ( select rost.STAFFNO from   TD_M_INSIDESTAFFROLE rost, 
                          TD_M_ROLE roll,TD_M_ROLEPOWER powr where rost.ROLENO = roll.ROLENO AND   roll.ROLENO = powr.ROLENO 
                          AND powr.POWERCODE = '201001' AND powr.POWERTYPE = '2') order by STAFFNO";

            return Connection.Query<InsideStaff>(sql).ToList();
        }

        public IEnumerable<InsideStaff> GetStaffList(string departNo = "",string staffNo ="")
        {
            string sql = @"SELECT STAFFNO || ':' || STAFFNAME AS STAFFNAME,  STAFFNO,DEPARTNO 
                    FROM TD_M_INSIDESTAFF
                    Where  DIMISSIONTAG = '1'";
            if (!string.IsNullOrEmpty(departNo))
            {
                sql = sql + " AND DEPARTNO = :DEPARTNO";
            }
            if (!string.IsNullOrEmpty(staffNo))
            {
                sql = sql + " AND STAFFNO = :STAFFNO";
            }
            sql = sql + " ORDER BY STAFFNO";
            return Connection.Query<InsideStaff>(sql, new {DEPARTNO = departNo, STAFFNO = staffNo}, transaction: Tx);
        }

        public IEnumerable<InsideStaff> GetStaffListForDeptBalunit(string dbalUnitNo)
        {
            string sql = @"SELECT STAFFNO || ':' || STAFFNAME AS STAFFNAME, STAFFNO, DEPARTNO FROM TD_M_INSIDESTAFF
                             WHERE DIMISSIONTAG = '1' AND DEPARTNO IN
                            (SELECT DEPARTNO FROM TD_DEPTBAL_RELATION WHERE DBALUNITNO =:DBALUNITNO AND USETAG = '1'
                           ORDER BY STAFFNO";
            return Connection.Query<InsideStaff>(sql, new { DBALUNITNO = dbalUnitNo}, transaction: Tx);
        }

        public InsideStaff GetStaffByToken(string staffNo, string token)
        {
            var sql = @"SELECT * FROM TD_M_INSIDESTAFF WHERE STAFFNO =:STAFFNO AND TRIM(TOKEN) = :TOKEN ";
            return Connection.Query<InsideStaff>(sql, new {STAFFNO = staffNo, TOKEN = token}, transaction: Tx).FirstOrDefault();
        } 
    }
}
