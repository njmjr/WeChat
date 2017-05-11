using System;
using System.Collections.Generic;
using System.Linq;
using WeChat.DomainService.Repository.IRepositories;
using WeChat.DomainService.Application.IService;
using WeChat.Models;
using WeChat.ServiceModel.Base;

namespace WeChat.DomainService.Application.Service
{
    public class CommonService : ApplicationService,ICommonService
    {
        private readonly ICommonRepository _commonRepository; 
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
            TransientDependencies.Add(_commonRepository);
        }

        public void GetAll(Common request, CommonResponse response)
        {
            GetService(request, response);
            GetRole(request, response);
        }

        public void GetService(Common request, CommonResponse response)
        {
            response.Services = _commonRepository.GetServices().ToList();
        }

        public void GetRole(Common request, CommonResponse response)
        {
            response.Roles = _commonRepository.GetRoles().ToList();
        }

        public void GetMenu(Common request, CommonResponse response)
        {
            //查询菜单表 
            var menus = _commonRepository.GetMenuList(request.CurrOper).ToList();

            //构造菜单 
            Func<Menu, IEnumerable<Menu>, MenuView> getMenuTree = null;
            getMenuTree = (menu, source) =>
            {
                MenuView view = new MenuView(menu);
                var enumerable = source as Menu[] ?? source.ToArray();
                List<Menu> children = enumerable.Where(m => m.PMenuNo == menu.MenuNo).OrderBy(m => m.MenuNo).ToList();
                foreach (Menu child in children)
                {
                    MenuView childView = getMenuTree(child, enumerable);
                    view.Children.Add(childView);
                }
                return view;
            };
            List<Menu> roots = menus.Where(m => m.PMenuNo == "000000").OrderBy(m => m.MenuNo).ToList();
            List<MenuView> datas = (from root in roots
                                    let source = menus.Where(m => m.PMenuNo.Equals(root.MenuNo)).ToList()
                                    select getMenuTree(root, source)).ToList();

            response.Menus = datas;
        }
    }
}
