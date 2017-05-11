using ServiceStack.FluentValidation;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceModel.Validatores
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RolePowerVaildator : AbstractValidator<RolePower>
    {
        public RolePowerVaildator()
        {
            RuleSet("Query", QueryValidator);
            RuleSet("Modify", ModifyValidator);
        }

        private void QueryValidator()
        {
            RuleFor(r => r.RequestType).NotEmpty().WithMessage("请求类型不能为空");
        }

        private void ModifyValidator()
        {
            RuleFor(r => r.RequestType).NotEmpty().WithMessage("请求类型不能为空");
            RuleFor(r => r.RoleNo).NotEmpty().WithMessage("角色编码不能为空");
        }
    }
}
