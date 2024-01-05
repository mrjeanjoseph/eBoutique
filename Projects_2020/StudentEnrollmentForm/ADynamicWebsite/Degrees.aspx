<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Degrees.aspx.cs" Inherits="Degrees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align:center;">
        <asp:LinkButton ID="LinkButton_Logout" runat="server" ForeColor="Blue" 
        Font-Underline="true" OnClick="LinkButton_Logout_Click">Log Out</asp:LinkButton>
        <br />
        Insert New Materials :<br />
        Student Name: 
            <asp:DropDownList ID="ddl_Sname" runat="server" DataSourceID="SqlDataSource_Students"
            DataTextField="fullname" DataValueField="Id"></asp:DropDownList>
            <br />
         Degrees: <asp:TextBox ID="tb_degree" runat="server"></asp:TextBox><br />
         Notes: <asp:TextBox ID="tb_notes" runat="server"></asp:TextBox><br />
        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label><br />
        <asp:Button ID="btn_insertDegree" runat="server" Text="Insert Degree" OnClick="btn_insertDegree_Click"/>
                    <asp:SqlDataSource ID="SqlDataSource_Students" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnStr %>" 
                SelectCommand="SELECT * FROM [Students] WHERE ([active] = @active)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="active" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
        <hr />
            <div style="text-align:center;"><asp:Label ID="Label3" runat="server" Text="Table of Materials" Font-Size="X-Large" ></asp:Label></div>
            <asp:GridView ID="GridView_Degrees" runat="server" AutoGenerateColumns="False" Cellpadding="15"
                DataSourceID="SqlDataSource_Degrees" style="margin: 0 auto; text-align:center;">
              <HeaderStyle BackColor ="Teal" ForeColor="White" Width="200px" />
              <EmptyDataTemplate>
                    <asp:Label ID="Materials" runat="server" Text="There are no Degrees" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>

                <Columns>
                    <asp:BoundField DataField="MName" HeaderText="MName" SortExpression="MName" />
                    <asp:BoundField DataField="deg" HeaderText="deg" SortExpression="deg" />
                    <asp:BoundField DataField="fullname" HeaderText="fullname" SortExpression="fullname" />
                    <asp:BoundField DataField="notes" HeaderText="notes" SortExpression="notes" />
                </Columns>
            </asp:GridView>        
            <asp:SqlDataSource ID="SqlDataSource_Degrees" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnStr %>">
        </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
