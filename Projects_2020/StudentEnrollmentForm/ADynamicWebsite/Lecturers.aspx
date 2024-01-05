<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lecturers.aspx.cs" Inherits="Lecturers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center;">
        <asp:LinkButton ID="LinkButton_Logout" runat="server" ForeColor="Blue" 
        Font-Underline="true" OnClick="LinkButton_Logout_Click">Log Out</asp:LinkButton>
        <br /><br />
        <div style="text-align: center"><asp:Label ID="Label2" runat="server" Text="Table of Materials" Font-Size="X-Large" ></asp:Label></div>
        <asp:GridView ID="GridView_Materials" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_OnRowDataBound"
            DataSourceID="SqlDataSource_Materials" style ="margin:0 auto; text-align: center; ">
            <HeaderStyle BackColor ="Teal" ForeColor="White" />
                <EmptyDataTemplate>
                    <asp:Label ID="Materials" runat="server" Text="There are no Materials" ForeColor="Red"></asp:Label>
                </EmptyDataTemplate>
            <Columns>                
                <asp:TemplateField>
                    <HeaderStyle Width="200px" /> 
                    <HeaderTemplate>Material Name</HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hf_id" runat="server" Value='<%# Eval("Id") %>' />
                        <asp:HyperLink ID="HyperLink_M" NavigateUrl="~/Materials.aspx" runat="server"><%# Eval("MName") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField>
                <HeaderStyle Width="200px" />
                    <HeaderTemplate>Material Notes</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_notes" runat="server" Text='<%# Eval("notes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource_Materials" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnStr %>" 
            SelectCommand="SELECT [Id], [MName], [notes] FROM [Materials] WHERE ([LID] = @LID)" >
            <SelectParameters>
                <asp:SessionParameter Name="LID" SessionField="LID" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
