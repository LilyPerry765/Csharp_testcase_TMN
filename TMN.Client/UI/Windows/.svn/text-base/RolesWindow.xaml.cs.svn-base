using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMN
{
    public partial class RolesWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();

        public RolesWindow()
        {
            InitializeComponent();

            cmbRole.ItemsSource = db.Roles.OrderBy(r => r.Name);
        }

        private void SetPermission(string roleID)
        {
            TreeViewModel root = (TreeViewModel)treeView.Items[0];
            int count = root.Children.Count;

            // get sub menu id
            for (int i = 0; i < count; i++)
            {
                foreach (TreeViewModel item in root.Children[i].Children)
                {
                    if (item.Children.Count == 0)
                    {
                        if (item.IsChecked != false)
                        {
                            db.RolePermissions.InsertOnSubmit(new RolePermission()
                            {
                                PermissionID = int.Parse(item.ID),
                                RoleID = roleID
                            });
                        }
                    }
                    else
                    {
                        // get Alarm Panel and Alarm Region panel id . because they have many children
                        if (item.IsChecked != false )
                        {
                            db.RolePermissions.InsertOnSubmit(new RolePermission()
                            {
                                PermissionID = int.Parse(item.ID),
                                RoleID = roleID
                            });
                        }

                        int t = item.Children.Count;
                        for (int j = 0; j < t; j++)
                        {
                            if (item.Children[j].IsChecked != false)
                            {
                                db.RolePermissions.InsertOnSubmit(new RolePermission()
                                {
                                    PermissionID = int.Parse(item.Children[j].ID),
                                    RoleID = roleID
                                });
                            }
                        }
                    }
                }
            }

            // get menu id
            foreach (TreeViewModel item in root.Children)
            {
                if (item.IsChecked != false)
                {
                    db.RolePermissions.InsertOnSubmit(new RolePermission()
                    {
                        PermissionID = int.Parse(item.ID),
                        RoleID = roleID
                    });
                }
            }

            db.SubmitChanges();
        }

        private void GetPermission(string roleID)
        {
            List<int> list =
                (
                    from p in db.Permissions
                    join rp in db.RolePermissions on p.PermissionID equals rp.PermissionID
                    join r in db.Roles on rp.RoleID equals r.ID
                    where r.ID == roleID
                    select p.PermissionID
                ).ToList();


            TreeViewModel root = (TreeViewModel)treeView.Items[0];
            int count = root.Children.Count;

            for (int i = 0; i < count; i++)
            {
                foreach (TreeViewModel item in root.Children[i].Children)
                {
                    if (item.Children.Count == 0)
                    {
                        if (IsInRole(list, int.Parse(item.ID)))
                        {
                            item.IsChecked = true;
                        }
                    }
                    else
                    {
                        int t = item.Children.Count;
                        for (int j = 0; j < t; j++)
                        {
                            if (IsInRole(list, int.Parse(item.Children[j].ID)))
                            {
                                item.Children[j].IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        private void DeletePermission(string roleID)
        {
            // db.RolePermissions.DeleteAllOnSubmit(db.RolePermissions.Where(rp => rp.RoleID == roleID));

            IEnumerable<RolePermission> list = (from rp in db.RolePermissions
                                                where rp.RoleID == roleID
                                                select rp);
            if (list != null)
                db.RolePermissions.DeleteAllOnSubmit(list);

        }

        public bool IsInRole(List<int> list, int permissionID)
        {
            return (from l in list
                    select l).Contains(permissionID);
        }

        private void UnSelectAll()
        {
            TreeViewModel root = (TreeViewModel)treeView.Items[0];
            int count = root.Children.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (TreeViewModel item in root.Children[i].Children)
                {
                    item.IsChecked = false;
                }
            }
        }

        private void SelectAll()
        {
            TreeViewModel root = (TreeViewModel)treeView.Items[0];
            int count = root.Children.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (TreeViewModel item in root.Children[i].Children)
                {
                    item.IsChecked = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            treeView.ItemsSource = TreeViewModel.SetTree();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRole.SelectedIndex != -1)
            {
                DeletePermission((string)cmbRole.SelectedValue);
                SetPermission((string)cmbRole.SelectedValue);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }

        private void btnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRole.SelectedIndex != -1)
            {
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                try
                {
                    DeletePermission((string)cmbRole.SelectedValue);

                    db.Roles.DeleteOnSubmit(db.Roles.SingleOrDefault(r => r.ID == (string)cmbRole.SelectedValue));

                    db.SubmitChanges();
                    db.Transaction.Commit();
                    db.Connection.Close();
                }
                catch (Exception)
                {
                    db.Transaction.Rollback();
                    db.Connection.Close();
                }
            }

            cmbRole.ItemsSource = db.Roles.OrderBy(r => r.Name);
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnSelectAll();
            GetPermission((string)cmbRole.SelectedValue);
        }
    }
}
