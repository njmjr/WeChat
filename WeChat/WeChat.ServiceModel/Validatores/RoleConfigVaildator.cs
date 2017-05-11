using ServiceStack.FluentValidation;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceModel.Validatores
{
    /// <summary>
    /// 角色信息录入验证
    /// </summary>
    public class RoleConfigVaildator : AbstractValidator<RoleConfig>
    {
        public RoleConfigVaildator()
        {
            RuleSet("Add", AddValidator);
            RuleSet("Edit", AddValidator);
            RuleSet("Delete", DeleteValidator);
        }

        private void AddValidator()
        {
            RuleFor(r => r.RoleNo).NotNull().WithMessage("角色编号不能为空").Length(6).WithMessage("角色编号长度须为4位").Matches(@"^[A-Za-z0-9]+$").WithMessage("角色编号必须为英数字");
            RuleFor(r => r.RoleName).NotNull().WithMessage("角色姓名不能为空").Length(0, 20).WithMessage("角色姓名长度超长"); 
            RuleFor(r => r.CurrOper).NotNull().WithMessage("操作角色不能为空");
        }

        private void DeleteValidator()
        {
            RuleFor(r => r.RoleNo).NotNull().WithMessage("角色编号不能为空");
        }
    }
}
