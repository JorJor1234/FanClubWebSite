<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifyFanClub.aspx.cs" Inherits="Employee_ModifyFanClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Modify Fan Club</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlFanClubName" CssClass="col-md-2 control-label">Select a fan club</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlFanClubName" runat="server" AutoPostBack="True" ForeColor="Black" OnSelectedIndexChanged="ddlFanClubName_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a fan club." ControlToValidate="ddlFanClubName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" InitialValue="-- Select --"></asp:RequiredFieldValidator>
            </div>
            <div>
            </div>
        </div>
        <asp:Panel ID="pnlFanClubInfo" runat="server" Visible="False">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFanClubName" CssClass="col-md-2 control-label">Fan club name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtFanClubName" CssClass="form-control input-sm" MaxLength="50" ToolTip="Fan club name" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFanClubName"
                        CssClass="text-danger" ErrorMessage="The fan club name field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-2 control-label">Description</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control input-sm" MaxLength="150" ToolTip="Fan club description" Height="100px" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription"
                        CssClass="text-danger" ErrorMessage="The description field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtDateEstablished" CssClass="col-md-2 control-label">Date Established</asp:Label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtDateEstablished" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy" ToolTip="Date established" TextMode="Date">dd-MMM-yyyy</asp:TextBox>
                    <asp:CustomValidator ID="cvDateEstablished" runat="server" ControlToValidate="txtDateEstablished" 
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid date." OnServerValidate="cvDateEstablished_ServerValidate" ValidateEmptyText="True" 
                         ></asp:CustomValidator>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCalendar" runat="server" Text="Show Calendar" CssClass="btn-sm" OnClick="btnCalendar_Click" CausesValidation="False" />
                </div>
                <div class="col-md-6">
                    <asp:Calendar ID="calDateEstablished" runat="server" OnSelectionChanged="calBirthdate_SelectionChanged" CssClass="CalendarClass" Visible="False"></asp:Calendar>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Update Fan Club" CssClass="btn-sm" ID="btnUpdateFanClub" OnClick="btnUpdateFanClub_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

