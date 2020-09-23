<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="StudentApplication.AccountPages.StudentRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>

    <div class="container">
        <div>
            <h1>Insert Student</h1>
        </div>
       <div class="row">
           <div class="col-lg-6 offset-3">
    <form runat="server" id="fmRegistration">
          <div class="form-group">
               <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" Font-Size="X-Large" ForeColor="Red"></asp:Label>
              </div>
        <div class="form-group">
                            <asp:Label ID="lblSaveMessage" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                          
        </div>
        <div class="form-group">
              <asp:Label ID="lblFname" runat="server" Text="FirstName"></asp:Label>
              <asp:TextBox CssClass="form-control" ID="txtFname" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" ControlToValidate="txtFname"
                                    ErrorMessage="Enter a legal First Name" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revFname" Text="Invalid Value!!" ForeColor="Red" ControlToValidate="txtFname" runat="server"
                                    ValidationExpression="^[A-Za-z]+"></asp:RegularExpressionValidator>
                            
        </div>
        <div class="form-group">
              <asp:Label ID="lblLname" runat="server" Text="LastName"></asp:Label>
             <asp:TextBox ID="txtLname" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLname" ControlToValidate="txtLname"
                                    ErrorMessage="Enter a legal Last  Name" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revLname" Text="Invalid Value!!" ForeColor="Red" ControlToValidate="txtLname" runat="server" ValidationExpression="^[A-Za-z]+"></asp:RegularExpressionValidator>

        </div>
          <div class="form-group">
                <asp:Label ID="lblClass" runat="server" Text="Class"></asp:Label>
               <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvCLass" Display="Dynamic" ControlToValidate="ddlClass"
                                    ErrorMessage="Class is required." Text="*" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>


        </div>
          <div class="form-group">
               <asp:Repeater ID="rptSubject" runat="server" OnItemDataBound="rptSubject_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="table" style="border: 0px; border-color: black; text-align: left">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <label style="height: 24px; width: 180px">
                                                    Subject<%# (Container.ItemIndex + 1).ToString() %></label></td>
                                            <td>&nbsp;
                                                        &nbsp;
                                                        :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubject" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" runat="server"></asp:DropDownList>
<%--                                               Marks: <asp:TextBox ID="txtMarks" runat="server"></asp:TextBox>--%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddDdlSubject" runat="server" CausesValidation="false" Text="+" CommandArgument="<%# Container.ItemIndex %>" OnCommand="btnAddDdlSubject_Command" />

                                            </td>
                                            <td>
                                                <asp:Button ID="btnRemoveSubject" runat="server" CausesValidation="false" Text="-" CommandArgument="<%# Container.ItemIndex %>" OnCommand="btnRemoveSubject_Command" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="ddlSubject"
                                                    ErrorMessage="Class is required." InitialValue="-1" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
        </div>
         <div class="form-group">
              <asp:Button ID="btnSaveData" class="btn btn-success"  CausesValidation="true" runat="server" Text="Save" OnClick="btnSaveData_Click" />
            <asp:Button ID="btnClear" runat="server" class="btn btn-light" Text="Clear" CausesValidation="false" OnClick="btnClear_Click" />
                          
        </div>
       
    </form>
        </div>
        </div>
        </div>
</body>
</html>
