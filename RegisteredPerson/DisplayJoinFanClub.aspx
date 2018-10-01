<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DisplayJoinFanClub.aspx.cs" Inherits="RegisteredPerson_DisplayJoinFanClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .template_checkbox {
            text-align: center;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #990000">Manage Your Fan Club Membership</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" ForeColor="#CC0000" CssClass="label-info"></asp:Label>
        <hr />
        <asp:Label ID="lblClubsJoined" runat="server" Font-Bold="True"
            Font-Size="Medium" Font-Underline="True" ForeColor="Maroon"
            Text="Fan Clubs You Have Joined" CssClass="label col-md-12"></asp:Label>
        <br />
        <br />
        <div class="form-group">
            <asp:GridView ID="gvClubsJoined" runat="server" Visible="False" CssClass="table-condensed col-md-12">
            </asp:GridView>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnShowFanClubsAvailableToJoin" runat="server" Text="Join More Fan Clubs" Visible="True" CssClass="btn-sm" OnClick="btnShowFanClubsAvailableToJoin_Click" />
            </div>
        </div>
        <asp:Panel ID="pnlFanClubsAvailableToJoin" runat="server" Visible="False">
            <br />
            <asp:Label ID="lblClubsAvailableToJoin" runat="server" Font-Bold="True"
                Font-Size="Medium" Font-Underline="True" ForeColor="Maroon"
                Text="Fan Clubs You Can Join" CssClass="label col-md-12"></asp:Label>
            <br />
            <br />
            <div class="form-group">
                <asp:GridView ID="gvClubsAvailableToJoin" runat="server" Visible="False" CssClass="table-condensed col-md-12" OnRowDataBound="gvClubsAvailableToJoin_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT" ItemStyle-CssClass="template_checkbox">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkSelected" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button ID="btnJoinSelectedFanClubs" runat="server" Text="Join Selected Fan Clubs" Visible="True" CssClass="btn-sm" OnClick="btnJoinSelectedFanClubs_Click"  />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

