using System.Collections.Generic;
using ServiceStack;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.ServiceModel.PrivilegePR
{
    [Route("/RolePower")]
    public class RolePower : BaseRequest
    {
        [ApiMember(Name = "RequestType", Description = "请求方法", DataType = "string", IsRequired = true)]
        public short RequestType { get; set; }

        [ApiMember(Name = "RoleNo", Description = "角色编号", DataType = "string", IsRequired = true)]
        public string RoleNo { get; set; }

        [ApiMember(Name = "Menus", Description = "菜单项", DataType = "IList<string>", IsRequired = true)]
        public IList<string> Menus { get; set; }

        [ApiMember(Name = "Handles", Description = "操作权限", DataType = "IList<string>", IsRequired = true)]
        public IList<string> Handles { get; set; }
    }

    public class RolePowerResponse : BaseResponse
    {
        [ApiMember(Name = "QueryData", Description = "返回权限信息", DataType = "List", IsRequired = true)]
        public Report QueryData { get; set; }

        public List<MenuPriView> Menus { get; set; }
    }

    public enum RolePowerRequestType : short
    {
        Menu = 0,
        Handle = 1,
        Modify = 2
    }
}
