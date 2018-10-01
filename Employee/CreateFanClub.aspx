<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateFanClub.aspx.cs" Inherits="Employee_CreateFanClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type='text/javascript'>
        function clearText(source) {
            source.value = "";
        }
    </script>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Create A Fan Club</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
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
                <asp:TextBox runat="server" ID="txtDateEstablished" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy" ToolTip="Date established" onclick='clearText(this)' TextMode="Date">dd-MMM-yyyy</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDateEstablished"
                    CssClass="text-danger" ErrorMessage="The date established field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:CustomValidator ID="cvDateEstablished" runat="server" ErrorMessage="Please enter a valid date." ControlToValidate="txtDateEstablished" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" OnServerValidate="cvDateEstablished_ServerValidate"></asp:CustomValidator>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCalendar" runat="server" Text="Show Calendar" CssClass="btn-sm" OnClick="btnCalendar_Click" CausesValidation="False" />
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <asp:Calendar ID="calDateEstablished" runat="server" OnSelectionChanged="calBirthdate_SelectionChanged" CssClass="CalendarClass" Visible="False"></asp:Calendar>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Create Fan Club" CssClass="btn-sm" ID="btnCreateFanClub" OnClick="btnCreateFanClub_Click" />
            </div>
        </div>
    </div>
</asp:Content>

