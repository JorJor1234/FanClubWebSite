<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProcessFanClubRemarks.aspx.cs" Inherits="Employee_ProcessFanClubRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Process A Fan Club Remark</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <asp:Panel ID="pnlFanClubs" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label ID="lblSelectFanClub" runat="server" Text="FanClub" CssClass="control-label col-md-2" 
                    AssociatedControlID="ddlFanClubs"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddlFanClubs" runat="server" CssClass="dropdown col-md-9" AutoPostBack="True" ForeColor="Black" 
                        ToolTip="Fan Club" OnSelectedIndexChanged="ddlFanClubs_SelectedIndexChanged" CausesValidation="True"></asp:DropDownList>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubs" 
                        CssClass="text-danger" Display="Dynamic" InitialValue="none selected" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSubject" runat="server">
            <div class="form-group">
                <asp:Label ID="lblSubject" runat="server" Text="Subject" CssClass="control-label col-md-2" AssociatedControlID="ddlSubject"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="dropdown col-md-9" AutoPostBack="True" ForeColor="Black" 
                        ToolTip="Subject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CausesValidation="True"></asp:DropDownList>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a subject" ControlToValidate="ddlSubject" 
                        CssClass="text-danger" Display="Dynamic" InitialValue="none selected" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRemark" runat="server">
            <div class="form-group">
                <asp:Label ID="lblRemark" runat="server" Text="Remark" CssClass="control-label col-md-2" AssociatedControlID="lblRemarkText"></asp:Label>
                <div>
                    <asp:Label ID="lblRemarkText" runat="server" CssClass="col-md-7"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="control-label col-md-2" AssociatedControlID="lblUserNameText"></asp:Label>
                <div>
                    <asp:Label ID="lblUserNameText" runat="server" CssClass="col-md-2"></asp:Label>
                </div>
                <asp:Label ID="lblSubmissionDate" runat="server" Text="Submission Date" CssClass="control-label col-md-2" 
                    AssociatedControlID="lblSubmissionDateText"></asp:Label>
                <div>
                    <asp:Label ID="lblSubmissionDateText" runat="server" CssClass="col-md-2"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div>
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="control-label col-md-2" AssociatedControlID="ddlStatus"></asp:Label>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdown" ForeColor="Black">
                            <asp:ListItem Value="read">Read</asp:ListItem>
                            <asp:ListItem Value="processing">Processing</asp:ListItem>
                            <asp:ListItem Value="done">Done</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                    </div>
                </div>
                <asp:Label ID="lblActionTaken" runat="server" Text="Action Taken" CssClass="control-label col-md-2" AssociatedControlID="txtActionTaken"></asp:Label>
                <div>
                    <asp:TextBox ID="txtActionTaken" runat="server" CssClass="form-control input-sm col-md-5" MaxLength="50" ToolTip="Action Taken"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Update" CssClass="btn-sm" ID="btnUpdateRemark" OnClick="btnUpdateRemark_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>