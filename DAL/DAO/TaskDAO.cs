using DAL.DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static void AddTask(TASK task)
        {
            try
            {
                db.TASKs.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                TASK tsk = db.TASKs.First(x => x.ID == taskID);
                if (isAdmin)
                {
                    tsk.TaskState = TaskStates.Approved;
                }
                else
                {
                    tsk.TaskState = TaskStates.Delivered;
                }
                tsk.TaskDeliveryState = DateTime.Today;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                TASK task = db.TASKs.First(x => x.ID == taskID);
                db.TASKs.DeleteOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> tasklist = new List<TaskDetailDTO>();

            var list = (from t in db.TASKs
                        join s in db.TASKSTATEs on t.TaskState equals s.ID
                        join e in db.EMPLOYEEs on t.EmployeeID equals e.ID
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryState,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmployeeID = t.EmployeeID,
                            departmentName = d.DepartmentName,
                            positionName = p.PositionName,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID,
                            
                        }).OrderBy(x => x.startDate).ToList();

            foreach ( var item in list )
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.DepartmentName = item.departmentName;
                dto.PositionName = item.positionName;
                dto.DepartmentID = item.departmentID;
                dto.EmployeeID = item.EmployeeID;
                dto.PositionID = item.positionID;
                tasklist.Add(dto);
            }
            return tasklist;
        }

        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATEs.ToList();
        }

        public static void UpdateTask(TASK update)
        {
            try
            {
                TASK ts = db.TASKs.First(x => x.ID == update.ID);
                ts.TaskTitle = update.TaskTitle;
                ts.TaskContent = update.TaskContent;
                ts.TaskState = update.TaskState;
                ts.EmployeeID = update.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
