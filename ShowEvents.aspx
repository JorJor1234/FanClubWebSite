<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShowEvents.aspx.cs" Inherits="ShowEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Events</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:GridView ID="gvEvents" runat="server" CssClass="col-md-12" OnRowDataBound="gvEvents_RowDataBound" Visible="True" CellPadding="5" CellSpacing="3">
            </asp:GridView>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:HyperLink runat="server" NavigateUrl="~/Account/Register.aspx">Register</asp:HyperLink>
                or 
                <asp:HyperLink runat="server" NavigateUrl="~/Account/Login.aspx">Login</asp:HyperLink>
                to join an event.
            </div>
        </div>
    </div>
</asp:Content>

