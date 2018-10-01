<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ClubMemberDefault.aspx.cs" Inherits="ClubMember_ClubMemberDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div>
        <h4><span style="text-decoration: underline; color: #990000">Select A Club Member Activity</span></h4>
        <p>
            <asp:HyperLink ID="hlSubmitPaper" runat="server" NavigateUrl="~/Author/SubmitPaper.aspx">Submit A Paper</asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="hlFindSubmittedPaper" runat="server" NavigateUrl="~/Author/FindASubmittedPaper.aspx">Find A Submitted Paper</asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="hlFindAllAuthorPapers" runat="server" NavigateUrl="~/Author/FindAllSubmittedPapers.aspx">Find All Submitted Papers By An Author</asp:HyperLink>
        </p>
    </div>
</asp:Content>