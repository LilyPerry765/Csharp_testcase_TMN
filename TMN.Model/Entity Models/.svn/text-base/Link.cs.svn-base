using System;
using System.Linq;
using TMN.Interfaces;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace TMN
{

    public partial class Link : Entity, IDeletable, IChild
    {
        private TimeSlot[] TimeSlots = new TimeSlot[32];

        partial void OnCreated()
        {
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Link_PropertyChanged);
        }

        void Link_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DDF")
            {
                SendPropertyChanged("Image");
            }
        }

        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;

                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Links.DeleteOnSubmit(db.Links.Where(p => p.ID == this.ID).SingleOrDefault());
                try
                {
                    db.SubmitChanges();
                    db.Transaction.Commit();
                    return true;
                }
                catch
                {
                    db.Transaction.Rollback();
                    MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                    return false;
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            return false;
        }

        public object Parent
        {
            get
            {
                return this.Center;
            }
        }

        public LinkStates State
        {
            get
            {

                if (ContainsSignaling)
                {
                    return LinkStates.Signal;
                }
                else if (Channels.Count > 0)
                {
                    return LinkStates.Voice;
                }
                else if (DDF == null)
                {
                    return LinkStates.Free;
                }
                else
                {
                    return LinkStates.DDF;
                }
            }
        }

        public bool ContainsSignaling
        {
            get
            {
                return Channels.Any(c => c.Link == this && (c.Route.IsSignaling ?? false));
            }
        }

        public bool IsUnique()
        {
            return IsAddressUnique() && IsSysUnique();
        }

        private bool IsAddressUnique()
        {
            if (DB.Instance.Links.Count(l => l.ID != this.ID && l.Address == this.Address && l.CenterID == this.CenterID) > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName, "آدرس");
                return false;
            }
            return true;
        }

        private bool IsSysUnique()
        {
            if (DB.Instance.Links.Count(l => l.ID != this.ID && l.Sys == this.Sys && l.CenterID == this.CenterID) > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName, "Sys");
                return false;
            }
            return true;
        }

        //public static Link UpdateOrCreate(int? cic, string ltg, int diu, bool isImported)
        //{
        //    var db = DB.Instance;
        //    Link link = db.Links.SingleOrDefault(l => l.CenterID == Center.CurrentCenterID && l.LTG == ltg && l.DIU == diu);
        //    if (link == null)
        //    {
        //        link = new Link()
        //        {
        //            ID = Guid.NewGuid(),
        //            LTG = ltg,
        //            DIU = diu,
        //            CenterID = Center.CurrentCenterID,
        //            Address = ltg + "-" + diu.ToString()
        //        };
        //        db.Links.InsertOnSubmit(link);
        //    }
        //    link.IsImported = isImported;
        //    link.CIC = cic;
        //    db.SubmitChanges();
        //    return link;
        //}

        /// <summary>
        /// If all VOICE channels go to the same route, returns that route; returns null otherwise.
        /// </summary>
        /// <remarks>
        /// If there are some signalling channels combined with voice channels, the signallings are ignored.
        /// </remarks>
        public Route UniqueRoute
        {
            get
            {
                var voiceChannels = Channels.Where(c => c.Route.IsSignaling == false);
                // Only if all channels go to the same route
                if (voiceChannels.GroupBy(p => p.RouteID).Count() == 1)
                    return voiceChannels.First().Route;

                return null;
            }
        }
    }
}
