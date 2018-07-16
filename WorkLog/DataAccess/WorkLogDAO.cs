using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkLogApp.DataAccess
{
    public class WorkLogDAO : DAO
    {
        private static WorkLogDAO _instance;
        public static WorkLogDAO Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new WorkLogDAO();

                return _instance;
            }
        }

        public List<WorkLog> GetWorkLogs()
        {
            using (WorkLogDBEntities context = new WorkLogDBEntities())
            {
                return context.WorkLogs.ToList();
            }
        }

        public WorkLog GetLastWorkLog()
        {
            using (WorkLogDBEntities context = new WorkLogDBEntities())
            {
                return GetWorkLog(context, context.WorkLogs.Max(i => i.WorkLogID));
            }
        }

        public WorkLog GetWorkLog(int workLogID)
        { 
            using (WorkLogDBEntities context = new WorkLogDBEntities())
            {
                return GetWorkLog(context, workLogID);
            }
        }

        private WorkLog GetWorkLog(WorkLogDBEntities context, int workLogID)
        {
            WorkLog obj = null;
            
            obj = context.WorkLogs.Where(x => x.WorkLogID == workLogID).FirstOrDefault();

            if (obj == null)
                obj = new WorkLog() { CreatedOn = DateTime.Now };

            return obj;
        }

        public void Insert(WorkLog obj)
        {
            Operation(CrudOperations.Insert, obj);
        }

        public void SaveChanges(List<WorkLog> obj)
        {
            obj.ForEach(x => {
                if (x.WorkLogID > 0)
                    Update(x);
                else
                    Insert(x);
            });
        }

        public void Update(WorkLog obj)
        {
            Operation(CrudOperations.Update, obj);
        }

        public void Delete(WorkLog obj)
        {
            Operation(CrudOperations.Delete, obj);
        }

        public void Operation(CrudOperations op, WorkLog obj)
        {
            try
            {
                using (WorkLogDBEntities context = new WorkLogDBEntities())
                {
                    switch (op)
                    {
                        case CrudOperations.Insert:
                            obj.CreatedOn = DateTime.Now;
                            
                            Insert(context, obj);
                            break;
                        case CrudOperations.Update:
                            Update(context, obj,
                                "Notes");
                            break;
                        case CrudOperations.Delete:
                            SoftDelete(context, obj);
                            break;
                        case CrudOperations.HardDelete: //Permanent Delete
                            HardDelete(context, obj);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
