using System.Collections.Generic;
using WeChat.Models;

namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IInsideDepartRepository : IRepository<InsideDepart,string>
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="departNo"></param>
        /// <returns></returns>
        IEnumerable<InsideDepart> GetDepartList(string departNo = "");

        /// <summary>
        /// 获取代理营业厅部门列表
        /// </summary>
        /// <param name="dbalUnitNo"></param>
        /// <param name="departNo"></param>
        /// <returns></returns>
        IEnumerable<InsideDepart> GetDepartBakunitList(string dbalUnitNo, string departNo = "");

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <param name="departNo"></param>
        /// <returns></returns>
        List<dynamic> QueryDepartInfo(string departNo = null);

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="departNo"></param>
        /// <param name="departName"></param>
        /// <param name="curOper"></param>
        /// <param name="remark"></param>
        void InsertDepart(string departNo, string departName, string curOper, string remark);

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="departNo"></param>
        /// <param name="departName"></param>
        /// <param name="curOper"></param>
        /// <param name="remark"></param>
        void UpdateDepartInfo(string departNo, string departName, string curOper, string remark);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departNo"></param>
        void DeleteDepart(string departNo);
    }
}
