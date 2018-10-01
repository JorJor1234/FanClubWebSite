<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageRegisteredUserInformation.aspx.cs" Inherits="RegisteredUser_ManageRegisteredUserInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type='text/javascript'>
        function clearText(source) {
            source.value = "";
        }
    </script>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Change Your Information</span></h4>
        <asp:Label ID="lblResultMessage" runat="server" Font-Bold="True" CssClass="label"></asp:Label>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtUserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" MaxLength="10" ReadOnly="True" />
            </div>
            <asp:Label runat="server" AssociatedControlID="txtUserEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtUserEmail" CssClass="form-control input-sm" MaxLength="30" ToolTip="Email address" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserEmail"
                    CssClass="text-danger" ErrorMessage="The email field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:CustomValidator ID="cvUserEmail" runat="server" ControlToValidate="txtUserEmail" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The email already exists." OnServerValidate="cvUserEmail_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 control-label">First name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control input-sm" MaxLength="15" ToolTip="First name" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
            <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 control-label">Last name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control input-sm" MaxLength="20" ToolTip="Last name" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
                    CssClass="text-danger" ErrorMessage="The last name field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtGender" CssClass="col-md-2 control-label">Gender</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtGender" CssClass="form-control input-sm" MaxLength="1" ToolTip="Gender" Width="40px" Wrap="False" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGender"
                    CssClass="text-danger" ErrorMessage="The gender field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:RegularExpressionValidator ID="revGender" runat="server" ControlToValidate="txtGender" CssClass="text-danger"
                    ErrorMessage="Please enter M or F for gender." ValidationExpression="^(?:m|M|f|F|)$" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
            </div>
        <asp:Label runat="server" AssociatedControlID="txtPhoneNo" CssClass="col-md-2 control-label">Phone number</asp:Label>
        <div class="col-md-3">
            <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control input-sm" MaxLength="8" ToolTip="Phone number" TextMode="Number" Width="80px" Wrap="False" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNo"
                CssClass="text-danger" ErrorMessage="The phone number field is required." Display="Dynamic" EnableClientScript="False" />
            <asp:RegularExpressionValidator ID="revPhoneNo" runat="server" ErrorMessage="Please enter exactly 8 digits."
                ControlToValidate="txtPhoneNo" CssClass="text-danger" ValidationExpression="\d{8}" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
        </div>
        </div>
        <asp:Panel ID="pnlClubMemberInfo" runat="server">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtOccupation" CssClass="col-md-2 control-label">Occupation</asp:Label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOccupation" CssClass="form-control input-sm" MaxLength="25" ToolTip="Occupation" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOccupation"
                        CssClass="text-danger" ErrorMessage="The occupation field is required." Display="Dynamic" EnableClientScript="False" />
                </div>
                <asp:Label runat="server" AssociatedControlID="ddlEducationLevel" CssClass="col-md-2 control-label">Education level</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlEducationLevel" runat="server" CssClass="dropdown" CausesValidation="True" ForeColor="Black">
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
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtBirthdate" CssClass="col-md-2 control-label">Birth date</asp:Label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtBirthdate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy" ToolTip="Birth date" onclick='clearText(this)' TextMode="Date">dd-MMM-yyyy</asp:TextBox>
                    <asp:CustomValidator ID="cvBirthdate" runat="server" ControlToValidate="txtBirthdate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid date." OnServerValidate="cvBirthdate_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCalendar" runat="server" Text="Show Calendar" CssClass="btn-sm" OnClick="btnCalendar_Click" CausesValidation="False" />
                </div>
                <div class="col-md-4">
                    <asp:Calendar ID="calBirthdate" runat="server" OnSelectionChanged="calBirthdate_SelectionChanged" CssClass="CalendarClass" Visible="False"></asp:Calendar>
                </div>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Update" CssClass="btn-sm" ID="btnUpdate" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>

