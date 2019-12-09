<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="_365_Portal.Admin.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Roles Permissions</h2>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <%--<a class="btn bg-yellow float-left" onclick="AddNew();">Add New</a> <%--<a class="btn bg-blue text-white float-right" onclick="SaveGrid();">Save Changes</a>--%>
  <%--                  <div class="w-100"></div>--%>
                    <div id="divTable" class="mt-5 table-responsive">
                        <table class="table table-bordered">
                            <tr>
                                <th>Role</th>
                                <th>Manage Organizations</th>
                                <th>Manage Courses</th>
                                <th>Manage Users</th>
                                <th>Company Settings</th>
                                <th>View Courses</th>
                            </tr>
                            <tr>
                                <td>Super Admin/Global Admin</td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked"  disabled/>
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                            </tr>
                            <tr>
                                <td>Admin/Manager</td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled/>
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked"  disabled/>
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                            </tr>
                            <tr>
                                <td>Sub Admin/Creator</td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                            </tr>
                            <tr>
                                <td>End User/Learner</td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox"  disabled />
                                </td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" disabled />
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" disabled />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 d-none" id="divForm">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">

                    <div class="row input-validation">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtTitle">Title</label>
                                <input type="text" class="form-control required" id="txtTitle" maxlength="50" placeholder="Title" />
                            </div>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="submit" onclick="Submit();">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>


</script>
</asp:Content>
