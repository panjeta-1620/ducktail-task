using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentApplicationEntities;
using StudentApplicationDAL;

namespace StudentApplication.AccountPages
{
    public partial class StudentRegistration : System.Web.UI.Page
    {
        // string idToUpdate = Page.Request.QueryString["StudentID"];
        int idToUpdate = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 idToUpdate = Convert.ToInt32(Request.QueryString["StudentID"]);


                if (!Page.IsPostBack)
                {
                    BindClasses();
                }
                if(idToUpdate>0)
                {
                    LoadData(idToUpdate);
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void LoadData(int Id)
        {
            try
            {
                StudentDAL studentDAL = new StudentDAL();
                StudentEntity stuField = new StudentEntity();
                //List<SubjectEntity> subjectField = new List<SubjectEntity>();
                studentDAL.GetStudentById(Id, out stuField);
                txtFname.Text = stuField.FirstName;
                txtLname.Text = stuField.LastName;
                ddlClass.SelectedIndex = Convert.ToInt32(stuField.ClassID);
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
           
            
        }
        public void BindClasses()
        {
           StudentDAL stu = new StudentDAL();
            var cls = stu.GetClasses();
            var clsLine = new ClassEntity();
            clsLine.ClassID = -1;
            clsLine.ClassName = "Select Class";
            cls.Insert(0, clsLine);
            ddlClass.DataSource = cls;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassID";
            ddlClass.DataBind();
        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<SubjectItem> items = GetRepeaterItems();
                BindSubjects(items);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private List<SubjectItem> GetRepeaterItems()
        {
            List<SubjectItem> items = new List<SubjectItem>();
            List<StudentSubjectMarks> subjectMarks = new List<StudentSubjectMarks>();

            foreach (RepeaterItem item in rptSubject.Items)
            {
                var ddlSubject = item.FindControl("ddlSubject") as DropDownList;
                var txtMarks = item.FindControl("txtMarks") as TextBox;
                SubjectItem sItem = new SubjectItem();
                sItem.ItemIndex = item.ItemIndex;
                MarksItem sMarks = new MarksItem();
                sMarks.ItemIndex = item.ItemIndex;


                if (!string.IsNullOrEmpty(ddlSubject.SelectedValue) && ddlSubject.SelectedValue != "-1")
                {
                    sItem.SubjectID = Convert.ToInt32(ddlSubject.SelectedValue);
                    sItem.SubjectName = ddlSubject.SelectedItem.Text;
                   // sItem.Marks= Convert.TO
                }
                if(!string.IsNullOrEmpty(txtMarks.Text)&& txtMarks.Text!=null)
                {
                    sMarks.SubjectID = Convert.ToInt32(ddlSubject.SelectedValue);
                    sMarks.Marks = Convert.ToDecimal(txtMarks.Text);
                }
                items.Add(sItem);

            }
            return items;
        }
        private void BindSubjects(List<SubjectItem> items)
        {
            if (items.Count == 0)
            {
                items.Insert(0, new SubjectItem { ItemIndex = 0, SubjectID = null });
            }

            rptSubject.DataSource = items;
            rptSubject.DataBind();
        }
        List<SubjectEntity> subjects = null;
        private void BindSubject(DropDownList ddlSubjectDropdowm, int? selectedSubjectID)
        {
            StudentDAL dalLayer = new StudentDAL();

            if (subjects == null)
            {
                subjects = dalLayer.GetSubjects(Convert.ToInt32(ddlClass.SelectedValue));
                subjects.Insert(0, new SubjectEntity { SubjectID = -1, SubjectName = "Select" });
            }

            ddlSubjectDropdowm.DataSource = subjects;
            ddlSubjectDropdowm.DataTextField = "SubjectName";
            ddlSubjectDropdowm.DataValueField = "SubjectID";
            ddlSubjectDropdowm.DataBind();
            if (selectedSubjectID.HasValue)
            {
                ddlSubjectDropdowm.SelectedValue = Convert.ToString(selectedSubjectID);
            }
        }
        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ddlSubject = e.Item.FindControl("ddlSubject") as DropDownList;

                var subjectItem = e.Item.DataItem as SubjectItem;

                BindSubject(ddlSubject, subjectItem.SubjectID);

                ddlSubject.SelectedValue = Convert.ToString(subjectItem.SubjectID);

                if (e.Item.ItemIndex == 0)
                {
                    var btnRemove = e.Item.FindControl("btnRemoveSubject") as Button;
                    btnRemove.Visible = false;
                }
            }
        }

        protected void btnAddDdlSubject_Command(object sender, CommandEventArgs e)
        {
            int itemIndex = Convert.ToInt32(e.CommandArgument);

            List<SubjectItem> items = GetRepeaterItems();

            items.Insert(itemIndex + 1, new SubjectItem { ItemIndex = itemIndex + 1 });

            BindSubjects(items);
            FixSubjectDropdown(items);
        }
        protected void btnRemoveSubject_Command(object sender, CommandEventArgs e)
        {

            int itemIndex = Convert.ToInt32(e.CommandArgument);

            var items = GetRepeaterItems();

            items.RemoveAll(x => x.ItemIndex == itemIndex);

            BindSubjects(items);
            FixSubjectDropdown(items);
        }
        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                List<SubjectEntity> subject = new List<SubjectEntity>();
                List<SubjectItem> subjects = GetRepeaterItems();
                List<int> subList = new List<int>();
                StudentEntity stu = new StudentEntity();
                StudentDAL stuDAL = new StudentDAL();

                stu.FirstName = txtFname.Text;
                stu.LastName = txtLname.Text;
                stu.ClassID = ddlClass.SelectedIndex;
                stu.SubjectsIDs = subjects.Select(x => x.SubjectID ?? 0).ToList();
                    int studentID = stuDAL.InsertStudentData(stu);
               
                   // int studentID = stuDAL.InsertStudentData(stu);
                    if (studentID != 0)
                    {
                        lblSaveMessage.Text = "Student Data Inserted Succesfully!!";

                    }
                else
                {
                    lblSaveMessage.Text = "Student Data Updated Succesfully!!";
                }


                
               
              //  stu.Marks = subjects.Select(x => x.Marks).ToList();
               

               
                Response.Redirect("~/AccountPages/DisplayStudentData.aspx");
                RefreshPage();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }

        }
        public void RefreshPage()
        {
          
            txtFname.Text = "";
            txtLname.Text = "";       
            ddlClass.SelectedValue = "-1";
            rptSubject.DataSource = null;
            rptSubject.DataBind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshPage();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

       
       
        private void FixSubjectDropdown(List<SubjectItem> items)
        {
            List<int> selectedSubjects = items.Where(x => x.SubjectID.HasValue).Select(x => x.SubjectID.Value).ToList();

            foreach (RepeaterItem rptItem in rptSubject.Items)
            {
                var ddlSubject = rptItem.FindControl("ddlSubject") as DropDownList;

                int currentItemSelectedSubject = 0;

                if (!string.IsNullOrEmpty(ddlSubject.SelectedValue) && ddlSubject.SelectedValue != "-1")
                {
                    currentItemSelectedSubject = Convert.ToInt32(ddlSubject.SelectedValue);
                }

                foreach (var selectedSubject in selectedSubjects)
                {
                    if (selectedSubject != currentItemSelectedSubject)
                    {
                        var ddlItem = ddlSubject.Items.FindByValue(selectedSubject.ToString());

                        ddlSubject.Items.Remove(ddlItem);
                    }
                }

                if (ddlSubject.Items.Count > 2)
                {
                    (rptItem.FindControl("btnAddDdlSubject") as Button).Visible = true;
                }
                else
                {
                    (rptItem.FindControl("btnAddDdlSubject") as Button).Visible = false;
                }
            }
        }
        public void HandleException(Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                var items = GetRepeaterItems();
                FixSubjectDropdown(items);
            
        }
    }
}