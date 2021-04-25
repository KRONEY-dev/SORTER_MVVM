using System;
using System.Windows.Forms;

namespace SORTER_MVVM.Model.Interfaces
{
    internal interface ISystem_tray_Application
    {
        void NotifyIconContextMenu_Add_new_Item(string Name, EventHandler Command_Name);
        ContextMenu NotifyIconContextMenu { get; set; }
        void NotifyIcon_On();
        void NotifyIcon_Off();
    }
}
