<%@ Page Title="Bulk Upload" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="bulkupload.aspx.cs" Inherits="_365_Portal.Admin.BulkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Flashcards</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Bulk Upload</h1>
        </div>

        <div class="col-md-12">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row input-validation">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="1">Topic - Module - Content</asp:ListItem>
                                    <asp:ListItem Value="2">Flashcards</asp:ListItem>
                                    <asp:ListItem Value="3">Survey - Flashcard Quiz - Final Quiz</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnDownload" runat="server" CssClass="btn bg-blue text-white" Text="Download Template" OnClick="btnDownload_Click" />
                        </div>
                        <div class="w-100 mb-4"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="custom-file">
                                    <asp:FileUpload ID="FileUpload" runat="server" class="custom-file-input" />
                                    <label class="custom-file-label" for="customFile">Choose file</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-blue text-white" OnClick="btnSubmit_Click" Text="Upload" />
                        </div>
                        <div class="w-100 mb-4"></div>
                        <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                        <asp:Button Visible="false" CssClass="btn bg-blue text-white" ID="btnExport" runat="server" Text="Export Sheet" OnClick="btnExport_Click" />
                        <div class="table-responsive">
                            <asp:GridView Visible="false" ID="gvRecords" CssClass="table table-bordered" runat="server" style="width: 100%">
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>


<%--        <div>
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
        </div>--%>
    </div>

    <script>

        $(document).ready(function () {

        });


    </script>
</asp:Content>
