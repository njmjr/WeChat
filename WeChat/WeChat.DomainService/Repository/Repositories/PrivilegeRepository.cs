using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.Models;
using WeChat.Utility;

namespace WeChat.DomainService.Repository.Repositories
{
    public class PrivilegeRepository : Repository<InsideStaff, string>, IPrivilegeRepository
    {
        public List<dynamic> QueryStaffInfo(string departNo = null, string staffNo = null)
        {
            string sql = @" SELECT staff.StaffNo ,staff.StaffName ,depart.DepartName ,staff.DimissionTag ,staff.DepartNo FROM TD_M_INSIDESTAFF staff,TD_M_INSIDEDEPART depart
                            WHERE staff.DEPARTNO  = depart.DEPARTNO AND (:DEPARTNO IS NULL OR :DEPARTNO='' OR depart.DEPARTNO =:DEPARTNO) AND (:STAFFNO IS NULL OR :STAFFNO='' OR staff.STAFFNO = :STAFFNO) ORDER BY staff.DEPARTNO,staff.STAFFNO";
            return Connection.Query(sql, new { DEPARTNO = departNo, STAFFNO = staffNo }).ToList();
        }

        /// <summary>
        /// 插入员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        /// <param name="staffName"></param>
        /// <param name="operCardPwd"></param>
        /// <param name="departNo"></param>
        /// <param name="dimissionTag"></param>
        /// <param name="curOper"></param>
        /// <param name="updateTime"></param>
        public void InsertStaffInfo(string staffNo, string staffName, string operCardPwd, string departNo, string dimissionTag, string curOper, DateTime updateTime)
        {
            string sql =
                 @"insert into td_m_insidestaff (STAFFNO, STAFFNAME, OPERCARDPWD, DEPARTNO, DIMISSIONTAG, UPDATESTAFFNO, UPDATETIME)
	               VALUES(:STAFFNO,:STAFFNAME,:OPERCARDPWD,:DEPARTNO,:DIMISSIONTAG,:UPDATESTAFFNO,:UPDATETIME) ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        STAFFNO = staffNo,
                        STAFFNAME = staffName,
                        OPERCARDPWD = operCardPwd,
                        DEPARTNO = departNo,
                        DIMISSIONTAG = dimissionTag,
                        UPDATESTAFFNO = curOper,
                        UPDATETIME = updateTime
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("INSERT_INSIDESTAFF_ERR", "插入员工信息表失败");
            }
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        /// <param name="staffName"></param>
        /// <param name="departNo"></param>
        /// <param name="dimissionTag"></param>
        /// <param name="curOper"></param>
        /// <param name="updateTime"></param>
        public void UpdateStaffInfo(string staffNo, string staffName, string departNo, string dimissionTag, string curOper, DateTime updateTime)
        {
            string sql =
              @"UPDATE td_m_insidestaff
		                SET STAFFNAME = :STAFFNAME,
			                DEPARTNO	 = :DEPARTNO,
                            DIMISSIONTAG = :DIMISSIONTAG,
			                UPDATETIME	 = :UPDATETIME,
			                UPDATESTAFFNO = :UPDATESTAFFNO
		                WHERE STAFFNO = :STAFFNO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        STAFFNO = staffNo,
                        STAFFNAME = staffName,
                        DEPARTNO = departNo,
                        DIMISSIONTAG = dimissionTag,
                        UPDATETIME = updateTime,
                        UPDATESTAFFNO = curOper
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("UPDATE_INSIDESTAFF_ERR", "更新员工信息表失败");
            }
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="staffNo"></param>
        public void DeleteStaffInfo(string staffNo)
        {
            string sql =
             @"DELETE td_m_insidestaff WHERE STAFFNO = :STAFFNO ";
            if (
                Connection.Execute(
                    sql,
                    new
                    {
                        STAFFNO = staffNo,
                    }, transaction: Tx) != 1)
            {
                throw new WeChatException("DELETE_INSIDESTAFF_ERR", "删除员工信息表失败");
            }
        }
    }
}
