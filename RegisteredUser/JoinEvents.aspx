<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JoinEvents.aspx.cs" Inherits="RegisteredUser_JoinEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .template_checkbox {
            text-align: center;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Events You Can Join</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <asp:Panel ID="pnlEventsNotJoined" runat="server" Visible="False">
            <div class="form-group">
                <asp:GridView ID="gvEventsNotJoined" runat="server" CssClass="col-md-12" OnRowDataBound="gvEventsNotJoined_RowDataBound" CellPadding="5" CellSpacing="3">
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT" ItemStyle-CssClass="template_checkbox">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" runat="server" />
                            </ItemTemplate>
                            <ItemStyle CssClass="template_checkbox" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <div class="form-group">
                <div class="col-md-offset-1 col-md-11">
                    <asp:Button ID="btnJoinSelectedEvents" runat="server" Text="Join Selected Events" Visible="True" CssClass="btn-sm" OnClick="btnJoinSelectedEvents_Click" />
                </div>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-12">
                <asp:HyperLink runat="server" CssClass="btn" NavigateUrl="~/RegisteredUser/CurrentEventsJoined.aspx">Your Current Events</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>