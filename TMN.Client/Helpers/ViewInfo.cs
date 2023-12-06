using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Views.Details;
using TMN.Views.Lists;

namespace TMN
{
    public struct ViewInfo
    {
        public TMN.Interfaces.IDetailsView DetailsView
        {
            get;
            set;
        }

        public TMN.Views.Lists.ItemsListBase ListView
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public static ViewInfo GetByEntityType(ViewType infoType, EntityTypes entityType, object arg)
        {
            ViewInfo output = new ViewInfo();

            switch (entityType)
            {
                case EntityTypes.SwitchType:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new SwitchTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<SwitchTypesListView>.Instance;
                    output.Title = "انواع سوييچ";
                    break;
                case EntityTypes.RackType:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new RackTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<RackTypesListView>.Instance;
                    output.Title = "انواع رك";
                    break;
                case EntityTypes.ShelfType:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new ShelfTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<ShelfTypesListView>.Instance;
                    output.Title = "انواع شلف";
                    break;
                case EntityTypes.CardType:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new CardTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<CardTypesListView>.Instance;
                    output.Title = "انواع كارت";
                    break;
                case EntityTypes.Center:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new CenterView();
                    output.Title = "مركز";
                    break;
                case EntityTypes.Rack:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new RackView(arg as Center);
                    output.Title = "رك";
                    break;
                case EntityTypes.Shelf:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new ShelfView(arg as Rack);
                    output.Title = "شلف";
                    break;
                case EntityTypes.Card:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new CardView(arg as Shelf);
                    output.Title = "كارت";
                    break;
                case EntityTypes.SpareCard:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new SpareCardView();
                    if (infoType == ViewType.List)
                        output.ListView = new SpareCardsListView(arg as Center);
                    output.Title = "كارت يدك";
                    break;
                case EntityTypes.Link:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new LinkView(arg as Center);
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<LinksListView>.Instance;
                    output.Title = "لينك";
                    break;
                case EntityTypes.Instruction:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new InstructionView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<InstructionsListView>.Instance;
                    output.Title = "دستور مداري";
                    break;
                case EntityTypes.Route:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new RoutesView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<RoutesListView>.Instance;
                    output.Title = "مسير";
                    break;
                case EntityTypes.Channel:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new ChannelView(arg as Link);
                    if (infoType == ViewType.List)
                        output.ListView = new ChannelListView(arg as Link);
                    output.Title = "کانال";
                    break;
                case EntityTypes.Event:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new EventView((DateTime)arg);
                    if (infoType == ViewType.List)
                        output.ListView = new EventsListView((DateTime)arg);
                    output.Title = "Log Book";
                    break;
                case EntityTypes.Task:
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new TaskView((DateTime)arg);
                    if (infoType == ViewType.List)
                        output.ListView = new TasksListView((DateTime)arg);
                    output.Title = "ثبت تست";
                    break;
                case EntityTypes.User:
                    output.Title = "تعريف کاربر";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new UserView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<UserListView>.Instance;
                    break;
                case EntityTypes.EventType:
                    output.Title = "انواع عمليات";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new EventTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<EventTypesListView>.Instance;
                    break;
                case EntityTypes.TaskType:
                    output.Title = "انواع تست";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new TaskTypeView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<TaskTypesListView>.Instance;
                    break;
                case EntityTypes.FailureReason:
                    output.Title = "علل قطعی";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new FailureReasonView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<FailureReasonsListView>.Instance;
                    break;
                case EntityTypes.Alarm:
                    output.Title = "آلارم های مرکز";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new AlarmView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<AlarmsListView>.Instance;
                    break;
                case EntityTypes.UserShift:
                    output.Title = "شيفت ها";
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new ShiftView();
                    if (infoType == ViewType.List)
                        output.ListView = Singleton<ShiftsListView>.Instance;
                    break;
                case EntityTypes.DDFRoute:
                    if (infoType == ViewType.List)
                        output.ListView = new DDFRoutesListView(arg as DDF);
                    output.Title = "مسير ها";
                    break;
                case EntityTypes.AlarmType:
                    output.Title = "انواع آلارم";
                    if (infoType == ViewType.List)
                        output.ListView = new AlarmTypesListView();
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new AlarmTypeView();
                    break;
                case EntityTypes.ReportType:
                    output.Title = "انواع گزارش";
                    if (infoType == ViewType.List)
                        output.ListView = new ReportTypesListView();
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new ReportTypeView();
                    break;
                case EntityTypes.LongRecord:
                    output.Title = "رکورد طولانی";
                    if (infoType == ViewType.List)
                        output.ListView = new LongRecordsListView();
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new LongRecordView();
                    break;

                case EntityTypes.Role:
                    output.Title = "نقش جدید";
                    if (infoType == ViewType.List)
                        output.ListView = new LongRecordsListView();
                    if (infoType == ViewType.Detail)
                        output.DetailsView = new RoleView();
                    break;

                //case EntityTypes.Contact:
                //    output.Title = "لیست مخاطبین";
                //    if (infoType == ViewType.List)
                //        output.ListView = Singleton<ContactListView>.Instance;
                //    if (infoType == ViewType.Detail)
                //        output.DetailsView = new ContactView();
                //    break;

                default:
                    throw new NotSupportedException("The given entity is not declared as a basic info item.");
            }


            return output;
        }

    }
}
