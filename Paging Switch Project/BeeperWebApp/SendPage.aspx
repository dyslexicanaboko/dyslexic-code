<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="SendPage.aspx.cs" Inherits="BeeperWebApp.SendPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ajaxScriptManager" runat="server">
    </asp:ScriptManager>
    <div><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
    <div>
        <table border="0">
            <tr>
                <td colspan="2" style="font-weight:bold; font-size:larger; text-align:center;">
                    Send a Page
                </td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Subscriber ID</td>
                <td>
                    <asp:TextBox ID="txtSubscriberID" runat="server" CausesValidation="True" 
                        MaxLength="4" Width="43px" Font-Size="Large"></asp:TextBox>
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtSubscriberID" ErrorMessage="Must be a 4 digit number" 
                        ValidationExpression="\d+"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Message</td>
                <td>
                    <asp:TextBox 
                        ID="txtMessageText" 
                        runat="server"
                        Text=""
                        Rows="5"
                        ToolTip="Messages over 500 characters are sent as multiple messages." 
                        TextMode="MultiLine" 
                        Width="327px" 
                        Columns="10" />
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" Enabled="true" 
                        ControlToValidate="txtMessageText" ErrorMessage="Message cannot be blank"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Options</td>
                <td>
                    <asp:CheckBox ID="chkBxPreserveData" runat="server" AutoPostBack="false" Text="Keep information on the form after sending Page?" />
                </td>
            </tr>
            </table>
            <br />
            <div>Note: Hidden grids will not get messages sent to them, even if they have selected elements.</div>            
            <br />
            <asp:UpdatePanel 
                ID="ajaxUpdatePanel" 
                runat="server"
                UpdateMode="Conditional"
                RenderMode="Inline">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td style="vertical-align:top;">
                                <asp:LinkButton ID="lnkBtnShowOwners" runat="server" 
                                    onclick="lnkBtnShowOwners_Click">Show Owners</asp:LinkButton>
                            </td>
                            <td>
                                <asp:GridView 
                                ID="gvwOwners" 
                                runat="server"
                                Visible="false" 
                                AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#999999" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <Columns>
                                    <asp:BoundField DataField="PagerOwnerID" HeaderText="ID#" />
                                    <asp:BoundField DataField="SubscriberID" HeaderText="Subscriber ID" />                                                                        
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                    <asp:BoundField DataField="EmailAddress" HeaderText="Email" />
                                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone#" />
                                    <asp:TemplateField HeaderText="Include">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBxInclude" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td style="vertical-align:top;">
                                <asp:LinkButton ID="lnkBtnShowGroups" runat="server" 
                                    onclick="lnkBtnShowGroups_Click">Show Groups</asp:LinkButton>
                            </td>
                            <td>
                                <asp:GridView 
                                    ID="gvwGroups" 
                                    runat="server"
                                    Visible="False"
                                    AutoGenerateColumns="false"  
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" 
                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:BoundField DataField="GroupID" HeaderText="ID#" />
                                        <asp:BoundField DataField="GroupDescription" HeaderText="Group Name" />
                                        <asp:BoundField DataField="CreatedDTM" HeaderText="Created On" />
                                        <asp:TemplateField HeaderText="Include">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBxInclude" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        <br />
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_OnClick" />
    </div>
</asp:Content>
