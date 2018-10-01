<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RecordEventInformation.aspx.cs" Inherits="Employee_RecordEventInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .template_dropdown {
            text-align: center;
        }
    </style>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Record Event Fees Paid and Attendance</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlFanClubs" CssClass="col-md-2 control-label">Club name</asp:Label>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlFanClubs" runat="server" ToolTip="Fan club" ForeColor="Black" AutoPostBack="True" OnSelectedIndexChanged="ddlFanClubs_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubs"
                        CssClass="text-danger" Display="Dynamic" InitialValue="not selected" ToolTip="Fan club" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Panel ID="pnlEventNames" runat="server">
                <asp:Label runat="server" AssociatedControlID="ddlEvents" CssClass="col-md-2 control-label" ID="lblEventName">Event name</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlEvents" runat="server" ToolTip="Event" ForeColor="Black" OnSelectedIndexChanged="ddlEvents_SelectedIndexChanged" AutoPostBack="True" CssClass="dropdown"></asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select an event."
                            ControlToValidate="ddlEvents" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <br />
        <asp:Panel ID="pnlJoinsEvent" runat="server" Visible="False">
            <div class="form-group">
                <div class="col-md-offset-1 col-md-5">
                    <asp:GridView ID="gvJoinsEvent" runat="server" OnRowDataBound="gvJoinsEvent_RowDataBound" 
                        Caption="Event Fees Paid and Attendance" CaptionAlign="Top" CellPadding="5" CellSpacing="3" CssClass="col-md-12">
                        <Columns>
                            <asp:TemplateField HeaderText="Fee paid?" ItemStyle-CssClass="template_checkbox">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPaidFee" runat="server" ForeColor="Black">
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle CssClass="dropdown" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attended?">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAttended" runat="server" ForeColor="Black">
                                        <asp:ListItem Value="">Not specified</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle CssClass="dropdown" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-1 col-md-11">
                    <asp:Button ID="btnRecordEventInformation" runat="server" Text="Record Event Information" Visible="True" 
                        CssClass="btn-sm" OnClick="btnRecordEventInformation_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>