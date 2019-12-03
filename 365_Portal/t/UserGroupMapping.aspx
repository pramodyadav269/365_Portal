<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="UserGroupMapping.aspx.cs" Inherits="_365_Portal.t.UserGroupMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">User Group Mapping</h1>
        </div>


        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="w-100"></div>
                    <div id="divTable" class="mt-3 table-responsive"></div>
                </div>
            </div>
        </div>
    </div>

    <script>

        var accessToken = '<%=Session["access_token"]%>';
        var Role = '<%=Session["RoleName"]%>';
        var id = '';

        $(document).ready(function () {            
            debugger
            ShowLoader();
            GetUsersGroup();
        });
        
        function GetUsersGroup() {
            debugger
            var getUrl = "/API/User/GetUsersGroup";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                contentType: "application/json",
                success: function (response) {
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        debugger
                        if (DataSet.StatusCode == "1") {
                            BindTable(DataSet.Data);
                        }
                        else {
                            Swal.fire(DataSet.StatusDescription, {
                                icon: "error",
                            });
                        }
                    }
                    catch (e) {
                        HideLoader();
                    }
                },
                failure: function (response) {
                    HideLoader();
                }
            });
        }

        function BindTable(Table) {
            debugger
            $('#divTable').empty().append();
            var tbl = '<table id="tblGird" class="table table-bordered" style="width:100%">' +
                '<thead><tr><th>#</th><th>Email ID</th><th>Group</th><th>Action</th></thead>'

            tbl += '<tbody>';

            if (Table != undefined && Table.length > 0)
            {
                var CurrentUserID = '';
                for (var i = 0; i < Table.length; i++)
                {
                    CurrentUserID = Table[i].UserID;

                    tbl += '<tr>';
                    tbl += '<td>' + (i + 1) + '</td>';
                    tbl += '<td title="' + Table[i].EmailID + '" >' + Table[i].EmailID + '</td>';
                    //tbl += '<td title="' + Table[i].GroupName + '" >' + Table[i].GroupName + '</td>';
                    tbl += '<td title="' + Table[i].GroupName + '" >';

                    for (var j = i; j < Table.length; j++)
                    {
                        if (Table[j].UserID == CurrentUserID)
                        {
                            if (Table[j].IsActive == "1") {
                                tbl += '<input type="checkbox" value="' + Table[j].GroupId + '" name="' + Table[j].GroupId + '" checked>' + Table[j].GroupName;
                            }
                            else {
                                tbl += '<input type="checkbox" value="' + Table[j].GroupId + '" name="' + Table[j].GroupId + '">' + Table[j].GroupName;
                            }
                            i++;
                        }
                    }                    

                    tbl += '</td>';
                    tbl += '<td><i title="Edit" onclick="Edit(this,' + Table[i].GroupID + ');" class="fas fa-edit text-warning"></i>' +
                        '<i title="Delete" onclick="Delete(this,' + Table[i].GroupID + ');" class="fas fa-trash text-danger"></i></td>';
                    tbl += '</tr>';
                }
            }
            tbl += '</tbody>';
            tbl += '</table>';
            $('#divTable').empty().append(tbl);
            $('#tblGird').DataTable();
        }

    </script>

</asp:Content>