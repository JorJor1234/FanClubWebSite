<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SubmitFanClubRemark.aspx.cs" Inherits="ClubMember_SubmitFanClubRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Submit A Remark For A Fan Club</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label ID="lblSelectFanClub" runat="server" Text="Fan Club" CssClass="control-label col-md-2" AssociatedControlID="ddlFanClubs"></asp:Label>
            <div>
                <asp:DropDownList ID="ddlFanClubs" runat="server" CssClass="dropdown col-md-10" AutoPostBack="False" ToolTip="Fan club" ForeColor="Black"></asp:DropDownList>
            </div>
            <div>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubs" CssClass="text-danger" Display="Dynamic" InitialValue="none selected"></asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Panel ID="pnlRemark" runat="server">
            <div class="form-group">
                <asp:Label ID="lblSubject" runat="server" Text="Subject" CssClass="control-label col-md-2" AssociatedControlID="txtSubject"></asp:Label>
                <div>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control input-sm col-md-10" MaxLength="50" ToolTip="Subject"></asp:TextBox>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="The Subject field is required." ControlToValidate="txtSubject" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblRemark" runat="server" Text="Remark" CssClass="control-label col-md-2" AssociatedControlID="txtRemark"></asp:Label>
                <div>
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control input-sm col-md-10" Height="150px" MaxLength="150" TextMode="MultiLine" ToolTip="Remark"></asp:TextBox>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="The Remark field is required." ControlToValidate="txtRemark" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-1 col-md-11">
                    <asp:Button ID="btnSubmitRemark" runat="server" Text="Submit" Visible="True" CssClass="btn-sm" OnClick="btnSubmitRemark_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

