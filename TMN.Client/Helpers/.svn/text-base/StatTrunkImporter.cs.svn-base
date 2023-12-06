using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Transactions;

namespace TMN
{

    public class StatTrunkImporter
    {
        //private TMNModelDataContext db = new TMNModelDataContext();
        private List<Link> links;
        private List<Route> routes;
        private OPM[] opms;

        public StatTrunkImporter()
        {
            Logger.WriteInfo("Intantiating StatTrunk Importer.");
            LoadInitData();
        }

        private void LoadInitData()
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                Logger.WriteInfo("Deleting all channels...");
                db.ExecuteCommand("TRUNCATE TABLE  Channel;");
                Logger.WriteInfo("Loading links into memory...");
                links = db.Links.Where(l => l.CenterID == Center.CurrentCenterID).ToList();
                Logger.WriteInfo("Loading routes into memory...");
                routes = db.Routes.Where(r => r.SourceCenter == Center.CurrentCenterID).ToList();
                Logger.WriteInfo("Loading opms into memory...");
                opms = db.OPMs.ToArray();
            }
        }

        public void ImportRow(string tgno, int lno, int? cic, byte ts, string opm, string ltg, int diu)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                Guid linkID = UpdateOrCreateLink(cic, ltg, diu, true).ID;
                Guid routeID = UpdateOrCreateRoute(tgno, opm, IsSignalling(tgno), true).ID;
                db.Channels.InsertOnSubmit(new Channel()
                 {
                     ID = Guid.NewGuid(),
                     IsImported = true,
                     TimeSlot = ts,
                     LNO = lno,
                     LinkID = linkID,
                     RouteID = routeID,
                     Tag = "inserted"
                 });
                db.SubmitChanges();
            }
        }

        public Route UpdateOrCreateRoute(string tgno, string opm, bool isSignalling, bool isImported)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                Route route = routes.SingleOrDefault(r => r.TGNO.Trim() == tgno.Trim());
                if (route == null)
                {
                    route = new Route()
                    {
                        ID = Guid.NewGuid(),
                        TGNO = tgno.Trim(),
                        SourceCenter = Center.CurrentCenterID,
                        IsSignaling = isSignalling,
                        Tag = "inserted"
                    };
                    db.Routes.InsertOnSubmit(route);
                    routes.Add(route);
                }
                else
                    route.Tag = "updated";
                route.OPMID = opms.Single(o => o.Name.ToLower() == opm.ToLower().Trim()).ID;
                route.IsImported = isImported;
                db.SubmitChanges();
                return route;
            }
        }

        public Link UpdateOrCreateLink(int? cic, string ltg, int diu, bool isImported)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                Link link = links.SingleOrDefault(l => l.LTG == ltg && l.DIU == diu);
                if (link == null)
                {
                    link = new Link()
                    {
                        ID = Guid.NewGuid(),
                        LTG = ltg,
                        DIU = diu,
                        CenterID = Center.CurrentCenterID,
                        Address = ltg + "-" + diu.ToString(),
                        Tag = "inserted"
                    };
                    db.Links.InsertOnSubmit(link);
                    links.Add(link);
                }
                else
                    link.Tag = "updated";
                link.IsImported = isImported;
                link.CIC = cic;
                db.SubmitChanges();
                return link;
            }
        }

        private bool IsSignalling(string tgno)
        {
            string TGNO = tgno.ToUpper();
            if (TGNO.ToUpper().StartsWith("L") && !TGNO.EndsWith("V"))
            {
                return true;
            }
            return false;
        }

        private void EnableConstraints(bool isEnable = true)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                Logger.WriteInfo("{0} constraints...", isEnable ? "Enabling" : "Disabling");
                string command = "ALTER TABLE [{0}] " + (isEnable ? "WITH CHECK CHECK" : "NOCHECK") + " CONSTRAINT ALL;";
                db.ExecuteCommand(string.Format(command, typeof(Link).Name));
                db.ExecuteCommand(string.Format(command, typeof(Route).Name));
            }
        }

        internal void SubmitChanges()
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {

                Logger.WriteInfo("Deleting {0} unused route(s)...", routes.Count(r => r.Tag == null));
                db.Routes.DeleteAllOnSubmit(routes.Where(r => r.Tag == null));
                Logger.WriteInfo("Deleting {0} unused link(s)...", links.Count(r => r.Tag == null));
                db.Links.DeleteAllOnSubmit(links.Where(l => l.Tag == null));
                Logger.WriteInfo("Submitting deletion of routes and links...");
                db.SubmitChanges();
            }
        }
    }
}
