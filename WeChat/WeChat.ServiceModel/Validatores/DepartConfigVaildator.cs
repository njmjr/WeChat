using ServiceStack.FluentValidation;
using WeChat.ServiceModel.PrivilegePR;

namespace WeChat.ServiceModel.Validatores
{
    /// <summary>
    /// 部门维护
    /// </summary>
    public class DepartConfigVaildator : AbstractValidator<DepartConfig>
    {
        public DepartConfigVaildator()
        {
            RuleSet("Add", AddValidator);
            RuleSet("Edit", AddValidator);
            RuleSet("Delete", DeleteValidator);
        }

        private void AddValidator()
        {
            RuleFor(r => r.DepartNo).NotNull().WithMessage("部门编号不能为空").Length(4).WithMessage("部门编号长度须为4位").Matches(@"^[A-Za-z0-9]+$").WithMessage("部门编号必须为英数字");
            RuleFor(r => r.DepartName).NotNull().WithMessage("部门名称不能为空").Length(0, 40).WithMessage("部门名称长度超长"); 
            RuleFor(r => r.DepartNo).NotNull().WithMessage("员工部门不能为空");
        }

        private void DeleteValidator()
        {
            RuleFor(r => r.DepartNo).NotNull().WithMessage("部门编号不能为空");
        }
    }
}
