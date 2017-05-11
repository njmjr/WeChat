using System.Collections.Generic;

namespace WeChat.Models
{
    public class MenuPri
    { 
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool? @checked { get; set; }
        public List<MenuPri> children { get; set; }
        public Attributes attributes { get; set; }
        public string Pid { get; set; }
    }

    public class Attributes
    {
        public string Pid { get; set; }
    }

    public class MenuPriView
    {
        public MenuPriView(MenuPri menuPri)
        {
            id = menuPri.id;
            text = menuPri.text;
            state = menuPri.state;
            Pid = menuPri.Pid;
            @checked = menuPri.@checked;
            children = new List<MenuPriView>();
            attributes = new Attributes();
        }

        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool? @checked { get; set; }
        public List<MenuPriView> children { get; set; }
        public Attributes attributes { get; set; }
        public string Pid { get; set; }

    }
}
