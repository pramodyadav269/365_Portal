<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="UserGroupMapping.aspx.cs" Inherits="_365_Portal.t.UserGroupMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">User Group Mapping</h2>
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
                '<thead><tr><th>Sr.No.</th><th>Email ID</th><th>Group</th><th>Action</th></thead>'

            tbl += '<tbody>';

            if (Table != undefined && Table.length > 0)
            {
                var CurrentUserID = '';
                var srno = 1;
                for (var i = 0; i < Table.length; i++)
                {
                    CurrentUserID = Table[i].UserID;

                    tbl += '<tr>';
                    tbl += '<td width="5%">' + (srno) + '</td>';
                    tbl += '<td width="23%" title="' + Table[i].EmailID + '" >' + Table[i].EmailID + '</td>';
                    tbl += '<td width="65%" title="' + Table[i].GroupName + '" >';

                    for (var j = i; j < Table.length; j++)
                    {
                        if (Table[j].UserID == CurrentUserID)
                        {
                            if (Table[j].IsActive == "1") {
                                tbl += '<input type="checkbox" value="' + Table[j].GroupId + '" name="' + Table[j].UserID + '" checked>' + Table[j].GroupName;
                            }
                            else {
                                tbl += '<input type="checkbox" value="' + Table[j].GroupId + '" name="' + Table[j].UserID + '">' + Table[j].GroupName;
                            }
                            i++;
                        }
                    }
                    i--;

                    tbl += '</td>';
                    tbl += '<td width="7%"><a class="btn bg-yellow" onclick="submit(this,' + Table[i].UserID + ');">Submit</a>';
                    tbl += '</tr>';

                    srno++;
                }
            }
            tbl += '</tbody>';
            tbl += '</table>';
            $('#divTable').empty().append(tbl);
            //$('#tblGird').DataTable();
        }

        function submit(obj, userID)
        {
            ShowLoader();
            var UserID = userID;
            var GroupID = '';
            $.each($("input[name='"+userID+"']:checked"), function () {               
                GroupID = $(this).val() + "," + GroupID;
            });

            GroupID = GroupID.replace(/\,$/, '');
            var requestParams = { UserID: UserID, GroupID: GroupID };

            var getUrl = "/API/User/UpdateUserMapping";

            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {
                        debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {
                            Swal.fire(DataSet.Data[0].ReturnMessage, {
                                icon: "success",
                            }).then((UpdateUserMapping) => {
                                location.reload();
                            });;
                        }
                        else {
                            if (DataSet.Data != undefined && DataSet.Data.length > 0) {
                                Swal.fire(DataSet.Data[0].ReturnMessage, {
                                    icon: "error",
                                });
                            }
                            else {
                                Swal.fire(DataSet.StatusDescription, {
                                    icon: "error",
                                });
                            }
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

    </script>

</asp:Content>