using System.Linq;
using System.Data.Linq;
using System.Windows.Media.Imaging;
using System.Reflection;
using System;
using TMN.Interfaces;
using System.Windows.Media;

namespace TMN
{

    public partial class Channel : Entity, IDeletable
    {

        public void Release()
        {
            this._Route = default(EntityRef<Route>);
            this._Link = default(EntityRef<Link>);
        }

        public static int DeleteImports()
        {
            var db = DB.Instance;
            return db.DeleteAll(db.Channels.Where(c => c.IsImported == true));
        }


        public BitmapImage Image
        {
            get
            {
                string path;

                if (this.Route.IsSignaling ?? false)
                {
                    path = "link_signal";
                }
                else
                {
                    path = "link_voice";
                }

                //string asmName = Assembly.GetExecutingAssembly().GetName().Name;
                //string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}.png", asmName, path);
                //return new BitmapImage(new Uri(uriString));
                return (BitmapImage)GetImageSource(path + ".png");
            }
        }

        private static ImageSource GetImageSource(string path)
        {
            string asmName = "Resources";
            string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}", asmName, path);
            ImageSource imgSrc = new BitmapImage(new Uri(uriString));
            return imgSrc;
        }

        #region IDeletable Members

        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Channels.DeleteOnSubmit(db.Channels.Single(c => c == this));
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        #endregion
    }
}
