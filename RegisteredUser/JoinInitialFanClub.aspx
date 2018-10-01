<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JoinInitialFanClub.aspx.cs" Inherits="RegisteredUser_JoinInitialFanClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type='text/javascript'>
        function clearText(source) {
            source.value = "";
        }
    </script>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Information Required To Join Fan Clubs</span><span style="font-size: 8pt; color: #FFFFFF"> (You will be required to login again.)</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtOccupation" CssClass="col-md-2 control-label">Occupation</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtOccupation" CssClass="form-control input-sm" MaxLength="25" ToolTip="Occupation" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOccupation"
                    CssClass="text-danger" ErrorMessage="The occupation field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
            <asp:Label runat="server" AssociatedControlID="ddlEducationLevel" CssClass="col-md-2 control-label">Education level</asp:Label>
            <div class="col-md-3">
                <div>
                    <asp:DropDownList ID="ddlEducationLevel" runat="server" CssClass="dropdown" ToolTip="Education level" ForeColor="Black">
                        <asp:ListItem Value="none selected">-- Select --</asp:ListItem>
                        <asp:ListItem Value="none">None</asp:ListItem>
                        <asp:ListItem Value="primary">Primary</asp:ListItem>
                        <asp:ListItem Value="secondary">Secondary</asp:ListItem>
                        <asp:ListItem Value="tertiary">Tertiary</asp:ListItem>
                        <asp:ListItem Value="post tertiary">Post tertiary</asp:ListItem>
                    </asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEducationLevel" CssClass="text-danger" Display="Dynamic"
                            ErrorMessage="Please select an education level." InitialValue="none selected" ToolTip="Education level" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtBirthdate" CssClass="col-md-2 control-label">Birth date</asp:Label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtBirthdate" CssClass="form-control input-sm" MaxLength="11" placeholder="dd-mmm-yy" ToolTip="Birth date" onclick='clearText(this)'>dd-MMM-yyyy</asp:TextBox>
                <asp:CustomValidator ID="cvBirthdate" runat="server" ErrorMessage="Please enter a valid date." ControlToValidate="txtBirthdate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" OnServerValidate="cvBirthdate_ServerValidate"></asp:CustomValidator>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCalendar" runat="server" Text="Show Calendar" CssClass="btn-sm" OnClick="btnCalendar_Click" CausesValidation="False" />
            </div>
            <div class="col-md-4">
                <asp:Calendar ID="calBirthdate" runat="server" OnSelectionChanged="calBirthdate_SelectionChanged" CssClass="CalendarClass"></asp:Calendar>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-1 col-md-11">
                <asp:Button runat="server" Text="Submit" CssClass="btn-sm" ID="btnSubmit" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>

