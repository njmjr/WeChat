using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;
using WeChat.Utility;

namespace WeChat.DomainService.Repository.Repositories
{
    public class RoleRepository : Repository<Role, string>, IRoleRepository
    {
        public List<dynamic> QueryRoleInfo(string roleNo = null)
        {
            string sql = @"SELECT T.ROLENO, T.ROLENAME, T.UPDATESTAFFNO, T.UPDATETIME, T.REMARK
                             FROM TD_M_ROLE T
                            WHERE (:ROLENO IS NULL OR :ROLENO = '' OR T.ROLENO = :ROLENO)
                             AND T.USETAG = '1'
                            ORDER BY T.ROLENO";
            return Connection.Query(sql, new { ROLENO = roleNo }).ToList();
        }

        public void InsertRole(string roleNo, string roleName, string curOper, string remark)
        {
            string sql =
                 @"INSERT INTO TD_M_ROLE (ROLENO, ROLENAME, UPDATESTAFFNO, UPDATETIME, REMARK, USETAG)
	               VALUES(:ROLENO, :ROLENAME, :UPDATESTAFFNO, :UPDATETIME, :REMARK, '1') ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        ROLENO = roleNo,
                        ROLENAME = roleName,
                        UPDATESTAFFNO = curOper,
                        UPDATETIME = DateTime.Now,
                        REMARK = remark
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("INSERT_ROLE_ERR", "插入角色信息表失败");
            }
        }

        public void UpdateRoleInfo(string roleNo, string roleName, string curOper, string remark)
        {
            string sql =
              @"UPDATE TD_M_ROLE
		                SET ROLENAME = :ROLENAME,
			                UPDATESTAFFNO	 = :UPDATESTAFFNO,
                            UPDATETIME = :UPDATETIME,
			                REMARK	 = :REMARK
		                WHERE ROLENO = :ROLENO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        ROLENO = roleNo,
                        ROLENAME = roleName,
                        UPDATESTAFFNO = curOper,
                        UPDATETIME = DateTime.Now,
                        REMARK = remark
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("UPDATE_ROLE_ERR", "更新角色信息表失败");
            }
        }

        public void DeleteRole(string roleNo)
        {
            string sql =
            @"DELETE TD_M_ROLE WHERE ROLENO = :ROLENO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        ROLENO = roleNo,
                    }, transaction: Tx) < 0)
            {
                throw new WeChatException("DELETE_ROLE_ERR", "删除角色表失败");
            }
        }

        public List<dynamic> QueryStaffRoleInfo(string stffNo = null)
        {
            string sql = @"SELECT T.ROLENO,T.ROLENAME,T.UPDATESTAFFNO,B.UPDATETIME,T.REMARK, DECODE(NVL(B.STAFFNO, '0'), '0', '0', '1') CHECKSTATE
                             FROM TD_M_ROLE T,(SELECT * FROM TD_M_INSIDESTAFFROLE A WHERE A.STAFFNO = :STAFFNO) B
                            WHERE T.ROLENO = B.ROLENO(+)
                             AND T.USETAG = '1'
                            ORDER BY T.ROLENO";
            return Connection.Query(sql, new { STAFFNO = stffNo }).ToList();
        }

        public void UpdateStaffRoleInfo(string stffNo, IEnumerable<string> roles, string curOper)
        {
            foreach (var role in roles)
            {
                string sql = @"INSERT INTO TD_M_INSIDESTAFFROLE (STAFFNO, ROLENO, UPDATESTAFFNO, UPDATETIME, REMARK)
	                        VALUES(:STAFFNO, :ROLENO, :UPDATESTAFFNO, :UPDATETIME, '')";
                if (
                    Connection.Execute(
                        sql,
                        new
                        {
                            STAFFNO = stffNo,
                            ROLENO = role,
                            UPDATESTAFFNO = curOper,
                            UPDATETIME = DateTime.Now
                        }, transaction: Tx) != 1)
                {
                    throw new WeChatException("UPDATE_ROLE_ERR", "更新员工角色信息失败");
                }
            }

        }

        public void DeleteStaffRoleInfo(string stffNo)
        {
            string sql =
            @"DELETE TD_M_INSIDESTAFFROLE WHERE STAFFNO = :STAFFNO";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        STAFFNO = stffNo,
                    }, transaction: Tx) < 0)
            {
                throw new WeChatException("DELETE_STAFFROLE_ERR", "删除员工角色失败");
            }
        }

        public List<dynamic> QueryRolePowerHandle(string roleNo = null)
        {
            string sql = @"SELECT T.POWERCODE,T.POWERNAME,T.REMARK,DECODE(NVL(B.ROLENO, '0'), '0', '0', '1') CHECKSTATE
                               FROM TD_M_POWER T,(SELECT * FROM TD_M_ROLEPOWER A WHERE A.ROLENO = :ROLENO AND A.POWERTYPE = '2') B
                             WHERE T.POWERCODE = B.POWERCODE(+)
                              ORDER BY T.POWERCODE";
            return Connection.Query(sql, new { ROLENO = roleNo }).ToList();
        }

        public IEnumerable<MenuPri> QueryRolePowerMenus(string roleNo = null)
        {
            string sql = @"select c.ID,
       c.TEXT,
       c.PID,
       decode(c.PID, '000000', '', c.CHECKED) CHECKED,
       c.STATE
  from (SELECT DISTINCT (T.MENUNO) ID,
                        T.MENUNAME TEXT,
                        T.PMENUNO PID,
                        DECODE(NVL(A.POWERCODE, 'false'),
                               'false',
                               'false',
                               'true') CHECKED,
                        decode(t.PMENUNO, '000000', 'closed', '') STATE
          FROM TD_M_MENU T,
               (SELECT * FROM TD_M_ROLEPOWER B WHERE B.ROLENO = :ROLENO) A
         WHERE T.MENUNO = A.POWERCODE(+)) c
";
            return Connection.Query<MenuPri>(sql, new { ROLENO = roleNo }).ToList();
        }

        public void DeleteAllRolePower(string roleNo)
        {
            const string sql = @"DELETE TD_M_ROLEPOWER WHERE ROLENO = :ROLENO";
            if (Connection.Execute(sql, new { ROLENO = roleNo, }, transaction: Tx) < 0)
            {
                throw new WeChatException("DELETE_STAFFROLE_ERR", "删除角色权限失败");
            }
        }

        public void InsertRoleMenus(string roleNo, IEnumerable<string> menus)
        {
            foreach (var menu in menus)
            {
                string sql = @"INSERT INTO TD_M_ROLEPOWER (ROLENO, POWERCODE, POWERTYPE, REMARK)
	                        VALUES(:ROLENO, :POWERCODE, '1', '')";
                if (Connection.Execute(sql, new { ROLENO = roleNo, POWERCODE = menu }, transaction: Tx) != 1)
                {
                    throw new WeChatException("UPDATE_ROLE_ERR", "新增角色菜单权限失败");
                }
            }
        }

        public void InsertRoleHandles(string roleNo, IEnumerable<string> handles)
        {
            foreach (var handle in handles)
            {
                string sql = @"INSERT INTO TD_M_ROLEPOWER (ROLENO, POWERCODE, POWERTYPE, REMARK)
	                        VALUES(:ROLENO, :POWERCODE, '2', '')";
                if (Connection.Execute(sql, new { ROLENO = roleNo, POWERCODE = handle }, transaction: Tx) != 1)
                {
                    throw new WeChatException("UPDATE_ROLE_ERR", "新增角色操作权限失败");
                }
            }
        }

        public void InsertRoleMenuFather(string roleNo)
        {
            string sql = @"INSERT INTO TD_M_ROLEPOWER (ROLENO, POWERCODE, POWERTYPE) SELECT :ROLENO, C.PMENUNO, '1'  FROM (SELECT DISTINCT B.PMENUNO 
                             FROM TD_M_ROLEPOWER T, TD_M_MENU B WHERE T.ROLENO = :ROLENO AND T.POWERTYPE = '1' AND T.POWERCODE = B.MENUNO)C";
            if (Connection.Execute(sql, new { ROLENO = roleNo }, transaction: Tx) < 0)
            {
                throw new WeChatException("UPDATE_ROLE_ERR", "新增角色菜单父节点权限失败");
            }
        }
    }
}
