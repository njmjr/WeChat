namespace WeChat.DomainService.Repository.IRepositories
{
    public interface IRolePowerRepository : IRepository
    {
        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="powerCode">权限编码</param>
        /// <param name="staffno">员工</param>
        bool HasOperPower(string powerCode, string staffno);

        /// <summary>
        /// 菜单权限验证
        /// </summary>
        /// <param name="staffno"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        bool Verify(string staffno, string url);
    }
}
