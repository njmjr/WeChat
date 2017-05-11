using System.Collections.Generic;
using WeChat.Models;

namespace WeChat.DomainService.Repository.IRepositories
{
    /// <summary>
    /// COMMON领域服务类
    /// </summary>
    public interface ICommonRepository : IRepository
    {
        /// <summary>
        /// 获取字符型序列
        /// </summary>
        /// <param name="sequenceName">序列名</param>
        /// <param name="length">长度</param>
        /// <param name="prefixStr">前缀</param>
        /// <returns></returns>
        string GetSequence(string sequenceName, int length, string prefixStr);

        /// <summary>
        /// 获取整形序列
        /// </summary>
        /// <param name="sequenceName">序列名</param>
        /// <returns></returns>
        int GetSequence(string sequenceName);

        IEnumerable<Menu> GetMenuList(string staffno);

        IEnumerable<WxService> GetServices();

        IEnumerable<Role> GetRoles();
    }
}
