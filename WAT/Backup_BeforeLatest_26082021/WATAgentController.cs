using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.Transactions;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using Prism.Model.WAT;
using Prism.DAL;
using Prism.Utility;
using Prism.Filters;
using InfoSoftGlobal;
using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml.Linq;
namespace Prism.Controllers
{
    [AllowAnonymous]
    public partial class WatController : Controller
    {
        /// <summary>
        /// Action : Open Work Detail Entry popup screen, Mehod : Get
        /// </summary>
        [HttpGet]
        public ActionResult WorkDetailEntry_Expedia(string id)
        {
            try
            {
                WatModel objBO = new WatModel();
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                string ControlsQueryStringWork = TempData["QueryString"].ToString();
                objBO.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryStringWork);
                objBO.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryStringWork);
                objBO.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryStringWork);
                objBO.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryStringWork);
                objBO.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryStringWork);
                ViewBag.AppName = objBO.AppName;
                objBO.URL = ControlsQueryStringWork;
                ViewBag.Message = "";
                return View(objBO);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "WorkDetailEntry_Expedia", ex.ToString());
                return View();
            }
        }
        public ActionResult WorkDetailEntry(string id)
        {
            try
            {
                WatModel objBO = new WatModel();
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                string ControlsQueryStringEntry = TempData["QueryString"].ToString();
                objBO.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryStringEntry);
                objBO.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryStringEntry);
                objBO.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryStringEntry);
                objBO.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryStringEntry);
                objBO.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryStringEntry);
                ViewBag.AppName = objBO.AppName;
                objBO.URL = ControlsQueryStringEntry;
                ViewBag.Message = "";
                return View(objBO);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "WorkDetailEntry", ex.ToString());
                return View();
            }
        }
        /// <summary>
        /// Get Agent Activity Details based on the LoginMID
        /// </summary>
        /// <param name="AuditSMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentActivityDetails(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWM = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWM.StartDate = FormatDate(StartDate);
            objWM.EndDate = FormatDate(EndDate);
            objWM.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentTaskDetails(objWM);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action to Download the Agent Activity Details
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentActivityDetails(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentTaskDetails(objWAT);
            ds.Tables[0].Columns.Remove("CampaignID");
            ds.Tables[0].Columns.Remove("WorkGMID");
            ds.Tables[0].Columns.Remove("WorkDMID");
            ds.Tables[0].Columns.Remove("WorkIMID");
            ds.Tables[0].Columns.Remove("AgentWorkDID");
            ds.Tables[0].Columns.Remove("TotalWork");
            ds.Tables[0].Columns["CarryForwardWork"].ColumnName = "WorkCarriedForward";
            ds.Tables[0].Columns["TodayWork"].ColumnName = "WorkReceived";
            ds.Tables[0].Columns["CompletedWork"].ColumnName = "WorkCompleted";
            ds.Tables[0].AcceptChanges();
            try
            {
                ViewBag.Message = "";
                ExcelDownload(ds.Tables[0], "Agent Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View(objWAT);
            }
            return View(objWAT);
        }
        /// <summary>
        /// Updates the Agent Tasks Details
        /// </summary>
        /// <param name="LoginMID">LoginMID of the agent</param>
        /// <param name="GlobalUserID">GlobalUserID of the agent</param>
        /// /// <param name="WorkGMID">WorkGMID of the task</param>
        /// /// <param name="WorkDMID">WorkDMID of the task</param>
        /// /// <param name="WorkIMID">WorkIMID of the task</param>
        /// /// <param name="WorkReceived">Work Received Count</param>
        /// /// <param name="Host">HostName</param>
        /// <returns></returns>
        public JsonResult InsertAgentTasks(string LoginMID, string GlobalUserID, string WorkGMID, string WorkDMID, string WorkIMID, string WorkReceived, string Host)
        {
            Int64 Result = 0;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.LoginMID = LoginMID;
            objWAT.GlobalUserID = GlobalUserID;
            string WorkGroup = EncodeDecode.Decode(WorkGMID);
            objWAT.WorkGMID = WorkGroup.Split('~').First();
            objWAT.CampaignID = WorkGroup.Split('~').Last();
            objWAT.WorkDMID = EncodeDecode.Decode(WorkDMID);
            objWAT.WorkIMID = EncodeDecode.Decode(WorkIMID);
            objWAT.WorkReceived = WorkReceived;
            objWAT.ActivityDate = DateTime.Now.ToString("yyyy-MM-dd");
            objWAT.StatusDID = "1";
            objWAT.Host = Host;
            Result = objDalWAT.InsertAgentTasks(objWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action : Open Historic Work Detail screen, Mehod : Get
        /// </summary>
        [HttpGet]
        public ActionResult HistoricWorkDetail(string id)
        {
            WatModel objWAT = new WatModel();
            try
            {
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                string ControlsQueryStringHist = TempData["QueryString"].ToString();
                objWAT.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryStringHist);
                objWAT.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryStringHist);
                objWAT.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryStringHist);
                objWAT.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryStringHist);
                objWAT.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryStringHist);
                ViewBag.AppName = objWAT.AppName;
                objWAT.URL = ControlsQueryStringHist;
                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.Message = "";
                return View(objWAT);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "HistoricWorkDetail", ex.ToString());
                return View(objWAT);
            }
        }

        /// <summary>
        /// Action: Opens view ActivityTracker, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTracker(string id, string id1)
        {
            WatModel model = new WatModel();
            DalwatAgent da = new DalwatAgent();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                model.hdnDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.ErrorMessage = "";
                if (TempData["WatModel"] != null)
                {
                    model = (WatModel)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                    Session["DefaultClientID"] = Prism.Utility.Querystring.QueryStrData("DefaultClientID", ControlsQueryString);
                }
                ds = da.FillDropDown("WAT_WorkGroups", Session["LoginMID"].ToString(), Session["AccessType"].ToString(), Session["GlobalUserID"].ToString());
                List<Prism.Model.WAT.ListData> objWGData = GetListData(ds.Tables[0], model, "Work Group", "WorkGroupName", "WorkGMID");
                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.ClientID = Session["DefaultClientID"].ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.ListData = objWGData;
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&DefaultClientID=" + model.ClientID + "&UniqueID=" + model.UniqueID);
                ds = da.GetActionStatusDetails(model);

                model.ClientID_Empower = ds.Tables[1].Rows[0]["ClientID"].ToString();
                model.ProjectID = ds.Tables[1].Rows[0]["ProjectID"].ToString();
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    model.StatusDID = ds.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = ds.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = ds.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = ds.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = ds.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.WorkGMID = ds.Tables[1].Rows[0]["WorkGMID"].ToString();
                    model.CampaignID = ds.Tables[1].Rows[0]["CampaignID"].ToString();
                    model.WorkDMID = ds.Tables[1].Rows[0]["WorkDMID"].ToString();
                    model.WorkIMID = ds.Tables[1].Rows[0]["WorkIMID"].ToString();
                    model.CampaignName = ds.Tables[1].Rows[0]["CampaignName"].ToString();
                    model.WorkGroupName = ds.Tables[1].Rows[0]["WorkGroupName"].ToString();
                    model.WorkDivisionName = ds.Tables[1].Rows[0]["WorkDivisionName"].ToString();
                    model.WorkItemName = ds.Tables[1].Rows[0]["WorkItemName"].ToString();
                    model.WorkCompleted = ds.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    model.AgentWorkDID = ds.Tables[1].Rows[0]["AgentWorkDID"].ToString();
                    model.ActiveWorkStatusDID = ds.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = ds.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.TotalWorkCount = ds.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "7")
                    {
                        ds = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                        List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                        model.ListOutcomeData = objOutcomeData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;

                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }

        /// <summary>
        /// Action: Opens view ActivityTracker_Expedia, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTracker_Expedia(string id, string id1)
        {
            WatModel model = new WatModel();
            DalwatAgent da = new DalwatAgent();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                model.hdnDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.ErrorMessage = "";
                if (TempData["WatModel"] != null)
                {
                    model = (WatModel)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                }
                DataSet dsworkGroup = da.FillDropDown("WAT_WorkGroups", Session["LoginMID"].ToString(), Session["AccessType"].ToString(), Session["GlobalUserID"].ToString());
                List<Prism.Model.WAT.ListData> objWGData = GetListData(dsworkGroup.Tables[0], model, "Work Group", "WorkGroupName", "WorkGMID");
                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.ClientID = Session["DefaultClientID"].ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.ListData = objWGData;
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&DefaultClientID=" + model.ClientID + "&UniqueID=" + model.UniqueID);
                ds = da.GetActionStatusDetails_Expedia(model);
                model.ClientID_Empower = ds.Tables[1].Rows[0]["ClientID"].ToString();
                model.ProjectID = ds.Tables[1].Rows[0]["ProjectID"].ToString();
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    model.StatusDID = ds.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = ds.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = ds.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = ds.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = ds.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.WorkGMID = ds.Tables[1].Rows[0]["WorkGMID"].ToString();
                    model.CampaignID = ds.Tables[1].Rows[0]["CampaignID"].ToString();
                    model.WorkDMID = ds.Tables[1].Rows[0]["WorkDMID"].ToString();
                    model.WorkIMID = ds.Tables[1].Rows[0]["WorkIMID"].ToString();
                    model.CampaignName = ds.Tables[1].Rows[0]["CampaignName"].ToString();
                    model.WorkGroupName = ds.Tables[1].Rows[0]["WorkGroupName"].ToString();
                    model.WorkDivisionName = ds.Tables[1].Rows[0]["WorkDivisionName"].ToString();
                    model.WorkItemName = ds.Tables[1].Rows[0]["WorkItemName"].ToString();
                    model.WorkCompleted = ds.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    model.AgentWorkDID = ds.Tables[1].Rows[0]["AgentWorkDID"].ToString();
                    model.ActiveWorkStatusDID = ds.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = ds.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.TotalWorkCount = ds.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "7")
                    {
                        DataSet dsOutcome = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                        List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(dsOutcome.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                        model.ListOutcomeData = objOutcomeData;
                        DataSet dsdrop = da.FillDropDown("WATDropDownFill", "1", model.WorkDMID, model.WorkIMID);//hard code Test
                        List<Prism.Model.WAT.ListData> ListQueueData = GetListData(dsdrop.Tables[0], model, "Select", "Name", "ID");
                        model.ListQueueData = ListQueueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;

                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_Expedia", ex.ToString());
                return View(model);
            }
        }
        /// <summary>
        /// Check the duplicate URN
        /// </summary>
        /// <param name="WorkDMID"></param>
        /// <param name="RefNo"></param>
        /// <returns></returns>
        public string DuplicateURN(string workIMID, string RefNo)
        {
            string Result = "0";
            if (RefNo != null)
            {
                DalwatAgent da = new DalwatAgent();
                DataSet ds_Res = da.CheckDuplicateURN(workIMID, RefNo);
                Result = ds_Res.Tables[0].Rows[0]["ErrorNumber"].ToString();
            }
            return Result;
        }



        /// <summary>
        /// Gets lists for dropdownlist
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Prism.Model.WAT.ListData> GetListData(DataTable dt1, Prism.Model.WAT.WatModel model, string DefaultText, string TextField, string ValueField)
        {
            List<Prism.Model.WAT.ListData> obj = new List<Prism.Model.WAT.ListData>();
            if (dt1.Rows.Count > 0)
            {
                Prism.Model.WAT.ListData objDefault = new Prism.Model.WAT.ListData();
                objDefault.Text = DefaultText;
                objDefault.Value = "";
                obj.Add(objDefault);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Prism.Model.WAT.ListData obj1 = new Prism.Model.WAT.ListData();
                    obj1.Text = dt1.Rows[i][TextField].ToString();
                    obj1.Value = EncodeDecode.Encode(dt1.Rows[i][ValueField].ToString());
                    obj.Add(obj1);
                }
            }
            return obj;
        }

        /// <summary>
        /// Action to save ActivityTrackerDetails, Method : Post
        /// </summary>
        /// <param name="model">object of type WatModel</param>
        /// <param name="form">object of type FormCollection</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ActivityTracker(WatModel model, FormCollection form)
        {
            CultureInfo info = new CultureInfo("en-GB");
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            int ValidURN = 1;
            try
            {

                if (model.ClientID_Empower == "165")
                {
                    string workIMID = model.WorkIMID;//model.WorkDMID; changed 1st it was on WorkDMID
                    if (model.ActionSMID == "22" && EncodeDecode.IsBase64(workIMID))
                    {
                        workIMID = EncodeDecode.Decode(workIMID);
                    }
                    if (!model.URNByPass && DuplicateURN(workIMID, model.RefNo) == "1")
                    {
                        ValidURN = 0;
                    }
                }

                if (ValidURN == 1)
                {
                    if (model.WorkDMID == "28")
                    {
                        if (model.StartDateTime != "")
                        {
                            DateTime date_t = Convert.ToDateTime(model.StartDateTime, info);
                            string datetime = date_t.ToString("yyyy-MM-dd") + " " + model.Hour + ":" + model.minute;
                            model.StartDateTime = datetime;
                        }
                    }
                    else
                    {
                        model.StartDateTime = "";
                    }
                    Session["LoginMID"] = model.LoginMID;
                    Session["GlobalUserID"] = model.GlobalUserID;
                    Session["DefaultClientID"] = model.ClientID;
                    Session["AccessType"] = model.AccessType;
                    Session["UniqueID"] = model.UniqueID;
                    Session["Host"] = model.Host;
                    if (model.ActionSMID == "0")
                    {
                        model.ActionSMID = model.CurrentStatusID;
                        result = da.EndWATActionStatus(model);
                        if (result == 0)
                        {
                            TempData["Message"] = "An error occurred while processing the request. Please try again later";
                            ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                            return View(model);
                        }
                        else
                        {
                            return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                        }
                    }
                    else if (model.ActionSMID == "16")
                    {
                        DalLoginMaster dLoginMaser = new DalLoginMaster();
                        dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                    }
                    else if (model.ActionSMID == "2")
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                            model.WorkGMID = WorkItem[2];
                            model.CampaignID = WorkItem[3];
                            model.WorkDMID = WorkItem[1];
                            model.WorkIMID = WorkItem[0];
                            model.AgentWorkDID = WorkItem[4];
                            result = da.UpdateWATActionStatus(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                            }
                        }
                        else
                        {
                            if (model.Type == "Cancel")
                            {
                                result = da.CancelAutoWrapForActiveWork(model);
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                                }
                            }
                            else if (Convert.ToInt32(model.DataValue) <= Convert.ToInt32(model.TotalWorkCount))
                            {
                                model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                                model.DataValue = "1";
                                result = da.ADDUpdateWATWorkDetails(model);
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else if (result == 3)
                                {
                                    TempData["Message"] = "Duplicate URN exists";
                                    ViewBag.ErrorMessage = "Duplicate URN exists";
                                    TempData["WatModel"] = model;
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                                }
                            }
                            else
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                        }
                    }
                    else if (model.ActionSMID == "22")
                    {
                        if (model.CurrentStatusID != "23")
                        {
                            model.ActionSMID = model.CurrentStatusID;
                            model.ActiveWorkStatus = "22";
                            result = da.EndWATActionStatus(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                            }
                        }
                        else
                        {
                            string WorkGroup = EncodeDecode.Decode(model.WorkGMID);
                            model.WorkGMID = WorkGroup.Split('~').First();
                            model.CampaignID = WorkGroup.Split('~').Last();
                            model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                            model.WorkIMID = "0";
                            model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                            result = da.CompleteTelephoneCall(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                            }
                        }
                    }
                    else
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            result = da.UpdateWATActionStatus(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                            }
                        }
                        else
                        {
                            if (model.CurrentStatusID == "2" && model.ActionSMID != "22")
                            {
                                model.ActiveWorkStatus = "2";
                            }
                            result = da.EndWATActionStatus(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                            }
                        }
                    }
                }
                else
                {
                    ds = new DataSet();
                    ds = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                    List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                    model.ListOutcomeData = objOutcomeData;
                    TempData["Message"] = "Duplicate URN exists";
                    ViewBag.ErrorMessage = "Duplicate URN exists";
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker/" + model.URL);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ActivityTracker_Expedia(WatModel model, FormCollection form)
        {
            CultureInfo info = new CultureInfo("en-GB");
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            int ValidURN = 1;
            try
            {
                ///will be used in future
                ////if (model.ClientID_Empower == "165")//
                //{//
                //    string workIMID = model.WorkIMID;//model.WorkDMID; changed 1st it was on WorkDMID
                ////   if (model.ActionSMID == "22" && EncodeDecode.IsBase64(workIMID))   //
                //    { //
                //        model.WorkDMID = EncodeDecode.Decode(workIMID);//
                //    }  //
                //}//

                if (ValidURN == 1)
                {
                    if (model.WorkDMID == "28")
                    {
                        if (model.StartDateTime != "")
                        {
                            DateTime date_t = Convert.ToDateTime(model.StartDateTime, info);
                            string datetime = date_t.ToString("yyyy-MM-dd") + " " + model.Hour + ":" + model.minute;
                            model.StartDateTime = datetime;
                        }
                    }
                    else
                    {
                        model.StartDateTime = "";
                    }
                    Session["LoginMID"] = model.LoginMID;
                    Session["GlobalUserID"] = model.GlobalUserID;
                    Session["DefaultClientID"] = model.ClientID;
                    Session["AccessType"] = model.AccessType;
                    Session["UniqueID"] = model.UniqueID;
                    Session["Host"] = model.Host;
                    if (model.ActionSMID == "0")
                    {
                        model.ActionSMID = model.CurrentStatusID;
                        result = da.EndWATActionStatus_Expedia(model);
                        if (result == 0)
                        {
                            TempData["Message"] = "An error occurred while processing the request. Please try again later";
                            ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                            return View(model);
                        }
                        else
                        {
                            return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                        }
                    }
                    else if (model.ActionSMID == "16")
                    {
                        DalLoginMaster dLoginMaser = new DalLoginMaster();
                        dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                    }
                    else if (model.ActionSMID == "2")
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                            model.WorkGMID = WorkItem[2];
                            model.CampaignID = WorkItem[3];
                            model.WorkDMID = WorkItem[1];
                            model.WorkIMID = WorkItem[0];
                            result = da.UpdateWATActionStatus_Expedia(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                            }
                        }
                        else
                        {
                            if (model.Type == "Cancel")
                            {
                                result = da.CancelAutoWrapForActiveWork_Expedia(model);//lrft
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                                }
                            }
                            else if (Convert.ToInt32(model.DataValue) <= Convert.ToInt32(model.TotalWorkCount))
                            {
                                model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                                model.QueueID = EncodeDecode.Decode(model.QueueID);
                                model.SubQueueID = EncodeDecode.Decode(model.SubQueueID);
                                model.DataValue = "1";
                                DataSet DsEncryption = da.GetEncryptionTypeStatus(model.WorkGMID, Session["LoginMID"].ToString());
                                if (DsEncryption != null && DsEncryption.Tables[0].Rows.Count > 0 && Convert.ToBoolean(DsEncryption.Tables[0].Rows[0]["Status"].ToString()))
                                {

                                    model.Ticket = AesEncryption.Encode(model.Ticket);
                                    model.TransactionNO = AesEncryption.Encode(model.TransactionNO);
                                    model.AirName = AesEncryption.Encode(model.AirName);
                                    model.OutcomeRemarks = AesEncryption.Encode(model.OutcomeRemarks);

                                }

                                result = da.ADDUpdateWATWorkDetails_Expedia(model);
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else if (result == 3)
                                {
                                    TempData["Message"] = "Duplicate URN exists";
                                    ViewBag.ErrorMessage = "Duplicate URN exists";
                                    TempData["WatModel"] = model;
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                                }
                            }
                            else
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                        }
                    }
                    else if (model.ActionSMID == "22")
                    {
                        if (model.CurrentStatusID != "23")
                        {
                            model.ActionSMID = model.CurrentStatusID;
                            model.ActiveWorkStatus = "22";
                            result = da.EndWATActionStatus_Expedia(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                            }
                        }
                        else
                        {
                            string WorkGroup = EncodeDecode.Decode(model.WorkGMID);
                            model.WorkGMID = WorkGroup.Split('~').First();
                            model.CampaignID = WorkGroup.Split('~').Last();
                            model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                            model.WorkIMID = "0";
                            model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                            result = da.CompleteTelephoneCall_Expedia(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                            }
                        }
                    }
                    else
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            result = da.UpdateWATActionStatus_Expedia(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                            }
                        }

                        else
                        {
                            if (model.CurrentStatusID == "2" && model.ActionSMID != "22")
                            {
                                model.ActiveWorkStatus = "2";
                            }
                            result = da.EndWATActionStatus_Expedia(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                            }
                        }
                    }
                }
                else
                {
                    ds = new DataSet();
                    ds = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                    List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                    model.ListOutcomeData = objOutcomeData;
                    TempData["Message"] = "Duplicate URN exists";
                    ViewBag.ErrorMessage = "Duplicate URN exists";
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Expedia/" + model.URL);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_Expedia", ex.ToString());
                return View(model);
            }
        }

        public JsonResult FetchSubQueue(string ParentID)
        {
            string Result = string.Empty;
            ParentID = EncodeDecode.Decode(ParentID);
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WATDropDownFill", "2", ParentID);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["ID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["Name"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used to Fetch WorkGroup dropdown
        /// </summary>  
        [HttpPost]
        [AllowAnonymous]
        public JsonResult FetchWorkGroups(string LoginMID, string AccessType, string GlobalUserID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_WorkGroups", LoginMID, AccessType, GlobalUserID);
            TempData["workgroup"] = ds;
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkGMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["WorkGroupName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used to Fetch WorkGroup dropdown ALG
        /// </summary>
        public JsonResult FetchWorkGroupnew(string LoginMID, string AccessType, string GlobalUserID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_WorkGroups", LoginMID, AccessType, GlobalUserID);
            TempData["workgroup"] = ds;
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkGMID"].ToString().Split('~')[0]);
                dr["Name"] = ds.Tables[0].Rows[i]["WorkGroupName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used to Fetch WorkDivisions dropdown based on WorkGroup ALG
        /// </summary>  
        public JsonResult FetchWorkDivisionsNew(string LoginMID, string AccessType, string WorkGMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            string WorkGMIDs = EncodeDecode.Decode(WorkGMID);
            ds = da.FillDropDown("WAT_WorkDivisions", WorkGMIDs, LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkDMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["WorkDivisionName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to Fetch WorkGroup dropdown
        /// </summary>  
        public JsonResult FetchAgentWorkGroups(string LoginMID, string AccessType)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_AgentWorkGroups", LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkGMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["WorkGroupName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Used to Fetch WorkDivisions dropdown based on WorkGroup
        /// </summary>  
        public JsonResult FetchWorkDivisions(string LoginMID, string AccessType, string WorkGMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            string WorkGMIDs = EncodeDecode.Decode(WorkGMID).Split('~')[0];
            ds = da.FillDropDown("WAT_WorkDivisions", WorkGMIDs, LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkDMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["WorkDivisionName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to Fetch WorkItems dropdown based on WorkDivisions
        /// </summary>  
        public JsonResult FetchWorkItems(string LoginMID, string AccessType, string WorkDMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_WorkItems", EncodeDecode.Decode(WorkDMID), LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkIMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["WorkItemName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to Fetch Call Outcomes dropdown based on WorkDivisions
        /// </summary>  
        public JsonResult FetchCallOutcomes(string LoginMID, string AccessType, string WorkDMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_CallOutcomesByWorkDMID", EncodeDecode.Decode(WorkDMID), LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["OutcomeMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["Outcome"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used to Fetch WorkDivision dropdown based on WorkGroup
        /// </summary>  
        public JsonResult FetchWorkDivision(string LoginMID, string AccessType, string WorkGMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_CallOutcomesByWorkDMID", EncodeDecode.Decode(WorkGMID), LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["OutcomeMID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["Outcome"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to Fetch WorkItems dropdown based on WorkDivisions
        /// </summary>  
        public JsonResult WorkItemsDetails(string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            objWAT.LoginMID = LoginMID;
            ds = da.GetWorkItem(objWAT);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                string WorkIMID = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkIMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkDMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkGMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["CampaignID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["AgentWorkDID"].ToString()) + "#~#" +
                                    ds.Tables[0].Rows[i]["TotalWork"].ToString();
                dr["ID"] = WorkIMID;
                dr["Name"] = ds.Tables[0].Rows[i]["WorkItemName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WorkItemsDetails_Expedia(string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            objWAT.LoginMID = LoginMID;
            ds = da.GetWorkItem_Expedia(objWAT);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                string WorkIMID = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkIMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkDMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkGMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["CampaignID"].ToString()
                                    + "~" + ds.Tables[0].Rows[i]["AgentWorkDID"].ToString()) + "#~#" +
                                    ds.Tables[0].Rows[i]["TotalWork"].ToString();
                dr["ID"] = WorkIMID;
                dr["Name"] = ds.Tables[0].Rows[i]["WorkItemName"].ToString();
                dtWAT.Rows.Add(dr);
            }

            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WorkItemsDetails_ALG(string LoginMID, string WorkDMID = "", string WorkGMID = "")
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            objWAT.LoginMID = LoginMID;
            objWAT.WorkDMID = EncodeDecode.Decode(WorkDMID);
            objWAT.WorkGMID = EncodeDecode.Decode(WorkGMID).Split('~')[0];
            ds = da.GetWorkItem(objWAT);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                string WorkIMID = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkIMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkDMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["WorkGMID"].ToString() + "~" +
                                    ds.Tables[0].Rows[i]["CampaignID"].ToString()
                                    + "~" + ds.Tables[0].Rows[i]["AgentWorkDID"].ToString()) + "#~#" +
                                    ds.Tables[0].Rows[i]["TotalWork"].ToString();
                dr["ID"] = WorkIMID;
                dr["Name"] = ds.Tables[0].Rows[i]["WorkItemName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action : Open Agent Activity Details popup screen, Mehod : Get
        /// </summary>
        [HttpGet]
        public ActionResult AgentActivityDetails(string id)
        {
            WatModel objWAT = new WatModel();
            try
            {
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                ControlsQueryString = string.Empty;
                ControlsQueryString = TempData["QueryString"].ToString();
                objWAT.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                objWAT.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                objWAT.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                objWAT.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                objWAT.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                ViewBag.AppName = objWAT.AppName;
                objWAT.URL = ControlsQueryString;
                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                return View(objWAT);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "AgentActivityDetails", ex.ToString());
                return View(objWAT);
            }
        }

        [HttpGet]
        public ActionResult AgentActivityDetails_Expedia(string id)
        {
            WatModel objWAT = new WatModel();
            try
            {
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                ControlsQueryString = string.Empty;
                ControlsQueryString = TempData["QueryString"].ToString();
                objWAT.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                objWAT.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                objWAT.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                objWAT.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                objWAT.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                ViewBag.AppName = objWAT.AppName;
                objWAT.URL = ControlsQueryString;
                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.LastDateDate = DateTime.Now;
                return View(objWAT);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "AgentActivityDetails", ex.ToString());
                return View(objWAT);
            }
        }
        /// <summary>
        /// Get Agent Activity Details based on the LoginMID
        /// </summary>
        /// <param name="AuditSMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentProductivityDetails(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            ds = objDalWAT.GetDataEntryDetails(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAgentProductivityDetails_Expedia(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            ds = objDalWAT.GetDataEntryDetails_Expedia(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action to Download the AgentProductivityDetails
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <param name="AccessType"></param>
        /// <param name="GlobalUserID"></param>
        /// <param name="StatusDID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentProductivityDetails(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            DataSet dsAgent = objDalWAT.GetDataEntryDetails_Expedia(objWAT);
            dsAgent.Tables[0].Columns.Remove("DataDID");
            dsAgent.Tables[0].Columns.Remove("WorkGMID");
            dsAgent.Tables[0].Columns.Remove("CampaignID");
            dsAgent.Tables[0].Columns.Remove("WorkDMID");
            dsAgent.Tables[0].Columns.Remove("WorkIMID");
            dsAgent.Tables[0].Columns.Remove("OutcomeMID");
            dsAgent.Tables[0].Columns.Remove("Outcome");
            dsAgent.Tables[0].Columns.Remove("StartDate");
            dsAgent.Tables[0].Columns["DataValue"].ColumnName = "Count";
            dsAgent.Tables[0].Columns["RefNoStatus"].ColumnName = "Reference No. Required";
            try
            {
                ExcelDownload(dsAgent.Tables[0], "Agent Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        public ActionResult DownloadAgentProductivityDetails_Expedia(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            DataSet dsExpedia = objDalWAT.GetDataEntryDetails(objWAT);
            dsExpedia.Tables[0].Columns.Remove("DataDID");
            dsExpedia.Tables[0].Columns.Remove("WorkGMID");
            dsExpedia.Tables[0].Columns.Remove("CampaignID");
            dsExpedia.Tables[0].Columns.Remove("WorkDMID");
            dsExpedia.Tables[0].Columns.Remove("WorkIMID");
            dsExpedia.Tables[0].Columns.Remove("OutcomeMID");
            dsExpedia.Tables[0].Columns.Remove("Outcome");
            dsExpedia.Tables[0].Columns.Remove("StartDate");
            dsExpedia.Tables[0].Columns["DataValue"].ColumnName = "Count";
            dsExpedia.Tables[0].Columns["RefNoStatus"].ColumnName = "Reference No. Required";
            try
            {
                ExcelDownload(ds.Tables[0], "Agent Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }

        /// <summary>
        /// Get Agent Overall Activity Details based on the LoginMID
        /// </summary>
        /// <param name="AuditSMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentOverallActivityDetails(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentOverallActivityDetails(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgentOverallActivityDetails_Expedia(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentOverallActivityDetails_Expedia(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action to Download the Agent Overall Activity Details
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentOverallActivityDetails(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            DataSet dsOverall = objDalWAT.GetAgentOverallActivityDetails(objWAT);
            dsOverall.Tables[0].Columns.Remove("StatusDID");
            dsOverall.Tables[0].Columns.Remove("ActionSMID");
            dsOverall.Tables[0].Columns.Remove("StartTime");
            try
            {
                ExcelDownload(dsOverall.Tables[0], "Agent Overall Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        public ActionResult DownloadAgentOverallActivityDetails_Expedia(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            DataSet dsOverallA = objDalWAT.GetAgentOverallActivityDetails_Expedia(objWAT);
            dsOverallA.Tables[0].Columns.Remove("StatusDID");
            dsOverallA.Tables[0].Columns.Remove("ActionSMID");
            dsOverallA.Tables[0].Columns.Remove("StartTime");
            try
            {
                ExcelDownload(dsOverallA.Tables[0], "Agent Overall Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }

        /// <summary>
        /// Action to open the Close View in case of error or Session lost.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Close()
        {
            WatModel model = new WatModel();
            return View(model);
        }

        /// <summary>
        /// Action: Opens view ActivityTracker, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTrackerHilton(string id, string id1)
        {
            WatModelHilton model = new WatModelHilton();
            DalwatAgent da = new DalwatAgent();
            try
            {
                string ControlsQueryString1 = string.Empty;
                var rd = Request.RequestContext.RouteData;
                ViewBag.ErrorMessage = "";
                ViewBag.CurrentDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                if (TempData["WatModel"] != null)
                {
                    model = (WatModelHilton)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString1 = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString1);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString1);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString1);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString1);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString1);
                }

                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&UniqueID=" + model.UniqueID);
                ds = da.GetActionStatusDetailsHilton(model);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    model.StatusDID = ds.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = ds.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = ds.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = ds.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = ds.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.ActiveWorkStatusDID = ds.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = ds.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.DeskMID = EncodeDecode.Encode(ds.Tables[1].Rows[0]["DeskMID"].ToString());
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "2")
                    {
                        DataSet ds1 = da.FillDropDown("WAT_Hilton_New", Session["LoginMID"].ToString(), Session["AccessType"].ToString());
                        List<Prism.Model.WAT.ListData> objRescueData = GetListDataHilton(ds1.Tables[2], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objPurposeData = GetListDataHilton(ds1.Tables[1], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objCallTypeData = GetListDataHilton(ds1.Tables[0], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objReasonData = GetListDataHilton(ds1.Tables[3], model, "Reason", "Name", "ID");
                        model.ListReasonData = objReasonData;
                        model.ListTypeCallData = objCallTypeData;
                        model.ListPurposeData = objPurposeData;
                        model.ListRescueData = objRescueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }

        /// <summary>
        /// Action to save ActivityTrackerDetails, Method : Post
        /// </summary>
        /// <param name="model">object of type WatModel</param>
        /// <param name="form">object of type FormCollection</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActivityTrackerHilton(WatModelHilton model, FormCollection form)
        {
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            try
            {

                if (model.ActionSMID == "16")
                {
                    DalLoginMaster dLoginMaser = new DalLoginMaster();
                    dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                }
                else if (model.CurrentStatusID == "2" && model.ActionSMID != "7")
                {
                    model.DeskMID = EncodeDecode.Decode(model.DeskMID);
                    model.ReasonMID = EncodeDecode.Decode(model.ReasonMID);
                    model.CheckInDate = FormatDateHilton(model.CheckInDate);
                    model.CheckOutDate = FormatDateHilton(model.CheckOutDate);
                    model.AltReasonMID = EncodeDecode.Decode(model.AltReasonMID);
                    model.CallTypeMID = EncodeDecode.Decode(model.CallTypeMID);
                    model.PurposeMID = EncodeDecode.Decode(model.PurposeMID);
                    model.RescueMID = EncodeDecode.Decode(model.RescueMID);
                    result = da.ADDUpdateWATBookingDetailsHilton(model);
                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else
                    {
                        TempData["Message"] = "Record saved successfully.";
                        ViewBag.ErrorMessage = "Record saved successfully.";
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTrackerHilton/" + model.URL);
                    }
                }
                else
                {
                    model.DeskMID = EncodeDecode.Decode(model.DeskMID);
                    result = da.UpdateWATActionStatusHilton(model);
                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else
                    {
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTrackerHilton/" + model.URL);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }
        /// <summary> Commented due to sonar issue(24072018)
        ///  static string GetHtmlPage()
        /// {
        /// using (WebClient client = new WebClient())
        ///  {

        ///   NameValueCollection vals = new NameValueCollection();
        ///  vals.Add("txtUserName", "II00023981");
        ///  vals.Add("txtPassword", "Prism@786");
        ///   byte[] bytedata = client.UploadValues("https://samurai.intelenetglobal.com/TimeManagement_New/", vals);
        ///  return System.Text.Encoding.UTF8.GetString(bytedata);
        /// }
        /// }
        /// </summary>

        /// <summary>
        /// Gets lists for dropdownlist
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Prism.Model.WAT.ListData> GetListDataHilton(DataTable dt3, Prism.Model.WAT.WatModelHilton model,
                                                                    string DefaultText, string TextField, string ValueField)
        {
            List<Prism.Model.WAT.ListData> obj = new List<Prism.Model.WAT.ListData>();
            if (dt3.Rows.Count > 0)
            {
                Prism.Model.WAT.ListData objDefault = new Prism.Model.WAT.ListData();
                objDefault.Text = DefaultText;
                objDefault.Value = "";
                obj.Add(objDefault);
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    Prism.Model.WAT.ListData obj1 = new Prism.Model.WAT.ListData();
                    obj1.Text = dt3.Rows[i][TextField].ToString();
                    obj1.Value = EncodeDecode.Encode(dt3.Rows[i][ValueField].ToString());
                    obj.Add(obj1);
                }
            }
            return obj;
        }




        public JsonResult FillWATDropDown(string Type, string ID)
        {
            string Result = string.Empty;
            DalwatAgent dADD = new DalwatAgent();
            ds = new DataSet();
            ds = dADD.FillDropDown(Type, Session["LoginMID"].ToString(), Session["AccessType"].ToString(), ID);
            Result = CommonFunctions.ListToString(ds);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action : Open Agent Activity Details popup screen, Mehod : Get
        /// </summary>
        [HttpGet]
        public ActionResult AgentActivityDetailsHilton(string id)
        {
            WatModel objWAT = new WatModel();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                ControlsQueryString = TempData["QueryString"].ToString();
                objWAT.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                objWAT.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                objWAT.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                objWAT.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                objWAT.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                ViewBag.AppName = objWAT.AppName;
                objWAT.URL = ControlsQueryString;
                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.lastTrim = DateTime.Now;
                return View(objWAT);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "AgentActivityDetails", ex.ToString());
                return View(objWAT);
            }
        }
        /// <summary>
        /// Get Agent Activity Details based on the LoginMID
        /// </summary>
        /// <param name="AuditSMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentProductivityDetailsHilton(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            string Result = string.Empty;
            WatModelHilton objWAT = new WatModelHilton();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            ds = objDalWAT.GetDataEntryDetailsHilton(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action to Download the AgentProductivityDetails
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <param name="AccessType"></param>
        /// <param name="GlobalUserID"></param>
        /// <param name="StatusDID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentProductivityDetailsHilton(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            WatModelHilton objWAT = new WatModelHilton();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            DataSet dsHilton = objDalWAT.GetDataEntryDetailsHilton(objWAT);
            dsHilton.Tables[0].Columns.Remove("DataDID");
            dsHilton.Tables[0].Columns.Remove("StatusDID");
            dsHilton.Tables[0].Columns.Remove("HotelCodeMID");
            dsHilton.Tables[0].Columns.Remove("OutcomeMID");
            dsHilton.Tables[0].Columns.Remove("ReasonMID");
            dsHilton.Tables[0].Columns.Remove("AltHotelCodeMID");
            dsHilton.Tables[0].Columns.Remove("AltOutcomeMID");
            dsHilton.Tables[0].Columns.Remove("AltReasonMID");
            dsHilton.Tables[0].Columns["AdultCount"].ColumnName = "Adult";
            dsHilton.Tables[0].Columns["ChildCount"].ColumnName = "Child";
            dsHilton.Tables[0].Columns["Outcome"].ColumnName = "BookingStatus";
            dsHilton.Tables[0].Columns["Reason"].ColumnName = "NotBookedReason";
            dsHilton.Tables[0].Columns["AltHotelCode"].ColumnName = "AlternateHotelCode";
            dsHilton.Tables[0].Columns["AltRate"].ColumnName = "AlternateRate";
            dsHilton.Tables[0].Columns["AltOutcome"].ColumnName = "AlterOfferStatus";
            dsHilton.Tables[0].Columns["AltReason"].ColumnName = "AlternateOfferRejectedReason";
            try
            {
                ExcelDownload(dsHilton.Tables[0], "Agent Productivity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }

        /// <summary>
        /// Get Agent Overall Activity Details based on the LoginMID
        /// </summary>
        /// <param name="AuditSMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentOverallActivityDetailsHilton(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentOverallActivityDetailsHilton(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Action to Download the Agent Overall Activity Details
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentOverallActivityDetailsHilton(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            DataSet dsActivity = objDalWAT.GetAgentOverallActivityDetailsHilton(objWAT);
            dsActivity.Tables[0].Columns.Remove("StatusDID");
            dsActivity.Tables[0].Columns.Remove("ActionSMID");
            dsActivity.Tables[0].Columns.Remove("StartTime");
            try
            {
                ExcelDownload(dsActivity.Tables[0], "Agent Overall Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }

        /// <summary>
        /// Used to Fetch Desks dropdown
        /// </summary>  
        public JsonResult FetchHiltonDesks(string LoginMID, string AccessType)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_HiltonDesks", LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["ID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["Name"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        #region WAT_HRCC
        /// <summary>
        /// Action: Opens view ActivityTracker, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTrackerHRCC(string id, string id1)
        {
            WatModelHrcc model = new WatModelHrcc();
            DalwatAgent da = new DalwatAgent();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                ViewBag.ErrorMessage = "";
                ViewBag.CurrentDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                if (TempData["WatModel"] != null)
                {
                    model = (WatModelHrcc)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                }

                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&UniqueID=" + model.UniqueID);
                ds = da.GetActionStatusDetailsHRCC(model);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    model.StatusDID = ds.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = ds.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = ds.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = ds.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = ds.Tables[1].Rows[0]["ActionStartDateTime"].ToString();

                    model.ActiveWorkStatus = ds.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.DeskMID = EncodeDecode.Encode(ds.Tables[1].Rows[0]["DeskMID"].ToString());
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "2")
                    {
                        DataSet ds1 = da.FillDropDown("WAT_HRCC", Session["LoginMID"].ToString(), Session["AccessType"].ToString());
                        List<Prism.Model.WAT.ListData> objRescueData = GetListDataHRCC(ds1.Tables[3], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objBedData = GetListDataHRCC(ds1.Tables[1], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objCallTypeData = GetListDataHRCC(ds1.Tables[0], model, "Select", "Name", "ID");

                        model.ListTypeCallData = objCallTypeData;
                        model.ListBedTypeData = objBedData;
                        model.ListRescueData = objRescueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }
        /// <summary>
        /// Gets lists for dropdownlist
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Prism.Model.WAT.ListData> GetListDataHRCC(DataTable dt2, Prism.Model.WAT.WatModelHrcc model,
                                                                    string DefaultText, string TextField, string ValueField)
        {
            List<Prism.Model.WAT.ListData> obj = new List<Prism.Model.WAT.ListData>();
            if (dt2.Rows.Count > 0)
            {
                Prism.Model.WAT.ListData objDefault = new Prism.Model.WAT.ListData();
                objDefault.Text = DefaultText;
                objDefault.Value = "";
                obj.Add(objDefault);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Prism.Model.WAT.ListData obj1 = new Prism.Model.WAT.ListData();
                    obj1.Text = dt2.Rows[i][TextField].ToString();
                    obj1.Value = EncodeDecode.Encode(dt2.Rows[i][ValueField].ToString());
                    obj.Add(obj1);
                }
            }
            return obj;
        }



        /// <summary>
        /// Action to save ActivityTrackerDetailsHRCC, Method : Post
        /// </summary>
        /// <param name="model">object of type WatModelHrcc</param>
        /// <param name="form">object of type FormCollection</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActivityTrackerHRCC(WatModelHrcc model, FormCollection form)
        {
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            try
            {
                if (model.ActionSMID == "16")
                {
                    DalLoginMaster dLoginMaser = new DalLoginMaster();
                    dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                }
                else if (model.CurrentStatusID == "2" && model.ActionSMID != "7")
                {
                    model.DeskMID = EncodeDecode.Decode(model.DeskMID);
                    model.RescueMID = EncodeDecode.Decode(model.RescueMID);
                    model.CallTypeMID = EncodeDecode.Decode(model.CallTypeMID);
                    model.BedTypeMID = EncodeDecode.Decode(model.BedTypeMID);
                    model.CheckInDate = FormatDateHilton(model.CheckInDate);
                    model.CheckOutDate = FormatDateHilton(model.CheckOutDate);
                    result = da.ADDUpdateWATBookingDetailsHRCC(model);
                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else
                    {
                        TempData["Message"] = "Record saved successfully.";
                        ViewBag.ErrorMessage = "Record saved successfully.";
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTrackerHRCC/" + model.URL);
                    }
                }
                else
                {
                    model.DeskMID = EncodeDecode.Decode(model.DeskMID);
                    result = da.UpdateWATActionStatusHRCC(model);

                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else
                    {
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTrackerHRCC/" + model.URL);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker", ex.ToString());
                return View(model);
            }
        }

        #endregion

        #region Activity Tracker ALG
        /// <summary>
        /// Action: Opens view ActivityTracker_ALG, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTracker_ALG(string id, string id1)
        {
            WatModel model = new WatModel();
            DalwatAgent da = new DalwatAgent();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                model.hdnDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.ErrorMessage = "";
                if (TempData["WatModel"] != null)
                {
                    model = (WatModel)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                    Session["DefaultClientID"] = Prism.Utility.Querystring.QueryStrData("DefaultClientID", ControlsQueryString);
                }
                ds = da.FillDropDown("WAT_WorkGroups", Session["LoginMID"].ToString(), Session["AccessType"].ToString(), Session["GlobalUserID"].ToString());
                List<Prism.Model.WAT.ListData> objWGData = GetListData(ds.Tables[0], model, "Work Group", "WorkGroupName", "WorkGMID");
                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.ClientID = (Session["DefaultClientID"] == null ? "242" : Session["DefaultClientID"]).ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.ListData = objWGData;
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&DefaultClientID=" + model.ClientID + "&UniqueID=" + model.UniqueID);
                DataSet dsStatus = da.GetActionStatusDetails_ALG(model);
                DataSet dsDynamicActionStatus = da.GetActionDynamicStatusDetails(model);

                if (dsDynamicActionStatus != null)
                {
                    List<LoadDynamicStatusClass> objLoadDynamicStatus = GetDynamicStatusList(dsDynamicActionStatus.Tables[0], model);
                    model.LoadDynamicStatusForComActivity = objLoadDynamicStatus;
                    model.hidDynamicStatus = strDynamicStatus.ToString();
                }

                if (dsStatus != null && dsStatus.Tables.Count > 1 && dsStatus.Tables[1].Rows.Count > 0)
                {

                    model.ClientID_Empower = dsStatus.Tables[1].Rows[0]["ClientID"].ToString();
                    model.ProjectID = dsStatus.Tables[1].Rows[0]["ProjectID"].ToString();
                    model.StatusDID = dsStatus.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = dsStatus.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = dsStatus.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = dsStatus.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = dsStatus.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.WorkGMID = EncodeDecode.Encode(dsStatus.Tables[1].Rows[0]["WorkGMID"].ToString());
                    model.CampaignID = dsStatus.Tables[1].Rows[0]["CampaignID"].ToString();
                    model.WorkDMID = EncodeDecode.Encode(dsStatus.Tables[1].Rows[0]["WorkDMID"].ToString());
                    model.WorkIMID = dsStatus.Tables[1].Rows[0]["WorkIMID"].ToString();
                    model.CampaignName = dsStatus.Tables[1].Rows[0]["CampaignName"].ToString();
                    model.WorkGroupName = dsStatus.Tables[1].Rows[0]["WorkGroupName"].ToString();
                    model.WorkDivisionName = dsStatus.Tables[1].Rows[0]["WorkDivisionName"].ToString();
                    model.WorkItemName = dsStatus.Tables[1].Rows[0]["WorkItemName"].ToString();
                    model.WorkCompleted = dsStatus.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    model.AgentWorkDID = dsStatus.Tables[1].Rows[0]["AgentWorkDID"].ToString();
                    model.ActiveWorkStatusDID = dsStatus.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = dsStatus.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.TotalWorkCount = dsStatus.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "7")
                    {

                        LoadDynamicFilter(EncodeDecode.Decode(model.WorkGMID.ToString()), model);
                        model.MultiselectDropdown = MultiselectDropdown.ToString();
                        DataSet ds1 = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                        List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds1.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                        model.ListOutcomeData = objOutcomeData;
                        DataSet ds2 = da.FillDropDown("WATDropDownFill", "1", EncodeDecode.Decode(model.WorkDMID), model.WorkIMID);//hard code Test
                        List<Prism.Model.WAT.ListData> ListQueueData = GetListData(ds2.Tables[0], model, "Select", "Name", "ID");
                        model.ListQueueData = ListQueueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;
                }
                DataSet dsAssignment = da.GetSelfAssignment(model);
                if (dsAssignment != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Assignment = Convert.IsDBNull(dsAssignment.Tables[0].Rows[0]["Assignment"]) ? default(bool) : Convert.ToBoolean(dsAssignment.Tables[0].Rows[0]["Assignment"]);
                    model.SelfAssignment = Convert.IsDBNull(dsAssignment.Tables[0].Rows[0]["SelfAssignment"]) ? default(bool) : Convert.ToBoolean(dsAssignment.Tables[0].Rows[0]["SelfAssignment"]);
                }


                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_ALG", ex.ToString());
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ActivityTracker_ALG(WatModel model, FormCollection form)
        {
            CultureInfo info = new CultureInfo("en-GB");
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            int ValidURN = 1;
            try
            {
                //will be use in future
                ////if (model.ClientID_Empower == "165")
                ////{
                ////    string workIMID = model.WorkIMID;//model.WorkDMID; changed 1st it was on WorkDMID
                ////    if (model.ActionSMID == "22" && EncodeDecode.IsBase64(workIMID))
                ////   {
                ////            model.WorkIMID = EncodeDecode.Decode(workIMID);
                ////   }
                ////}

                if (ValidURN == 1)
                {
                    if (model.WorkDMID == "28")
                    {
                        if (model.StartDateTime != "")
                        {
                            DateTime date_t = Convert.ToDateTime(model.StartDateTime, info);
                            string datetime = date_t.ToString("yyyy-MM-dd") + " " + model.Hour + ":" + model.minute;
                            model.StartDateTime = datetime;
                        }
                    }
                    else
                    {
                        model.StartDateTime = "";
                    }
                    Session["LoginMID"] = model.LoginMID;
                    Session["GlobalUserID"] = model.GlobalUserID;
                    Session["DefaultClientID"] = model.ClientID;
                    Session["AccessType"] = model.AccessType;
                    Session["UniqueID"] = model.UniqueID;
                    Session["Host"] = model.Host;
                    if (model.ActionSMID == "0")
                    {
                        model.ActionSMID = model.CurrentStatusID;
                        result = da.EndWATActionStatus_ALG(model);
                        if (result == 0)
                        {
                            TempData["Message"] = "An error occurred while processing the request. Please try again later";
                            ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                            return View(model);
                        }
                        else
                        {
                            return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                        }
                    }
                    else if (model.ActionSMID == "16")
                    {
                        DalLoginMaster dLoginMaser = new DalLoginMaster();
                        dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                    }
                    else if (model.ActionSMID == "2")
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                            model.WorkGMID = WorkItem[2];
                            model.CampaignID = WorkItem[3];
                            model.WorkDMID = WorkItem[1];
                            model.WorkIMID = WorkItem[0];
                            result = da.UpdateWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                            }
                        }
                        else
                        {
                            if (model.Type == "Cancel")
                            {
                                result = da.CancelAutoWrapForActiveWork_ALG(model);//lrft
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                                }
                            }
                            else if (Convert.ToInt32(model.DataValue) <= Convert.ToInt32(model.TotalWorkCount))
                            {
                                string[] DDLIDS = { "1", "3" };
                                model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                                model.QueueID = "1";
                                model.SubQueueID = EncodeDecode.Decode(model.SubQueueID);
                                model.DataValue = "1";
                                model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                                model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);

                                DataSet DsEncryption = da.GetEncryptionTypeStatus(model.WorkGMID, Session["LoginMID"].ToString());
                                if (DsEncryption != null && DsEncryption.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToBoolean(DsEncryption.Tables[0].Rows[0]["Status"].ToString()))
                                    {
                                        XDocument DynamicCol = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                   new XElement("NewDataSet",
                   from DynamicSet in model.LoadDynamicControlForComActivity
                   select new XElement("Table",
                   new XElement("ParaName", DynamicSet.MiscCOLName),
                    new XElement("Value", DDLIDS.Contains(DynamicSet.MiscType) ? AesEncryption.Encode(form["txt" + DynamicSet.MiscMID + ""].ToString()) : AesEncryption.Encode(DynamicSet.MiscValue)),
                    new XElement("ID", DDLIDS.Contains(DynamicSet.MiscType) ? AesEncryption.Encode(form["hdn" + DynamicSet.MiscMID + ""].ToString()) : AesEncryption.Encode(DynamicSet.MiscValue))
                  )));
                                        model.DynamicControls = DynamicCol.ToString();
                                    }
                                    else
                                    {
                                        XDocument DynamicCol = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                   new XElement("NewDataSet",
                   from DynamicSet in model.LoadDynamicControlForComActivity
                   select new XElement("Table",
                   new XElement("ParaName", DynamicSet.MiscCOLName),
                    new XElement("Value", DDLIDS.Contains(DynamicSet.MiscType) ? form["txt" + DynamicSet.MiscMID + ""].ToString() : DynamicSet.MiscValue),
                    new XElement("ID", DDLIDS.Contains(DynamicSet.MiscType) ? form["hdn" + DynamicSet.MiscMID + ""].ToString() : DynamicSet.MiscValue)
                  )));
                                        model.DynamicControls = DynamicCol.ToString();
                                    }
                                }


                                result = da.ADDUpdateWATWorkDetails_ALG(model);
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else if (result == 3)
                                {
                                    TempData["Message"] = "Duplicate URN exists";
                                    ViewBag.ErrorMessage = "Duplicate URN exists";
                                    TempData["WatModel"] = model;
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                                }
                            }
                            else
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                        }
                    }
                    else if (model.ActionSMID == "22")
                    {
                        if (model.CurrentStatusID != "23")
                        {
                            model.ActionSMID = model.CurrentStatusID;
                            model.ActiveWorkStatus = "22";
                            result = da.EndWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                            }
                        }
                        else
                        {
                            string WorkGroup = EncodeDecode.Decode(model.WorkGMID);
                            model.WorkGMID = WorkGroup.Split('~').First();
                            model.CampaignID = WorkGroup.Split('~').Last();
                            model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                            model.WorkIMID = "0";
                            model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                            result = da.CompleteTelephoneCall_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                            }
                        }
                    }
                    else
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            if (model.WorkIMID != "0")
                            {
                                string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                                model.WorkGMID = WorkItem[2];
                                model.CampaignID = WorkItem[3];
                                model.WorkDMID = WorkItem[1];
                                model.WorkIMID = WorkItem[0];
                            }
                            else
                            {
                                model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                                model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                            }
                            result = da.UpdateWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                            }
                        }

                        else
                        {
                            if (model.CurrentStatusID == "2" && model.ActionSMID != "22")
                            {
                                model.ActiveWorkStatus = "2";
                            }
                            result = da.EndWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                            }
                        }
                    }
                }
                else
                {
                    ds = new DataSet();
                    ds = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                    List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                    model.ListOutcomeData = objOutcomeData;
                    TempData["Message"] = "Duplicate URN exists";
                    ViewBag.ErrorMessage = "Duplicate URN exists";
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_ALG/" + model.URL);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_ALG", ex.ToString());
                return View(model);
            }
        }
        public JsonResult GetMultiselectDropdown(string MiscMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = da.FillDropDown("MultiselectMiscColumnData", MiscMID, Session["AccessType"].ToString(), Session["LoginMID"].ToString());
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindDynamicDependentDropdown(string ParentDocMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = da.BindDynamicDependentDropDown(ParentDocMID, Session["LoginMID"].ToString());
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        //bind Dynamic Value for
        public void GetDynaminControlValues(WatModel model, FormCollection form)
        {
            DataSet dsDynamicControls = new DataSet();
            dsDynamicControls = (DataSet)TempData["dynamicControl"];
            StringBuilder strobjDynamicControls = new StringBuilder();
            for (int i = 0; i < model.LoadDynamicControlForComActivity.Count; i++)
            {
                if (model.LoadDynamicControlForComActivity[i].MiscType != "1")
                {
                    if (model.LoadDynamicControlForComActivity[i].MiscType != "3")
                    {
                        strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscCOLName).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscValue).Append("|").Append("0").Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);
                    }
                    else
                    {
                        string hidden = string.Empty;
                        hidden = form["hdn" + model.LoadDynamicControlForComActivity[i].MiscMID + ""].ToString();
                        if (!string.IsNullOrEmpty(hidden))
                        {
                            var hiddenArr = hidden.Split(',');
                            foreach (var row in hiddenArr)
                            {
                                Int16 flag = 0;
                                if (row != "0")
                                {
                                    DataTable filteredTable = (from n in dsDynamicControls.Tables[1].AsEnumerable()
                                                               where n.Field<Int64>("MiscMID") == Int64.Parse(model.LoadDynamicControlForComActivity[i].MiscMID)
                                                               select n).CopyToDataTable();
                                    if (filteredTable.Rows.Count > 0)
                                    {
                                        for (int i1 = 0; i1 < filteredTable.Rows.Count; i1++)
                                        {
                                            if (row == filteredTable.Rows[i1]["MiscDID"].ToString())
                                            {
                                                strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append(filteredTable.Rows[i1]["MiscData"].ToString()).Append("|").Append(row.ToString()).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);
                                                flag = 1;
                                            }
                                        }
                                        if (flag != 1)
                                        {
                                            strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append("").Append("|").Append("0").Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);

                                        }
                                    }
                                }
                                else
                                {
                                    strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append("").Append("|").Append("0").Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);
                                }
                                strobjDynamicControls.Append("~");
                            }

                            strobjDynamicControls.Length--;
                        }
                        else
                        {
                            strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append("").Append("|").Append("0").Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);
                        }

                    }
                }
                else
                {
                    Int16 flag = 0;
                    DataTable filteredTable = (from n in dsDynamicControls.Tables[1].AsEnumerable()
                                               where n.Field<Int64>("MiscMID") == Int64.Parse(model.LoadDynamicControlForComActivity[i].MiscMID)
                                               select n).CopyToDataTable();
                    if (filteredTable.Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < filteredTable.Rows.Count; i1++)
                        {
                            if (model.LoadDynamicControlForComActivity[i].MiscValue == filteredTable.Rows[i1]["MiscDID"].ToString())
                            {
                                strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append(filteredTable.Rows[i1]["MiscData"].ToString()).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscValue).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);
                                flag = 1;
                            }

                        }
                        if (flag != 1)
                        {
                            strobjDynamicControls.Append(model.LoadDynamicControlForComActivity[i].MiscMID).Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscName).Append("|").Append("").Append("|").Append("0").Append("|").Append(model.LoadDynamicControlForComActivity[i].MiscType);

                        }
                    }
                }
                strobjDynamicControls.Append("~");
            }
            model.DynamicControls = strobjDynamicControls.ToString();
        }

        [HttpGet]
        public ActionResult AgentActivityDetails_ALG(string id)
        {

            WatModel objBO = new WatModel();
            DalwatAgent da = new DalwatAgent();
            ControlsQueryString = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    ControlsQueryString = id;
                }
                objBO.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                objBO.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                objBO.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                objBO.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                objBO.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                ViewBag.AppName = objBO.AppName;
                objBO.URL = ControlsQueryString;
                ds = da.FillDropDown("WAT_WorkGroups", objBO.LoginMID, objBO.AccessType, objBO.GlobalUserID);

                List<ListData> lstwg = new List<ListData>();
                ListData ddlWorkGroup = new ListData();
                if (ds != null)
                {
                    int i;
                    DataTable dttab = ds.Tables[0];
                    if (dttab.Rows.Count != 1)
                    {
                        ddlWorkGroup.Text = "Select";
                        ddlWorkGroup.Value = "0";
                        lstwg.Add(ddlWorkGroup);
                    }
                    for (i = 0; i < dttab.Rows.Count; i++)
                    {
                        ddlWorkGroup = new ListData();
                        ddlWorkGroup.Text = dttab.Rows[i]["WorkGroupName"].ToString();
                        ddlWorkGroup.Value = dttab.Rows[i]["ID"].ToString();
                        lstwg.Add(ddlWorkGroup);
                    }
                    objBO.ListData = lstwg;
                }

                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                return View(objBO);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "AgentActivityDetails", ex.ToString());
                return View(objBO);
            }
        }
        public JsonResult GetAgentProductivityDetails_ALG(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID, string WorkGMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            objWAT.WorkGMID = WorkGMID;
            ds = objDalWAT.GetDataEntryDetails_ALG(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Agent Overall Activity Details based on the LoginMID
        /// </summary>
        /// <param name="LoginMID"></param>
        /// <returns>List of FreeText as JsonResult</returns>
        public JsonResult GetAgentOverallActivityDetails_ALG(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentOverallActivityDetails_ALG(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action to Download the Agent Productivity Details
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentProductivityDetails_ALG(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID, string WorkGMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            objWAT.WorkGMID = WorkGMID;
            ds = objDalWAT.GetDataEntryDetails_ALG(objWAT);
            try
            {
                ExcelDownload(ds.Tables[0], "Agent Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        public ActionResult DownloadAgentOverallActivityDetails_ALG(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            DataSet dsALG = objDalWAT.GetAgentOverallActivityDetails_ALG(objWAT);
            dsALG.Tables[0].Columns.Remove("StatusDID");
            dsALG.Tables[0].Columns.Remove("ActionSMID");
            dsALG.Tables[0].Columns.Remove("StartTime");
            try
            {
                ExcelDownload(dsALG.Tables[0], "Agent Overall Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        /// <summary>
        /// Bind Audit Sheet dynamic Columns from the database
        /// <param name="AuditSCMID">Contains Audit sheet unique ID </param>  
        /// <param name="model">Contain object of type TMAuditSheet </param>        
        /// </summary>
        private void LoadDynamicFilter(string WorkGMID, WatModel model)
        {
            DalwatAgent objDalWAT = new DalwatAgent();
            DataSet dsDynamicControls = objDalWAT.GetDynamicControls(WorkGMID);
            TempData["dynamicControl"] = dsDynamicControls;
            if (dsDynamicControls != null)
            {
                List<LoadDynamicControlClass> objLoadDynamicControl = GetDynamicControlList(dsDynamicControls.Tables[0], dsDynamicControls.Tables[1], model);
                model.LoadDynamicControlForComActivity = objLoadDynamicControl;
                model.hidDynamicControls = strDynamicControls.ToString();
            }

        }
        StringBuilder strDynamicControlsHideSectionLevel1 = new StringBuilder();
        StringBuilder strDynamicControlsHideSectionLevel2 = new StringBuilder();
        StringBuilder strDynamicControlsHideParameter = new StringBuilder();
        StringBuilder hidCntrl = new StringBuilder();
        ////StringBuilder hidAction = new StringBuilder();////
        StringBuilder MultiselectDropdown = new StringBuilder();
        StringBuilder strDynamicControls = new StringBuilder();
        StringBuilder strDynamicStatus = new StringBuilder();

        /// <summary>
        /// Bind model's dynamic column list object
        /// <param name="dt1">Contains Column Name and type </param>  
        /// <param name="dt2">Contains Column data in case of dropdown </param> 
        /// <param name="model">Contain object of type TMAuditSheet </param>        
        /// </summary>
        public List<LoadDynamicControlClass> GetDynamicControlList(DataTable dt1, DataTable dt2, WatModel model)
        {
            List<LoadDynamicControlClass> obj = new List<LoadDynamicControlClass>();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    LoadDynamicControlClass obj1 = new LoadDynamicControlClass();
                    obj1.MiscMID = dt1.Rows[i]["DOCDID"].ToString();
                    obj1.MiscName = dt1.Rows[i]["LabelName"].ToString();
                    obj1.MiscType = dt1.Rows[i]["ControlType"].ToString();
                    obj1.MiscISMandatory = dt1.Rows[i]["IsMandatory"].ToString();
                    obj1.MiscValidation = dt1.Rows[i]["validationtype"].ToString();
                    obj1.MiscCOLName = dt1.Rows[i]["DBColumnName"].ToString();
                    obj1.RadioButtonValues = dt1.Rows[i]["RadioButtonValue"].ToString();
                    obj1.Minlenght = Convert.ToInt32(dt1.Rows[i]["Minlen"]);
                    obj1.AudotCaps = dt1.Rows[i]["AutoCaps"].ToString();
                    obj1.MiscLength = dt1.Rows[i]["Maxlen"].ToString();
                    ////obj1.CheckApplicable = dt1.Rows[i]["CheckApplicable"].ToString();
                    obj1.CustomValidation = dt1.Rows[i]["CustomValidation"].ToString();
                    obj1.Count = dt1.Rows[i]["Count"].ToString();
                    obj1.OrderId = dt1.Rows[i]["OrderId"].ToString();
                    obj1.DefaultValue = Convert.ToString(dt1.Rows[i]["DefaultValue"]);
                    obj1.HideSectionParameterIDs = "0";
                    if (dt1.Rows[i]["ControlType"].ToString() == "1" || dt1.Rows[i]["ControlType"].ToString() == "3")
                    {
                        if (strDynamicControlsHideSectionLevel1.Length > 1)
                        {
                            strDynamicControlsHideSectionLevel1.Append("~");
                        }
                        if (strDynamicControlsHideSectionLevel2.Length > 1)
                        {
                            strDynamicControlsHideSectionLevel2.Append("~");
                        }
                        if (strDynamicControlsHideParameter.Length > 1)
                        {
                            strDynamicControlsHideParameter.Append("~");
                        }
                        List<DdlDataClass> objDDLData = GetDDLList(dt2, model, obj1.MiscMID, obj1);
                        obj1.DDLData = objDDLData;

                        if (dt1.Rows[i]["ControlType"].ToString() == "3")
                        {
                            MultiselectDropdown.Append(obj1.MiscMID).Append("~");
                        }
                    }
                    strDynamicControls.Append(obj1.MiscCOLName).Append("|").Append(
                                                              obj1.MiscType.ToString()).Append("|").Append(
                                                              obj1.MiscISMandatory).Append("|").Append(
                                                              obj1.MiscName).Append("|").Append(
                                                              obj1.Minlenght).Append("|").
                                                              Append(obj1.MiscMID);
                    if (i + 1 != dt1.Rows.Count)
                    {
                        strDynamicControls.Append("~");
                    }
                    if (dt1.Rows[i]["ControlType"].ToString() == "2")
                    {
                        hidCntrl.Append("txt").Append(dt1.Rows[i]["DOCDID"].ToString()).Append(",");
                    }

                    List<ListData> listDatas = new List<ListData>();

                    string strings = obj1.RadioButtonValues;
                    if (!string.IsNullOrEmpty(obj1.RadioButtonValues))
                    {
                        string[] words = strings.Split('|');
                        for (int j = 0; j < words.Length; j++)
                        {
                            listDatas.Add(new ListData
                            {
                                Text = words[j],
                                Value = words[j]
                            });
                        }
                        obj1.RadiobuttonList = listDatas;
                    }
                    obj.Add(obj1);
                }
                if (hidCntrl.Length > 1)
                {
                    hidCntrl.Length--;
                }

            }

            return obj.OrderBy(x => x.MiscMID).ThenBy(x => x.OrderId).ToList();

        }

        /// <summary>
        /// Bind dynamic column's data to list object
        /// <param name="dt1">Contains Column Data</param>  
        /// <param name="MiscMID">Contains Column ID </param> 
        /// <param name="model">Contain object of type TMAuditSheet </param>        
        /// </summary>
        public List<DdlDataClass> GetDDLList(DataTable dt1, WatModel model, string MiscMID, LoadDynamicControlClass objDCC)
        {
            List<DdlDataClass> obj = new List<DdlDataClass>();
            if (dt1.Rows.Count > 0)
            {
                var filteredTable = (from n in dt1.AsEnumerable()
                                     where n.Field<Int64>("DOCDID") == Convert.ToInt64(MiscMID)
                                     select n).CopyToDataTable();
                Boolean DependentStatus = false;
                if (filteredTable.Rows[0]["IsDependent"].ToString() == null || filteredTable.Rows[0]["IsDependent"].ToString() == "")
                {
                    DependentStatus = false;
                }
                else
                {
                    DependentStatus = true;
                }
                if (DependentStatus)
                {
                    if (filteredTable.Rows.Count > 0)
                    {
                        DdlDataClass objDefault = new DdlDataClass();
                        objDefault.Text = "Select";
                        objDefault.Value = "0";
                        objDefault.IsDependent = "1";
                        objDefault.ParentDocDID = filteredTable.Rows[0]["ParentDocDID"].ToString();
                        objDefault.ParentDocMID = filteredTable.Rows[0]["ParentDocMID"].ToString();
                        obj.Add(objDefault);
                        for (int i = 0; i < filteredTable.Rows.Count; i++)
                        {
                            DdlDataClass obj1 = new DdlDataClass();
                            obj1.Text = filteredTable.Rows[i]["Name"].ToString();
                            obj1.Value = filteredTable.Rows[i]["DDLMID"].ToString();
                            obj.Add(obj1);
                        }
                    }
                }
                else
                {

                    if (filteredTable.Rows.Count > 0)
                    {
                        DdlDataClass objDefault = new DdlDataClass();
                        objDefault.Text = "Select";
                        objDefault.Value = "0";
                        objDefault.IsDependent = "0";
                        objDefault.ParentDocDID = filteredTable.Rows[0]["ParentDocDID"].ToString();
                        objDefault.ParentDocMID = filteredTable.Rows[0]["ParentDocMID"].ToString();
                        obj.Add(objDefault);
                        for (int i = 0; i < filteredTable.Rows.Count; i++)
                        {
                            DdlDataClass obj1 = new DdlDataClass();
                            obj1.Text = filteredTable.Rows[i]["Name"].ToString();
                            obj1.Value = filteredTable.Rows[i]["DDLMID"].ToString();
                            obj.Add(obj1);
                        }
                    }
                    else
                    {
                        DdlDataClass objDefault = new DdlDataClass();
                        objDefault.Text = "Select";
                        objDefault.Value = "0";
                        objDefault.IsDependent = "0";
                        objDefault.ParentDocDID = "";
                        objDefault.ParentDocMID = "";
                        obj.Add(objDefault);
                    }


                }
            }
            return obj;
        }

        public List<LoadDynamicStatusClass> GetDynamicStatusList(DataTable dt1, WatModel model)
        {
            List<LoadDynamicStatusClass> obj = new List<LoadDynamicStatusClass>();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    LoadDynamicStatusClass obj1 = new LoadDynamicStatusClass();
                    obj1.ActionSMID = dt1.Rows[i]["ActionSMID"].ToString();
                    obj1.ActionStatus = dt1.Rows[i]["ActionStatus"].ToString();
                    obj1.AuxName = dt1.Rows[i]["AuxName"].ToString();
                    obj1.ActionGroup = dt1.Rows[i]["ActionGroup"].ToString();
                    obj1.StatusMessage = dt1.Rows[i]["StatusMessage"].ToString();
                    obj1.Colour = dt1.Rows[i]["Colour"].ToString();

                    strDynamicStatus.Append(obj1.ActionSMID).Append("|").Append(
                                                              obj1.ActionStatus.ToString()).Append("|").Append(
                                                              obj1.AuxName).Append("|").Append(
                                                              obj1.ActionGroup).Append("|").Append(
                                                              obj1.StatusMessage).Append("|").Append(
                                                              obj1.Colour);
                    if (i + 1 != dt1.Rows.Count)
                    {
                        strDynamicStatus.Append("~");
                    }

                    obj.Add(obj1);
                }
            }

            return obj.OrderBy(x => x.ActionSMID).ThenBy(x => x.OrderId).ToList();
        }

        #endregion







        #region TP recommender
        /// <summary>
        /// Action : Open Agent Activity Details popup screen, Mehod : Get
        /// </summary>
        [HttpGet]
        public ActionResult TPRecommender(string id, string id1)
        {
            WatModelTPRecommender model = new WatModelTPRecommender();
            DalwatAgent da = new DalwatAgent();
            try
            {
                string ControlsQueryString1 = string.Empty;
                var rd = Request.RequestContext.RouteData;
                ViewBag.ErrorMessage = "";
                ViewBag.CurrentDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                if (TempData["WatModel"] != null)
                {
                    model = (WatModelTPRecommender)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString1 = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString1);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString1);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString1);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString1);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString1);
                }

                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&UniqueID=" + model.UniqueID);
                ds = da.GetActionStatusDetailsTPRecommender(model);
                //*******ds = da.GetActionStatusDetailsHilton(model);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    model.StatusDID = ds.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = ds.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = ds.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = ds.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = ds.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.ActiveWorkStatusDID = ds.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = ds.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.DeskMID = EncodeDecode.Encode(ds.Tables[1].Rows[0]["DeskMID"].ToString());
                    model.WorkGMID = EncodeDecode.Encode(ds.Tables[1].Rows[0]["WorkGMID"].ToString());
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "2")
                    {
                        DataSet ds1 = da.FillDropDown("WAT_Hilton_New", Session["LoginMID"].ToString(), Session["AccessType"].ToString());
                        List<Prism.Model.WAT.ListData> objRescueData = GetListDataTPRecommender(ds1.Tables[2], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objPurposeData = GetListDataTPRecommender(ds1.Tables[1], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objCallTypeData = GetListDataTPRecommender(ds1.Tables[0], model, "Select", "Name", "ID");
                        List<Prism.Model.WAT.ListData> objReasonData = GetListDataTPRecommender(ds1.Tables[3], model, "Reason", "Name", "ID");

                        model.ListReasonData = objReasonData;
                        model.ListTypeCallData = objCallTypeData;
                        model.ListPurposeData = objPurposeData;
                        model.ListRescueData = objRescueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TPRecommender", ex.ToString());
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult TPRecommender(WatModelTPRecommender model, FormCollection form)
        {
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            try
            {

                if (model.ActionSMID == "16")
                {
                    DalLoginMaster dLoginMaser = new DalLoginMaster();
                    dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                }
                else if (model.CurrentStatusID == "2" && model.ActionSMID != "7")
                {
                    //model.DeskMID =  EncodeDecode.Decode(model.DeskMID);                   
                    model.ReasonMID = EncodeDecode.Decode(model.ReasonMID);
                    model.CustomerChoice = EncodeDecode.Decode(model.CustomerChoice);
                    model.RescueMID = EncodeDecode.Decode(model.RescueMID);

                    if (!string.IsNullOrEmpty(model.WorkGMID))
                    {
                        model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                    }
                    model.DeskMID = model.WorkGMID;// EncodeDecode.Decode(model.WorkGMID);
                    result = da.ADDUpdateWATCrossSellDetailsTPRecommender(model);
                    ////result = da.ADDUpdateWATBookingDetailsTPRecommender(model);
                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else if (result == 2)
                    {
                        TempData["Message"] = "ChatId already exists.";
                        ViewBag.ErrorMessage = "ChatId already exists.";
                        return View(model);
                    }
                    else
                    {
                        TempData["Message"] = "Record saved successfully.";
                        ViewBag.ErrorMessage = "Record saved successfully.";
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/TPRecommender/" + model.URL);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.WorkGMID))
                    {
                        model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                    }
                    model.DeskMID = model.WorkGMID;// EncodeDecode.Decode(model.WorkGMID);
                    //model.DeskMID = EncodeDecode.Decode(model.DeskMID);
                    result = da.UpdateWATActionStatusTPRecommender(model);
                    if (result == 0)
                    {
                        TempData["Message"] = "An error occurred while processing the request. Please try again later";
                        ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                        return View(model);
                    }
                    else
                    {
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/TPRecommender/" + model.URL);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TPRecommender", ex.ToString());
                return View(model);
            }
        }

        public List<Prism.Model.WAT.ListData> GetListDataTPRecommender(DataTable dt3, Prism.Model.WAT.WatModelTPRecommender model,
                                                                   string DefaultText, string TextField, string ValueField)
        {
            List<Prism.Model.WAT.ListData> obj = new List<Prism.Model.WAT.ListData>();
            if (dt3.Rows.Count > 0)
            {
                Prism.Model.WAT.ListData objDefault = new Prism.Model.WAT.ListData();
                objDefault.Text = DefaultText;
                objDefault.Value = "";
                obj.Add(objDefault);
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    Prism.Model.WAT.ListData obj1 = new Prism.Model.WAT.ListData();
                    obj1.Text = dt3.Rows[i][TextField].ToString();
                    obj1.Value = EncodeDecode.Encode(dt3.Rows[i][ValueField].ToString());
                    obj.Add(obj1);
                }
            }
            return obj;
        }

        [HttpGet]
        public ActionResult AgentActivityDetailsTPRecommender(string id)
        {
            WatModel objWAT = new WatModel();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                ControlsQueryString = TempData["QueryString"].ToString();
                objWAT.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                objWAT.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                objWAT.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                objWAT.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                objWAT.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                ViewBag.AppName = objWAT.AppName;
                objWAT.URL = ControlsQueryString;
                ViewBag.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.lastTrim = DateTime.Now;
                return View(objWAT);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "AgentActivityDetails", ex.ToString());
                return View(objWAT);
            }
        }
        public JsonResult GetAgentProductivityDetailsTPRecommender(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            string Result = string.Empty;
            WatModelTPRecommender objWAT = new WatModelTPRecommender();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            ds = objDalWAT.GetDataEntryDetailsTPRecommender(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgentOverallActivityDetailsTPRecommender(string StartDate, string EndDate, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            ds = objDalWAT.GetAgentOverallActivityDetailsTPRecommender(objWAT);
            if (ds != null)
            {
                Result = CommonFunctions.ListToString(ds);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action to Download the AgentProductivityDetails
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LoginMID"></param>
        /// <param name="AccessType"></param>
        /// <param name="GlobalUserID"></param>
        /// <param name="StatusDID"></param>
        /// <returns></returns>
        public ActionResult DownloadAgentProductivityDetailsTPRecommender(string StartDate, string EndDate, string LoginMID, string AccessType, string GlobalUserID, string StatusDID)
        {
            WatModelTPRecommender objWAT = new WatModelTPRecommender();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            objWAT.AccessType = AccessType;
            objWAT.GlobalUserID = GlobalUserID;
            objWAT.StatusDID = StatusDID;
            DataSet TPRecommender = objDalWAT.GetDataEntryDetailsTPRecommender(objWAT);
            TPRecommender.Tables[0].Columns.Remove("DataDID");
            TPRecommender.Tables[0].Columns.Remove("StatusDID");
            TPRecommender.Tables[0].Columns.Remove("HotelCodeMID");
            TPRecommender.Tables[0].Columns.Remove("OutcomeMID");
            TPRecommender.Tables[0].Columns.Remove("ReasonMID");
            TPRecommender.Tables[0].Columns.Remove("AltHotelCodeMID");
            TPRecommender.Tables[0].Columns.Remove("AltOutcomeMID");
            TPRecommender.Tables[0].Columns.Remove("AltReasonMID");
            TPRecommender.Tables[0].Columns["AdultCount"].ColumnName = "Adult";
            TPRecommender.Tables[0].Columns["ChildCount"].ColumnName = "Child";
            TPRecommender.Tables[0].Columns["Outcome"].ColumnName = "BookingStatus";
            TPRecommender.Tables[0].Columns["Reason"].ColumnName = "NotBookedReason";
            TPRecommender.Tables[0].Columns["AltHotelCode"].ColumnName = "AlternateHotelCode";
            TPRecommender.Tables[0].Columns["AltRate"].ColumnName = "AlternateRate";
            TPRecommender.Tables[0].Columns["AltOutcome"].ColumnName = "AlterOfferStatus";
            TPRecommender.Tables[0].Columns["AltReason"].ColumnName = "AlternateOfferRejectedReason";
            try
            {
                ExcelDownload(TPRecommender.Tables[0], "Agent Productivity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        public ActionResult DownloadAgentOverallActivityDetailsTPRecommender(string StartDate, string EndDate, string LoginMID)
        {
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.StartDate = FormatDate(StartDate);
            objWAT.EndDate = FormatDate(EndDate);
            objWAT.LoginMID = LoginMID;
            DataSet dsActivity = objDalWAT.GetAgentOverallActivityDetailsTPRecommender(objWAT);
            dsActivity.Tables[0].Columns.Remove("StatusDID");
            dsActivity.Tables[0].Columns.Remove("ActionSMID");
            dsActivity.Tables[0].Columns.Remove("StartTime");
            try
            {
                ExcelDownload(dsActivity.Tables[0], "Agent Overall Activity Details");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "TotalExportExcel", ex.ToString());
                return View();
            }
            return View(objWAT);
        }
        public JsonResult FetchTPRecommenderDesks(string LoginMID, string AccessType)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_TPRecommenderDesks", LoginMID, AccessType);
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["ID"].ToString());
                dr["Name"] = ds.Tables[0].Rows[i]["Name"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillWATCustomerDropDown(string prefix)
        {
            string Result = string.Empty;
            DalwatAgent dADD = new DalwatAgent();
            ds = new DataSet();
            ds = dADD.FillDropDown("TPCoustomerRecommender", Session["LoginMID"].ToString(), Session["AccessType"].ToString(), prefix);
            if (ds != null && ds.Tables.Count > 0)
            {
                Result = CommonFunctions.ListToStringEnc(ds.Tables[0], "ID");
            }
            ////Result = CommonFunctions.ListToString(ds);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillWATCustomerDevicePlan(string productID)
        {
            string Result = string.Empty;
            DalwatAgent dADD = new DalwatAgent();
            ds = new DataSet();
            if (!string.IsNullOrEmpty(productID))
            {
                productID = EncodeDecode.Decode(productID);
            }
            ds = dADD.GetAgentTPRecommenderRelatedProduct(Convert.ToInt32(productID), Session["LoginMID"].ToString(), Session["Host"].ToString());
            Result = CommonFunctions.ListToString(ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                Result = "{\"DataSet1\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[0]) + "," + "\"DataSet2\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[1]) + "," + "\"DataSet3\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[2]) + "}";
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillWATDropDownTPRecommender(string Type, string ID)
        {
            string Result = string.Empty;
            DalwatAgent dADD = new DalwatAgent();
            ds = new DataSet();
            ds = dADD.FillDropDown(Type, Session["LoginMID"].ToString(), Session["AccessType"].ToString(), ID);
            if (ds != null && ds.Tables.Count > 0)
            {
                Result = CommonFunctions.ListToStringEnc(ds.Tables[0], "ID");
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorkALGDetailEntry(string id)
        {
            try
            {
                WatModel objBO = new WatModel();
                var rd = Request.RequestContext.RouteData;
                if (rd.GetRequiredString("id") != null)
                {
                    TempData["QueryString"] = rd.GetRequiredString("id");
                }
                string ControlsQueryStringEntry = TempData["QueryString"].ToString();
                objBO.LoginMID = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryStringEntry);
                objBO.GlobalUserID = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryStringEntry);
                objBO.AccessType = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryStringEntry);
                objBO.Host = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryStringEntry);
                objBO.UniqueID = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryStringEntry);
                ViewBag.AppName = objBO.AppName;
                objBO.URL = ControlsQueryStringEntry;
                ViewBag.Message = "";
                DalwatAgent da = new DalwatAgent();
                return View(objBO);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "WorkDetailEntry", ex.ToString());
                return View();
            }
        }

        public JsonResult InsertAgentALGTasks(string LoginMID, string GlobalUserID, string WorkGMID, string WorkDMID, string WorkIMID, string WorkReceived, string Host)
        {
            Int64 Result = 0;
            WatModel objWAT = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWAT.LoginMID = LoginMID;
            objWAT.GlobalUserID = GlobalUserID;
            string WorkGroup = EncodeDecode.Decode(WorkGMID);
            objWAT.WorkGMID = WorkGroup.Split('~').First();
            objWAT.CampaignID = WorkGroup.Split('~').Last();
            objWAT.WorkDMID = EncodeDecode.Decode(WorkDMID);
            if (WorkIMID != "0")
            {
                objWAT.WorkIMID = EncodeDecode.Decode(WorkIMID);
                ////string[] WorkItem = EncodeDecode.Decode(WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');              
                ////objWAT.WorkIMID = WorkItem[0];
            }
            objWAT.WorkReceived = WorkReceived;
            objWAT.ActivityDate = DateTime.Now.ToString("yyyy-MM-dd");
            objWAT.StatusDID = "1";
            objWAT.Host = Host;
            Result = objDalWAT.InsertAgentTasks(objWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WorkItemsALGDetailsDataEntry(string LoginMID, string WorkDMID = "", string WorkGMID = "")
        {
            string Result = string.Empty;
            WatModel objWAT = new WatModel();
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            ////DataSet dsWAT = new DataSet();
            ////DataTable dtWAT = new DataTable();
            objWAT.LoginMID = LoginMID;
            objWAT.WorkDMID = EncodeDecode.Decode(WorkDMID);
            objWAT.WorkGMID = EncodeDecode.Decode(WorkGMID).Split('~')[0];
            ds = da.FillDropDown("WorkItemALG", objWAT.WorkGMID, objWAT.LoginMID, objWAT.WorkDMID);
            ////dtWAT.Columns.Add("ID");
            ////dtWAT.Columns.Add("Name");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["ID"] = Convert.IsDBNull(ds.Tables[0].Rows[i]["ID"]) ? default(string) : EncodeDecode.Encode(Convert.ToString(ds.Tables[0].Rows[i]["ID"]));
                    ////DataRow dr = dtWAT.NewRow();
                    ////string WorkIMID = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkIMID"].ToString() + "~" +
                    ////                    ds.Tables[0].Rows[i]["WorkDMID"].ToString() + "~" +
                    ////                    ds.Tables[0].Rows[i]["WorkGMID"].ToString() + "~" +
                    ////                    ds.Tables[0].Rows[i]["CampaignID"].ToString()
                    ////                    + "~" + ds.Tables[0].Rows[i]["AgentWorkDID"].ToString()) + "#~#" +
                    ////                    ds.Tables[0].Rows[i]["TotalWork"].ToString();
                    ////dr["ID"] = WorkIMID;
                    ////dr["Name"] = ds.Tables[0].Rows[i]["WorkItemName"].ToString();
                    ////dtWAT.Rows.Add(dr);
                }
            }
            ////dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(ds);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FetchRecommenderWorkGroups(string LoginMID, string AccessType)
        {
            string Result = string.Empty;
            ds = new DataSet();
            DalwatAgent da = new DalwatAgent();
            ds = da.WAT_RecommenderWorkGorup(LoginMID, AccessType);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Result = CommonFunctions.ListToStringEnc(ds.Tables[0], "ID");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchTPRecommenderWorkGroup(string LoginMID, string AccessType, string WorkGMID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            string WorkGMIDs = string.Empty;
            if (!string.IsNullOrEmpty(WorkGMID))
            {
                WorkGMIDs = EncodeDecode.Decode(WorkGMID);
            }
            ds = da.FillDropDown("WAT_AgentWorkGroups", LoginMID, AccessType);
            //ds = da.FillDropDown("WAT_TPRecommenderDesks", LoginMID, AccessType, WorkGMIDs);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Result = CommonFunctions.ListToStringEnc(ds.Tables[0], "ID");
            }
            //dtWAT.Columns.Add("ID");
            //dtWAT.Columns.Add("Name");
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dr = dtWAT.NewRow();
            //    dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["ID"].ToString());
            //    dr["Name"] = ds.Tables[0].Rows[i]["Name"].ToString();
            //    dtWAT.Rows.Add(dr);
            //}
            //dsWAT.Tables.Add(dtWAT);
            //Result = CommonFunctions.ListToString(dsWAT);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region Activity Tracker EasyJet
        /// <summary>
        /// Action: Opens view ActivityTracker_ALG, Method: Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActivityTracker_EasyJet(string id, string id1)
        {
            WatModel model = new WatModel();
            DalwatAgent da = new DalwatAgent();
            try
            {
                ControlsQueryString = string.Empty;
                var rd = Request.RequestContext.RouteData;
                model.hdnDate = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.ErrorMessage = "";
                if (TempData["WatModel"] != null)
                {
                    model = (WatModel)TempData["WatModel"];
                }
                if (TempData["Message"] != null)
                {
                    ViewBag.ErrorMessage = TempData["Message"];
                }
                if (rd.GetRequiredString("id") != null)
                {
                    ControlsQueryString = rd.GetRequiredString("id");
                    Session["LoginMID"] = Prism.Utility.Querystring.QueryStrData("LoginMID", ControlsQueryString);
                    Session["GlobalUserID"] = Prism.Utility.Querystring.QueryStrData("GlobalUserID", ControlsQueryString);
                    Session["AccessType"] = Prism.Utility.Querystring.QueryStrData("AccessType", ControlsQueryString);
                    Session["Host"] = Prism.Utility.Querystring.QueryStrData("Host", ControlsQueryString);
                    Session["UniqueID"] = Prism.Utility.Querystring.QueryStrData("UniqueID", ControlsQueryString);
                    Session["DefaultClientID"] = Prism.Utility.Querystring.QueryStrData("DefaultClientID", ControlsQueryString);
                }
                ds = da.FillDropDown("WAT_WorkGroups", Session["LoginMID"].ToString(), Session["AccessType"].ToString(), Session["GlobalUserID"].ToString());
                List<Prism.Model.WAT.ListData> objWGData = GetListData(ds.Tables[0], model, "Work Group", "WorkGroupName", "WorkGMID");
                model.LoginMID = Session["LoginMID"].ToString();
                model.GlobalUserID = Session["GlobalUserID"].ToString();
                model.ClientID = (Session["DefaultClientID"] == null ? "242" : Session["DefaultClientID"]).ToString();
                model.AccessType = Session["AccessType"].ToString();
                model.UniqueID = Session["UniqueID"].ToString();
                model.Host = Session["Host"].ToString();
                model.ListData = objWGData;
                model.URL = Utility.Querystring.EncodePairs("LoginMID=" + model.LoginMID + "&GlobalUserID=" + model.GlobalUserID +
                                                            "&AccessType=" + model.AccessType + "&Host=" + model.Host + "&DefaultClientID=" + model.ClientID + "&UniqueID=" + model.UniqueID);
                DataSet dsStatus = da.GetActionStatusDetails_ALG(model);
                DataSet dsDynamicActionStatus = da.GetActionDynamicStatusDetails(model);

                if (dsDynamicActionStatus != null)
                {
                    List<LoadDynamicStatusClass> objLoadDynamicStatus = GetDynamicStatusList(dsDynamicActionStatus.Tables[0], model);
                    model.LoadDynamicStatusForComActivity = objLoadDynamicStatus;
                    model.hidDynamicStatus = strDynamicStatus.ToString();
                }

                if (dsStatus != null && dsStatus.Tables.Count > 1 && dsStatus.Tables[1].Rows.Count > 0)
                {

                    model.ClientID_Empower = dsStatus.Tables[1].Rows[0]["ClientID"].ToString();
                    model.ProjectID = dsStatus.Tables[1].Rows[0]["ProjectID"].ToString();
                    model.StatusDID = dsStatus.Tables[1].Rows[0]["StatusDID"].ToString();
                    model.CurrentStatusID = dsStatus.Tables[1].Rows[0]["CurrentStatusID"].ToString();
                    model.CurrentStatus = dsStatus.Tables[1].Rows[0]["CurrentStatus"].ToString();
                    model.CurrentStatusMessage = dsStatus.Tables[1].Rows[0]["CurrentStatusMessage"].ToString();
                    model.ActionStartDateTime = dsStatus.Tables[1].Rows[0]["ActionStartDateTime"].ToString();
                    model.WorkGMID = EncodeDecode.Encode(dsStatus.Tables[1].Rows[0]["WorkGMID"].ToString());
                    model.CampaignID = dsStatus.Tables[1].Rows[0]["CampaignID"].ToString();
                    model.WorkDMID = EncodeDecode.Encode(dsStatus.Tables[1].Rows[0]["WorkDMID"].ToString());
                    model.WorkIMID = dsStatus.Tables[1].Rows[0]["WorkIMID"].ToString();
                    model.CampaignName = dsStatus.Tables[1].Rows[0]["CampaignName"].ToString();
                    model.WorkGroupName = dsStatus.Tables[1].Rows[0]["WorkGroupName"].ToString();
                    model.WorkDivisionName = dsStatus.Tables[1].Rows[0]["WorkDivisionName"].ToString();
                    model.WorkItemName = dsStatus.Tables[1].Rows[0]["WorkItemName"].ToString();
                    model.WorkCompleted = dsStatus.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    model.AgentWorkDID = dsStatus.Tables[1].Rows[0]["AgentWorkDID"].ToString();
                    model.ActiveWorkStatusDID = dsStatus.Tables[1].Rows[0]["ActiveWorkStatusDID"].ToString();
                    model.ActiveWorkStatus = dsStatus.Tables[1].Rows[0]["ActiveWorkStatus"].ToString();
                    model.TotalWorkCount = dsStatus.Tables[1].Rows[0]["WorkCompleted"].ToString();
                    DateTime dateOne = Convert.ToDateTime(model.ActionStartDateTime);
                    DateTime dateTwo = DateTime.Now;
                    if (model.CurrentStatusID == "7")
                    {

                        LoadDynamicFilter(EncodeDecode.Decode(model.WorkGMID.ToString()), model);
                        model.MultiselectDropdown = MultiselectDropdown.ToString();
                        DataSet ds1 = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                        List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds1.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                        model.ListOutcomeData = objOutcomeData;
                        DataSet ds2 = da.FillDropDown("WATDropDownFill", "1", EncodeDecode.Decode(model.WorkDMID), model.WorkIMID);//hard code Test
                        List<Prism.Model.WAT.ListData> ListQueueData = GetListData(ds2.Tables[0], model, "Select", "Name", "ID");
                        model.ListQueueData = ListQueueData;
                    }
                    string diff = dateTwo.Subtract(dateOne).TotalSeconds.ToString();
                    model.ActionTime = diff;
                }
                DataSet dsAssignment = da.GetSelfAssignment(model);
                if (dsAssignment != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Assignment = Convert.IsDBNull(dsAssignment.Tables[0].Rows[0]["Assignment"]) ? default(bool) : Convert.ToBoolean(dsAssignment.Tables[0].Rows[0]["Assignment"]);
                    model.SelfAssignment = Convert.IsDBNull(dsAssignment.Tables[0].Rows[0]["SelfAssignment"]) ? default(bool) : Convert.ToBoolean(dsAssignment.Tables[0].Rows[0]["SelfAssignment"]);
                }


                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_Easyjet", ex.ToString());
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ActivityTracker_EasyJet(WatModel model, FormCollection form)
        {
            CultureInfo info = new CultureInfo("en-GB");
            DalwatAgent da = new DalwatAgent();
            Int64 result = 0;
            int ValidURN = 1;
            try
            {
                //will be use in future
                ////if (model.ClientID_Empower == "165")
                ////{
                ////    string workIMID = model.WorkIMID;//model.WorkDMID; changed 1st it was on WorkDMID
                ////    if (model.ActionSMID == "22" && EncodeDecode.IsBase64(workIMID))
                ////   {
                ////            model.WorkIMID = EncodeDecode.Decode(workIMID);
                ////   }
                ////}

                if (ValidURN == 1)
                {
                    if (model.WorkDMID == "28")
                    {
                        if (model.StartDateTime != "")
                        {
                            DateTime date_t = Convert.ToDateTime(model.StartDateTime, info);
                            string datetime = date_t.ToString("yyyy-MM-dd") + " " + model.Hour + ":" + model.minute;
                            model.StartDateTime = datetime;
                        }
                    }
                    else
                    {
                        model.StartDateTime = "";
                    }
                    Session["LoginMID"] = model.LoginMID;
                    Session["GlobalUserID"] = model.GlobalUserID;
                    Session["DefaultClientID"] = model.ClientID;
                    Session["AccessType"] = model.AccessType;
                    Session["UniqueID"] = model.UniqueID;
                    Session["Host"] = model.Host;
                    if (model.ActionSMID == "0")
                    {
                        model.ActionSMID = model.CurrentStatusID;
                        result = da.EndWATActionStatus_ALG(model);
                        if (result == 0)
                        {
                            TempData["Message"] = "An error occurred while processing the request. Please try again later";
                            ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                            return View(model);
                        }
                        else
                        {
                            return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                        }
                    }
                    else if (model.ActionSMID == "16")
                    {
                        DalLoginMaster dLoginMaser = new DalLoginMaster();
                        dLoginMaser.LogLogOff(model.UniqueID, model.LoginMID, model.AccessType);
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/Close/");
                    }
                    else if (model.ActionSMID == "2")
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                            model.WorkGMID = WorkItem[2];
                            model.CampaignID = WorkItem[3];
                            model.WorkDMID = WorkItem[1];
                            model.WorkIMID = WorkItem[0];
                            result = da.UpdateWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                            }
                        }
                        else
                        {
                            if (model.Type == "Cancel")
                            {
                                result = da.CancelAutoWrapForActiveWork_ALG(model);//lrft
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                                }
                            }
                            else if (Convert.ToInt32(model.DataValue) <= Convert.ToInt32(model.TotalWorkCount))
                            {
                                string[] DDLIDS = { "1", "3" };
                                model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                                model.QueueID = "1";
                                model.SubQueueID = EncodeDecode.Decode(model.SubQueueID);
                                model.DataValue = "1";
                                model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                                model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);

                                DataSet DsEncryption = da.GetEncryptionTypeStatus(model.WorkGMID, Session["LoginMID"].ToString());
                                if (DsEncryption != null && DsEncryption.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToBoolean(DsEncryption.Tables[0].Rows[0]["Status"].ToString()))
                                    {
                                        XDocument DynamicCol = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                   new XElement("NewDataSet",
                   from DynamicSet in model.LoadDynamicControlForComActivity
                   select new XElement("Table",
                   new XElement("ParaName", DynamicSet.MiscCOLName),
                    new XElement("Value", DDLIDS.Contains(DynamicSet.MiscType) ? AesEncryption.Encode(form["txt" + DynamicSet.MiscMID + ""].ToString()) : AesEncryption.Encode(DynamicSet.MiscValue)),
                    new XElement("ID", DDLIDS.Contains(DynamicSet.MiscType) ? AesEncryption.Encode(form["hdn" + DynamicSet.MiscMID + ""].ToString()) : AesEncryption.Encode(DynamicSet.MiscValue))
                  )));
                                        model.DynamicControls = DynamicCol.ToString();
                                    }
                                    else
                                    {
                                        XDocument DynamicCol = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
                   new XElement("NewDataSet",
                   from DynamicSet in model.LoadDynamicControlForComActivity
                   select new XElement("Table",
                   new XElement("ParaName", DynamicSet.MiscCOLName),
                    new XElement("Value", DDLIDS.Contains(DynamicSet.MiscType) ? form["txt" + DynamicSet.MiscMID + ""].ToString() : DynamicSet.MiscValue),
                    new XElement("ID", DDLIDS.Contains(DynamicSet.MiscType) ? form["hdn" + DynamicSet.MiscMID + ""].ToString() : DynamicSet.MiscValue)
                  )));
                                        model.DynamicControls = DynamicCol.ToString();
                                    }
                                }


                                result = da.ADDUpdateWATWorkDetails_ALG(model);
                                if (result == 0)
                                {
                                    TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                    ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                    return View(model);
                                }
                                else if (result == 3)
                                {
                                    TempData["Message"] = "Duplicate URN exists";
                                    ViewBag.ErrorMessage = "Duplicate URN exists";
                                    TempData["WatModel"] = model;
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                                }
                                else
                                {
                                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                                }
                            }
                            else
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                        }
                    }
                    else if (model.ActionSMID == "22")
                    {
                        if (model.CurrentStatusID != "23")
                        {
                            model.ActionSMID = model.CurrentStatusID;
                            model.ActiveWorkStatus = "22";
                            result = da.EndWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                            }
                        }
                        else
                        {
                            string WorkGroup = EncodeDecode.Decode(model.WorkGMID);
                            model.WorkGMID = WorkGroup.Split('~').First();
                            model.CampaignID = WorkGroup.Split('~').Last();
                            model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                            model.WorkIMID = "0";
                            model.OutcomeMID = EncodeDecode.Decode(model.OutcomeMID);
                            result = da.CompleteTelephoneCall_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                            }
                        }
                    }
                    else
                    {
                        if (model.ActiveWorkStatusDID == "0")
                        {
                            if (model.WorkIMID != "0")
                            {
                                string[] WorkItem = EncodeDecode.Decode(model.WorkIMID.Split(new string[] { "#~#" }, StringSplitOptions.None)[0]).Split('~');
                                model.WorkGMID = WorkItem[2];
                                model.CampaignID = WorkItem[3];
                                model.WorkDMID = WorkItem[1];
                                model.WorkIMID = WorkItem[0];
                            }
                            else
                            {
                                model.WorkDMID = EncodeDecode.Decode(model.WorkDMID);
                                model.WorkGMID = EncodeDecode.Decode(model.WorkGMID);
                            }
                            result = da.UpdateWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                            }
                        }

                        else
                        {
                            if (model.CurrentStatusID == "2" && model.ActionSMID != "22")
                            {
                                model.ActiveWorkStatus = "2";
                            }
                            result = da.EndWATActionStatus_ALG(model);
                            if (result == 0)
                            {
                                TempData["Message"] = "An error occurred while processing the request. Please try again later";
                                ViewBag.ErrorMessage = "An error occurred while processing the request. Please try again later";
                                return View(model);
                            }
                            else
                            {
                                return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                            }
                        }
                    }
                }
                else
                {
                    ds = new DataSet();
                    ds = da.FillDropDown("WAT_OutcomeByWorkItemID", model.LoginMID, model.AccessType, model.WorkIMID);
                    List<Prism.Model.WAT.ListData> objOutcomeData = GetListData(ds.Tables[0], model, "Select", "Outcome", "OutcomeMID");
                    model.ListOutcomeData = objOutcomeData;
                    TempData["Message"] = "Duplicate URN exists";
                    ViewBag.ErrorMessage = "Duplicate URN exists";
                    return Redirect(model.AppName + Path.DirectorySeparatorChar + "WAT/ActivityTracker_Easyjet/" + model.URL);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Problem performing operation, please try later.";
                ErrorLogger.ErrorLog(Path.GetFileName(Request.PhysicalPath), "ActivityTracker_Easyjet", ex.ToString());
                return View(model);
            }
        }


        public JsonResult FetchWorkGroupnew_EasyJet(string LoginMID, string AccessType, string GlobalUserID)
        {
            string Result = string.Empty;
            DalwatAgent da = new DalwatAgent();
            ds = new DataSet();
            DataSet dsWAT = new DataSet();
            DataTable dtWAT = new DataTable();
            ds = da.FillDropDown("WAT_WorkGroups_EasyJet", LoginMID, AccessType, GlobalUserID);
            TempData["workgroup"] = ds;
            dtWAT.Columns.Add("ID");
            dtWAT.Columns.Add("Name");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtWAT.NewRow();
                dr["ID"] = EncodeDecode.Encode(ds.Tables[0].Rows[i]["WorkGMID"].ToString().Split('~')[0]);
                dr["Name"] = ds.Tables[0].Rows[i]["WorkGroupName"].ToString();
                dtWAT.Rows.Add(dr);
            }
            dsWAT.Tables.Add(dtWAT);
            Result = CommonFunctions.ListToString(dsWAT);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FetchCaseFromDataMasterFIFO(string WorkGroupMID, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWM = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWM.LoginMID = LoginMID;
            objWM.hidWorkGroupMID= EncodeDecode.Decode(WorkGroupMID);
            ds = objDalWAT.WatFetchCaseFromDataMasterFIFO(objWM);
            if (ds != null)
            {
                Result = "{\"MainData\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[0]) + "," + "\"RowData\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[1]) + "," + "\"SectorsData\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[2]) + "}";
            }

           

            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FetchCaseSectorFromFormatedData(string Date,string PNR,string UMID, string LoginMID)
        {
            string Result = string.Empty;
            WatModel objWM = new WatModel();
            DalwatAgent objDalWAT = new DalwatAgent();
            objWM.PNRDate = Date;
            objWM.PNRNumber = PNR;
            objWM.UMIDNumber = UMID;
            objWM.LoginMID = LoginMID;
            
            ds = objDalWAT.WatFetchCaseSectorFromFormatedData(objWM);
            if (ds != null)
            {
                Result = "{\"MainData\" :" + CommonFunctions.ListToStringMultiple(ds.Tables[0]) + "}";
            }



            return Json(Result, JsonRequestBehavior.AllowGet);
        }





        #endregion


    }
}
