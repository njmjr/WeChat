using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;
using WeChat.Utility;

namespace WeChat.DomainService.Repository.Repositories
{
    public class InsideDepartRepository :Repository<InsideDepart,string>,IInsideDepartRepository
    {
        public IEnumerable<InsideDepart> GetDepartList(string departNo = "")
        {
            var sql = @"SELECT DEPARTNO || ':' || DEPARTNAME AS DEPARTNAME, DEPARTNO
                                FROM TD_M_INSIDEDEPART
                                WHERE USETAG = '1'";
            if (!string.IsNullOrEmpty(departNo))
            {
                sql = sql + " AND DEPARTNO=:DEPARTNO";
            }
            sql = sql + " ORDER BY DEPARTNO";
            return Connection.Query<InsideDepart>(sql,new { DEPARTNO =departNo},transaction:Tx);
        }

        public IEnumerable<InsideDepart> GetDepartBakunitList(string dbalUnitNo,string departNo = "")
        {
            string sql = @"SELECT DEPARTNO || ':' || DEPARTNAME AS DEPARTNAME, DEPARTNO
                                    FROM TD_DEPTBAL_RELATION R,TD_M_INSIDEDEPART D 
                                    WHERE R.DEPARTNO=D.DEPARTNO AND R.DBALUNITNO=:DBALUNITNO 
                                    AND R.USETAG='1'";
            if (!string.IsNullOrEmpty(departNo))
            {
                sql = sql + " AND D.DEPARTNO = :DEPARTNO";
            }
            sql = sql + " ORDER BY DEPARTNO";
            return Connection.Query<InsideDepart>(sql, new { DBALUNITNO = dbalUnitNo,DEPARTNO = departNo }, transaction: Tx);
        }

        public List<dynamic> QueryDepartInfo(string departNo = null)
        {
            string sql = @"SELECT T.DEPARTNO, T.DEPARTNAME, T.UPDATESTAFFNO, T.UPDATETIME, T.REMARK
                             FROM TD_M_INSIDEDEPART T
                            WHERE (:DEPARTNO IS NULL OR :DEPARTNO = '' OR T.DEPARTNO = :DEPARTNO)
                              AND T.USETAG = '1'
                            ORDER BY T.DEPARTNO";
            return Connection.Query(sql, new { DEPARTNO = departNo}).ToList();
        }

        public void InsertDepart(string departNo, string departName, string curOper, string remark)
        {
            string sql =
                 @"INSERT INTO TD_M_INSIDEDEPART (DEPARTNO, DEPARTNAME, UPDATESTAFFNO, UPDATETIME, REMARK, USETAG)
	               VALUES(:DEPARTNO, :DEPARTNAME, :UPDATESTAFFNO, :UPDATETIME, :REMARK, '1') ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        DEPARTNO = departNo,
                        DEPARTNAME = departName,
                        UPDATESTAFFNO = curOper,
                        UPDATETIME = DateTime.Now,
                        REMARK = remark
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("INSERT_INSIDEDEPART_ERR", "插入部门信息表失败");
            }
        }

        public void UpdateDepartInfo(string departNo, string departName, string curOper, string remark)
        {
            string sql =
              @"UPDATE TD_M_INSIDEDEPART
		                SET DEPARTNAME = :DEPARTNAME,
			                UPDATESTAFFNO	 = :UPDATESTAFFNO,
                            UPDATETIME = :UPDATETIME,
			                REMARK	 = :REMARK
		                WHERE DEPARTNO = :DEPARTNO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        DEPARTNO = departNo,
                        DEPARTNAME = departName,
                        UPDATESTAFFNO = curOper,
                        UPDATETIME = DateTime.Now,
                        REMARK = remark
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("UPDATE_INSIDEDEPART_ERR", "更新部门信息表失败");
            }
        }

        public void DeleteDepart(string departNo)
        {
            string sql =
             @"DELETE TD_M_INSIDEDEPART WHERE DEPARTNO = :DEPARTNO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        DEPARTNO = departNo,
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("DELETE_INSIDEDEPART_ERR", "删除部门表失败");
            }
        }
    }
}
