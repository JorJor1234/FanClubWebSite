<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FanClubsJoined.aspx.cs" Inherits="RegisteredPerson_FanClubsJoined" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .template_checkbox {
            text-align: center;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Fan Clubs You Have Joined</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <asp:Panel ID="pnlFanClubsJoined" runat="server">
            <div class="form-group">
                <asp:GridView ID="gvFanClubsJoined" runat="server" CssClass="col-md-12" OnRowDataBound="gvFanClubsJoined_RowDataBound" Visible="True" CellPadding="5" CellSpacing="3">
                </asp:GridView>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-12">
                <asp:HyperLink ID="hlJoinFanClub" runat="server" NavigateUrl="~/ClubMember/JoinFanClubs.aspx">Join A Fan Club</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>