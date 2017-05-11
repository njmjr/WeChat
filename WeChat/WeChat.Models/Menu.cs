using System.Collections.Generic;

namespace WeChat.Models
{
    public class Menu
    { 
        public string MenuNo { get; set; }

        public string MenuName { get; set; } 

        public string PMenuNo { get; set; }

        public string Url { get; set; }

        public ICollection<Menu> Children { get; set; } 
    }

    public class MenuView
    {
        public MenuView(Menu menu)
        {
            MenuNo = menu.MenuNo;
            MenuName = menu.MenuName;
            PMenuNo = menu.PMenuNo;
            Url = menu.Url;
            Children = new List<MenuView>();
        }

        public string MenuNo { get; set; }

        public string MenuName { get; set; }

        public string PMenuNo { get; set; }

        public string Url { get; set; }

        public ICollection<MenuView> Children { get; set; }

    }
}
