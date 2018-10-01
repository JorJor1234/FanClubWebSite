<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifyEvent.aspx.cs" Inherits="Employee_ModifyEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Modify Event</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlFanClubs" CssClass="col-md-2 control-label">Club name</asp:Label>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlFanClubs" runat="server" ToolTip="Fan club" ForeColor="Black" AutoPostBack="True" OnSelectedIndexChanged="ddlFanClubs_SelectedIndexChanged"></asp:DropDownList>
                <div>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubs"
                        CssClass="text-danger" Display="Dynamic" InitialValue="not selected" ToolTip="Fan club" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
            </div>
            <asp:Panel ID="pnlEventNames" runat="server">
                <asp:Label runat="server" AssociatedControlID="ddlEvents" CssClass="col-md-2 control-label" ID="lblEventName">Event name</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlEvents" runat="server" ToolTip="Event name" ForeColor="Black" OnSelectedIndexChanged="ddlEvents_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select an event."
                            ControlToValidate="ddlEvents" CssClass="text-danger" Display="Dynamic" InitialValue="not selected" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlEventInformation" runat="server">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventName" CssClass="col-md-2 control-label">Event Name</asp:Label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtEventName" CssClass="form-control input-sm" MaxLength="50" ToolTip="Event name" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEventName"
                        CssClass="text-danger" ErrorMessage="The name field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
                <asp:Label runat="server" AssociatedControlID="txtVenue" CssClass="col-md-2 control-label">Venue</asp:Label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtVenue" CssClass="form-control input-sm" MaxLength="50" ToolTip="Event venue" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVenue"
                        CssClass="text-danger" ErrorMessage="The venue field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventQuota" CssClass="col-md-2 control-label">Quota</asp:Label>
                <div class="col-md-1">
                    <asp:TextBox runat="server" ID="txtEventQuota" CssClass="form-control input-sm" MaxLength="5" ToolTip="Event quota" TextMode="Number">0</asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEventQuota"
                        CssClass="text-danger" ErrorMessage="The quota field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
                <asp:Label runat="server" AssociatedControlID="ddlIsAvailable" CssClass="col-md-2 control-label">Available?</asp:Label>
                <div class="col-md-1">
                    <asp:DropDownList ID="ddlIsAvailable" runat="server" ForeColor="Black">
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Label runat="server" AssociatedControlID="ddlIsMemberOnly" CssClass="col-md-2 control-label">Member only?</asp:Label>
                <div class="col-md-1">
                    <asp:DropDownList ID="ddlIsMemberOnly" runat="server" AutoPostBack="True" ForeColor="Black" OnSelectedIndexChanged="ddlIsMemberOnly_SelectedIndexChanged">
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtMemberFee" CssClass="col-md-2 control-label">Member Fee</asp:Label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtMemberFee" CssClass="form-control input-sm" MaxLength="8" Placeholder="0" 
                        ToolTip="Member fee" Text="0"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMemberFee" CssClass="text-danger" 
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="The member fee must be a number in the range 0.00 to 99999.99." 
                        ValidationExpression="^\s*\d{0,5}(?:\.\d{1,2})?\s*$"></asp:RegularExpressionValidator>
                </div>
                <asp:Label runat="server" AssociatedControlID="txtNonmemberFee" CssClass="col-md-2 control-label">Nonmember Fee</asp:Label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtNonmemberFee" CssClass="form-control input-sm" MaxLength="8" Placeholder="0" 
                        ToolTip="Nonmember Fee" Text="0"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNonmemberFee" CssClass="text-danger" 
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="The nonmember fee must be a number in the range 0.00 to 99999.99." 
                        ValidationExpression="^\s*\d{0,5}(?:\.\d{1,2})?\s*$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventTime" CssClass="col-md-2 control-label">Time</asp:Label>
                <div class="col-md-1">
                    <asp:TextBox runat="server" ID="txtEventTime" CssClass="form-control input-sm" MaxLength="4" ToolTip="Event time" TextMode="Number" Text="0000">0000</asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEventTime"
                        CssClass="text-danger" ErrorMessage="The time field is required." Display="Dynamic" EnableClientScript="False" />
                    <asp:RegularExpressionValidator ID="revEventTime" runat="server" ErrorMessage="Please enter a valid 24 hour time." ControlToValidate="txtEventTime" CssClass="text-danger" Display="Dynamic" ValidationExpression="^([01]\d|2[0-3]):?([0-5]\d)$" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
                <asp:Label runat="server" AssociatedControlID="txtEventDate" CssClass="col-md-2 control-label">Date</asp:Label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtEventDate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy" ToolTip="Event date">dd-MMM-yyyy</asp:TextBox>
                    <asp:CustomValidator ID="cvEventDate" runat="server" ControlToValidate="txtEventDate" CssClass="text-danger" 
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid date." 
                        OnServerValidate="cvEventDate_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCalendar" runat="server" Text="Show Calendar" CssClass="btn-sm" OnClick="btnCalendar_Click" CausesValidation="False" />
                </div>
                <div class="col-md-3">
                    <asp:Calendar ID="calEventDate" runat="server" OnSelectionChanged="calEventDate_SelectionChanged" CssClass="CalendarClass" Visible="False"></asp:Calendar>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlEmployee" CssClass="col-md-2 control-label">Supervisor</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlEmployee" runat="server" ToolTip="Event supervisor" ForeColor="Black"></asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select an employee to supervise the event."
                            ControlToValidate="ddlEmployee" CssClass="text-danger" Display="Dynamic" InitialValue="none selected"
                            ToolTip="Supervising employee" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Update Event" CssClass="btn-sm" ID="btnUpdateEvent" OnClick="btnUpdateEvent_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

