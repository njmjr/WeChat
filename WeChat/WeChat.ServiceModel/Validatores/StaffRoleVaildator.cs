using ServiceStack.FluentValidation;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceModel.Validatores
{
    /// <summary>
    /// 员工信息录入验证
    /// </summary>
    public class StaffRoleVaildator : AbstractValidator<StaffRole>
    {
        public StaffRoleVaildator()
        {
            RuleFor(r => r.StaffNo).NotEmpty().WithMessage("员工编号不能为空").Length(6).WithMessage("员工编号长度须为6位").Matches(@"^[A-Za-z0-9]+$").WithMessage("员工编号必须为英数字");
        }
    }
}
