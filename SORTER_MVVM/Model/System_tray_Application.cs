using SORTER_MVVM.Model.Interfaces;
using System;
using System.Windows.Forms;

namespace SORTER_MVVM.Model
{
    internal class System_tray_Application : ISystem_tray_Application
    {
        public ContextMenu NotifyIconContextMenu { get; set; }
        private NotifyIcon NotifyIcon { get; set; }

        private readonly string tip_Title = "The application runs in the background";
        private readonly string tip_Text = "Click 'Open' to deploy application";
        public System_tray_Application()
        {
            NotifyIcon = new NotifyIcon();
            NotifyIconContextMenu = new ContextMenu();

            NotifyIcon.ContextMenu = NotifyIconContextMenu;
            NotifyIcon.Icon = Properties.Resources.output__1_;

        }

        public void NotifyIconContextMenu_Add_new_Item(string Name, EventHandler Command_Name)
        {
            NotifyIconContextMenu.MenuItems.Add(Name, new EventHandler(Command_Name));
        }

        public void NotifyIcon_On()
        {
            NotifyIcon.Visible = true;
            NotifyIcon.ShowBalloonTip(1000, tip_Title, tip_Text, ToolTipIcon.Info);
        }

        public void NotifyIcon_Off()
        {
            NotifyIcon.Visible = false;
        }
    }
}
