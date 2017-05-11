using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;

namespace WeChat.DomainService.Repository.Repositories
{
    public class CommonRepository : Repository, ICommonRepository
    {
        public string GetSequence(string sequenceName, int length, string prefixStr)
        {
            string result = string.Empty;
            string sql = "SELECT " + sequenceName + ".NEXTVAL FROM DUAL";
            long seq = Connection.Query<long>(sql, transaction: Tx).SingleOrDefault();
            if (!string.IsNullOrEmpty(prefixStr))
            {
                result = prefixStr + seq.ToString(CultureInfo.InvariantCulture).PadLeft(length, '0');
            }
            return result;
        }

        public IEnumerable<WxService> GetServices()
        {
            return Connection.GetAll<WxService>(transaction: Tx);
        }

        public IEnumerable<Role> GetRoles()
        {
            return Connection.Query<Role>(@"SELECT * FROM TD_M_ROLE T ORDER BY T.ROLENO", transaction: Tx);
        }

        public int GetSequence(string sequenceName)
        {
            string sql = "SELECT " + sequenceName + ".NEXTVAL FROM DUAL";
            int seq = Connection.Query<int>(sql, transaction: Tx).SingleOrDefault();
            return seq;
        }


        public IEnumerable<Menu> GetMenuList(string staffno)
        {
            return
                Connection.Query<Menu>(@"Select Distinct(MI.MENUNO) MENUNO,MI.MENUNAME , MI.PMENUNO , MI.URL , MI.TARGET ,MI.TIPS,MI.CLICKFUC,
                                        MI.Defaulflag,MI.MENULEVEL,MI.UPDATESTAFFNO,MI.UPDATETIME,MI.REMARK,MI.ISNEW 
                                        From TD_M_INSIDESTAFFROLE INSROLL ,TD_M_ROLEPOWER ROLEP ,TD_M_MENU MI 
                                        Where INSROLL.STAFFNO = :STAFFNO And INSROLL.ROLENO = ROLEP.ROLENO AND MI.ISNEW='2'
                                        And ROLEP.POWERTYPE = '1' And ROLEP.POWERCODE = MI.MENUNO Order By MI.MENUNO",
                    new
                    {
                        STAFFNO = staffno
                    }, transaction: Tx);
        }
    }
}
