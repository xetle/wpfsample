namespace Owlsure.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using System.Collections.ObjectModel;

    public class MenuEntry
    {
        public String Text { get; set; }
        public String IconUrl { get; set; }
        public List<MenuEntry> Children { get; private set; }
        public ICommand Command { get; set; }

        public MenuEntry()
        {
            Children = new List<MenuEntry>();
        }
    }

    public interface IMenuService
    {
        ObservableCollection<MenuEntry> MenuEntries {get;}
        void RegisterMenu(MenuEntry menuEntry);
    }
}
