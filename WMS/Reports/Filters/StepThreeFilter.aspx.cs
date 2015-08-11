﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports.Filters
{
    public partial class StepThreeFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewDepartment("");
                BindGridViewType("");
                //dateFrom.Value = "2015-08-09";
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewDepartment, Session["FiltersModel"] as FiltersModel, "Department");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewType, Session["FiltersModel"] as FiltersModel, "Type");
            }
        }
        protected void ButtonSearchDepartment_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            BindGridViewDepartment(tbSearch_Department.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewDepartment, Session["FiltersModel"] as FiltersModel, "Department");
        }
        protected void ButtonSearchType_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveTypeIDs();
            BindGridViewType(tbSearch_Type.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewType, Session["FiltersModel"] as FiltersModel, "Type");
        }
        protected void GridViewDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();

            //change page index
            GridViewDepartment.PageIndex = e.NewPageIndex;
            BindGridViewDepartment("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewDepartment, Session["FiltersModel"] as FiltersModel, "Department");
        }

        protected void GridViewType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveTypeIDs();

            //change page index
            GridViewType.PageIndex = e.NewPageIndex;
            BindGridViewType("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewType, Session["FiltersModel"] as FiltersModel, "Type");
        }

        private void SaveDepartmentIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewDepartment, Session["FiltersModel"] as FiltersModel, "Department");
            Session["FiltersModel"] = FM;
        }
        private void SaveTypeIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewType, Session["FiltersModel"] as FiltersModel, "Type");
            Session["FiltersModel"] = FM;
        }

        private void BindGridViewDepartment(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Department> _View = new List<Department>();
            List<Department> _TempView = new List<Department>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyFilters(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Department " + query);
            _View = dt.ToList<Department>();
            if (fm.DivisionFilter.Count > 0)
            {
                foreach (var comp in fm.DivisionFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.DivID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewDepartment.DataSource = _View.Where(aa => aa.DeptName.Contains(search)).ToList();
            GridViewDepartment.DataBind();
        }

        private void BindGridViewType(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<EmpType> _View = new List<EmpType>();
            List<EmpType> _TempView = new List<EmpType>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanySegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from EmpType " + query);
            _View = dt.ToList<EmpType>();
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CompanyID == _compID).ToList());
                }
                _View = _TempView;
            }
            GridViewType.DataSource = _View.Where(aa => aa.TypeName.Contains(search)).ToList();
            GridViewType.DataBind();
        }

        #region Navigation Buttons
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            SaveTypeIDs();
            // Go to the next page
            string url = "~/Reports/Filters/StepFourFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            // Go to the next page
            string url = "~/Filters/DeptFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonFinish_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            SaveTypeIDs();
            // Go to the next page
            string url = "~/Reports/ReportContainer.aspx";
            Response.Redirect(url);
        }
        #endregion

        protected void GridViewDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewDepartment.PageIndex + 1) + " of " + GridViewDepartment.PageCount;
            }
        }

        protected void GridViewType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewType.PageIndex + 1) + " of " + GridViewType.PageCount;
            }
        }
    }
}