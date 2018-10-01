<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <h4><span style="text-decoration: underline; color: #FFFFFF">Create An Account</span></h4>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control input-sm" MaxLength="10" ToolTip="User name" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control input-sm" MaxLength="30" ToolTip="Email address" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:CustomValidator ID="cvUserEmail" runat="server" ControlToValidate="Email" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The email already exists." OnServerValidate="cvUserEmail_ServerValidate"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control input-sm" MaxLength="15" ToolTip="First name" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="LastName" CssClass="form-control input-sm" MaxLength="20" ToolTip="Last name" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                    CssClass="text-danger" ErrorMessage="The last name field is required." Display="Dynamic" EnableClientScript="False" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Gender" CssClass="col-md-2 control-label">Gender</asp:Label>
            <div class="col-md-1">
                <asp:TextBox runat="server" ID="Gender" CssClass="form-control input-sm" MaxLength="1" ToolTip="Gender" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Gender"
                    CssClass="text-danger" ErrorMessage="The gender field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:RegularExpressionValidator ID="revGender" runat="server" ControlToValidate="Gender" CssClass="text-danger"
                    ErrorMessage="Please enter M or F for gender." ValidationExpression="^(?:m|M|f|F|)$" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
            </div>
            <asp:Label runat="server" AssociatedControlID="PhoneNo" CssClass="col-md-2 control-label">Phone number</asp:Label>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="PhoneNo" CssClass="form-control input-sm" MaxLength="8" ToolTip="Phone number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNo"
                    CssClass="text-danger" ErrorMessage="The phone number field is required." Display="Dynamic" EnableClientScript="False" />
            <asp:RegularExpressionValidator ID="revPhoneNo" runat="server" ErrorMessage="Please enter exactly 8 digits."
                ControlToValidate="PhoneNo" CssClass="text-danger" ValidationExpression="\d{8}" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
            </div>
            <div class="col-md-5">
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn-sm" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control input-sm" Visible="False">FanClub1#</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." Display="Dynamic" EnableClientScript="False" />
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control input-sm" Visible="False">FanClub1#</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." EnableClientScript="False" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." EnableClientScript="False" />
            </div>
        </div>
    </div>
</asp:Content>

