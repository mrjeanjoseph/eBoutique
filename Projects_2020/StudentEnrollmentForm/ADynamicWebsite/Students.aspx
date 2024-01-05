<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Students.aspx.cs" Inherits="Students" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:LinkButton ID="LinkButton_Logout" runat="server" ForeColor="Blue"
                Font-Underline="true" OnClick="LinkButton_Logout_Click">Log Out</asp:LinkButton>
            <br />
            <div style="text-align: center">
                <asp:Label ID="Label2" runat="server" Text="Table of Degrees" Font-Size="X-Large"></asp:Label></div>
            <asp:GridView ID="GridView_Students" runat="server" AutoGenerateColumns="False"
                DataSourceID="SqlDataSource_Degrees" Style="margin: 0 auto; text-align: center;">
                <HeaderStyle BackColor="Teal" ForeColor="White" />
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="There are no Degrees" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>

                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle Width="200px" />
                        <HeaderTemplate>Material Name</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="HyperLink_M" runat="server" Text='<%# Eval("MName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle Width="200px" />
                        <HeaderTemplate>Degree</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_deg" runat="server" Text='<%# Eval("deg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle Width="200px" />
                        <HeaderTemplate>Notes</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_notes" runat="server" Text='<%# Eval("notes") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource_Degrees" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnStr %>"
                SelectCommand="SELECT Degrees.deg, Degrees.notes, Materials.MName FROM Degrees INNER JOIN Materials ON Degrees.MID = Materials.Id"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
