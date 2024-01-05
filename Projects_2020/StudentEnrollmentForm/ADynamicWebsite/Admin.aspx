<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat ="server">
        </asp:ScriptManager>
        <div>
        <asp:LinkButton ID="LinkButton_Logout" runat="server" ForeColor="Blue" 
                Font-Underline="true" OnClick="LinkButton_Logout_Click">Log Out</asp:LinkButton>
        <br /><br />
        Insert New Lecturer : <br />
        Fullname: <asp:TextBox ID="tb_fullname" runat="server"></asp:TextBox><br />
        Username: <asp:TextBox ID="tb_usernameLec" runat="server"></asp:TextBox><br />
        Password: <asp:TextBox ID="tb_passLec" runat="server"></asp:TextBox><br />
        Notes: <asp:TextBox ID="tb_notes" runat="server"></asp:TextBox><br />
        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label><br />
        <asp:Button ID="btn_signupLec" runat="server" Text="Insert new Lecturer" OnClick="btn_signupLec_Click" />

        <hr />
        Insert New Materials :<br />
            Material Name: <asp:TextBox ID="tb_Mname" runat="server"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="tb_Mname_AutoCompleteExtender" runat="server"
                MinimumPrefixLength="1" CompletionInterval="1" ServiceMethod="GetCompletionList"
                Enabled="True" TargetControlID="tb_Mname"></ajaxToolkit:AutoCompleteExtender><br />
        Lecturer Name: <asp:DropDownList ID="ddl_Lec" runat="server" DataSourceID="SqlDataSource_Lectures" 
            DataTextField="fullname" DataValueField="Id"></asp:DropDownList><br />
         Material Notes: <asp:TextBox ID="tb_MNotes" runat="server"></asp:TextBox><br />
        <asp:Label ID="lb_msgM" runat="server" Text="" ForeColor="Red"></asp:Label><br />
        <asp:Button ID="btn_insertM" runat="server" Text="Insert new Material Information" OnClick="btn_insertM_Click" />


        <hr />
        <div style="text-align: center;"><asp:Label ID="Label2" runat="server" Text="Table of Students" Font-Size="X-Large" ></asp:Label></div>
        <asp:GridView ID="GridView_Students" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" 
            DataSourceID="SqlDataSource_Students" style ="margin:0 auto; text-align: center; ">
            <HeaderStyle BackColor ="Teal" ForeColor="White" />
                <EmptyDataTemplate>
                    <asp:Label ID="Students" runat="server" Text="There are no students" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
                <asp:BoundField DataField="pass" HeaderText="Password" SortExpression="pass" />
                <asp:BoundField DataField="fullname" HeaderText="Full Name" SortExpression="fullname" />
                <asp:BoundField DataField="notes" HeaderText="Notes" SortExpression="notes" />
                <asp:BoundField DataField="active" HeaderText="Activation Status" SortExpression="active" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource_Students" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnStr %>" 
            SelectCommand="SELECT * FROM [Students]" 
            DeleteCommand="DELETE FROM [Students] WHERE [Id] = @Id" 
            InsertCommand="INSERT INTO [Students] ([username], [pass], [fullname], [notes], [active]) VALUES (@username, @pass, @fullname, @notes, @active)" 
            UpdateCommand="UPDATE [Students] SET [username] = @username, [pass] = @pass, [fullname] = @fullname, [notes] = @notes, [active] = @active WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="pass" Type="String" />
                <asp:Parameter Name="fullname" Type="String" />
                <asp:Parameter Name="notes" Type="String" />
                <asp:Parameter Name="active" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="pass" Type="String" />
                <asp:Parameter Name="fullname" Type="String" />
                <asp:Parameter Name="notes" Type="String" />
                <asp:Parameter Name="active" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />


        <div style="text-align: center;"><asp:Label ID="Label1" runat="server" Text="Table of Lectures" Font-Size="X-Large" ></asp:Label></div>
        <asp:GridView ID="GridView_Lectures" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" 
            DataSourceID="SqlDataSource_Lectures" style ="margin:0 auto; text-align: center;">
                            <EmptyDataTemplate>
                    <asp:Label ID="Lecture" runat="server" Text="There are no students" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="Id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="username" HeaderText="Lecture User Name" SortExpression="username" />
                <asp:BoundField DataField="pass" HeaderText="Password" SortExpression="pass" />
                <asp:BoundField DataField="fullname" HeaderText="Full Name" SortExpression="fullname" />
                <asp:BoundField DataField="notes" HeaderText="Notes" SortExpression="notes" />
                <asp:BoundField DataField="active" HeaderText="Activation Status" SortExpression="active" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource_Lectures" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnStr %>" 
            SelectCommand="SELECT * FROM [Lectures]" 
            DeleteCommand="DELETE FROM [Lectures] WHERE [Id] = @Id" 
            InsertCommand="INSERT INTO [Lectures] ([username], [pass], [fullname], [notes], [active]) VALUES (@username, @pass, @fullname, @notes, @active)" 
            UpdateCommand="UPDATE [Lectures] SET [username] = @username, [pass] = @pass, [fullname] = @fullname, [notes] = @notes, [active] = @active WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="pass" Type="String" />
                <asp:Parameter Name="fullname" Type="String" />
                <asp:Parameter Name="notes" Type="String" />
                <asp:Parameter Name="active" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="pass" Type="String" />
                <asp:Parameter Name="fullname" Type="String" />
                <asp:Parameter Name="notes" Type="String" />
                <asp:Parameter Name="active" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />


        <div style="text-align: center"><asp:Label ID="Label3" runat="server" Text="Table of Materials" Font-Size="X-Large" ></asp:Label></div>
        <asp:GridView ID="GridView_Materials" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Materials"
                style ="margin:0 auto; text-align: center;">
                <HeaderStyle BackColor ="Teal" ForeColor="White" />
              <EmptyDataTemplate>
                    <asp:Label ID="Materials" runat="server" Text="There are no Materials" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="MName" HeaderText="Material Name" SortExpression="MName" />
                    <asp:BoundField DataField="Notes" HeaderText="Material Notes" SortExpression="notes" />
                    <asp:BoundField DataField="fullname" HeaderText="Lecturer Name" SortExpression="fullname" />
                    <asp:BoundField DataField="Expr1" HeaderText="Lecturer Notes" SortExpression="Expr1" />
                </Columns>
            </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource_Materials" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnStr %>" 
                SelectCommand="SELECT Materials.MName, Materials.Notes, Lectures.fullname, Lectures.notes AS Expr1 FROM Lectures INNER JOIN Materials ON Lectures.Id = Materials.Id"></asp:SqlDataSource>
     </div>

    </form>
</body>
</html>
