<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="ManageOwnersAndPagers.aspx.cs" Inherits="BeeperWebApp.ManageOwnersAndPagers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ajaxScriptManager" runat="server" EnablePartialRendering="true" />
    <div><asp:Label ID="lblMessage" runat="server" Text="" /></div>
    <div>
        <table border="0">
            <tr>
                <td colspan="2" class="TitleCell">Pager Owners</td>
            </tr>
            <tr>    
                <td id="OwnersCell">
                    <asp:UpdatePanel 
                        ID="ajaxPagerDDLUpdatePanel" 
                        UpdateMode="Conditional"
                        RenderMode="Inline"  
                        runat="server">
                        <ContentTemplate>
                            <table border="0">
                                <tr><td colspan="2">&nbsp;</td></tr>
                                <tr>
                                    <td style="font-weight:bold;">First</td><td><asp:TextBox ID="txtFirstName" runat="server" /></td>
                                </tr>
                                <tr>    
                                    <td style="font-weight:bold;">Last</td><td><asp:TextBox ID="txtLastName" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">Email</td><td><asp:TextBox ID="txtEmail" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">Phone</td><td><asp:TextBox ID="txtPhone" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">Notes</td><td><asp:TextBox ID="txtNotes" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">Pager</td>
                                    <td>
                                        <asp:TextBox ID="txtSubscriberIDEditView" runat="server" Visible="false" 
                                            Text="" ToolTip="Your selected pager" Enabled="false" Width="50px" />
                                        &nbsp;
                                        <asp:DropDownList ID="ddlExistingPagers" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>  
                                    <td>
                                        <asp:Button ID="btnAddOwner" runat="server" Text="Add" 
                                            onclick="btnAddOwner_Click" />
                                        <asp:Button ID="btnUpdateOwner" runat="server" Text="Save Changes" 
                                            Visible="false" onclick="btnUpdateOwner_Click" />   
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                    </asp:UpdatePanel> 
                    <br />
                    <div>
                        <asp:UpdatePanel 
                            ID="ajaxOwnersUpdatePanel" 
                            UpdateMode="Conditional"
                            RenderMode="Inline"   
                            runat="server">
                            <ContentTemplate>
                                <asp:GridView 
                                    ID="gvwOwners" 
                                    runat="server"
                                    AutoGenerateColumns="False" 
                                    OnRowCommand="gvwOwners_OnRowCommand"
                                    OnRowEditing="gvwOwners_OnRowEditing"
                                    OnRowDeleting="gvwOwners_OnRowDeleting"
                                    CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField HeaderText="ID#" DataField="PagerOwnerID" />
                                        <asp:BoundField HeaderText="First" DataField="FirstName" />
                                        <asp:BoundField HeaderText="Last" DataField="LastName" />
                                        <asp:BoundField HeaderText="Email" DataField="EmailAddress" />
                                        <asp:BoundField HeaderText="Phone" DataField="PhoneNumber" />
                                        <asp:BoundField HeaderText="Notes" DataField="AdditionalInfo" />
                                        <asp:BoundField HeaderText="SubscriberID" DataField="SubscriberID" />
                                        <asp:BoundField HeaderText="Created On" DataField="CreatedDTM" />
                                        <asp:BoundField HeaderText="PagerID" DataField="PagerID" Visible="false" />
                                        <asp:ButtonField HeaderText="Edit Owner" ButtonType="Button" CommandName="EDIT" Text="Edit Owner" />    
                                        <asp:ButtonField HeaderText="Delete Owner" ButtonType="Button" CommandName="DELETE" Text="Delete Owner" />
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>        
                    </div>        
                </td>
            </tr>
            <tr><td colspan="2">&nbsp;<br /><br /><br /></td></tr>
            <tr><td colspan="2" class="TitleCell">Pagers</td></tr>
            <tr>   
                <td id="PagersCell">
                    <asp:UpdatePanel 
                        ID="ajaxPagersUpdatePanel" 
                        UpdateMode="Conditional"
                        RenderMode="Inline"  
                        runat="server">
                        <ContentTemplate>
                            <table border="0">
                                <tr><td colspan="2">&nbsp;</td></tr>
                                <tr>
                                    <td style="font-weight:bold;">SubscriberID</td><td><asp:TextBox ID="txtSubscriberID" runat="server" /></td>
                                </tr>
                                <tr>    
                                    <td style="font-weight:bold;">IndividualID</td><td><asp:TextBox ID="txtIndividualID" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">GroupID</td><td><asp:TextBox ID="txtGroupID" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">MailDropID</td><td><asp:TextBox ID="txtMailDropID" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">BagID</td><td><asp:TextBox ID="txtBagID" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td style="font-weight:bold;">Notes</td><td><asp:TextBox ID="txtPagerNotes" runat="server" /></td>
                                </tr>
                                <tr>  
                                    <td>
                                        <asp:Button ID="btnAddPager" runat="server" Text="Add" 
                                            onclick="btnAddPager_Click" />
                                        <asp:Button ID="btnUpdatePager" runat="server" Text="Save Changes" 
                                            Visible="false" onclick="btnUpdatePager_Click" />                                    
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div>
                                <asp:GridView 
                                    ID="gvwPagers" 
                                    runat="server"
                                    AutoGenerateColumns="False" 
                                    OnRowCommand="gvwPagers_OnRowCommand"
                                    OnRowEditing="gvwPagers_OnRowEditing"
                                    OnRowDeleting="gvwPagers_OnRowDeleting"
                                    BackColor="White" BorderColor="#999999" 
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" >
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundField HeaderText="ID#" DataField="PagerID" />
                                        <asp:BoundField HeaderText="SubscriberID" DataField="SubscriberID" />
                                        <asp:BoundField HeaderText="BagID" DataField="BagID" />
                                        <asp:BoundField HeaderText="Notes" DataField="AdditionalNotes" />
                                        <asp:BoundField HeaderText="Created On" DataField="CreatedDTM" />
                                        <asp:ButtonField HeaderText="Edit Pager" ButtonType="Button" CommandName="EDIT" Text="Edit Pager" />    
                                        <asp:ButtonField HeaderText="Delete Pager" ButtonType="Button" CommandName="DELETE" Text="Delete Pager" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
