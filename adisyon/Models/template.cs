using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adisyon.Models
{
    public class template
    {
        public static void ShowMessageInfo(string message, Form owner)
        {
            Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
            messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
            messageDialog.Caption = "Bilgi";
            messageDialog.Text = message;
            messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            messageDialog.Parent = owner;
            messageDialog.Show();
        }

        public static DialogResult ShowMessageQuestion(string message, Form owner)
        {
            Guna.UI2.WinForms.Guna2MessageDialog messageDialog = new Guna.UI2.WinForms.Guna2MessageDialog();
            messageDialog.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
            messageDialog.Caption = "Soru";
            messageDialog.Text = message;
            messageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            messageDialog.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            messageDialog.Parent = owner;
            return messageDialog.Show();
        }
    }
}
