<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanList.ascx.cs" Inherits="Control_PlanList" %>
<form id="form1" runat="server">
<div id="gridView">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="5"
        GridLines="Horizontal" Width="700px" BorderWidth="1px" BorderColor="#E7E7FF"
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
        <Columns>
            <asp:TemplateField HeaderText="标题">
                <HeaderStyle HorizontalAlign="Left " />
                <ItemStyle Width="56%" Height="22px" />
                <ItemTemplate>
                    <li><a class="edit" href='planContent.aspx?StationPlanID=<%# Eval("StationPlanID")%>'>
                        <%# Eval("PlanName").ToString().Length > 20 ? Eval("PlanName").ToString().Substring(0, 20) + " …… " : Eval("PlanName")%></a>
                        &#8195;&#8195;&nbsp;&nbsp; &nbsp; </a></li>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作者">
                <HeaderStyle HorizontalAlign="Left " />
                <ItemStyle Width="22%" Height="22px" />
                <ItemTemplate>
                    <%# Eval("PlanAuthor")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <HeaderStyle HorizontalAlign="Left " />
                <ItemStyle Width="22%" Height="22px" />
                <ItemTemplate>
                    <%# Eval("PlanDate")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#d9d8d8" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#d9d8d8" Font-Bold="True" ForeColor="#161617" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#F9F9F9" ForeColor="#161617" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
    </asp:GridView>
</div>
</form>
