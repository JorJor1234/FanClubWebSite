﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewFanClubRemarks.aspx.cs" Inherits="ClubMember_ViewFanClubRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">View Your Fan Club Remarks</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <asp:Panel ID="pnlFanClub" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label ID="lblSelectFanClub" runat="server" Text="Fan Club" CssClass="control-label col-md-2" AssociatedControlID="ddlFanClubs"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddlFanClubs" runat="server" CssClass="dropdown col-md-5" AutoPostBack="True" ToolTip="Fan club" ForeColor="Black" OnSelectedIndexChanged="ddlFanClubs_SelectedIndexChanged" CausesValidation="True"></asp:DropDownList>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubs" CssClass="text-danger" Display="Dynamic" InitialValue="none selected" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSubject" runat="server">
            <div class="form-group">
                <asp:Label ID="lblSubject" runat="server" Text="Subject" CssClass="control-label col-md-2" AssociatedControlID="ddlSubject"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="dropdown col-md-5" AutoPostBack="True" ToolTip="Subject" ForeColor="Black" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CausesValidation="True"></asp:DropDownList>
                </div>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a subject" ControlToValidate="ddlSubject" CssClass="text-danger" Display="Dynamic" InitialValue="none selected" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRemark" runat="server">
            <div class="form-group">
                <asp:Label ID="lblRemark" runat="server" Text="Remark" CssClass="control-label col-md-2" AssociatedControlID="lblRemarkText"></asp:Label>
                <div>
                    <asp:Label ID="lblRemarkText" runat="server" CssClass="col-md-5"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblSubmissionDate" runat="server" Text="Submission Date" CssClass="control-label col-md-2" AssociatedControlID="lblSubmissionDateText"></asp:Label>
                <div>
                    <asp:Label ID="lblSubmissionDateText" runat="server" CssClass="col-md-5"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblActionTaken" runat="server" Text="Action Taken" CssClass="control-label col-md-2" AssociatedControlID="lblActionTakenText"></asp:Label>
                <div>
                    <asp:Label ID="lblActionTakenText" runat="server" CssClass="col-md-5"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>