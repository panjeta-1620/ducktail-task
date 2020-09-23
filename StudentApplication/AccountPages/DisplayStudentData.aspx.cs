using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplicationDAL;
using StudentApplicationEntities;

namespace StudentApplication.AccountPages
{
    public partial class DisplayStudentData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }

        }

        protected void gvStudentData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                StudentDAL studentDAL = new StudentDAL();
                StudentEntity stud = new StudentEntity();
                int stuID = Convert.ToInt32(gvStudentData.Rows[e.NewEditIndex].Cells[1].Text);
                string idToUpdate = Request.QueryString["stuID"];
                Response.Redirect("~/AccountPages/StudentRegistration.aspx?StudentID="+stuID);
                stud.StudentID = stuID;
                //studentDAL.DeleteStudentData(stud);
                BindGrid();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
           
    }



protected void gvStudentData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvStudentData.EditIndex)
                {
                    (e.Row.Cells[6].Controls[0] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";                   
                    //BindGrid();

                }
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
           

        }
        protected void gvStudentData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                StudentDAL studentDAL = new StudentDAL();
                StudentEntity stud = new StudentEntity();
                int stuID = Convert.ToInt32(gvStudentData.Rows[e.RowIndex].Cells[1].Text);
                stud.StudentID = stuID;
                studentDAL.DeleteStudentData(stud);
                BindGrid();


            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }



        private void BindGrid()
        {
            try
            {
                StudentDAL stuDAL = new StudentDAL();
                var gridData = stuDAL.GetStudentData();
                gvStudentData.DataSource = gridData;
                gvStudentData.DataBind();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
           
          
        }
        public void HandleException(Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountPages/StudentRegistration.aspx");
        }

        protected void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StudentDAL stuDAL = new StudentDAL();
                ShowDatainGrid stu = new ShowDatainGrid();
                stu.Keyword = txtKeyword.Text;
                var searchData= stuDAL.SearchData(stu);
                gvStudentData.DataSource = searchData;
                gvStudentData.DataBind();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected void ddlFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}