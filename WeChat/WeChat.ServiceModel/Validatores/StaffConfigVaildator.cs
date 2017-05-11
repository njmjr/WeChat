using ServiceStack.FluentValidation;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceModel.Validatores
{
    /// <summary>
    /// 员工信息录入验证
    /// </summary>
    public class StaffConfigVaildator : AbstractValidator<StaffConfig>
    {
        public StaffConfigVaildator()
        {
            RuleSet("Add", AddValidator);
            RuleSet("Edit", AddValidator);
            RuleSet("Delete", DeleteValidator);
        }

        private void AddValidator()
        {
            RuleFor(r => r.StaffNo).NotNull().WithMessage("员工编号不能为空").Length(6).WithMessage("员工编号长度须为6位").Matches(@"^[A-Za-z0-9]+$").WithMessage("员工编号必须为英数字");
            RuleFor(r => r.StaffName).NotNull().WithMessage("员工姓名不能为空").Length(0, 20).WithMessage("员工姓名长度超长"); 
            RuleFor(r => r.DepartNo).NotNull().WithMessage("员工部门不能为空");
            RuleFor(r => r.DimissionTag).NotNull().WithMessage("员工状态不能为空");
            RuleFor(r => r.CurrOper).NotNull().WithMessage("操作员工不能为空");
            When(r => r.OperatorCardNo != null, () =>
            {
                RuleFor(c => c.OperatorCardNo).Length(16).WithMessage("操作员卡号长度须为16位").Matches(@"^[0-9]+$").WithMessage("操作员卡号必须为数字");
            });
        }

        private void DeleteValidator()
        {
            RuleFor(r => r.StaffNo).NotNull().WithMessage("员工编号不能为空");
        }
    }
}
