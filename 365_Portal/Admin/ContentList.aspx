<%@ Page Title="Content List" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="_365_Portal.Admin.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Content List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Contents</h1>
        </div>
        <table>
            <tr>
                <td>
                    <h2>Content</h2>
                </td>
            </tr>
            <tr>
                <td>Doc Type</td>
                <td>
                    <select id="ddlDocType">
                        <option value="PDF">PDF</option>
                        <option value="VIDEO">Video</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>Is Gift</td>
                <td>
                    <input type="checkbox" id="chkIsGift" />
                </td>
            </tr>
            <tr>
                <td>Title</td>
                <td>
                    <input type="text" id="txtTitle" />
                </td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <textarea rows="4" cols="50" id="txtDescription"></textarea>
                </td>
            </tr>
            <tr>
                <td>Overview</td>
                <td>
                    <textarea rows="4" cols="50" id="txtOverview"></textarea>
                </td>
            </tr>
            <tr>
                <td>File Path/URL</td>
                <td>
                    <input type="url" placeholder="https://example.com" id="txtFilePath" style="width: 500px;" />
                </td>
            </tr>
            <tr>
                <td>Is Published</td>
                <td>
                    <input type="checkbox" id="chkIsPublished" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <button id="btnSaveChanges" onclick="SaveChanges(this);return false;">Add Content</button>

                    <button id="btnCancel" onclick="Cancel(this);return false;" style="margin-left: 20px; display: none;">Cancel</button>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvJson"></div>
    <table id="tblContent" border="1">
        <thead>
            <th>Sr No</th>
            <th>Doc Type</th>
            <th>Title</th>
            <th>Description</th>
            <th>Overview</th>
            <th>File Path</th>
            <th>Is Published</th>
            <th>Is Gift</th>
            <th colspan="2">Action</th>
        </thead>
        <tbody id="tBodyContent"></tbody>
    </table>
    <script>

        contentList = [];

        $(document).ready(function () {
            GetContentList(this);
        });

        function SaveChanges(cntrl) {
            if ($("#ddlDocType").val() != "" &&
                $("#txtTitle").val() != "" &&
                $("#txtDescription").val() != "" &&
                $("#txtOverview").val() != "" &&
                $("#txtFilePath").val() != "") {

                var index = contentList.length + 1;
                var newContent = {
                    "ContentID": (index * 10),
                    "SrNo": index,
                    "DocType": $("#ddlDocType").val()
                    , "Title": $("#txtTitle").val()
                    , "Description": $("#txtDescription").val()
                    , "Overview": $("#txtOverview").val()
                    , "FilePath": $("#txtFilePath").val()
                    , "IsGift": $("#chkIsGift").prop("checked")
                    , "IsPublished": $("#chkIsPublished").prop("checked")
                };

                if ($(cntrl).attr("index") == null) {
                    // Add Content
                    // Ajax Call

                    if (IsTitleDuplicate(contentList, newContent.Title)) {
                        alert("Title cannot be duplicate.");
                        return false;
                    }

                    contentList.push(newContent);
                }
                else {
                    // Update Content
                    var index = $(cntrl).attr("index");

                    if (IsTitleDuplicate(contentList, newContent.Title, index)) {
                        alert("Title cannot be duplicate.");
                        return false;
                    }

                    var oldContent = $.grep(contentList, function (n, i) {
                        return n.ContentID == parseInt(index);
                    })[0];

                    oldContent.DocType = newContent.DocType;
                    oldContent.Title = newContent.Title;
                    oldContent.Description = newContent.Description;
                    oldContent.Overview = newContent.Overview;
                    oldContent.FilePath = newContent.FilePath;
                    oldContent.IsGift = newContent.IsGift;
                    oldContent.IsPublished = newContent.IsPublished;

                    // Ajax Call
                }

                Cancel(cntrl);
                GetContentList(cntrl);

            }
            else {
                alert("Please enter all required fields.");
            }
        }

        function ClearAllFields(cntrl) {
            $("#ddlDocType").val("PDF");
            $("#txtTitle").val("");
            $("#txtDescription").val("");
            $("#txtOverview").val("");
            // $("#txtFilePath").val("");
            $("#chkIsGift").prop("checked", false);
            $("#chkIsPublished").prop("checked", false);
        }

        function GetContentList(cntrl) {

            $("#dvJson").html(JSON.stringify(contentList));

            var tableBody = $("#tblContent #tBodyContent");
            tableBody.html("");
            if (contentList.length == 0) {
                tableBody.append("<td colspan='10'><center>No Contents</center></td>");
            }
            $.grep(contentList, function (content, i) {
                try {
                    var isGiftValue = content.IsGift == true ? "Checked disabled" : "disabled";
                    var isPublishedValue = content.IsPublished == true ? "Checked disabled" : "disabled";
                    var markup = "<tr>";
                    markup += "<td>" + content.SrNo + "</td>";
                    markup += "<td>" + content.DocType + "</td>";
                    markup += "<td>" + content.Title + "</td>";
                    markup += "<td>" + content.Description + "</td>";
                    markup += "<td>" + content.Overview + "</td>";
                    markup += "<td>" + content.FilePath + "</td>";
                    markup += "<td><input type='checkbox' " + isPublishedValue + " /></td>";
                    markup += "<td><input type='checkbox' " + isGiftValue + " /></td>";
                    markup += "<td index=" + content.ContentID + " onclick ='EditContent($(this))'>Edit</td>";
                    markup += "<td index=" + content.ContentID + " onclick ='DeleteContent($(this))'>Delete</td>";
                    markup += "</tr>";
                    tableBody.append(markup);
                }
                catch (ex) {
                    alert("Exception occured." + ex.message);
                }
            });
        }

        function EditContent(row) {
            var index = $(row).attr("index");

            var content = $.grep(contentList, function (n, i) {
                return n.ContentID == parseInt(index);
            })[0];

            $("#ddlDocType").val(content.DocType);
            $("#txtTitle").val(content.Title);
            $("#txtDescription").val(content.Description);
            $("#txtOverview").val(content.Overview);
            $("#txtFilePath").val(content.FilePath);
            $("#chkIsGift").prop("checked", content.IsGift);
            $("#chkIsPublished").prop("checked", content.IsPublished);

            $("#btnSaveChanges").text("Save Content");
            $("#btnSaveChanges").attr("index", index);
            $("#btnCancel").show();
        }

        function DeleteContent(row) {
            var index = $(row).attr("index");

            contentList = $.grep(contentList, function (n, i) {
                return n.ContentID != parseInt(index);
            });

            if (contentList == null)
                contentList = [];

            // Ajax Call

            GetContentList(row);
        }

        function IsTitleDuplicate(contents, title, ID) {
            var duplicateTitle = false;
            $.grep(contents, function (n, i) {
                if (n.Title.trim().toUpperCase() == title.trim().toUpperCase() && n.ContentID != ID) {
                    duplicateTitle = true;
                    return false;
                }
            });
            return duplicateTitle;
        }

        function Cancel(cntrl) {
            $("#btnCancel").hide();
            $("#btnSaveChanges").show();
            $("#btnSaveChanges").text("Add Content");
            $("#btnSaveChanges").removeAttr("index");
            ClearAllFields(this);
        }
    </script>
</asp:Content>
