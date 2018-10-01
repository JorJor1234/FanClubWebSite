<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CurrentEventsJoined.aspx.cs" Inherits="RegisteredUser_CurrentEventsJoined" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Current Events You Have Joined</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <asp:Panel ID="pnlEventsJoined" runat="server" Visible="False">
            <div class="form-group">
                <asp:GridView ID="gvCurrentEventsJoined" runat="server" CssClass="col-md-12" OnRowDataBound="gvCurrentEventsJoined_RowDataBound" Visible="True" CellPadding="5" CellSpacing="3">
                </asp:GridView>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-3">
                <asp:HyperLink runat="server" CssClass="btn" NavigateUrl="~/RegisteredUser/JoinEvents.aspx">Join More Events</asp:HyperLink>
            </div>
            <div class="col-md-3">
                <asp:HyperLink runat="server" CssClass="btn" NavigateUrl="~/RegisteredUser/PastEventsJoined.aspx">Your Past Events</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>