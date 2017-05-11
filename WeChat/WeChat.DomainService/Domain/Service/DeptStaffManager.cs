using System.Collections.Generic;
using WeChat.DomainService.Domain.IService;
using WeChat.DomainService.Repository.IRepositories;

namespace WeChat.DomainService.Domain.Service
{
    public class DeptStaffManager : DomainService, IDeptStaffManager
    {
        private readonly IInsideStaffRepository _insideStaffRepository;
        private readonly IRolePowerRepository _rolePowerRepository;
        private readonly IInsideDepartRepository _insideDepartRepository;

        public DeptStaffManager(IInsideStaffRepository insideStaffRepository, IRolePowerRepository rolePowerRepository, IInsideDepartRepository insideDepartRepository)
        {
            _insideStaffRepository = insideStaffRepository;
            _rolePowerRepository = rolePowerRepository;
            _insideDepartRepository = insideDepartRepository;
            TransientDependencies.AddRange(new List<ITransientDependency>
            {
                _insideStaffRepository , _rolePowerRepository , _insideDepartRepository 
            });
        }

        public IInsideStaffRepository InsideStaffRepository { get { return _insideStaffRepository; } }
        public IRolePowerRepository RolePowerRepository { get { return _rolePowerRepository; } }
        public IInsideDepartRepository InsideDepartRepository { get { return _insideDepartRepository; } }

        public string GetStaffName(string staffno)
        {
            var staff = _insideStaffRepository.GetDefault(staffno);
            if (staff == null)
            {
                return staffno;
            }
            else
            {
                return staff.STAFFNAME;
            }
        }

    }
}
