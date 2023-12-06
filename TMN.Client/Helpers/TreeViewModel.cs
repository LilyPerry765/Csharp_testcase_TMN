using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TMN
{
    public class TreeViewModel : INotifyPropertyChanged
    {
        public TreeViewModel(string name, string id)
        {
            ID = id;
            Name = name;
            Children = new List<TreeViewModel>();
        }

        #region Properties

        public string ID { get; private set; }
        public string Name { get; private set; }
        public List<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }

        bool? _isChecked = false;
        TreeViewModel _parent;

        #region IsChecked

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked) return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue) Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null) _parent.VerifyCheckedState();

            NotifyPropertyChanged("IsChecked");
        }

        void VerifyCheckedState()
        {
            bool? state = null;

            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            SetIsChecked(state, false, true);
        }

        #endregion

        #endregion

        private void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public static List<TreeViewModel> SetTree()
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();

            TreeViewModel root = new TreeViewModel("TMN", "tmn");
            treeView.Add(root);

            TreeViewModel system = new TreeViewModel("سیستم", "1");
            TreeViewModel baseinfo = new TreeViewModel("اطلاعات پایه", "2");
            TreeViewModel center = new TreeViewModel(" مرکز", "3");
            TreeViewModel reports = new TreeViewModel("گزارشات", "4");
            TreeViewModel settings = new TreeViewModel("تنظیمات", "5");
            TreeViewModel centermanager = new TreeViewModel("مدیریت مراکز", "6");

            root.Children.Add(system);
            root.Children.Add(centermanager);
            root.Children.Add(baseinfo);
            root.Children.Add(center);
            root.Children.Add(reports);
            root.Children.Add(settings);

            system.Children.Add(new TreeViewModel("وضعیت سرویس ها", "7"));
            system.Children.Add(new TreeViewModel("سنسور های غیر فعال", "8"));

            baseinfo.Children.Add(new TreeViewModel("انواع سوییچ", "9"));
            baseinfo.Children.Add(new TreeViewModel("انواع رک", "10"));
            baseinfo.Children.Add(new TreeViewModel("انواع شلف", "11"));
            baseinfo.Children.Add(new TreeViewModel("انواع کارت", "12"));
            baseinfo.Children.Add(new TreeViewModel("انواع عملیات", "13"));
            baseinfo.Children.Add(new TreeViewModel("آیتم های گزارش", "14"));
            baseinfo.Children.Add(new TreeViewModel("مسیرها", "15"));
            baseinfo.Children.Add(new TreeViewModel("انواع آلارم", "16"));
            baseinfo.Children.Add(new TreeViewModel("تعریف سنسور ها", "17"));
            
            center.Children.Add(new TreeViewModel("شماي مركز", "18"));
            center.Children.Add(new TreeViewModel("ليست E1", "19"));
            center.Children.Add(new TreeViewModel("ارتباط مراكز", "20"));
            center.Children.Add(new TreeViewModel("DDF", "21"));
            center.Children.Add(new TreeViewModel("دستور هاي مداري", "22"));
			center.Children.Add(new TreeViewModel("گزارشات مرکز", "24"));
			center.Children.Add(new TreeViewModel("تست های مرکز", "25"));
			center.Children.Add(new TreeViewModel("خرابی های مرکز", "26"));

			TreeViewModel alarmRegion = new TreeViewModel("نقشه ناحيه", "31");
			center.Children.Add(alarmRegion);
			alarmRegion.Children.Add(new TreeViewModel("سوییچ", "32"));
			alarmRegion.Children.Add(new TreeViewModel("سنسور Power", "33"));
			alarmRegion.Children.Add(new TreeViewModel("سنسور کابل ", "34"));
			alarmRegion.Children.Add(new TreeViewModel("اتصال به مرکز", "35"));
			alarmRegion.Children.Add(new TreeViewModel("اتصال به VPN", "36"));
			alarmRegion.Children.Add(new TreeViewModel("به روز رسانی", "37"));
			alarmRegion.Children.Add(new TreeViewModel("چيدن مراکز جديد", "38"));
			alarmRegion.Children.Add(new TreeViewModel("قفل مراکز ", "39"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای مراکز", "40"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای Critical", "41"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای Major", "42"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای Minor", "43"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای سنسور", "44"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای منبع تغذیه", "45"));
			alarmRegion.Children.Add(new TreeViewModel("قطع صدای کابل", "46"));

			TreeViewModel alarmPanel = new TreeViewModel("آلارم پنل", "27");
			center.Children.Add(alarmPanel);
			alarmPanel.Children.Add(new TreeViewModel("تعلیق آلارم", "28"));
			alarmPanel.Children.Add(new TreeViewModel("تایید آلارم", "29"));
			alarmPanel.Children.Add(new TreeViewModel("حذف آلارم", "30"));

			center.Children.Add(new TreeViewModel("آلارم Power", "48"));
			center.Children.Add(new TreeViewModel("آلارم کابل", "75"));
            center.Children.Add(new TreeViewModel("آرشیو الارم", "23"));
            center.Children.Add(new TreeViewModel("سوابق کاربران", "74"));
			center.Children.Add(new TreeViewModel("مدیریت ارسال پیامک", "76"));
            
			//center.Children.Add(new TreeViewModel("نقشه کابل", "47"));
			//center.Children.Add(new TreeViewModel("نقشه Power", "49"));

            center.Children.Add(new TreeViewModel("Log Book", "50"));
            center.Children.Add(new TreeViewModel("رکورد طولانی", "51"));
            center.Children.Add(new TreeViewModel("کارت های يدکی", "52"));
			

            reports.Children.Add(new TreeViewModel("گزارش مسير ها", "53"));
            reports.Children.Add(new TreeViewModel("گزارش لينک ها", "54"));
            reports.Children.Add(new TreeViewModel("گزارش ترانک ها", "55"));
            reports.Children.Add(new TreeViewModel("گزارش دستور مداری", "56"));
            reports.Children.Add(new TreeViewModel("گزارش آلارم و خرابی ها", "57"));
            reports.Children.Add(new TreeViewModel("Log Book", "58"));
            reports.Children.Add(new TreeViewModel("گزارش تست های مرکز", "59"));
            reports.Children.Add(new TreeViewModel("گزارش رکورد طولانی", "60"));
            reports.Children.Add(new TreeViewModel("گزارش کارت ها", "61"));
            reports.Children.Add(new TreeViewModel("گزارش کارت های يدکی", "62"));
            reports.Children.Add(new TreeViewModel("نمودار پاسخگويی سنسورها", "63"));
            reports.Children.Add(new TreeViewModel("نمودار تغييرات سنسورها", "64"));
            reports.Children.Add(new TreeViewModel("گزارش مقادير سنسورها", "65"));

            settings.Children.Add(new TreeViewModel("تنظیمات سیستم", "66"));
            settings.Children.Add(new TreeViewModel("مدیریت کاربران", "67"));
            settings.Children.Add(new TreeViewModel("مدیریت شیفت ها", "68"));

            centermanager.Children.Add(new TreeViewModel("ایجاد مرکز", "69"));
            centermanager.Children.Add(new TreeViewModel("ویرایش مرکز", "70"));
            centermanager.Children.Add(new TreeViewModel("حذف مرکز", "71"));
            centermanager.Children.Add(new TreeViewModel("جزییات مراکز", "72"));
            centermanager.Children.Add(new TreeViewModel("به روز رسانی مراکز", "73"));

            root.Initialize();
            return treeView;
        }

        #region INotifyPropertyChanged Members

        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
