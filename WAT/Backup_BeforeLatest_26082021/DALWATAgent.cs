using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Prism.Model.WAT;
using Prism.Model;

namespace Prism.DAL
{
    public class DalwatAgent : BaseDataAccess
    {
        #region "Constructor"
        public DalwatAgent()
        {
            ObjModel = new WatModel();
        }
        #endregion

        #region "Specialised Functions"
        /// <summary>
        /// Gets Action Status Details of on Agent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>dataset</returns>
        public DataSet GetActionStatusDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails", true);
        }
        public DataSet GetActionStatusDetails_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails_Expedia", true);
        }
        public DataSet GetActionStatusDetails_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails_ALG", true);
        }
        
        public DataSet CheckDuplicateURN(string WorkIMID, string RefNo)
        {
            SqlParam.Clear();
            SqlParamAdd("@WorkIMID", DbType.String, WorkIMID);
            SqlParamAdd("@RefNo", DbType.String, RefNo);
            return datasetFromDB("Proc_CheckDuplicateURN", true);
        }

        /// <summary>
        /// This function updates the ActionStatus of an agent From Activity Tracker page
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 UpdateWATActionStatus(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@WorkGMID", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus", true);
        }
        public Int64 UpdateWATActionStatus_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@WorkGMID", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus_Expedia", true);
        }
        public Int64 UpdateWATActionStatus_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@WorkGMID", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus_ALG", true);
        }
        /// <summary>
        /// This function Ends and Action in WAT
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 EndWATActionStatus(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@CampaignID", DbType.Int32, obj.CampaignID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@CurrentStatusID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_EndActionStatus", true);
        }
        public Int64 EndWATActionStatus_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@CampaignID", DbType.Int32, obj.CampaignID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@CurrentStatusID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_EndActionStatus_Expedia", true);
        }
        public Int64 EndWATActionStatus_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@CampaignID", DbType.Int32, obj.CampaignID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@CurrentStatusID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_EndActionStatus_ALG", true);
        }
        /// <summary>
        /// This function gets the task details of an Agent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetAgentTaskDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentTaskDetails", true);
        }
        /// <summary>
        /// This function adds tasks for an Agent in the WAT
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 InsertAgentTasks(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@AgentWorkDID", DbType.String, obj.AgentWorkDID);
            SqlParamAdd("@ActivityDate", DbType.String, obj.ActivityDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@WorkIMID", DbType.String, obj.WorkIMID);
            SqlParamAdd("@CarryForwardWork", DbType.String, obj.CarryForwardWork);
            SqlParamAdd("@TodayWork", DbType.String, obj.WorkReceived);
            SqlParamAdd("@TransferredCarryForwardWork", DbType.String, obj.TransferredCarryForwardWork);
            SqlParamAdd("@TransferredTodayWork", DbType.String, obj.TransferredTodayWork);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            SqlParamAdd("@WorkDMID", DbType.String, obj.WorkDMID);
            SqlParamAdd("@CampaignID", DbType.String, obj.CampaignID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_InsertAgentTasks", true);
        }
        /// <summary>
        /// This function gets the Work Item Details for an agent in WAT to be used in Activity Tracker.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetWorkItem(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@WorkDMID", DbType.String, obj.WorkDMID);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
           
            return datasetFromDB("proc_WAT_GetWorkItem", true);
        }
        /// <summary>
        /// This function gets the Work Item Details for an agent in WAT to be used in Activity Tracker.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetWorkItem_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@WorkDMID", DbType.String, obj.WorkDMID);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            return datasetFromDB("proc_WAT_GetWorkItem_Expedia", true);
        }

        public DataSet GetEncryptionTypeStatus(string WorkGMID, string LoginMID)
        {
            SqlParam.Clear();
            SqlParamAdd("@WorkGMID", DbType.String, WorkGMID);
            SqlParamAdd("@LoginMID", DbType.String, LoginMID);
            return datasetFromDB("proc_AcitivtyTrackerALG_GetEncryptionStatus", true);
        }

        /// <summary>
        /// This function is user to Add/Update the agent Work Details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 ADDUpdateWATWorkDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@DataValue", DbType.Int32, obj.DataValue);
            SqlParamAdd("@RefNoStatus", DbType.Boolean, obj.RefNoStatus);
            SqlParamAdd("@RefNo", DbType.String, obj.RefNo);
            SqlParamAdd("@OutcomeMID", DbType.Int32, obj.OutcomeMID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@AgentWorkDID", DbType.Int64, obj.AgentWorkDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAdd("@StartDateTime", DbType.String, obj.StartDateTime);
            SqlParamAdd("@DocCount", DbType.Int32, obj.Doc_count == "" ? 0 : Convert.ToInt32(obj.Doc_count));
            SqlParamAdd("@OutcomeRemarks", DbType.String, obj.OutcomeRemarks);
            SqlParamAdd("@URNByPass", DbType.Boolean, obj.URNByPass);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_ADDUpdateWorkDetails", true);
        }
        public Int64 ADDUpdateWATWorkDetails_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@Queue", DbType.Int64, obj.QueueID);
            SqlParamAdd("@SubQueue", DbType.Int64, obj.SubQueueID);
            SqlParamAdd("@Valueofticket", DbType.Int64, obj.Ticket);
            SqlParamAdd("@TransactionNO", DbType.Int64, obj.TransactionNO);
            SqlParamAdd("@AirName", DbType.String, obj.AirName);
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@OutcomeMID", DbType.Int32, obj.OutcomeMID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@AgentWorkDID", DbType.Int64, obj.AgentWorkDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAdd("@StartDateTime", DbType.String, obj.StartDateTime);
            SqlParamAdd("@OutcomeRemarks", DbType.String, obj.OutcomeRemarks);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_ADDUpdateWorkDetails_Expedia", true);
        }
        public Int64 ADDUpdateWATWorkDetails_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAdd("@AgentWorkDID", DbType.Int64, obj.AgentWorkDID);
            SqlParamAdd("@ActiveWorkStatus", DbType.Int16, obj.ActiveWorkStatus);
            SqlParamAdd("@DynamicControls", DbType.String, obj.DynamicControls);
            SqlParamAdd("@StartDateTime", DbType.String, obj.ActionStartDateTime);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_ADDUpdateWorkDetails_ALG", true);
        }
        /// <summary>
        /// This is user to end a telephone call and save the outcome
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 CompleteTelephoneCall(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@OutcomeMID", DbType.Int32, obj.OutcomeMID);
            SqlParamAdd("@RefNoStatus", DbType.Boolean, obj.RefNoStatus);
            SqlParamAdd("@RefNo", DbType.String, obj.RefNo);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CompleteTelephoneCall", true);
        }
        public Int64 CompleteTelephoneCall_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@OutcomeMID", DbType.Int32, obj.OutcomeMID);
            SqlParamAdd("@RefNoStatus", DbType.Boolean, obj.RefNoStatus);
            SqlParamAdd("@RefNo", DbType.String, obj.RefNo);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CompleteTelephoneCall_Expedia", true);
        }
        public Int64 CompleteTelephoneCall_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@WorkGMID ", DbType.Int32, obj.WorkGMID);
            SqlParamAdd("@CampaignID ", DbType.Int64, obj.CampaignID);
            SqlParamAdd("@WorkDMID ", DbType.Int32, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.Int32, obj.WorkIMID);
            SqlParamAdd("@OutcomeMID", DbType.Int32, obj.OutcomeMID);
            SqlParamAdd("@RefNoStatus", DbType.Boolean, obj.RefNoStatus);
            SqlParamAdd("@RefNo", DbType.String, obj.RefNo);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.Int32, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CompleteTelephoneCall_ALG", true);
        }
        /// <summary>
        /// This is used to Change status of agent from Auto Wrap to Active
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 CancelAutoWrapForActiveWork(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CancelAutoWrapForActiveWork", true);
        }
        public Int64 CancelAutoWrapForActiveWork_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CancelAutoWrapForActiveWork_Expedia", true);
        }
        public Int64 CancelAutoWrapForActiveWork_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@ActiveWorkStatusDID", DbType.Int64, obj.ActiveWorkStatusDID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_CancelAutoWrapForActiveWork_ALG", true);
        }
        /// <summary>
        /// This is used to get the Agent's productivity data entry details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetDataEntryDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMIDs", DbType.String, obj.LoginMID);
            SqlParamAdd("@CampaignIDs", DbType.String, obj.CampaignID);
            SqlParamAdd("@WorkGMIDs", DbType.String, obj.WorkGMID);
            SqlParamAdd("@WorkDMIDs", DbType.String, obj.WorkDMID);
            SqlParamAdd("@WorkIMIDs", DbType.String, obj.WorkIMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@AccessType", DbType.String, obj.AccessType);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            return datasetFromDB("proc_WAT_GetDataEntryDetails", true);
        }

        public DataSet GetDataEntryDetails_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMIDs", DbType.String, obj.LoginMID);
            SqlParamAdd("@CampaignIDs", DbType.String, obj.CampaignID);
            SqlParamAdd("@WorkGMIDs", DbType.String, obj.WorkGMID);
            SqlParamAdd("@WorkDMIDs", DbType.String, obj.WorkDMID);
            SqlParamAdd("@WorkIMIDs", DbType.String, obj.WorkIMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@AccessType", DbType.String, obj.AccessType);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            return datasetFromDB("proc_WAT_GetDataEntryDetails_Expedia", true);
        }
        #region ALG
        /// <summary>
        /// Get Auditsheet dynamic controls 
        /// </summary>
        /// <param name="AuditSMID">Contains Audit Template unique id</param>       
        /// <param name="AType">AType=1 for Calibration, AType=2 for CrossAudit, AType=3 for Randomizer </param>      
        public DataSet GetDynamicControls(string WorkGMID)
        {
            SqlParam.Clear();
            SqlParamAdd("@WorkGMID", DbType.String, WorkGMID);
            return datasetFromDB("proc_WAT_GetDynamicControls", true);
        }
        #endregion
        public DataSet GetDataEntryDetails_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@CampaignID", DbType.String, obj.CampaignID);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            SqlParamAdd("@WorkDMID", DbType.String, obj.WorkDMID);
            SqlParamAdd("@WorkIMID", DbType.String, obj.WorkIMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@AccessType", DbType.String, obj.AccessType);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            return datasetFromDB("proc_WAT_GetDataEntryDetails_ALG", true);
        }
        /// <summary>
        /// This is used to get the agent's overall activity details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetAgentOverallActivityDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentActivityDetails", true);
        }
        public DataSet GetAgentOverallActivityDetails_Expedia(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentActivityDetails_Expedia", true);
        }
        public DataSet GetAgentOverallActivityDetails_ALG(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentActivityDetails_ALG", true);
        }
        /// <summary>
        /// This function adds tasks for an Agent in the WAT
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 InsertAgentTasksEmployeeWorkAssignment(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@AgentWorkDID", DbType.String, obj.AgentWorkDID);
            SqlParamAdd("@ActivityDate", DbType.String, obj.ActivityDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@WorkIMID", DbType.String, obj.WorkIMID);
            SqlParamAdd("@CarryForwardWork", DbType.String, obj.CarryForwardWork);
            SqlParamAdd("@TodayWork", DbType.String, obj.WorkReceived);
            SqlParamAdd("@TransferredCarryForwardWork", DbType.String, obj.TransferredCarryForwardWork);
            SqlParamAdd("@TransferredTodayWork", DbType.String, obj.TransferredTodayWork);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            SqlParamAdd("@WorkDMID", DbType.String, obj.WorkDMID);
            SqlParamAdd("@CampaignID", DbType.String, obj.CampaignID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@CreatedBy", DbType.String, obj.Createdby);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_InsertAgentTasks", true);
        }
        /// <summary>
        /// Gets Action Status Details of on Agent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>dataset</returns>
        public DataSet GetActionStatusDetailsHilton(WatModelHilton obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails_Hilton", true);
        }
        /// <summary>
        /// Gets Action Status Details of on Agent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>dataset</returns>
        public DataSet GetActionStatusDetailsHRCC(WatModelHrcc obj)
        {
            SqlParam.Clear();
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails_Hilton", true);
        }
        /// <summary>
        /// This function updates the ActionStatus of an agent From Activity Tracker page
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 UpdateWATActionStatusHilton(WatModelHilton obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@DeskMID", DbType.Int32, obj.DeskMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus_Hilton", true);
        }
        /// <summary>
        /// This function Ends and Action in WAT
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 ADDUpdateWATBookingDetailsHilton(WatModelHilton obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@DeskMID", DbType.Int64, obj.DeskMID);
            SqlParamAdd("@HotelCode", DbType.String, obj.HotelCode);
            SqlParamAdd("@PaymentType", DbType.String, obj.PaymentType);
            SqlParamAdd("@Rate", DbType.String, obj.Rate);
            SqlParamAdd("@Points", DbType.String, obj.Points);
            SqlParamAdd("@CheckIn", DbType.String, obj.CheckInDate);
            SqlParamAdd("@CheckOut", DbType.String, obj.CheckOutDate);
            SqlParamAdd("@RequestedRooms", DbType.String, obj.RequestedRooms);
            SqlParamAdd("@AdultCount", DbType.String, obj.AdultCount);
            SqlParamAdd("@ChildCount", DbType.String, obj.ChildCount);
            SqlParamAdd("@BookedRooms", DbType.String, obj.BookedRooms);
            SqlParamAdd("@OutcomeMID", DbType.String, obj.OutcomeMID);
            SqlParamAdd("@ReasonMID", DbType.String, obj.ReasonMID);
            SqlParamAdd("@AltHotelCode", DbType.String, obj.AltHotelCode);
            SqlParamAdd("@AltPaymentType", DbType.String, obj.AltPaymentType);
            SqlParamAdd("@AltRate", DbType.String, obj.AltRate);
            SqlParamAdd("@AltPoints", DbType.String, obj.AltPoints);
            SqlParamAdd("@AltOutcomeMID", DbType.String, obj.AltOutcomeMID);
            SqlParamAdd("@AltReasonMID", DbType.String, obj.AltReasonMID);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActionSMID", DbType.String, obj.ActionSMID);
            SqlParamAdd("@IsSalesCall", DbType.String, obj.IsSalesCall);
            SqlParamAdd("@CallTypeMID", DbType.String, obj.CallTypeMID);
            SqlParamAdd("@PurposeMID", DbType.String, obj.PurposeMID);
            SqlParamAdd("@UpSell", DbType.String, obj.UpSell);
            SqlParamAdd("@UpsellReason", DbType.String, obj.UpsellReason);
            SqlParamAdd("@NotUpsellReason", DbType.String, obj.NotUpsellReason);
            SqlParamAdd("@RescueMID", DbType.String, obj.RescueMID);
            SqlParamAdd("@CrossSell", DbType.String, obj.CrossSell);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_ADDUpdateBookingDetails_Hilton", true);
        }
        /// <summary>
        /// This is used to get the Agent's productivity data entry details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetDataEntryDetailsHilton(WatModelHilton obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMIDs", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@AccessType", DbType.String, obj.AccessType);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            return datasetFromDB("proc_WAT_GetDataEntryDetails_Hilton", true);
        }
        /// <summary>
        /// This is used to get the agent's overall activity details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet GetAgentOverallActivityDetailsHilton(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentActivityDetails_Hilton", true);
        }
        #endregion



        #region WAT_HRCC
        /// <summary>
        /// This function updates the ActionStatus of an agent From Activity Tracker page
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 UpdateWATActionStatusHRCC(WatModelHrcc obj)
        {
            SqlParam.Clear();
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@DeskMID", DbType.Int32, obj.DeskMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus_Hilton", true);
        }

        /// <summary>
        /// This function Ends and Action in WAT
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 ADDUpdateWATBookingDetailsHRCC(WatModelHrcc obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@DeskMID", DbType.Int64, obj.DeskMID);
            SqlParamAdd("@IsSalesCall", DbType.String, obj.IsSalesCall);
            SqlParamAdd("@CallTypeMID", DbType.String, obj.CallTypeMID);
            SqlParamAdd("@HotelCode", DbType.String, obj.HotelCode);
            SqlParamAdd("@GuestCount", DbType.String, obj.GuestCount);
            SqlParamAdd("@CheckIn", DbType.String, obj.CheckInDate);
            SqlParamAdd("@CheckOut", DbType.String, obj.CheckOutDate);
            SqlParamAdd("@Purpose", DbType.String, obj.Purpose);
            SqlParamAdd("@BedTypeMID", DbType.String, obj.BedTypeMID);
            SqlParamAdd("@UpSell", DbType.String, obj.UpSell);
            SqlParamAdd("@BookedRooms", DbType.String, obj.BookedRooms);
            SqlParamAdd("@Bookstatus", DbType.String, obj.Bookstatus);
            SqlParamAdd("@RescueMID", DbType.String, obj.RescueMID);
            SqlParamAdd("@CrossSell", DbType.String, obj.CrossSell);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActionSMID", DbType.String, obj.ActionSMID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_ADDUpdateBookingDetails_HRCC", true);
        }

        #endregion

        #region wat_Dyanmic Dropdwon
        public DataSet BindDynamicDependentDropDown(string ParentDocMID,string LoginMID)
        {
            SqlParam.Clear();
            SqlParamAdd("@ParentDocMID", DbType.String, ParentDocMID);
            SqlParamAdd("@LoginMID", DbType.String, LoginMID);
            return datasetFromDB("proc_AcitivityTracker_GetDynamicDropDownData", true);
        }

        #endregion

        public DataSet GetActionDynamicStatusDetails(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@ClientMID", DbType.String, obj.ClientID);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@Host", DbType.String, obj.Host);
            return datasetFromDB("proc_WAT_GetActionDynamicStatusDetails", true);
        }

        #region TPRecommend
        public DataSet GetActionStatusDetailsTPRecommender(WatModelTPRecommender obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetActionStatusDetails_TPRecommender", true);
        }

        public Int64 ADDUpdateWATCrossSellDetailsTPRecommender(WatModelTPRecommender obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@DataDID", DbType.Int64, obj.DataDID);
            SqlParamAdd("@DeskMID", DbType.Int64, obj.DeskMID);
            SqlParamAdd("@ProductID", DbType.Int64,Convert.ToString(obj.CustomerChoice));
            SqlParamAdd("@Device", DbType.String, obj.MostDevice);
            SqlParamAdd("@Plan", DbType.String, obj.MostPlan);
            SqlParamAdd("@MPlan", DbType.Boolean, obj.MPlan);
            SqlParamAdd("@MDevice", DbType.Boolean, obj.MDevice);
            SqlParamAdd("@ChatID", DbType.String, obj.ChartIDs);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@ActionSMID", DbType.String, obj.ActionSMID);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_AddCrossSellDetail_TPRecommender", true);            

        }

        public Int64 UpdateWATActionStatusTPRecommender(WatModelTPRecommender obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StatusDID", DbType.Int64, obj.StatusDID);
            SqlParamAdd("@ActionSMID", DbType.Int16, obj.ActionSMID);
            SqlParamAdd("@DeskMID", DbType.Int32, obj.DeskMID);
            SqlParamAdd("@LoginMID", DbType.Int64, obj.LoginMID);
            SqlParamAdd("@HostName", DbType.String, obj.Host);
            SqlParamAdd("@WorkGMID", DbType.String, obj.WorkGMID);
            SqlParamAddOut("@ErrorNumber", DbType.Int64, 0, ParameterDirection.Output);
            return ExecuteDBScalarCommand("proc_WAT_UpdateActionStatus_TPRecommender", true);
        }
        public DataSet GetDataEntryDetailsTPRecommender(WatModelTPRecommender obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMIDs", DbType.String, obj.LoginMID);
            SqlParamAdd("@GlobalUserID", DbType.String, obj.GlobalUserID);
            SqlParamAdd("@AccessType", DbType.String, obj.AccessType);
            SqlParamAdd("@StatusDID", DbType.String, obj.StatusDID);
            return datasetFromDB("proc_WAT_GetDataEntryDetails_TPRecommender", true);

        }        
        public DataSet GetAgentOverallActivityDetailsTPRecommender(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;
            SqlParamAdd("@StartDate", DbType.String, obj.StartDate);
            SqlParamAdd("@EndDate", DbType.String, obj.EndDate);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_GetAgentActivityDetails_TPRecommender", true);
        }
        public DataSet GetAgentTPRecommenderRelatedProduct(int projectid,string LoginMID, string Host)
        {
            SqlParam.Clear();
            
            SqlParamAdd("@ProductID", DbType.Int32, projectid);
            SqlParamAdd("@HostName", DbType.String, Host);
            SqlParamAdd("@LoginMID", DbType.String, LoginMID);
            return datasetFromDB("proc_TPRecommender_RelatedProduct", true);
        }

        public DataSet GetSelfAssignment(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj; 
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);       
            return datasetFromDB("proc_WAT_GetAssignment", true);
        }

        public DataSet WAT_RecommenderWorkGorup(string LoginMID, string AccessType)
        {
            SqlParam.Clear();
            SqlParamAdd("@LoginMID", DbType.Int32, LoginMID); 
            SqlParamAdd("@AccessType", DbType.String, AccessType);
            return datasetFromDB("proc_WAT_RecommenderWorkGorup", true);
        }

        #endregion

        #region Activity Tracker EasyJet
        public DataSet WatFetchCaseFromDataMasterFIFO(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;

            SqlParamAdd("@WorkGMID", DbType.String, obj.hidWorkGroupMID);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_FetchCaseFromDataMasterFIFO", true);
        }

        public DataSet WatFetchCaseSectorFromFormatedData(WatModel obj)
        {
            SqlParam.Clear();
            ObjModel = obj;

            SqlParamAdd("@Date", DbType.String, obj.PNRDate);
            SqlParamAdd("@PNR", DbType.String, obj.PNRNumber);
            SqlParamAdd("@UMID", DbType.String, obj.UMIDNumber);
            SqlParamAdd("@LoginMID", DbType.String, obj.LoginMID);
            return datasetFromDB("proc_WAT_FetchCaseSectorFromFormatedData", true);
        }

        #endregion


    }
}