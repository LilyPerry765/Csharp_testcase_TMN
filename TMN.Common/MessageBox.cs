using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace TMN
{
    public class MessageBox
    {
        #region API

        [DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool SetDlgItemText(IntPtr hWnd, int nIDDlgItem, string lpString);

        delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        static HookProc dlgHookProc;
        private const int FARSI_LCID = 1065;

        private const long WH_CBT = 5;
        private const long HCBT_ACTIVATE = 5;

        private const int ID_BUT_OK = 1;
        private const int ID_BUT_CANCEL = 2;
        private const int ID_BUT_ABORT = 3;
        private const int ID_BUT_RETRY = 4;
        private const int ID_BUT_IGNORE = 5;
        private const int ID_BUT_YES = 6;
        private const int ID_BUT_NO = 7;

        private const string BUT_OK = "تاييد";
        private const string BUT_CANCEL = "انصراف";
        private const string BUT_ABORT = "توقف";
        private const string BUT_RETRY = "دوباره";
        private const string BUT_IGNORE = "چشمپوشی";
        private const string BUT_YES = "بله";
        private const string BUT_NO = "خير";

        private static int _hook = 0;

        private static int DialogHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {

            if (nCode == HCBT_ACTIVATE)
            {
                SetDlgItemText(wParam, ID_BUT_OK, BUT_OK);
                SetDlgItemText(wParam, ID_BUT_CANCEL, BUT_CANCEL);
                SetDlgItemText(wParam, ID_BUT_ABORT, BUT_ABORT);
                SetDlgItemText(wParam, ID_BUT_RETRY, BUT_RETRY);
                SetDlgItemText(wParam, ID_BUT_IGNORE, BUT_IGNORE);
                SetDlgItemText(wParam, ID_BUT_YES, BUT_YES);
                SetDlgItemText(wParam, ID_BUT_NO, BUT_NO);
            }

            return CallNextHookEx(_hook, nCode, wParam, lParam);
        }

        #endregion

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxImage icon)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, icon);
        }

        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(messageBoxText, "");
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(messageBoxText, caption, button, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            dlgHookProc = new HookProc(DialogHookProc);
            // if the current default system locale is not farsi, the farsi characters will be shown as ???
            if (GetSystemDefaultLCID() == FARSI_LCID)
                _hook = SetWindowsHookEx((int)WH_CBT, dlgHookProc, (IntPtr)0, (int)GetCurrentThreadId());

            System.Windows.Forms.DialogResult dlgEmptyCheck = System.Windows.Forms.MessageBox.Show(messageBoxText
                                                            , caption
                                                            , (System.Windows.Forms.MessageBoxButtons)button
                                                            , (System.Windows.Forms.MessageBoxIcon)icon
                                                            , System.Windows.Forms.MessageBoxDefaultButton.Button1
                                                            , System.Windows.Forms.MessageBoxOptions.RightAlign | System.Windows.Forms.MessageBoxOptions.RtlReading);
            if (_hook != 0)
                UnhookWindowsHookEx(_hook);
            return (MessageBoxResult)dlgEmptyCheck;
        }

        public static MessageBoxResult Show(MessageTypes messageType)
        {
            switch (messageType)
            {
                case MessageTypes.CannotDeleteHasItems:
                    return Show("اين آيتم دارای متعلقات ديگری است و درحال حاضر امكان حذف آن وجود ندارد.", "حذف", MessageBoxImage.Error);
                case MessageTypes.CannotDelete:
                    return Show("امكان حذف اين آيتم وجود ندارد.", "حذف", MessageBoxImage.Error);
                case MessageTypes.ConfirmDelete:
                    return Show("آيتم انتخاب شده حذف شود؟", "حذف", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                case MessageTypes.RepeatedName:
                    return MessageBox.Show("اين نام تکراری است.", "نام تکراری", MessageBoxImage.Error);
                case MessageTypes.UnAuthorizedCenter:
                    return MessageBox.Show("شما از پرسنل مرکز ديگری بوده و امکان انجام اين کار را در اين مرکز نداريد.", "خطا", MessageBoxImage.Error);
                case MessageTypes.AccessDenied:
                    return MessageBox.ShowError("انجام اين عمليات برای شما مجاز نمی باشد.");
                case MessageTypes.HasChilds :
                    return MessageBox.ShowQuestion("این آیتم دارای متعلقات است . آیا میخواهید این متعلقات حذف شود ؟", MessageBoxButton.YesNo);
                default:
                    throw new NotSupportedException();
            }
        }

        public static MessageBoxResult Show(MessageTypes messageType, string itemName)
        {
            switch (messageType)
            {
                case MessageTypes.CannotDeleteHasItems:
                    return Show(string.Format("{0} خود داراي متعلقات ديگري است و درحال حاضر امكان حذف آن وجود ندارد.", itemName), "حذف", MessageBoxImage.Error);
                case MessageTypes.CannotDelete:
                    return Show(string.Format("امكان حذف {0} وجود ندارد.", itemName), "حذف", MessageBoxImage.Error);
                case MessageTypes.ConfirmDelete:
                    return Show(string.Format("{0} حذف شود؟", itemName), "حذف", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                case MessageTypes.RepeatedName:
                    return MessageBox.Show(string.Format("{0} تکراری است.", itemName), "تکراری", MessageBoxImage.Error);
                default:
                    throw new NotSupportedException();
            }
        }

        public static MessageBoxResult ShowError(string text, params object[] args)
        {
            return Show(string.Format(text, args), "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowWarning(string text, MessageBoxButton button)
        {
            return Show(text, "هشدار", button, MessageBoxImage.Warning);
        }

        public static MessageBoxResult ShowInfo(string text, string caption)
        {
            return Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult ShowQuestion(string text, MessageBoxButton button)
        {
            return Show(text, "پرسش", button, MessageBoxImage.Question);
        }

        ~MessageBox()
        {
            Enterprise.Logger.WriteInfo("Messagebox finalized");
        }
    }

    public enum MessageTypes
    {
        CannotDeleteHasItems = 1,
        CannotDelete,
        ConfirmDelete,
        RepeatedName,
        UnAuthorizedCenter,
        AccessDenied,
        HasChilds
    }
}
