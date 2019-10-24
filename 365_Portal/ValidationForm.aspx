<%@ Page Title="Validation Form" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="ValidationForm.aspx.cs" Inherits="_365_Portal.ValidationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row input-validation">
        <div class="col-sm-3">
            <div class="form-group">
                <label for="text">Text</label>
                <input type="text" class="form-control required" id="text" placeholder="Text" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label for="date">Date</label>
                <input type="text" class="form-control date required" id="date" placeholder="Select Date" />
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label>File</label>
                <div class="custom-file">
                    <input type="file" class="custom-file-input required" id="file">
                    <label class="custom-file-label" for="customFile">Choose file</label>
                </div>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label for="textarea">Textarea</label>
                <textarea class="form-control required" placeholder="Textarea" id="textarea"></textarea>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <label for="select">Select</label>
                <select class="form-control select2 required" id="select">
                    <option></option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                </select>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group">
                <label for="selectMultiple">Select Multiple</label>
                <select class="form-control select2 required" multiple id="selectMultiple">
                    <option></option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                </select>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-group radio required">
                <label>Radio</label>
                <div class="custom-control custom-radio custom-control-inline">
                    <input type="radio" id="rbQQ1" name="rgQuestion" class="custom-control-input">
                    <label class="custom-control-label" for="rbQQ1">Yes</label>
                </div>
                <div class="custom-control custom-radio custom-control-inline">
                    <input type="radio" id="rbQQ2" name="rgQuestion" class="custom-control-input">
                    <label class="custom-control-label" for="rbQQ2">No</label>
                </div>
            </div>
        </div>


        <div class="col-sm-3">
            <div class="form-group checkbox required">
                <label>Checkbox</label>
                <div class="custom-control custom-checkbox custom-control-inline">
                    <input type="checkbox" id="cbQ1" name="cgQuestion" class="custom-control-input">
                    <label class="custom-control-label" for="cbQ1">A</label>
                </div>
                <div class="custom-control custom-checkbox custom-control-inline">
                    <input type="checkbox" id="cbQ2" name="cgQuestion" class="custom-control-input">
                    <label class="custom-control-label" for="cbQ2">B</label>
                </div>
                <div class="custom-control custom-checkbox custom-control-inline">
                    <input type="checkbox" id="cbQ3" name="cgQuestion" class="custom-control-input">
                    <label class="custom-control-label" for="cbQ3">C</label>
                </div>
            </div>
        </div>

        <div class="w-100"></div>

        <div class="mt-4">
            <a class="btn bg-yellow" onclick="inputValidation('.input-validation');">Submit</a>
        </div>
    </div>

    <script>

        $(document).ready(function () {
            $('.datetime').datetimepicker({ uiLibrary: 'bootstrap4', footer: true, format: 'yyyy-dd-mm hh:mm' });
            $('.date').datepicker({ uiLibrary: 'bootstrap4', footer: true, format: 'yyyy-dd-mm' });
            $('.time').timepicker({ uiLibrary: 'bootstrap4', footer: true });

        });
    </script>
</asp:Content>
