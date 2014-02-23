namespace Owlsure.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Owlsure.Interfaces;
    using System.Collections.ObjectModel;

    public class MenuService: IMenuService
    {
        ObservableCollection<MenuEntry> menuEntries;

        public MenuService()
        {
            menuEntries = new ObservableCollection<MenuEntry>();
        }

        public void RegisterMenu(MenuEntry menuEntry)
        {
            if (!MenuEntries.Select(m => m.Text).Contains(menuEntry.Text))
            {
                MenuEntries.Add(menuEntry);
            }
        }

        public ObservableCollection<MenuEntry> MenuEntries
        {
            get
            {
                return menuEntries;
            }
        }
    }
}
