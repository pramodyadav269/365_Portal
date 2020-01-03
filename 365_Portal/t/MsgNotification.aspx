<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="MsgNotification.aspx.cs" Inherits="_365_Portal.t.MsgNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Message Notification</h2>
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
            ShowLoader();
            GetMsgNotification();
        });

        function GetMsgNotification() {

            var getUrl = "/API/Trainning/GetMsgNotifications";
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

                        if (DataSet.StatusCode == "1")
                        {
                            BindTable(DataSet.Data);                            
                            $('#lblNotiCount').text("");
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

            $('#divTable').empty().append();
            var tbl = '<table id="tblGird" class="table table-bordered" style="width:100%">' +
                '<thead><tr><th>Sr.No.</th><th>Message Notification</th></thead>'

            tbl += '<tbody>';

            if (Table != undefined && Table.length > 0)
            {                
                for (var i = 0; i < Table.length; i++)
                {                    
                    tbl += '<tr>';
                    tbl += '<td width="5%">' + (i + 1) + '</td>';
                    tbl += '<td width="95%" title="' + Table[i].Message + '" >' + Table[i].Message + '</td>';
                    tbl += '</tr>';
                }
            }
            tbl += '</tbody>';
            tbl += '</table>';
            $('#divTable').empty().append(tbl);
        }
    </script>


</asp:Content>
