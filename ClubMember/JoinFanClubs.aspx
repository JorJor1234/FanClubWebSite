<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JoinFanClubs.aspx.cs" Inherits="RegisteredPerson_JoinFanClubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .template_checkbox {
            text-align: center;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Fan Clubs You Can Join</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <asp:Panel ID="pnlFanClubsNotJoined" runat="server" Visible="False">
            <div class="form-group">
                <asp:GridView ID="gvFanClubsNotJoined" runat="server" CssClass="col-md-12" OnRowDataBound="gvFanClubsNotJoined_RowDataBound" CellPadding="5" CellSpacing="3">
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT" ItemStyle-CssClass="template_checkbox">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" runat="server" />
                            </ItemTemplate>
                            <ItemStyle CssClass="template_checkbox" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HOW&nbspINFORMED" ItemStyle-CssClass="template_checkbox">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlHowInformed" runat="server" ForeColor="Black">
                                    <asp:ListItem Value="none selected">-- Select --</asp:ListItem>
                                    <asp:ListItem>friend</asp:ListItem>
                                    <asp:ListItem>print ad</asp:ListItem>
                                    <asp:ListItem>social media</asp:ListItem>
                                    <asp:ListItem>web ad</asp:ListItem>
                                    <asp:ListItem>other</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <div class="form-group">
                <div class="col-md-offset-1 col-md-11">
                    <asp:Button ID="btnJoinSelectedFanClubs" runat="server" Text="Join Selected Fan Clubs" Visible="True" CssClass="btn-sm" OnClick="btnJoinSelectedFanClubs_Click" />
                </div>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-12">
                <asp:HyperLink ID="hlJoinedFanClubs" runat="server" CssClass="btn" NavigateUrl="~/ClubMember/FanClubsJoined.aspx">Your Fan Clubs</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>

