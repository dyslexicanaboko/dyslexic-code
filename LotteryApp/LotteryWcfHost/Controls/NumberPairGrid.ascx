<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumberPairGrid.ascx.cs" Inherits="LotteryWcfHost.Controls.NumberPairGrid" %>
<asp:GridView ID="gvw" runat="server" AutoGenerateColumns="False" 
    CellPadding="4" ForeColor="#333333" GridLines="None" 
    onrowdatabound="gvw_RowDataBound">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>
    <asp:TemplateField HeaderText="Numbers">
        <ItemTemplate>
            <%-- Color Storage for Winning Lottery Numbers --%>
            <asp:Label ID="lblLColor1" runat="server" Text='<%# Bind("LColor1") %>' Visible="false" />
            <asp:Label ID="lblLColor2" runat="server" Text='<%# Bind("LColor2") %>' Visible="false" />
            <asp:Label ID="lblLColor3" runat="server" Text='<%# Bind("LColor3") %>' Visible="false" />
            <asp:Label ID="lblLColor4" runat="server" Text='<%# Bind("LColor4") %>' Visible="false" />
            <asp:Label ID="lblLColor5" runat="server" Text='<%# Bind("LColor5") %>' Visible="false" />
            <asp:Label ID="lblLColor6" runat="server" Text='<%# Bind("LColor6") %>' Visible="false" />

            <%-- Color Storage for Player Lottery Numbers --%>
            <asp:Label ID="lblPColor1" runat="server" Text='<%# Bind("PColor1") %>' Visible="false" />
            <asp:Label ID="lblPColor2" runat="server" Text='<%# Bind("PColor2") %>' Visible="false" />
            <asp:Label ID="lblPColor3" runat="server" Text='<%# Bind("PColor3") %>' Visible="false" />
            <asp:Label ID="lblPColor4" runat="server" Text='<%# Bind("PColor4") %>' Visible="false" />
            <asp:Label ID="lblPColor5" runat="server" Text='<%# Bind("PColor5") %>' Visible="false" />
            <asp:Label ID="lblPColor6" runat="server" Text='<%# Bind("PColor6") %>' Visible="false" />

            <%-- Lottery Number Matrix --%>
            <table id="tblNumbers" runat="server" style="border:1px solid black;">
                <tr>
                    <td><asp:TextBox ID="txtLotto1" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto1") %>' /></td>
                    <td><asp:TextBox ID="txtLotto2" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto2") %>' /></td>
                    <td><asp:TextBox ID="txtLotto3" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto3") %>' /></td>
                    <td><asp:TextBox ID="txtLotto4" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto4") %>' /></td>
                    <td><asp:TextBox ID="txtLotto5" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto5") %>' /></td>
                    <td><asp:TextBox ID="txtLotto6" Width="20px" ReadOnly="true" runat="server" Text='<%# Bind("Lotto6") %>' /></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtPlayer1" Width="20px" runat="server" Text='<%# Bind("Player1") %>' /></td>
                    <td><asp:TextBox ID="txtPlayer2" Width="20px" runat="server" Text='<%# Bind("Player2") %>' /></td>
                    <td><asp:TextBox ID="txtPlayer3" Width="20px" runat="server" Text='<%# Bind("Player3") %>' /></td>
                    <td><asp:TextBox ID="txtPlayer4" Width="20px" runat="server" Text='<%# Bind("Player4") %>' /></td>
                    <td><asp:TextBox ID="txtPlayer5" Width="20px" runat="server" Text='<%# Bind("Player5") %>' /></td>
                    <td><asp:TextBox ID="txtPlayer6" Width="20px" runat="server" Text='<%# Bind("Player6") %>' /></td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Matches">
        <ItemTemplate>
            <asp:Label ID="lblMatch" runat="server" Text='<%# Bind("MatchCount", "{0} of 6") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Status">
        <ItemTemplate>
            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
</Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
