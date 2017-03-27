<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="BeeperWebApp.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="font-weight:bold;">This is for registering a new user</div>
        <div>After registering a new user, you have to logout and log back in to make the site stop being stupid.</div>    
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
    <br /><br />
    <div>
        <div style="font-weight:bold;">This is for changing YOUR password (the person currently logged in)</div>
        <asp:ChangePassword ID="ChangePassword1" runat="server">
        </asp:ChangePassword>
    </div>
    <br /><br />
    <div style="font-size:larger; color:Green;">
        For deleting, editing, adding users or what ever just do it through the database. There are a number of stored procedures that do all of the maintenance etc...
    </div>
</asp:Content>