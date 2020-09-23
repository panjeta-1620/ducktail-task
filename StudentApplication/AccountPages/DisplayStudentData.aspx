<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayStudentData.aspx.cs" Inherits="StudentApplication.AccountPages.DisplayStudentData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Application</title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" Font-Size="X-Large" ForeColor="Red"></asp:Label>
        <div>
            <asp:Panel ID="pnlGrid" runat="server" BorderStyle="Double">

                <table id="tblGrid" runat="server">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtKeyword" runat="server" OnTextChanged="txtKeyword_TextChanged" AutoPostBack="True" />
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnAdd" runat="server" CausesValidation="false" OnClick="btnAdd_Click" Text="Add Student" />
                          
                        </td>
                       
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvStudentData" runat="server" CellPadding="4" RowStyle-BackColor="#E6E6E6"
                                Font-Italic="True" Font-Size="small" HorizontalAlign="Left" AllowPaging="false"
                                Style="margin-bottom: 0px" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="2px"
                                AutoGenerateEditButton="true" OnRowEditing="gvStudentData_RowEditing"
                                OnRowDataBound="gvStudentData_RowDataBound"
                                OnRowDeleting="gvStudentData_RowDeleting">


                                <Columns>
                                    <asp:BoundField DataField="StudentID" HeaderText="StudentID" />

                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" />

                                    <asp:BoundField DataField="Class" HeaderText="Class" />
                                    <asp:BoundField DataField="Subject" HeaderText="Subject" />

                                    <asp:CommandField ButtonType="Link" ShowDeleteButton="true" />

                                </Columns>
                                <EmptyDataTemplate>Search to view Data</EmptyDataTemplate>
                                <HeaderStyle BackColor="#9900ff" Font-Bold="True" ForeColor="#CCCCFF" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>

                </table>


            </asp:Panel>
        </div>
    </form>
</body>
</html>
