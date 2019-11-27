<%@ Page Title="Content List" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="_365_Portal.Admin.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Content List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" id="back"><i class="fas fa-arrow-left"></i>Back to Modules</a>
            <h1 class="text-center font-weight-bold">Contents</h1>
        </div>

        <div class="col-md-12">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row input-validation">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlDocType">Doc Type</label>
                                <select class="form-control select2 required" id="ddlDocType" style="width: 100% !important">
                                    <option></option>
                                    <option value="PDF">PDF</option>
                                    <option value="VIDEO">Video</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtTitle">Title</label>
                                <input type="text" class="form-control required" id="txtTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Gift</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsGift" name="chkIsGift" class="custom-control-input">
                                    <label class="custom-control-label" for="chkIsGift">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsPublished" name="chkIsPublished" class="custom-control-input">
                                    <label class="custom-control-label" for="chkIsPublished">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>


                        <%--<div class="col-md-6">
                            <div class="form-group">
                                <label for="txtOverview">Overview</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Overview" id="txtOverview"></textarea>
                            </div>
                        </div>--%>
                        <div class="col-md-3">
                            <div class="custom-radio">
                                <input type="radio" class="custom-radio-input" id="rd_url" name="filetype" checked="checked" value="URL" onchange="ShowControl(this)">
                                <label class="custom-radio-label" for="filetype">File Url</label>

                                <input type="radio" class="custom-radio-input" id="rd_file" name="filetype" value="FILE" onchange="ShowControl(this)">
                                <label class="custom-radio-label" for="filetype">Upload File</label>
                            </div>
                        </div>
                        <div class="col-md-3" style="display: none" id="div_fileupload">
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="filepath" onchange="encodeImagetoBase64(this)" name="file">
                                <label class="custom-file-label" for="customFile" id="lblfilepath">File Path</label>
                            </div>
                        </div>
                        <div class="col-md-3" style="display: none" id="div_preview"><a id="preview">Preview Your ContentFile</a></div>
                        <div class="col-md-3" style="display: none" id="div_fileUrl">
                            <div class="form-group">
                                <label for="txtFileUrl">File Url</label>
                                <input type="text" class="form-control" id="txtFileUrl" placeholder="File Url" />
                            </div>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <%--<a class="btn bg-yellow float-left" id="btnCancel" onclick="toggle('divGird', 'divForm')">Back</a>--%>


                            <div class="float-right">
                                <a class="btn bg-yellow " id="btnSaveChanges" onclick="SaveChanges(this);return false;">Add Content</a>
                                <a class="btn bg-yellow" id="btnCancel" onclick="Cancel(this);return false;" style="display: none;">Cancel</a>
                                <a class="btn bg-blue text-white float-right" style="display: none;" id="savereorder" onclick="SaveGrid();">Save Reordering</a>

                            </div>
                        </div>
                    </div>


                    <div class="row mt-4">
                        <div class="col-md-12">
                            <div id="dvJson"></div>
                            <div id="divTable" class="mt-3 table-responsive">
                                <table id="tblContent" class="table table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Sr No</th>
                                            <th>Doc Type</th>
                                            <th>Title</th>
                                            <th>Description</th>
                                            <%--<th>Overview</th>--%>
                                            <th>File Path</th>
                                            <th>Is Gift</th>
                                            <th>Is Published</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tBodyContent"></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<table id="tblContent" border="1">
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
    </table>--%>
    <script>
        var accessToken = '<%=Session["access_token"]%>';
        contentList = [];
        var TopicID;
        var _ModuleID;
        var TypeID;
        var base64filestring;
        var FileURL;
        var allowedExtensions = ['pdf', 'mp4', 'avi', 'flv', 'wmv', 'mov', '3gp', 'webm', 'wav'];
        $(document).ready(function () {
           
            TopicID = GetParameterValues('TopicID');
            _ModuleID = GetParameterValues('ModuleID');
            $('#back').attr('href', "./Modules.aspx?Id=" + TopicID);
            GetContentList(this);
            ClearAllFields(this);
            ShowControl();

        });

        function SaveChanges(cntrl) {
            var regex = '';
           
            var formdata = new FormData();
            if ($("#ddlDocType").val() != "" &&
                $("#txtTitle").val() != "" &&
                $("#txtDescription").val() != "" &&
                TopicID != "" && _ModuleID != ""  ) {
                var isUrl;
            
                if ($("input[name=filetype]:checked").val() == "URL") {
                    isUrl = 1;
                    if (is_valid_url($('#txtFileUrl').val())) {
                        //base64filestring = $('#txtFileUrl').val();//Assigning url value
                        FileURL = $('#txtFileUrl').val();//Assigning url value
                    }
                    else {
                        Swal.fire({
                            title: "Failure",
                            text: "Entered url is not in Correct format.Please put Url in following format for eg https://www.google.com/",
                            icon: "error"
                        });
                        return false;
                    }
                }
                else {
                    isUrl = 0;
                    base64filestring = $('#filepath').get(0).files[0];
                }

                var index = contentList.length + 1;
                //var newContent = {
                //    ContentID: (index),
                //    SrNo: index,
                //    DocType: $("#ddlDocType").val()
                //    , Title: $("#txtTitle").val()
                //    , Description: $("#txtDescription").val()
                //    , Overview: ""
                //    , ContentFileID: base64filestring
                //    , IsGift: $("#chkIsGift").prop("checked")
                //    , IsPublished: $("#chkIsPublished").prop("checked")
                //    , TopicID: TopicID
                //    , ModuleID: _ModuleID
                //    , TypeID: "1"
                //    , FlashcardTitle: ""
                //    , IsActive: ""
                //    , TotalScore: ""
                //    , PassingPercent: ""
                //    , PassingScore: ""
                //    , FlashcardHighlights: ""
                //    , SkipFlashcards: ""
                //    , IsURL: isUrl

                //};

                if ($(cntrl).attr("index") == null) {
                    // Add Content
                    // Ajax Call
                    if (IsTitleDuplicate(contentList, $("#txtTitle").val())) {
                        //alert("Title cannot be duplicate.");
                        Swal.fire({
                            title: "Failure",
                            text: "Title cannot be duplicate.",
                            icon: "error"
                        });
                        return false;
                    }
                    

                    formdata.append("ContentID",(index));
                    formdata.append("SrNo",index);
                    formdata.append("DocType", $("#ddlDocType").val());
                    formdata.append("Title",$("#txtTitle").val());
                    formdata.append("Description", $("#txtDescription").val());
                    formdata.append("Overview", "");
                    if( ($('#filepath').val()!="" &&  $('#filepath').val()!=undefined) && isUrl==0 && $("input[name=filetype]:checked").val() != "URL")
                    {
                        var file = $('#filepath').get(0).files;
                        if (is_Valid_file(file[0]) && file.length>0) {
                         
                            formdata.append("ContentFileID", file[0]);
                        }
                        else {                         
                            return false;
                        }
                    }
                    else
                    {
                        if (is_valid_url(FileURL)) {
                            formdata.append("ContentFileID", FileURL);
                        }
                        else {
                            Swal.fire({
                                title: "Failure",
                                text: "Invalid URl",
                                icon: "error"
                            });
                            return false;
                        }
                    }
                    formdata.append( "IsGift", $("#chkIsGift").prop("checked"));
                    formdata.append( "IsPublished", $("#chkIsPublished").prop("checked"));
                    formdata.append( "TopicID", TopicID);
                    formdata.append( "ModuleID", _ModuleID);
                    formdata.append( "TypeID", "1");
                    formdata.append( "FlashcardTitle", "");
                    formdata.append( "IsActive", "");
                    formdata.append( "TotalScore", "");
                    formdata.append( "PassingPercent", "");
                    formdata.append( "PassingScore", "");
                    formdata.append( "FlashcardHighlights","");
                    formdata.append( "SkipFlashcards", "");
                    formdata.append( "IsURL", isUrl);
                    ShowLoader();
                  
                    $.ajax({
                        type: "POST",
                        url: "/API/Content/CreateContent",
                        headers: { "Authorization": "Bearer " + accessToken },                        
                        data:formdata,                       
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            try {
                          
                                var DataSet = $.parseJSON(response);
                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {
                                        ClearAllFields(this);
                                        HideLoader();
                                        Swal.fire({
                                            title: "Success",
                                            text: DataSet.StatusDescription,
                                            icon: "success",
                                        });
                                        //contentList.push(newContent);
                                        GetContentList(this);

                                    }
                                    else {
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: DataSet.StatusDescription,
                                            icon: "error"
                                        });
                                    }
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            catch (e) {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();
                        },
                        failure: function (response) {
                            HideLoader();
                            alert(response.data);
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                icon: "error"
                            });
                        }
                    });




                }
                else {
                    // Update Content
                    var index = $(cntrl).attr("index");
                    //newContent.ContentID = index;
                    if (IsTitleDuplicate(contentList, $("#txtTitle").val().replace(' ',''), index)) {
                        alert("Title cannot be duplicate.");
                        return false;
                    }


                    //*************** Old Logic ***************//
                    //var oldContent = $.grep(contentList, function (n, i) {
                    //    return n.ContentID == parseInt(index);
                    //})[0];
                    //oldContent.DocType = newContent.DocType;
                    //oldContent.Title = newContent.Title;
                    //oldContent.Description = newContent.Description;
                    //oldContent.Overview = newContent.Overview;
                    //if (newContent.IsURL == 1) {
                    //    if (oldContent.FilePath.trim() != newContent.ContentFileID.trim()) {
                    //        oldContent.FilePath = newContent.ContentFileID.trim();
                    //    }
                    //    else {
                    //        oldContent.FilePath = newContent.ContentFileID.trim();
                    //    }
                    //}
                    //else {
                    //    oldContent.FilePath = newContent.ContentFileID;
                    //}
                    //oldContent.IsGift = newContent.IsGift;
                    //oldContent.IsPublished = newContent.IsPublished;
                    //*************** Old Logic ***************//
                    //*************** New  Logic with form ***************//                   

                    formdata.append("ContentID", index);
                    formdata.append("DocType", $("#ddlDocType").val());
                    formdata.append("Title", $("#txtTitle").val());
                    formdata.append("Description", $("#txtDescription").val());
                    formdata.append("Overview", "");
                    if (($('#filepath').val() != "" && $('#filepath').val() != undefined) && isUrl == 0 && $("input[name=filetype]:checked").val() != "URL") {
                        var file = $('#filepath').get(0).files;
                        if (is_Valid_file(file[0]) && file.length > 0) {

                            formdata.append("ContentFileID", file[0]);
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        if (is_valid_url(FileURL)) {
                            formdata.append("ContentFileID", FileURL);
                        }
                        else {
                            Swal.fire({
                                title: "Failure",
                                text: "Invalid URl",
                                icon: "error"
                            });
                            return false;
                        }
                    }
                    formdata.append("IsGift", $("#chkIsGift").prop("checked"));
                    formdata.append("IsPublished", $("#chkIsPublished").prop("checked"));
                    formdata.append("TopicID", TopicID);
                    formdata.append("ModuleID", _ModuleID);
                    formdata.append("TypeID", "1");
                    formdata.append("FlashcardTitle", "");
                    formdata.append("IsActive", "");
                    formdata.append("TotalScore", "");
                    formdata.append("PassingPercent", "");
                    formdata.append("PassingScore", "");
                    formdata.append("FlashcardHighlights", "");
                    formdata.append("SkipFlashcards", "");
                    formdata.append("IsURL", isUrl);
                    ShowLoader();

                    // Ajax Call
                    $.ajax({
                        type: "POST",
                        url: "/API/Content/ModifyContent",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: formdata,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            try {

                                var DataSet = $.parseJSON(response);
                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {
                                        ClearAllFields(this);
                                        HideLoader();
                                        Swal.fire({
                                            title: "Success",
                                            text: DataSet.StatusDescription,
                                            icon: "success",

                                        });
                                        GetContentList(this);
                                    }
                                    else {
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: DataSet.StatusDescription,
                                            icon: "error"
                                        });
                                    }
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            catch (e) {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();
                        },
                        failure: function (response) {
                            HideLoader();
                            alert(response.data);
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                icon: "error"
                            });
                        }
                    });
                }

                Cancel(cntrl);
                GetContentList(cntrl);

            }
            else {
             
                Swal.fire({
                    title: "Failure",
                    text: "Please enter all required fields.",
                    icon: "error"
                });
            }
        }

        function ClearAllFields(cntrl) {
            $("#ddlDocType").val("");
            $("#txtTitle").val("");
            $("#txtDescription").val("");
            $("#txtOverview").val("");
            $("#filepath").val("");
            $('.custom-file-label').html('File Path');
            $("#chkIsGift").prop("checked", false);
            $("#chkIsPublished").prop("checked", false);
            $('#rd_url').prop('checked', true);
            $('#div_fileUrl').show();
            $('#div_fileupload').hide();
            $('#div_preview').hide();
            $('#txtFileUrl').val("");
            $("#ddlDocType").trigger('change');
        }

        function GetContentList(cntrl) {
            if ((TopicID != null && TopicID != '') && (_ModuleID != '' && _ModuleID != null)) {
                ShowLoader();
                var url = "/API/Content/GetContentList";
                try {
                    var requestParams = { TopicID: TopicID, ModuleID: _ModuleID, ContentID: "", ContentTypeID: "", IsGift: "true" };
                    $.ajax({
                        type: "POST",
                        url: url,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        processData: false,
                        success: function (response) {
                            if (response != null && response != undefined) {
                                //$("#dvJson").html(JSON.stringify(contentList));
                                var list = JSON.parse(response);
                                if (list.StatusCode = "1") {
                                    contentList = [];
                                    for (var i = 0; i < list.Data.length; i++) {
                                        contentList.push(list.Data[i]);
                                    }

                                    var tableBody = $("#tblContent #tBodyContent");
                                    tableBody.html("");
                                    if (contentList.length == 0) {
                                        tableBody.append("<td colspan='10'><center>No Contents</center></td>");
                                    }
                                    $.grep(contentList, function (content, i) {
                                        try {
                                            var isGiftValue = Number(content.IsGift) == "1" ? "Checked disabled" : "disabled";
                                            var isPublishedValue = Number(content.IsPublished) == "1" ? "Checked disabled" : "disabled";
                                            var FilePath = "";
                                        
                                            if (content.FilePath != "" && content.FilePath != undefined) {
                                                if (content.FilePath.split('.')[1] != undefined) {
                                                    if (allowedExtensions.indexOf(content.FilePath.split('.')[1])!=-1) {
                                                        FilePath = '/Files/Content/' + content.FilePath;
                                                    }
                                                    else {
                                                        FilePath = content.FilePath;
                                                    }
                                                }
                                                else {
                                                    FilePath = content.FilePath;
                                                }
                                            }
                                            var markup = '<tr id="' + content.ContentID + '">';
                                            markup += "<td>" + (i + 1) + "</td>";
                                            markup += "<td>" + content.DocType + "</td>";
                                            markup += "<td>" + content.Title + "</td>";
                                            markup += "<td>" + content.Description + "</td>";
                                            if (FilePath != '' && FilePath != undefined) {
                                                if (is_valid_url(FilePath)) {
                                                    markup += "<td><a href=" + FilePath + " target=_blank data-action='navigate'>"+FilePath+"</a></td>";
                                                }
                                                else {
                                                    markup += "<td><a href=" + FilePath + " target=_blank>File</a></td>";
                                                }
                                            }
                                            else {
                                                markup += "<td></td>";
                                            }
                                            markup += "<td><input type='checkbox' " + isGiftValue + " /></td>";
                                            markup += "<td><input type='checkbox' " + isPublishedValue + " /></td>";
                                            markup += '<td><i title="Edit" index=' + content.ContentID + ' onclick="EditContent($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + content.ContentID + ' onclick="DeleteContent($(this));" class="fas fa-trash text-danger"></i></td>';

                                            markup += "</tr>";
                                            tableBody.append(markup);
                                        }
                                        catch (ex) {

                                            Swal.fire({
                                                title: "Failure",
                                                text: "Exception occured." + ex.message,
                                                icon: "error"
                                            });
                                        }
                                    });
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            $('#tblContent').DataTable();
                            $('#tblContent').tableDnD({
                                onDragStart: function (table, row) {
                                    $('#savereorder').show();

                                }
                            });
                            HideLoader();
                        }
                    });
                }
                catch (e) {
                    HideLoader();
                    Swal.fire({
                        title: "Failure",
                        text: "Please try Again",
                        icon: "error"
                    });
                }
            }
            else {
                var tableBody = $("#tblContent #tBodyContent");
                tableBody.html("");
                if (contentList.length == 0) {
                    tableBody.append("<td colspan='10'><center>No Contents</center></td>");
                }
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"
                });
            }
        }

        function EditContent(row) {
            try {
                var index = $(row).attr("index");

                var content = $.grep(contentList, function (n, i) {
                    return n.ContentID == parseInt(index);
                })[0];

                $("#ddlDocType").val(content.DocType);
                $("#txtTitle").val(content.Title);
                $("#txtDescription").val(content.Description);
                if (content.FilePath.split('.')[1] != undefined) {
                    if (allowedExtensions.indexOf(content.FilePath.split('.')[1]) != -1) {
                        $('#rd_file').prop('checked', true);
                        $("#preview").attr("href", "/Files/Content/" + content.FilePath);
                        $("#preview").attr("target", '_blank');
                        $('#div_fileupload').show();
                        $('#div_preview').show();
                        $('#div_fileUrl').hide();
                    }
                    else {
                        $('#rd_url').prop('checked', true);
                        $('#txtFileUrl').val(content.FilePath);
                        $('#div_fileupload').hide();
                        $('#div_fileUrl').show();
                        $('#div_preview').hide();
                    }
                } else {
                    $('#rd_url').prop('checked', true);
                    $('#txtFileUrl').val(content.FilePath);
                    $('#div_fileupload').hide();
                    $('#div_fileUrl').show();
                    $('#div_preview').hide();
                }

                $("#chkIsGift").prop("checked", Number(content.IsGift));
                $("#chkIsPublished").prop("checked", Number(content.IsPublished));
                TypeID = content.TypeID;
                $("#btnSaveChanges").text("Save Content");
                $("#btnSaveChanges").attr("index", index);
                $("#btnCancel").show();
                $("#ddlDocType").trigger('change');
            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"
                });
            }
        }

        function DeleteContent(row) {
            debugger
            var index = $(row).attr("index");

            contentList = $.grep(contentList, function (n, i) {
                return n.ContentID != parseInt(index);
            });

            if (contentList != null) {

                var newcontentlist = { p_ContentID: index }
                // Ajax Call
                Swal.fire({
                    title: 'Are you sure?',
                    text: "Do you want to delete user!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.value) {
                        ShowLoader();
                        // Ajax Call
                        $.ajax({
                            type: "POST",
                            url: "/API/Content/DeleteContent",
                            headers: { "Authorization": "Bearer " + accessToken },
                            data: JSON.stringify(newcontentlist),
                            contentType: "application/json",
                            success: function (response) {
                                try {
                                    contentList = [];
                                    var DataSet = $.parseJSON(response);
                                    if (DataSet != null && DataSet != "") {
                                        if (DataSet.StatusCode == "1") {
                                            GetContentList(row);
                                            HideLoader();
                                            Swal.fire({
                                                title: "Success",
                                                text: DataSet.StatusDescription,
                                                icon: "success",
                                            });
                                        }
                                        else {
                                            HideLoader();
                                            Swal.fire({
                                                title: "Failure",
                                                text: DataSet.StatusDescription,
                                                icon: "error"
                                            });
                                        }
                                    }
                                    else {
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: "Please try Again",
                                            icon: "error"
                                        });
                                    }
                                }
                                catch (e) {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            },
                            complete: function () {
                                HideLoader();
                            },
                            failure: function (response) {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        });
                    }
                });
            }
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

        //Enode the file to base64
        function encodeImagetoBase64(element) {
            //debugger
            if (element != null && element != undefined) {
                var file = element.files[0];
                var size = file.size;
                var allowedExtensions = ['pdf', 'mp4', 'avi', 'flv', 'wmv', 'mov', '3gp', 'webm', 'wav'];

                if (file.size != undefined) {
                    if (allowedExtensions.indexOf(file.name.split('.')[1]) != -1) {
                        if (file.size < 5000000) {
                            var reader = new FileReader();
                            reader.onloadend = function () {
                                base64UserProfileString = reader.result;
                            }
                            reader.readAsDataURL(file);
                        }
                        else {
                            base64UserProfileString = "";
                            $('#filepath').val('');
                            Swal.fire({
                                text: "Error",
                                title: "File size should not be greater than 5MB",
                                icon: "error",
                            });

                            $('#lblfilepath').html("File Path");
                        }
                    }
                    else {

                        base64UserProfileString = "";
                        $('#filepath').val('');
                        Swal.fire({
                            title:"Error",
                            text: "Invalid File format! Allowed file formats are pdf,mp4,avi,flv,wmv,mov,3gp,webm,wav",
                            icon: "error",
                        });
                        $('#lblfilepath').html("File Path");
                    }
                }
                else {
                    base64UserProfileString = "";
                    $('#filepath').val('');
                    Swal.fire({
                        title: "Error",
                        text: "Invalid File",
                        icon: "error",
                    });
                    $('#lblfilepath').html("File Path");
                }
            }
            else {
                Swal.fire({
                    title: "Error",
                    text: "No Files Selected",
                    icon: "error",
                });
            }
        }

        //This funcion is to get and save changes of Serial No
        function SaveGrid() {
            try {
                ShowLoader();
                var sqnData = "";
                var array = [];
                var url = "/API/Content/ReOrderContent";
                $.each($('#tblContent tbody tr'), function (i, data) {

                    sqnData += $(data).attr('id') + ",";
                });
                sqnData = sqnData.replace(/,(?=\s*$)/, '');
                if (sqnData != "") {
                    var requestParams = { Type: "3", IDs: sqnData };
                    $.ajax({
                        type: "POST",
                        url: url,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        processData: false,
                        success: function (response) {
                            if (response != null && response != undefined) {
                                var DataSet = $.parseJSON(response);
                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {
                                        if (DataSet.Data.length > 0) {
                                            $('#savereorder').hide();
                                            GetContentList(this);
                                        }
                                        else {
                                            $('#savereorder').hide();
                                            Swal.fire({
                                                title: "Failure",
                                                text: "Please try Again",
                                                icon: "error"
                                            });
                                        }
                                    }
                                    else {
                                        $('#savereorder').hide();
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: DataSet.StatusDescription,
                                            icon: "error"
                                        });
                                    }
                                }
                                else {
                                    $('#savereorder').hide();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            else {
                                $('#savereorder').hide();
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();


                        }
                    });
                }
                else {
                    $('#savereorder').hide();
                    Swal.fire({
                        title: "Failure",
                        text: "Please try Again",
                        icon: "error"
                    });

                }

            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"
                });
            }

        }


        function is_Valid_file(file)
        {
            if (file != null && file != undefined ) {
                //var file = element.files[0];
                var size = file.size;
                var allowedExtensions = ['pdf', 'mp4', 'avi', 'flv', 'wmv', 'mov', '3gp', 'webm', 'wav'];

                if (file.size != undefined) {
                    if (allowedExtensions.indexOf(file.name.split('.')[1]) != -1) {
                        if (file.size < 5000000) {
                            return true;
                        }
                        else {
                            base64UserProfileString = "";
                            $('#filepath').val('');
                            
                            Swal.fire({
                                text: "Error",
                                title: "File size should not be greater than 5MB",
                                icon: "error",
                            });

                            $('#lblfilepath').html("File Path");
                            return false;
                        }
                    }
                    else {

                        base64UserProfileString = "";
                        $('#filepath').val('');
                        Swal.fire({
                            title: "Error",
                            text: "Invalid File format! Allowed file formats are pdf,mp4,avi,flv,wmv,mov,3gp,webm,wav",
                            icon: "error",
                        });
                        $('#lblfilepath').html("File Path");
                        return false;
                    }
                }
                else {
                    base64UserProfileString = "";
                    $('#filepath').val('');
                    Swal.fire({
                        title: "Error",
                        text: "Invalid File",
                        icon: "error",
                    });
                    $('#lblfilepath').html("File Path");
                    return false;
                }
            }
            else {
                Swal.fire({
                    title: "Error",
                    text: "No Files Selected",
                    icon: "error",
                });
                return false;
            }
        }


        function ShowControl(ctrl) {

            var linkvalue = $("input[name=filetype]:checked").val();
            if (linkvalue == "URL") {
                $('#div_fileUrl').show();
                $('#div_fileupload').hide();
                $('#div_preview').hide();
            }
            else {
                $('#div_fileUrl').hide();
                $('#div_fileupload').show();
            }
        }


        function is_valid_url(url) {
            return /^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)/.test(url);
        }
    </script>
</asp:Content>
