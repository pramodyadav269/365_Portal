<%@ Page Title="Flashcards" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="bulkupload.aspx.cs" Inherits="_365_Portal.Admin.BulkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Flashcards</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Flashcards</h1>
        </div>
        <div>
            <div>
                <div>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Value="1" Selected="True">Topic - Module - Content</asp:ListItem>
                        <asp:ListItem Value="2">Flashcards</asp:ListItem>
                        <asp:ListItem Value="3">Survey - Flashcard Quiz - Final Quiz</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btnDownload" runat="server" Text="Download Template" OnClick="btnDownload_Click" />
                    <br />
                    <br />
                    <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                    Select a file:
                    <asp:FileUpload ID="FileUpload" runat="server" />
                    <p>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Upload" />
                    </p>
                </div>
                <!-- ADD A GRIDVIEW CONTROL. -->
                <div>
                    <p>
                        <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                    </p>
                    <asp:Button Visible="false" ID="btnExport" runat="server" Text="Export Sheet" OnClick="btnExport_Click" />
                    <asp:GridView Visible="false" ID="gvRecords" CssClass="Grid" runat="server">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <script>
        
        $(document).ready(function () {
            
        });

       
    </script>
</asp:Content>
