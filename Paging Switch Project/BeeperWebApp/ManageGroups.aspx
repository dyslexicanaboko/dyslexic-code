<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="ManageGroups.aspx.cs" Inherits="BeeperWebApp.ManageGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:LinkButton ID="lnkBtnAddGroup" runat="server" OnClick="lnkBtnAddGroup_Click">Add Group</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnEditGroup" runat="server" OnClick="lnkBtnEditGroup_Click">Edit Group</asp:LinkButton>            
    </div>
    <br />
    <div>
        <asp:Label ID="lblMessages" runat="server" Text="" />
    </div>
    <br />
    <div>
        <asp:MultiView ID="multiViewManagement" runat="server">
            <asp:View ID="viewAdd" runat="server">
                <table border="0">
                <tr>
                    <td colspan="2" class="TitleCell">
                        <asp:Label ID="lblViewAddTitle" runat="server" Text="Add New Group"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="BoldTop">Group Name</td>
                    <td>
                        <asp:TextBox ID="txtGroupName" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td class="BoldTop">List of Owners</td>
                    <td>
                        <asp:GridView 
                            ID="gvwOwners" 
                            runat="server"
                            AutoGenerateColumns="False" 
                            OnRowDataBound="gvwOwners_OnRowDataBound" 
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
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnSaveChanges" OnClick="btnSaveChanges_Click" runat="server" Text="Save Changes" Visible="false" />
                        <asp:Button ID="btnCreateGroup" OnClick="btnCreateGroup_Click" runat="server" Text="Create Group" />
                    </td>
                </tr>
                </table>
            </asp:View>
            <asp:View ID="viewEdit" runat="server">
                <table border="0">
                    <tr>
                        <td class="TitleCell">Edit Existing Groups</td>
                    </tr>
                    <tr><td>&nbsp;<br /></td></tr>
                    <tr>
                        <td>
                            <asp:GridView 
                                ID="gvwEditOwners"
                                AutoGenerateColumns="False"
                                OnRowCommand="gvwEditOwners_OnRowCommand"
                                OnRowEditing="gvwEditOwners_OnRowEditing"
                                OnRowDeleting="gvwEditOwners_OnRowDeleting" 
                                runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                                BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <Columns>
                                    <asp:BoundField DataField="GroupID" HeaderText="Group ID" />
                                    <asp:BoundField DataField="GroupDescription" HeaderText="Group Name" />
                                    <asp:BoundField DataField="CreatedDTM" HeaderText="Created On" />
                                    <asp:ButtonField HeaderText="Edit Group" ButtonType="Button" CommandName="EDIT" Text="Edit Group" />    
                                    <asp:ButtonField HeaderText="Delete Group" ButtonType="Button" CommandName="DELETE" Text="Delete Group" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
