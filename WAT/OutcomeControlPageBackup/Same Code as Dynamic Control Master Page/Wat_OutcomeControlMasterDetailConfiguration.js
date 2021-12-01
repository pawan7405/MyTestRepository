
function BindSingleSelectClient(option, option1, Option2) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddlclient').empty();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: URl,
        data: JSON.stringify({ Option: option, Option1: option1, Option2: Option2 }),
        dataType: "json",
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            } else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
            ShowError();
            $('#DisableDiv').html("");
        },
        success: function (data) {
            var jsonstring = JSON.parse(data);
            $('#ddlclient').append($("<option>").val("").html("- Select -"));

            $(jsonstring).each(function (index) {
                $('#ddlclient').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));

            });
            if (jsonstring.length == 1) {
                $('#ddlclient').val(jsonstring[0].ID);
            }
            $('#ddlclient').val($('#hdnClientMID').val());
            $('#ddlclient').change(function () {
                var id = $('#ddlclient').val();
                $('#hdnClientMID').val(id);
                $('#ClientMID').val(id);
                BindOutcomeControlMaster('DynamicOutcomeControlMaster', $('#ddlclient').val());

            })


        }
    });
}

function BindOutcomeControlMaster(option, option1) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddloutcomecontrol').empty();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: URl,
        data: JSON.stringify({ Option: option, Option1: option1 }),
        dataType: "json",
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            } else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
            ShowError();
            $('#DisableDiv').html("");
        },
        success: function (data) {
            var jsonstring = JSON.parse(data);
            $('#ddloutcomecontrol').append($("<option>").val("").html("- Select -"));
            $(jsonstring).each(function (index) {
                $('#ddloutcomecontrol').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });

            if (jsonstring.length == 1) {
                $('#ddloutcomecontrol').val(jsonstring[0].ID);
            }

            $('#ddloutcomecontrol').val($('#DOCMID').val());
            $('#ddloutcomecontrol').change(function () {
                var id = $('#ddloutcomecontrol').val();
                $('#hdnDocMID').val(id);
                $('#DOCMID').val(id);
                LoadDynamicColumns();
            })


        }
    });
}


function LoadDynamicColumns() {

    var clientMID = $('#ddlclient').val()
    var DOCMID = $('#ddloutcomecontrol').val()
    if (DOCMID == null && clientMID == null) {
        DOCMID = "0";
        clientMID = "0";
    }
    if (clientMID == "") {
        LoadError("Select the Client.");
        $('#ddlclientSearch').focus();
        return false;
    }

    else if (DOCMID == "") {
        LoadError("Select the Dynamic Control Type.");
        $('#ddloutcomecontrolSearch').focus();
        return false;
    }

    $("#tbl_Data1").show();
    $('#tbl_Data1').html("");
    $.ajax({
        type: "POST",
        url: $('#hidAppName').val() + "/WAT/FetchDynamicOutcomeControlMasterDetail",
        data: '{ClientMID:"' + clientMID + '",DOCMID:"' + DOCMID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            if (data != "") {
                $("#hidData").val(data.Result);
                var jsonstring = JSON.parse(data.Result);
                //if (jsonstring.ParameterIDs != null) {
                //    $("#HideParameterIds").val(jsonstring.ParameterIDs);
                //}
                var TxtBoxCount = 0;
                $("#CountTextBox").val(data.TxtBxCount);
                $("#HeaderLevel").val(data.HeaderLevel);
                $('#tbl_Data1 tr td').parents('tr').remove();
                if (jsonstring.length > 0) {
                    jsonstring = data.Result.replace('[{', '{ "DataSet":[{') + "}";
                    $('#hdnData').val(jsonstring);
                }
                else {
                    $('#hdnData').val('');
                    $("#td_Message").html("No record found.");
                    $('#DisableDiv').html("");
                }
                SpreadValueTable1(1);
                //  LoadParentSection();
                //  LoadSubSection();
                //  LoadParameters();
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
                $('#DisableDiv').html("");

            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });

    return false;
}
function SpreadValueTable1(Value) {
    ShowProgress();
    FilterDataTable1();
}

function FilterDataTable1() {

    $('#tbl_Data1').html("");
    $("#tbl_Data1").show();
    var data = parseJSONSafely($("#hdnData").val());
    if ($("#hdnData").val().indexOf("DataSet") >= 0) {
        $('#tbl_Data1 tr:not(:first)').remove();
        $('#hdnFilteredRecordCount').val(data.DataSet.length);
        var datapaging = data.DataSet;
        $("#tbl_Data1").append("<tbody><tr><th>Client Name</th><th>Control Master Name</th><th>Lable Name</th><th>Column Type</th><th>Is Mandatory?</th><th>Validation</th><th>Max Length</th><th>Min Length</th><th>Default value</th><th>Edit</th><th>Delete</th></tbody>");
        for (var i = 0; i < datapaging.length; i++) {
            $("#tbl_Data1").append('<tr><td>' + datapaging[i].ClientName +
                '</td><td>' + datapaging[i].WatControlMasterName +
                '</td><td>' + datapaging[i].LabelName +
                '</td><td>' + datapaging[i].ControlName +
                '</td><td>' + datapaging[i].IsMandatory +
                '</td><td>' + datapaging[i].ValidationType +
                '</td><td>' + datapaging[i].Maxlen +
                '</td><td>' + datapaging[i].Minlen +
                '</td><td>' + datapaging[i].DefaultValue +
                '</td><td align="center">' +
                '<a href="#" class="hint--top" data-hint="Edit"><button type="button" id="btn' + i + '" onclick="return EditData( ' + "'" + datapaging[i].DOCDID + "'" + ', ' + "'" + datapaging[i].ClientIDEnc + "'" + ', ' + "'" + datapaging[i].DOCMIDEnc + "'" + ', ' + "'" + datapaging[i].DBColumnName + "'" + ', ' + "'" + datapaging[i].LabelName + "'" + ',' + "'" + datapaging[i].ControlID + "'" + ',' + "'" + datapaging[i].IsMandatory + "'" + ',' + "'" + datapaging[i].ValidationType + "'" + ',' + "'" + datapaging[i].Maxlen + "'" + ',' + "'" + datapaging[i].Minlen + "'" + ',' + "'" + datapaging[i].OrderID + "'" + ');" value="Edit" class="delete_user icon-pencil" ></button></a>' +
                '</td><td align="center">' +
                '<button type="submit" id="btn' + i + '" onclick="return DeleteMiscColumn(' + datapaging[i].MiscMID + ');" value="" class="call_breakdown icon-bin" ></button>' +
                '</td></tr>');

        }
    }
    else {
        //$('#tbl_Data1').remove();
        $("#hdnFilteredData").val('');
        $('#divPage').html('');
    }
    $('#DisableDiv').html("");
}

function BindGridPagging(Pagging) {
    var Page = Pagging - 1;
    var minindex = Page * 10
    var data = parseJSONSafely($("#hdnData").val());
    var datapaging = data.DataSet.splice(minindex, 10);
    $('#tbl_Data1 tr:not(:first)').remove();
    for (var i = 0; i < datapaging.length; i++) {
        $("#tbl_Data1").append('<tr><td>' + datapaging[i].ClientName +
            '</td><td>' + datapaging[i].WatControlMasterName +
            '</td><td>' + datapaging[i].LabelName +
            '</td><td>' + datapaging[i].ControlName +
            '</td><td>' + datapaging[i].IsMandatory +
            '</td><td>' + datapaging[i].ValidationType +
            '</td><td>' + datapaging[i].Maxlen +
            '</td><td>' + datapaging[i].Minlen +
            '</td><td>' + datapaging[i].DefaultValue +
            '</td><td align="center">' +
            '<a href="#" class="hint--top" data-hint="Edit"><button type="button" id="btn' + i + '" onclick="return EditData( ' + "'" + datapaging[i].DOCDID + "'" + ', ' + "'" + datapaging[i].ClientIDEnc + "'" + ', ' + "'" + datapaging[i].DOCMIDEnc + "'" + ', ' + "'" + datapaging[i].DBColumnName + "'" + ', ' + "'" + datapaging[i].LabelName + "'" + ',' + "'" + datapaging[i].ControlID + "'" + ',' + "'" + datapaging[i].IsMandatory + "'" + ',' + "'" + datapaging[i].ValidationType + "'" + ',' + "'" + datapaging[i].Maxlen + "'" + ',' + "'" + datapaging[i].Minlen + "'" + ',' + "'" + datapaging[i].OrderID + "'" + ');" value="Edit" class="delete_user icon-pencil" ></button></a>' +
            '</td><td align="center">' +
            '<button type="submit" id="btn' + i + '" onclick="return DeleteMiscColumn(' + datapaging[i].MiscMID + ');" value="" class="call_breakdown icon-bin" ></button>' +
            '</td></tr>');
    }
    $(".Pager").Pagging_Pager({
        ActiveCssClass: "current",
        PagerCssClass: "pager",
        PageIndex: parseInt(Pagging),
        PageSize: parseInt('10'),
        RecordCount: parseInt(data.DataSet.length.toString())
    });
    return true;
}

$(".Pager").on("click", ".page", function (e) {
    BindGridPagging(parseInt($(this).attr('page')));
});
function EditMiscColumn(MiscMID, MiscName, MiscType, MiscISMandatory, MiscValidation, MiscLength, DefaultVal, OrderIdvalue, ParamIds, hideParams) {
    //$("#liParentSection").css("display", "none");
    //$("#liHideSubSection").css("display", "none");
    //$("#HideParameterIds").val(ParamIds);
    //$("#HideParameter").val(hideParams);
    $("#hdnParentSubSectionDropdown").val("");
    $("#SubSectionDropdownIds").val("");
    $('#HideParameterIds').val("");
    $("#MiscMID").val(MiscMID);
    $("#MiscName").val(MiscName);
    $("#DefaultValue").val(DefaultVal);
    $("#OrderId").val(OrderIdvalue);
    if (MiscISMandatory.toUpperCase() == "YES") {
        $("#MiscISMandatory").prop("checked", true);
    }
    else {
        $("#MiscISMandatory").prop("checked", false);
    }
    if (MiscType.toUpperCase() == "DROPDOWNLIST") {
        $("#MiscType").val(1);
        $("#MiscValidation").val("");
        $("#MiscLength").val("0");
        $("#MiscLengthVisibility").css("display", "none");
        $("#MiscValidationVisibility").css("display", "none");
        $("#Defaultfield").css("display", "block");
        FetchEditOption(MiscMID);
        $("#ddlDiv").css("display", "");
        $("#tbl_Data").css("display", "");

        if (hideParams == "true") { $("#HideParameter").val(1); }
        if (hideParams == "false") { $("#HideParameter").val(0); }
        //$("#liHideParameterDropdown").css("display", "block");
        LoadMultiSelect('#HideParameterDropdown', '#HideParameterIds');
        LoadMultiSelect('#ParentSubSectionDropdown', "#hdnParentSubSectionDropdown");
        LoadMultiSelect('#SubSectionDropdown', "#SubSectionDropdownIds");
        // LoadParameters($('#hdnData').val());

    }
    if (MiscType.toUpperCase() == "MULTISELECT DROPDOWNLIST") {
        $("#MiscType").val(3);
        $("#MiscValidation").val("");
        $("#MiscLength").val("0");
        $("#MiscLengthVisibility").css("display", "none");
        $("#MiscValidationVisibility").css("display", "none");
        $("#Defaultfield").css("display", "none");
        FetchEditOption(MiscMID);
        $("#ddlDiv").css("display", "");
        $("#tbl_Data").css("display", "");
    }
    if (MiscType.toUpperCase() == "TEXTBOX") {
        $("#MiscType").val(0);
        $("#MiscValidation").val(MiscValidation);
        $("#MiscLength").val(MiscLength);
        $("#MiscLengthVisibility").css("display", "block");
        $("#MiscValidationVisibility").css("display", "block");
        $("#ddlDiv").css("display", "none");
        $("#Defaultfield").css("display", "block");
        $("#checkDuplicateValidation").css("display", "block");
    }
    if (MiscType.toUpperCase() == "MULTILINE TEXTBOX") {
        $("#MiscType").val(2);
        $("#MiscValidation").val(MiscValidation);
        $("#MiscLength").val(MiscLength);
        $("#MiscLengthVisibility").css("display", "block");
        $("#MiscValidationVisibility").css("display", "block");
        $("#ddlDiv").css("display", "none");
        $("#Defaultfield").css("display", "none");
    }
    if (MiscType.toUpperCase() == "DATE") {

        $("#MiscType").val(4);
        $("#MiscValidation").val("");
        $("#MiscLength").val("0");
        $("#MiscLengthVisibility").css("display", "none");
        $("#MiscValidationVisibility").css("display", "none");
        $("#ddlDiv").css("display", "none");
        $("#Defaultfield").css("display", "none");
    }

    $("#tbl_Data1").show();
}
function FetchEditOption(MiscMID) {
    $("#tbl_Data1").show();
    $.ajax({
        type: "POST",
        url: $('#hidAppName').val() + "/QMSAdmin/FetchEditOption",
        data: '{MiscMID:"' + MiscMID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            debugger;
            if (data != "") {
                $("#hidMiscData").val(data);
                var jsonstring = JSON.parse(data)
                //if (jsonstring.ParameterIDs != null) {
                //    $("#HideParameterIds").val(jsonstring.ParameterIDs);
                //}
                if (jsonstring.length > 0) {
                    jsonstring = data.replace('[{', '{ "DataSet":[{') + "}";
                    $('#hdnMiscData').val(jsonstring);
                }
                else {
                    $('#hdnMiscData').val('');
                    $("#td_Message").html("No record found.");
                    $('#DisableDiv').html("");
                }
                BindData();
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
                $('#DisableDiv').html("");

            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });
    return false;
}

function DeleteMiscColumn(MiscMID) {
    $.when(ShowYesNoConfirmBox("Are you sure want to delete this Column?")).then(
        function (status) {
            if (status == "Yes") {
                //if (confirm("Are you sure want to delete this Column?")) {
                $.ajax({
                    type: "POST",
                    url: $('#hidAppName').val() + "/QMSAdmin/DeleteMiscColumn",
                    data: JSON.stringify({ MiscMID: MiscMID }),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    Async: false,
                    beforeSend: function () {
                        ShowProgress();
                    },
                    complete: function () {
                        $('#DisableDiv').html("");
                    },
                    success: function (data) {
                        if (data != "") {
                            var jsonstring = JSON.parse(data);
                            if (jsonstring.length > 0) {
                                jsonstring = data.replace('[{', '{ "DataSet":[{') + "}";
                                var datajs = $.parseJSON(jsonstring);
                                var datapaging = datajs.DataSet.splice(0, 10);
                                for (var i = 0; i < datapaging.length; i++) {
                                    if (datapaging[i].ErrorNumber == "1") {
                                        $("#td_Message").html("Column Deleted Successfully");
                                        $("#ddlDiv").css("display", "none");
                                        CancelEdit();
                                    }
                                    else {
                                        $("#td_Message").html("Problem performing error.. Failed to delete dynamic column.");
                                    }
                                    $('#DisableDiv').html("");
                                }
                            }
                            else {
                                $('#DisableDiv').html("");
                            }

                        }
                        else {
                            $("#td_MessageEditSave").html("Problem performing operation, please try again.");
                            $('#DisableDiv').html("");

                        }
                    },
                    error: function (xhr) {
                        $('#DisableDiv').html("");
                        if (xhr.status === 401) {
                            window.location.href = $('#hidAppName').val() + "/Login";
                            return;
                        }
                        else {
                            $("#td_MessageEditSave").html("Problem performing operation, please try again.");
                        }
                    }
                });
                return false;
            }
            return false;
        }
    );
}
function LoadError(errormessage) {
    if (errormessage != null && errormessage != "") {
        $("#td_Message").html(errormessage);
        //ShowError();
    }
    else {
        $("#td_Message").html("");
    }
}

function LoadDependentColumn(obj) {
    debugger;
    if (typeof (obj) != "object") {
        obj = document.getElementById('' + obj);
    }
    var IsDep = $('#' + obj.id).val();
    if (IsDep == "1") {
        $("#ParentColumnVisibility").css("display", "block");
        $("#ParentDataVisibility").css("display", "block");
        $('#ddlParentMiscColumn').empty();
        $("#ParentSection").css("display", "block");
        //var AuditSMID = $('#AuditSMID').val();
        var DOCMID = $('#ddloutcomecontrol').val();
        // ShowProgress();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#hidAppName').val() + '/WAT/FetchDependentColumn',
            data: '{AuditSMID:"' + DOCMID + '"}',
            dataType: "json",
            beforeSend: function () {
                ShowProgress();
            },
            complete: function () {
                $('#DisableDiv').html("");
            },
            success: function (data) {
                var jsonstring = JSON.parse(data)
                $('#ddlParentMiscColumn').append($("<option>").val("").html("- Select -"));
                $(jsonstring).each(function (index) {
                    $('#ddlParentMiscColumn').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
                });
                //$('#ddlProject').val($('#ProjectIDs').val());
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/Login";
                    return;
                }
                else {
                    $("#td_Message").html("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlParentMiscColumn').empty();
        $('#ddlParentMiscData').empty();
        $("#ParentColumnVisibility").css("display", "none");
        $("#ParentDataVisibility").css("display", "none");
    }
}
function LoadDependentData(obj) {
    debugger;
    var MiscMID = $('#' + obj.id).val();
    $('#ddlParentMiscData').empty();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: $('#hidAppName').val() + '/WAT/FetchDependentData',
        data: '{MiscMID:"' + MiscMID + '"}',
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            var jsonstring = JSON.parse(data)
            $('#ddlParentMiscData').append($("<option>").val("").html("- Select -"));
            $(jsonstring).each(function (index) {
                $('#ddlParentMiscData').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });
            //$('#ddlProject').val($('#ProjectIDs').val());
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });
}
function ShowHideDDLDiv(obj) {
    debugger;
    var MiscType = $('#' + obj.id).val();
    $("#td_Message").html("");
    $("#DuplicateValidation").val(0);
    $("#liParentSection").css("display", "none");
    $("#liHideSubSection").css("display", "none");
    $("#liHideParameter").css("display", "none");
    if (MiscType == "1" || MiscType == "3") {
        $("#ddlDiv").css("display", "block");
        $("#MiscLengthVisibility").css("display", "none");
        $("#MiscLength").val("0");
        $("#MiscValidationVisibility").css("display", "none");
        $("#MiscValidation").val("");
        if (MiscType == "1") {
            $("#Defaultfield").css("display", "block");
            $("#liHideParameter").css("display", "block");
            $("#liHideSubSection").css("display", "block");
            var HeaderLevelValue = $('#HeaderLevel').val();
            if (HeaderLevelValue > 1)
                $("#liParentSection").css("display", "block");
            //$("#liHideParameterDropdown").css("display", "block");
        }
    }
    else if (MiscType == "4") {
        $("#ddlDiv").css("display", "none");
        $("#MiscLength").val("0");
        $("#MiscLengthVisibility").css("display", "none");
        $("#MiscValidationVisibility").css("display", "none");
        $("#Defaultfield").css("display", "none");
        $("#MiscValidation").val("");
    }
    else {
        $("#ddlDiv").css("display", "none");
        $("#MiscLengthVisibility").css("display", "block");
        $("#MiscLength").val("");
        $("#MiscValidationVisibility").css("display", "block");
        $("#MiscValidation").val("");
        if (MiscType == "0") {
            $("#Defaultfield").css("display", "block");
            $("#checkDuplicateValidation").css("display", "block");
        }
        else {
            $("#Defaultfield").css("display", "none");
            $("#checkDuplicateValidation").css("display", "none");
        }
    }
    HideParameter();
    HideSubSection();
    HideParentSection();
}

function ShowHideLenthDiv(obj) {
    debugger;
    var MiscType = $('#' + obj.id).val();
    if (MiscType == "3") {
        $("#ValidationLength").css("display", "block");
    }
    else {
        $("#ValidationLength").css("display", "none");
        $("#MinLength").val("");
    }

}
function ValidateAdd() {
    debugger;

    $("#tbl_Data").css("display", "");


    var ddlParentMiscColumn = "";
    var ddlParentMiscData = "";
    var ddlParentMiscColumnName = "";
    var ddlParentMiscDataName = "";


    if (MiscType == "1") {
        if ($("#hdnDataValue").val() == "") {
            LoadError("Please Add data value");
            $("#hdnDataValue").focus();
            return false;
        }
        if ($("#IsParent").val() == "1") {
            if ($("#ddlParentMiscColumn").val() == "") {
                LoadError("Please select parent column");
                $("#ddlParentMiscColumn").focus();
                return false;
            }
            else { $("#ParentMiscMID").val($("#ddlParentMiscColumn").val()) }
            if ($("#ddlParentMiscData").val() == "") {
                LoadError("Please select parent column data");
                $("#ddlParentMiscData").focus();
                return false;
            }
            else { $("#ParentMiscDDID").val($("#ddlParentMiscData").val()) }
        }

    }
    if ($('#ddlParentMiscColumn').val() != null) {
        ddlParentMiscColumn = $('#ddlParentMiscColumn').val();
        ddlParentMiscColumnName = $("#ddlParentMiscColumn option:selected").text();
    }
    if ($('#ddlParentMiscData').val() != null) {
        ddlParentMiscData = $('#ddlParentMiscData').val();
        ddlParentMiscDataName = $("#ddlParentMiscData option:selected").text();
    }

    LoadError("");
    var data1 = "";
    var jsonstring = "";
    var data = "";
    if ($("#hidMiscData").val() == "") {
        $("#hidMiscData").val("[{}]");
        data1 = $("#hidMiscData").val();
        jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
        $('#hdnMiscData').val(jsonstring);
        data = parseJSONSafely($("#hdnMiscData").val());
        data.DataSet.splice(0, 1);
    }
    else {
        data1 = $("#hidMiscData").val();
        jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
        $('#hdnMiscData').val(jsonstring);
        data = parseJSONSafely($("#hdnMiscData").val());

        var cdata1 = $("#hidMiscData").val();
        var jsonstringC = cdata1.replace('[{', '{ "DataSet":[{') + "}";
        $('#hdnMiscData').val(jsonstringC);
        var cdata = parseJSONSafely($("#hdnMiscData").val());
        var CheckName = 0;
        var datapaging = cdata.DataSet.splice(0, cdata.DataSet.length);
        for (var i = 0; i < datapaging.length; i++) {
            if (datapaging[i].MiscData == $("#DataValue").val()) {
                CheckName = 1;
            }
        }
        if (CheckName == 1) {
            LoadError("Column Name Already exists.");
            $("#DataValue").focus();
            return false
        }
    }
    var subIds = "";
    if ($("#SubSectionDropdownIds").val() == "") {
        subIds = "NULL";
    }
    else {
        subIds = $("#SubSectionDropdownIds").val();
    }
    var HeaderLevelIds = "";
    if ($("#hdnParentSubSectionDropdown").val() == "") {
        HeaderLevelIds = "NULL";
    }
    else {
        HeaderLevelIds = $("#hdnParentSubSectionDropdown").val();
    }
    var parameterIds = "";
    if ($("#HideParameterIds").val() == "") {
        parameterIds = "NULL";
    }
    else {
        parameterIds = $("#HideParameterIds").val();
    }
    data.DataSet.push({ "DDLMID": 0, "DOCDID": 0, "Name": $('#DataValue').val(), "IsParent": $("#IsParent").val(), "IsParentText": $("#IsParent option:selected").text(), "ParentDOCMID": ddlParentMiscColumn, "ParentMiscName": ddlParentMiscColumnName, "ParentDOCDID": ddlParentMiscData, "ParentMiscData": ddlParentMiscDataName, "FreezeStatus": 0, "HideSectionLevel1": $("#HideParentSection option:selected").val(), "SectionLevel1IDs": HeaderLevelIds, "HideSectionLevel2": $("#HideSubSection option:selected").val(), "SectionLevel2IDs": subIds, "HideParameter": $("#HideParameter option:selected").val()});
    var NewData = JSON.stringify(data).replace('{"DataSet":[{', '[{');
    $("#hidMiscData").val(NewData.substring(0, NewData.length - 1));
    BindData();
    $("#DataValue").val("");
    $("#IsParent").val("0");
    LoadDependentColumn("IsParent");
    $("#ParentColumnVisibility").css("display", "none");
    $("#ParentDataVisibility").css("display", "none");
    $("#HideParameter").val(0);
    //$("#HideParameterIds").val(0);
    // CancelEdit();
    return false;
}
function BindData() {
    $("#ddlDiv").css("display", "");
    $('#tbl_Data').html("");
    var data1 = $("#hidMiscData").val();
    var jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
    var data = $.parseJSON(jsonstring);
    if (jsonstring.indexOf("DataSet") >= 0) {
        $('#tbl_Data tr:not(:first)').remove();
        $('#hdnFilteredRecordCount').val(data.DataSet.length);
        if (data.DataSet.length > 0) {
            $("#dvSave").show();
        }
        else {
            $("#dvSave").hide();
        }
        var datapaging = data.DataSet.splice(0, data.DataSet.length);
        $("#tbl_Data").append("<tbody><tr><th>Data Value</th><th>Is Dependent?</th><th>Parent Column</th>" +
            "<th>Parent Data Value</th><th>Delete</th></tbody>");
        for (var i = 0; i < datapaging.length; i++) {
            $("#tbl_Data").append('<tr><td>' + datapaging[i].MiscData +
                '</td><td>' + datapaging[i].IsParentText +
                '</td><td>' + datapaging[i].ParentMiscName +
                '</td><td>' + datapaging[i].ParentMiscData +
                '</td><td align="center">' +
                '<a href="#" class="hint--top" data-hint="Delete"><button type="button" id="btn' + i + '" onclick="return DeleteDataValue(' + i + ');" value="Delete" class="delete_user icon-bin" ></button></a>' +
                '</td></tr>');
        }
    }
    else {
        $("#dvSave").hide();
        $('#tbl_Data').html('');
        $("#hdnFilteredData").val('');
        $('#divPage').html('');
    }
    $('#DisableDiv').html("");
}
function DeleteDataValue(id) {
    $.when(ShowYesNoConfirmBox("Are you sure want to delete this Column?")).then(
        function (status) {
            if (status == "Yes") {
                // if (confirm("Are you sure want to delete this data value?")) {
                var data1 = $("#hidMiscData").val();
                var jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
                $('#hdnMiscData').val(jsonstring);
                var data = parseJSONSafely($("#hdnMiscData").val());
                data.DataSet.splice(id, 1);
                var NewData = JSON.stringify(data).replace('{"DataSet":[{', '[{');
                $("#hidMiscData").val(NewData.substring(0, NewData.length - 1));
                BindData();
            }
            return false;
        });

}
function SaveDynamicColumns() {

    debugger;


    if ($("#ClientMID").val() == "") {
        LoadError("Please Select Client");
        $("#ClientMID").focus();
        return false;
    }

    if ($("#DOCMID").val() == "") {
        LoadError("Please select outcome control master");
        $("#DOCMID").focus();
        return false;
    }


    if ($("#LabelName").val() == "") {
        LoadError("Please Enter Lable Name");
        $("#LabelName").focus();
        return false;
    }


    var IsMandatory = "0";
    var MiscLength = "";

    if ($("#MiscValidation").val() == "3") {
        if ($("#DefaultValue").val() != "") {
            var value = $("#DefaultValue").val();
            var valid = validateEmail(value);
            if (!valid) {
                $("#td_Message").html("Not in Valid Format");
                $("#DefaultValue").focus();
                return false;
            }
            else {
                $("#td_Message").html("");
            }
        }
    }

    //Check Cann't add mroe than two textbox
    if ($("#DuplicateValidation option:selected").val() == "1") {
        if (parseInt($("#CountTextBox").val()) == 0) {
            $("#td_Message").html("Sorry, more than 2 Duplicate validation is not allow.");
            $("#DuplicateValidation").focus();
            return false;
        }
    }
    if ($("#MiscISMandatory").is(':checked') == true) {
        IsMandatory = "1";
    }
    if ($("#MiscName").val() == "") {
        LoadError("Please enter Column Name");
        $("#MiscName").focus();
        return false;
    }
    if ($("#MiscType").val() == "NA") {
        LoadError("Please select Column Type");
        $("#MiscType").focus();
        return false;
    }
    if ($("#HideParameter option:selected").val() == "1" && $("#HideParameterIds").val() == "") {
        LoadError("Please select Hide Parameter Type");
        $("#HideParameter").focus();
        return false;
    }
    if ($("#HideSubSection option:selected").val() == "1" && $("#SubSectionDropdownIds").val() == "") {
        LoadError("Please select Hide Section");
        $("#HideSubSection").focus();
        return false;
    }
    if ($("#HideParentSection option:selected").val() == "1" && $("#hdnParentSubSectionDropdown").val() == "") {
        LoadError("Please select Hide Parent Section");
        $("#HideParentSection").focus();
        return false;
    }
    if ($("#MiscType").val() != "1") {
        MiscLength = $("#MiscLength").val();
        // MiscValidation = $("#MiscValidation").val();
        if ($("#MiscLength").val() == "") {
            LoadError("Please enter Column Length");
            $("#MiscLength").focus();
            return false;
        }
    }
    else {
        $("#MiscLength").val("0");
        MiscLength = "0";
        // MiscValidation = "";
        var cdata1 = $("#hidMiscData").val();
        var jsonstringC = cdata1.replace('[{', '{ "DataSet":[{') + "}";
        $('#hdnMiscData').val(jsonstringC);
        var cdata = parseJSONSafely($("#hdnMiscData").val());
        var datapaging = cdata.DataSet.splice(0, cdata.DataSet.length);
        if (datapaging == "") {
            LoadError("Please enter data value");
            $("#DataValue").focus();
            return false;
        }
    }
    var str = "";
    //var parameter = JSON.stringify({ datatable: $("#hidMiscData").val(), "ObjBO": { MiscMID: $("#MiscMID").val(), AuditSMID: $("#AuditSMID").val(), MiscName: $("#MiscName").val(), MiscType: $("#MiscType").val(), MiscISMandatory: IsMandatory, MiscValidation: $("#MiscValidation").val(), MiscLength: MiscLength, DefaultValue: $("#DefaultValue").val(), DuplicateValidation: $("#DuplicateValidation option:selected").val(), HideParameter: $("#HideParameter option:selected").val(),HideParameterDropdown: $("#HideParameterIds").val()}});
    var parameter = JSON.stringify($("#hidMiscData").val());
    //alert(parameter);
    str = $("#DOCMID").val() + "," + $("#DBColumnName").val() + "," + $("#LabelName").val() + "," + $("#MiscType").val() + "," + IsMandatory + "," + $("#MiscValidation").val() + "," + $("#Maxlength").val() + "," + $("#Minlength").val() + "," + $("#DefaultValue").val() + "," + $("#OrderId").val() + "," + $("#DOCDID").val() + "";
    $.ajax({
        type: "POST",
        url: $('#hidAppName').val() + '/WAT/SaveData',
        //data: parameter,
        data: '{datatable:"' + parameter + '",ObjBO:"' + str + '"}',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            if (data != "") {
                var jsonstring = JSON.parse(data)

                if (jsonstring.length > 0) {
                    jsonstring = data.replace('[{', '{ "DataSet":[{') + "}";
                    var datajs = $.parseJSON(jsonstring);
                    var datapaging = datajs.DataSet.splice(0, 10);
                    for (var i = 0; i < datapaging.length; i++) {
                        // ShowAddOptionType();
                        if (datapaging[i].ErrorNumber == "1") {
                            if ($("#MiscMID").val() == "0") {
                                $("#td_Message").html("Record Saved Successfully");
                                $("#OrderId").val('');

                            }
                            else {
                                $("#td_Message").html("Record Updated Successfully");
                                $("#MiscMID").val("0");
                                $("#OrderId").val('');
                            }
                            $("#ddlDiv").css("display", "none");
                            CancelEdit();

                        }

                        else if (datapaging[i].ErrorNumber == "4") {
                            $("#td_Message").html("Control lable name already exists");
                            $("#OrderId").val('');
                        }

                        else if (datapaging[i].ErrorNumber == "3") {
                            $("#td_Message").html("DB Column Name already configured for selected client and outcome control group name");
                            $("#OrderId").val('');
                        }
                        else {
                            $("#td_Message").html("Problem performing error.. Failed to save data for dynamic column.");
                        }
                        $('#DisableDiv').html("");
                    }
                }
                else {
                    $('#DisableDiv').html("");
                }

            }
            else {
                $("#td_MessageEditSave").html("Problem performing operation, please try again.");
                $('#DisableDiv').html("");

            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });
    return false;
}
function CancelEdit() {
    $("#btnAddSubmit").show();
    $("#btnEdit").hide();
    $("#btnCancel").hide();
    $("#hdnType").val("Add");
    $("#MiscName").val("");
    $("#MiscType").val("NA");
    $("#MiscISMandatory").val("No");
    $("#MiscISMandatory").prop("checked", false);
    $("#MiscValidation").val("");
    $("#MiscLength").val("");
    $("#DefaultValue").val("");
    $("#ValidationLength").css("display", "none");
    $("#ddlCommentMandatoryOn").val("");
    $("#CommentLength").val("");
    $("#ParentColumnVisibility").css("display", "none");
    $("#ParentDataVisibility").css("display", "none");
    $("#tbl_Data").css("display", "none");
    $("#DuplicateValidation").val(0);
    $("#OrderId").val('');
    LoadDynamicColumns();
    $('#hdnMiscData').val("");
    $('#hidMiscData').val("");
    return false;
}

function Goback() {
    window.location.href = $('#hidAppName').val() + "/QMSAdmin/AuditTemplateConfiguration/Iz30MXpAGDW2EjLYtWmhLSdxXitio1kXiZYy7PmSW0@~@IUltVCbQ8dQavXTR7iYaqa2lJLRIx2e8=/" + $("#AuditSMIDEncr").val();
    return false;
}
var validateEmail = function (elementValue) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue);
}

function ValiationType(_this) {

    if ($("#MiscValidation").val() == "1") {
        return OnlyNumeric(_this);
    }
    else if ($("#MiscValidation").val() == "2") {
        return KeyPressDecimal(_this);
    }
    //    else if ($("#MiscValidation").val() == "3") {
    //        var value = $(_this).val();
    //        var valid = validateEmail(value);
    //        if (!valid) {
    //            $("#td_Message").html("Not in Valid Format");
    //            // return false;
    //        }
    //        else {
    //            $("#td_Message").html("");
    //        }

    //    }
}

function CheckDuplicacy() {
    if ($("#DuplicateValidation option:selected").val() == "1") {
        if ($("#CountTextBox").val() == 0) {
            $("#td_Message").html("Sorry, more than 2 Duplicate validation is not allow.");
            $("#DuplicateValidation").focus();
            return false;
        }
        else return true;
    }
}

function LoadParentSection() {
    debugger;
    var AuditSMID = $('#AuditSMID').val();
    var HeaderLevelValue = $('#HeaderLevel').val();
    $('#ParentSubSectionDropdown').html("");
    if (HeaderLevelValue > 1) {
        $("#liParentSection").css("display", "block");
        $.ajax({
            type: "POST",
            url: $('#hidAppName').val() + "/QMSAdmin/FetchSectionsList",
            data: '{AuditSMID:"' + AuditSMID + '",Sectionid:"' + HeaderLevelValue + '",ParametersIds:"Sections"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                ShowProgress();
            },
            complete: function () {
                //$('#DisableDiv').html("");
            },
            success: function (data) {
                var jsonstring = JSON.parse(data)
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ParentSubSectionDropdown').append($("<option>").val(jsonstring[i].SectionMID).html(jsonstring[i].SectionName));
                }
                $('#ParentSubSectionDropdown').multiselect({
                    beforeopen: function () {
                        //   $('#ParentSubSectionDropdown').multiselect('refresh');
                    },
                    close: function () {
                        var values = new Array();
                        $('#ParentSubSectionDropdown').multiselect("getChecked").each(function (index, item) {
                            values.push($(item).val());
                        });
                        $("#hdnParentSubSectionDropdown").val(values.join("~"));
                    }
                });
            },
            error: function (xhr) {
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/Login";
                    return;
                }
                else {
                    $("#td_Message").html("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $("#liParentSection").css("display", "none");
    }
}
function LoadSubSection() {
    debugger;
    var AuditSMID = $('#AuditSMID').val();
    var HeaderLevelValue = $('#HeaderLevel').val();
    $('#SubSectionDropdown').html("");
    if (HeaderLevelValue > 1) { HeaderLevelValue = 1; $("#liHideSubSection").css("display", "block"); }
    $.ajax({
        type: "POST",
        url: $('#hidAppName').val() + "/QMSAdmin/FetchSectionsList",
        data: '{AuditSMID:"' + AuditSMID + '",Sectionid:"' + HeaderLevelValue + '",ParametersIds:"Sections"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
        },
        success: function (data) {
            var jsonstring = JSON.parse(data)
            for (var i = 0; i < jsonstring.length; i++) {
                $('#SubSectionDropdown').append($("<option>").val(jsonstring[i].SectionMID).html(jsonstring[i].SectionName));
            }
            $('#SubSectionDropdown').multiselect({
                beforeopen: function () {
                    //$('#SubSectionDropdown').multiselect('refresh');
                },
                close: function () {
                    var values = new Array();
                    $('#SubSectionDropdown').multiselect("getChecked").each(function (index, item) {
                        values.push($(item).val());
                    });
                    $("#SubSectionDropdownIds").val(values.join("~"));
                }
            });
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });
}
function LoadParameters() {
    debugger;
    var AuditSMID = $('#AuditSMID').val();
    var HeaderLevelValue = $('#HeaderLevel').val();
    $('#SubSectionDropdown').html("");
    $('#HideParameterDropdown').html('');
    ShowProgress();
    $.ajax({
        type: "POST",
        url: $('#hidAppName').val() + "/QMSAdmin/FetchSectionsList",
        data: '{AuditSMID:"' + AuditSMID + '",Sectionid:"' + HeaderLevelValue + '",ParametersIds:"Parameters"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
        },
        success: function (data) {
            debugger;
            var jsonstring = JSON.parse(data)
            for (var i = 0; i < jsonstring.length; i++) {
                $('#HideParameterDropdown').append($("<option>").val(jsonstring[i].ParameterMID).html(jsonstring[i].Parametername));
            }
            LoadMultiSelect('#HideParameterDropdown', "#HideParameterIds");
            $('#HideParameterDropdown').multiselect({
                beforeopen: function () {
                    //$('#HideParameterDropdown').multiselect('refresh');
                },
                close: function () {
                    var values = new Array();
                    $('#HideParameterDropdown').multiselect("getChecked").each(function (index, item) {
                        values.push($(item).val());
                    });
                    $("#HideParameterIds").val(values.join("~"));
                }
            });
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $("#td_Message").html("Problem performing operation, please try again.");
            }
        }
    });
}
function HideParentSection() {
    debugger;
    if ($("#HideParentSection option:selected").val() == "1") {
        LoadMultiSelect('#ParentSubSectionDropdown', "#hdnParentSubSectionDropdown");
        $("#liparentSectionDropdown").css("display", "block");
    }
    else {
        $("#hdnParentSubSectionDropdown").val("");
        LoadMultiSelect('#ParentSubSectionDropdown', "#hdnParentSubSectionDropdown");
        $("#liparentSectionDropdown").css("display", "none");
    }
}
function HideSubSection() {
    debugger;
    if ($("#HideSubSection option:selected").val() == "1") {
        LoadMultiSelect('#SubSectionDropdown', "#SubSectionDropdownIds");
        $("#liSubSectionDropdown").css("display", "block");
    }
    else {
        $("#SubSectionDropdownIds").val("");
        LoadMultiSelect('#SubSectionDropdown', "#SubSectionDropdownIds");
        $("#liSubSectionDropdown").css("display", "none");
    }
}
function HideParameter() {
    debugger;
    if ($("#HideParameter option:selected").val() == "1") {
        LoadMultiSelect('#HideParameterDropdown', "#HideParameterIds");
        $("#liHideParameterDropdown").css("display", "block");
    }
    else {
        $('#HideParameterIds').val("");
        LoadMultiSelect('#HideParameterDropdown', "#HideParameterIds");
        $("#liHideParameterDropdown").css("display", "none");
    }
}
function ValiationTypes(_this) {
    return OnlyNumeric(_this);
}
function LoadMultiSelect(dropdown, hidden) {
    $(dropdown).multiselect("uncheckAll");

    var listAssigned = $(dropdown + "option:selected").length;
    if (listAssigned == 0) {

        if ($(hidden).val() != null && $(hidden).val() != undefined && $(hidden).val() != "") {
            var arrUserIdSelected = $(hidden).val().split(',');

            if (arrUserIdSelected != null && arrUserIdSelected.length > 0) {
                for (var iCount = 0; iCount < arrUserIdSelected.length; iCount++) {
                    $(dropdown).multiselect("widget").find("input:checkbox[value='" + arrUserIdSelected[iCount] + "']").attr("checked", "checked");
                    $(dropdown + " option[value='" + arrUserIdSelected[iCount] + "']").attr("selected", 1);
                    $(dropdown).multiselect("refresh");
                }
            }
        }
    }
}