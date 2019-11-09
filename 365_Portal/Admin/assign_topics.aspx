<%@ Page Title="Quiz" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="assign_topics.aspx.cs" Inherits="_365_Portal.Admin.AssignTopics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quiz</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Assign Topics</h1>
        </div>
        <div>
            Assign Mode:
            <input type="radio" id="rblBulk" name="TopicAssignment" value="BULK" onchange="BindGroupUserCheckboxList();">
            <label for="rblBulk">Assign Multiple</label>
            <input type="radio" id="tblIndividual" name="TopicAssignment" value="INDIVIDUAL" onchange="BindGroupUserCheckboxList();">
            <label for="tblIndividual">Assign Individually</label>
            <br />
            Assign By:
            <select id="ddlAssignType" onchange="BindGroupUserCheckboxList(this);">
                <option value="-1">--Select--</option>
                <option value="GROUP">Group</option>
                <option value="USER">User</option>
            </select>

            <div id="dvGroupContainer" style="display: none;">
                <h2>Select Groups</h2>
                <div id="dvGroups"></div>
            </div>
            <div id="dvUserContainer" style="display: none;">
                <h2>Select Users</h2>
                <div id="dvUsers"></div>
            </div>

            <div id="dvTopicContainer" style="display: none;">
                <h2>Select Topics</h2>
                <div id="dvTopics"></div>
            </div>

            <br />
            <br />
            <button id="btnSubmit" style="display: none;" onclick="SaveChanges();return false;">Save Changes</button>
            <br />
            <label id="lblJSON"></label>

        </div>
    </div>
    <script>
        $(document).ready(function () {
            BindTopics();
        });

        function BindTopics() {
            var htmlCheckboxes = "";

            var topics = [{ "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" },
            { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" },
            { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }, { "TopicID": "1", "TopicName": "Topic" }
            ];

            $.each(topics, function (index, topic) {
                //htmlCheckboxes += '<input type="checkbox" name="chk_"' + topic.TopicName + ' value="' + topic.TopicID + '"><label for="' + topic.TopicID + '">' + topic.TopicName + '</label>';
                htmlCheckboxes += '<input type="checkbox" id="chkTopic_' + index + '" name="TOPIC" value="' + index + '"><label for="' + index + '">' + topic.TopicName + '</label>';
            });

            $("#dvTopics").html(htmlCheckboxes);
        }

        function BindGroupUserCheckboxList(cntrl) {
            $("#dvGroupContainer").hide();
            $("#dvUserContainer").hide();
            $("#dvTopicContainer").show();
            $("#btnSubmit").show();

            $("#dvGroups").html("");
            $("#dvUsers").html("");
            var htmlCheckboxes = "";

            if ($("#ddlAssignType").val() == "GROUP") {

                var groupList = [{ "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" },
                { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }, { "GroupID": "1", "GroupName": "Group1" }
                ];

                if ($("input[name='TopicAssignment']:checked").val() == "BULK") {
                    $.each(groupList, function (index, group) {
                        //htmlCheckboxes += '<input type="checkbox" name="chk_"' + group.GroupName + ' value="' + group.GroupID + '"><label for="' + group.GroupID + '">' + group.GroupName + '</label>';
                        htmlCheckboxes += '<input type="checkbox" name="GROUP" value="' + index + '"><label for="' + index + '">' + group.GroupName + '</label>';
                    });
                }
                else {
                    htmlCheckboxes += '<select id="ddlGroup" onchange="GetSelectedTopics(this)";>';
                    htmlCheckboxes += '<option value="-1">--Select--</option>';
                    $.each(groupList, function (index, group) {
                        htmlCheckboxes += '<option value="' + index + '">' + group.GroupName + index + '</option>';
                    });
                    htmlCheckboxes += '</select>';
                }

                $("#dvGroups").html(htmlCheckboxes);
                $("#dvGroupContainer").show();
            }
            else if ($("#ddlAssignType").val() == "USER") {
                var userList = [{ "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" },
                { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }, { "UserId": "1", "UserName": "Yogesh" }
                ];

                if ($("input[name='TopicAssignment']:checked").val() == "BULK") {
                    $.each(userList, function (index, user) {
                        //htmlCheckboxes += '<input type="checkbox" name="chk_"' + user.UserName + ' value="' + user.UserId + '"><label for="' + user.UserId + '">' + user.UserName + '</label>';
                        htmlCheckboxes += '<input type="checkbox" name="USER" value="' + index + '"><label for="' + index + '">' + user.UserName + '</label>';
                    });
                }
                else {
                    htmlCheckboxes += '<select id="ddlUser" onchange="GetSelectedTopics(this)";>';
                    htmlCheckboxes += '<option value="-1">--Select--</option>';
                    $.each(userList, function (index, user) {
                        htmlCheckboxes += '<option value="' + index + '">' + user.UserName + index + '</option>';
                    });
                    htmlCheckboxes += '</select>';
                }

                $("#dvUsers").html(htmlCheckboxes);
                $("#dvUserContainer").show();
            }
            else {
                $("#dvTopicContainer").hide();
                $("#btnSubmit").hide();
            }
        }

        function GetSelectedTopics(cntrl) {
            var topicIds = [1, 4, 10, 12, 2, 3];

            if (cntrl.id == 'ddlGroup') {
                var selectedGroupID = $("#ddlGroup").val();
            }
            else if (cntrl.id == 'ddlUser') {
                var selectedUserId = $("#ddlUser").val();
            }

            $.each(topicIds, function (index, topicId) {
                $("#chkTopic_" + topicId).prop("checked", true);
            });
        }

        function SaveChanges() {
            var groupIds = "";
            var topicIds = "";
            var userIds = "";
            var requestParams = {};
            $("input[name='TOPIC']").each(function (index, obj) {
                if (obj.checked) {
                    topicIds += obj.value + ",";
                }
            });
            topicIds = topicIds.replace(/,\s*$/, "");

            if ($("#ddlAssignType").val() == "GROUP") {
                if ($("input[name='TopicAssignment']:checked").val() == "BULK") {
                    $("input[name='GROUP']").each(function (index, obj) {
                        if (obj.checked) {
                            groupIds += obj.value + ",";
                        }
                    });
                }
                else {
                    groupIds = $("#ddlGroup").val() != "-1" ? $("#ddlGroup").val() : "";
                }
                groupIds = groupIds.replace(/,\s*$/, "");
                requestParams = { "Type": $("#ddlAssignType").val(), "Ids": groupIds, "TopicIds": topicIds };
            }
            else if ($("#ddlAssignType").val() == "USER") {
                if ($("input[name='TopicAssignment']:checked").val() == "BULK") {
                    $("input[name='USER']").each(function (index, obj) {
                        if (obj.checked) {
                            userIds += obj.value + ",";
                        }
                    });
                }
                else {
                    userIds = $("#ddlUser").val() != "-1" ? $("#ddlUser").val() : "";
                }
                userIds = userIds.replace(/,\s*$/, "");
                requestParams = { "Type": $("#ddlAssignType").val(), "Ids": userIds, "TopicIds": topicIds };
            }

            if (requestParams.Ids.length == 0 || requestParams.TopicIds.length == 0) {
                alert("No options selected.");
            }

            $("#lblJSON").text(JSON.stringify(requestParams));

            // Make Ajax Call
        }
    </script>
</asp:Content>
