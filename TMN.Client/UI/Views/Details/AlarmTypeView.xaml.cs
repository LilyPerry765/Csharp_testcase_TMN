﻿using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    public partial class AlarmTypeView : UserControl, IDetailsView
    {
        private bool isNew;

        public AlarmTypeView()
        {
            InitializeComponent();
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public void BeginEdit(Entity entity)
        {
             DataContext = DataSource.AlarmTypes.Where(p => p == (entity as AlarmType)).SingleOrDefault();
        }

        public void BeginInsert()
        {
            isNew = true;
            DataContext = new AlarmType()
            {
                ID = Guid.NewGuid(),
            };
        }

        public Entity SaveData()
        {
            if (isNew)
                DataSource.AlarmTypes.InsertOnSubmit(DataContext as AlarmType);
         

            // user log
			//UserLog.Log(db, ActionType.AlarmTypeInsert, string.Format("Name={0}", (DataContext as AlarmType).Name),
				//string.Format("ID={0} , Name={1}", (DataContext as AlarmType).ID, (DataContext as AlarmType).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return NameTextBox.IsAnswered("نوع آلارم");
            
        }

    }
}
