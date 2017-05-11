using System;
using System.Linq;
using Dapper;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;

namespace WeChat.DomainService.Repository.Repositories
{
    public  class RolePowerRepository :Repository<RolePower,String>, IRolePowerRepository
    {
        public bool HasOperPower(string powerCode, string staffno)
        {
            return Connection.Query<int>(@"Select COUNT(*) From TD_M_ROLEPOWER 
                      Where POWERCODE = :POWERCODE 
                      And ROLENO IN ( SELECT ROLENO From TD_M_INSIDESTAFFROLE Where STAFFNO = :STAFFNO)",
                     new { POWERCODE = powerCode, STAFFNO = staffno },transaction:Tx).First() > 0;
        }

        public bool  Verify(string staffno,string url)
        { 
            string sql = @" SELECT DISTINCT(MI.MENUNO) MENUNO,MI.MENUNAME , MI.PMENUNO , MI.URL FROM TD_M_INSIDESTAFFROLE INSROLL ,TD_M_ROLEPOWER ROLEP ,TD_M_MENU MI
  WHERE  INSROLL.STAFFNO = :STAFFNO AND UPPER(MI.URL)= :URL AND INSROLL.ROLENO = ROLEP.ROLENO AND ROLEP.POWERTYPE = '1' AND ROLEP.POWERCODE = MI.MENUNO Order By MI.MENUNO";
            var menus = Connection.Query<Menu>(sql, new { STAFFNO = staffno, URL = url.ToUpper() }, transaction: Tx); 
            if (menus != null && menus.Any())
            {
                return true;
            }
            return false;
        }
    }
}
