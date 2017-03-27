<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="PagingMessageQueue.aspx.cs" Inherits="BeeperWebApp.PagingMessageQueue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0">
        <tr><td class="TitleCell">Paging Queue</td></tr>
        <tr>
            <td>
                Status&nbsp;
                <asp:DropDownList ID="ddlMessageTypes" AutoPostBack="true" runat="server" 
                    onselectedindexchanged="ddlMessageTypes_SelectedIndexChanged">
                    <asp:ListItem Text="All" Value="-1" />
                    <asp:ListItem Text="Sent" Value="1" />
                    <asp:ListItem Text="Unsent" Value="0" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr><td>&nbsp;<br /></td></tr>
        <tr>
            <td>
                <asp:GridView 
                    ID="gvwPagingQueue"
                    AutoGenerateColumns="false"
                    runat="server"
                    OnRowCommand="gvwPagingQueue_OnRowCommand" 
                    OnRowDataBound="gvwPagingQueue_OnRowDatabound" >
                    <Columns>
                        <asp:BoundField HeaderText="#" DataField="PagingQueueID" />
                        <asp:BoundField HeaderText="Owner" DataField="PagerOwnerID" />                            
                        <asp:BoundField HeaderText="SubscriberID" DataField="SubscriberID" />
                        <asp:BoundField HeaderText="Message" DataField="MessageText" />
                        <asp:BoundField HeaderText="Status" DataField="IsSent" />
                        <asp:BoundField HeaderText="Response Text" DataField="ResponseText" />                            
                        <asp:BoundField HeaderText="Sent On" DataField="DateTimeSent" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlActions" runat="server">
                                    <asp:ListItem Text="Enable" Value="1" />
                                    <asp:ListItem Text="Disable" Value="0" />
                                    <asp:ListItem Text="Delete" Value="-1" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField HeaderText="Apply" ButtonType="Button" CommandName="apply" Text="Apply" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
