//ShowHidePCCfunction HideShow2(e) {
//    $("#" + e).slideToggle();
//}

function ShowError() {
    var showHideErrorBox = document.getElementById('showHideErrorBox');
    if ($("#td_Message").text() == '' || showHideErrorBox.style.display == 'none') {
        HideShow2('showHideErrorBox');
        return false;
    }
}
var td_Message = document.getElementById("td_Message");
function validateDynamicControl() {

    var hidDynamicControls = document.getElementById('hidDynamicControls');
    var str = hidDynamicControls.value;
    if (str != "") {
        var strResult = str.split("~");
        for (var i = 0; i < strResult.length; i++) {
            var strInner = strResult[i];
            var strInnerResult = strInner.split("|");
            if (strInnerResult[2] == "True") {
                if ((strInnerResult[1] == "0") || (strInnerResult[1] == "2")) {
                    var txt = document.getElementById('txt' + strInnerResult[0]);
                    if ((typeof txt != 'undefined') && (txt != null)) {
                        if (trim(txt.value) == "") {
                            LoadError("Please Enter " + strInnerResult[3]);
                            //td_Message.innerHTML = "Please enter " + strInnerResult[3] + ".";
                            txt.style.backgroundColor = "LightBlue";
                            txt.focus();
                            return false;
                        }
                        else {
                            if (strInnerResult[4] != "0") {
                                if (trim(txt.value).length == strInnerResult[4]) {
                                    txt.style.backgroundColor = "White";
                                }
                                else {
                                    txt.style.backgroundColor = "LightBlue";
                                    LoadError(strInnerResult[3] + " should not less than " + strInnerResult[4] + " digits /characters.");
                                    txt.focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
                else if (strInnerResult[1] == "1") {
                    var ddl = document.getElementById('ddl' + strInnerResult[5]);
                    if ((typeof ddl != 'undefined') && (ddl != null)) {
                        if (trim(ddl.value) == "0") {
                            LoadError("Please select " + strInnerResult[3]);
                            //td_Message.innerHTML = "Please select " + strInnerResult[3] + ".";
                            ddl.style.backgroundColor = "LightBlue";
                            ddl.focus();
                            return false;
                        }
                        else { ddl.style.backgroundColor = "White"; }
                    }

                }
                else if (strInnerResult[1] == "5") {
                    var DatTimetxt = document.getElementById('txtNTime_' + strInnerResult[0]);
                    if ((typeof DatTimetxt != 'undefined') && (DatTimetxt != null)) {
                        if (trim(DatTimetxt.value) == "") {
                            LoadError("Please Enter " + strInnerResult[3]);
                            DatTimetxt.style.backgroundColor = "LightBlue";
                            DatTimetxt.focus();
                            return false;
                        }
                        else {
                            DatTimetxt.style.backgroundColor = "White";
                        }
                    }
                }
            }
            /// else { return false; }
        }
    }
    return true;
}

function LoadError(errormessage) {
    if (errormessage != null && errormessage != "") {
        $("#td_Message").html(errormessage);
        ShowError();
    }
    else {
        $("#td_Message").html("");
        $("#showHideErrorBox").hide();
    }
}
function openAgentDetailEntryALG() {
    var Querystring = $("#URL").val();
    window.open($("#AppName").val() + "/WAT/WorkALGDetailEntry/" + Querystring, "name", "width:900px;height:345px");
    return false;
}
function openAgentDetailEntry() {

    var Querystring = $("#URL").val();
    window.open($("#AppName").val() + "/WAT/WorkDetailEntry_Expedia/" + Querystring, "name", "width:900px;height:345px");
    return false;
}

function openAgentHistoricData() {
    window.location = $("#AppName").val() + "/WAT/HistoricWorkDetail/" + $("#URL").val();
    return false;
}

function openAgentTaskDetails() {
    window.location = $("#AppName").val() + "/WAT/WorkDetailEntry/" + $("#URL").val();
    return false;
}

function openAgentActivityDetails() {

    var Querystring = $("#URL").val();
    window.open($("#AppName").val() + "/WAT/AgentActivityDetails_EasyJet/" + Querystring, "name", "width:900px;height:345px");
    return false;
}

function ChangeStatus(ActionSMID, Btn) {
    $("#ActionSMID").val(ActionSMID);
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }
}

function LoadCalender(TxtDate, Watermark) {
    $(function () {
        $("#" + TxtDate).datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            todayHighlight: true,
            onSelect: function (selectedDate) {
                //$("#" + TxtDate).datepicker("option", "minDate", "01/03/2015");
            }
        });

        $("#" + TxtDate).datepicker("option", "dateFormat", "dd/mm/yy");
        if ($("#" + TxtDate).val() != null) {
            if ($("#" + TxtDate).val().length == 0) {
                $("#" + TxtDate).val(Watermark).addClass('watermark');
            }
        }
    });
}

//for binding the multiselect dropdown for client.
function LoadWorkGroups() {
    $('#ddlWorkGroup').empty();
    ShowProgress();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: $('#AppName').val() + '/WAT/FetchAgentWorkGroups',
        data: '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '" }',
        dataType: "json",
        success: function (data) {
            $('#ddlWorkGroup').empty();
            var jsonstring = JSON.parse(data)
            $('#ddlWorkGroup').append($("<option>").val("").html("- Select -"));
            $(jsonstring).each(function (index) {
                $('#ddlWorkGroup').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });
            $('#ddlWorkGroup').val($('#WorkGMID').val());
            $('#ddlWorkGroup').change(function () {
                var id = $('#ddlWorkGroup').val();
                $('#WorkGMID').val(id);
                LoadWorkDivisions($('#WorkGMID').val());
                LoadWorkDivisionsDataEntry($('#WorkGMID').val());
            })
            LoadWorkDivisions("");
            LoadWorkDivisionsDataEntry("");
            $('#DisableDiv').html("");
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
}

function LoadWorkDivisions(WorkGMID) {
    ShowProgress();

    LoadError("");
    $('#ddlWorkDivision').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkGMID: "' + WorkGMID + '" }'
    if (WorkGMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/FetchWorkDivisions',
            data: parameter,
            dataType: "json",
            success: function (data) {
                $('#ddlWorkDivision').empty();
                var jsonstring = JSON.parse(data)
                $('#ddlWorkDivision').append($("<option>").val("").html("- Select -"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkDivision').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                $('#ddlWorkDivision').val($('#WorkDMID').val());
                $('#ddlWorkDivision').change(function () {
                    var id = $('#ddlWorkDivision').val();
                    $('#WorkDMID').val(id);
                    LoadWorkItems($('#WorkDMID').val(), $('#WorkGMID').val());
                })
                LoadWorkItems($('#WorkDMID').val(), $('#WorkGMID').val());
                $('#DisableDiv').html("");
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlWorkDivision').append($("<option>").val("").html("- Select -"));
        $('#WorkDMID').val("");
        LoadWorkItems("");
    }
    $('#DisableDiv').html("");
}

//for binding the multiselect dropdown for client.
function LoadWorkGroupsForCall() {
    $('#ddlWorkGroup').empty();
    ShowProgress();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: $('#AppName').val() + '/WAT/FetchWorkGroups',
        data: '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID: "' + $("#GlobalUserID").val() + '" }',
        dataType: "json",
        success: function (data) {
            var jsonstring = JSON.parse(data)
            $('#ddlWorkGroup').append($("<option>").val("").html("Work Group"));
            $(jsonstring).each(function (index) {
                $('#ddlWorkGroup').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });
            $('#ddlWorkGroup').val($('#WorkGMID').val());
            $('#ddlWorkGroup').change(function () {
                var id = $('#ddlWorkGroup').val();
                $('#WorkGMID').val(id);
                LoadWorkDivisionsForCall($('#WorkGMID').val());
            })
            LoadWorkDivisionsForCall("");
            $('#DisableDiv').html("");
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
}

function LoadWorkDivisionsForCall(WorkGMID) {
    ShowProgress();
    $('#ddlWorkDivision').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkGMID: "' + WorkGMID + '" }'
    if (WorkGMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/FetchWorkDivisions',
            data: parameter,
            dataType: "json",
            success: function (data) {
                var jsonstring = JSON.parse(data)
                $('#ddlWorkDivision').append($("<option>").val("").html("Work Division"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkDivision').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }

                $('#ddlWorkDivision').val($('#WorkDMID').val());
                $('#ddlWorkDivision').change(function () {
                    var id = $('#ddlWorkDivision').val();
                    $('#WorkDMID').val(id);
                    LoadCallOutcomes($('#WorkDMID').val());
                })
                LoadCallOutcomes($('#WorkDMID').val());
                $('#DisableDiv').html("");
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlWorkDivision').append($("<option>").val("").html("Work Division"));
        $('#WorkDMID').val("");
        LoadCallOutcomes("");
    }
}

function LoadCallOutcomes(WorkDMID) {

    ShowProgress();
    $('#ddlCallOutcome').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkDMID: "' + WorkDMID + '" }'
    if (WorkDMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/FetchCallOutcomes',
            data: parameter,
            dataType: "json",
            success: function (data) {

                var jsonstring = JSON.parse(data)
                $('#ddlCallOutcome').empty();
                $('#ddlCallOutcome').append($("<option>").val("").html("Outcome"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlCallOutcome').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                $('#ddlCallOutcome').val($('#OutcomeMID').val());
                $('#ddlCallOutcome').change(function () {
                    var id = $('#ddlCallOutcome').val();
                    $('#OutcomeMID').val(id);
                })
                $('#DisableDiv').html("");
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlCallOutcome').append($("<option>").val("").html("Outcome"));
        $('#OutcomeMID').val("");
    }
}

function LoadWorkItems(WorkDMID, WorkGMID) {
    ShowProgress();
    LoadError("");

    $('#ddlWorkItem').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",WorkDMID: "' + WorkDMID + '",WorkGMID: "' + WorkGMID + '" }'
    if (WorkDMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/WorkItemsDetails_ALG',
            data: parameter,
            dataType: "json",
            success: function (data) {
                debugger;
                var jsonstring = JSON.parse(data)
                $('#ddlWorkItem').empty();
                $('#ddlWorkItem').append($("<option>").val("").html("WorkItem"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkItem').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                $('#ddlWorkItem').val($('#WorkIMID').val());

                if (jsonstring.length == 1) {
                    $('#ddlWorkItem').val(jsonstring[0].ID);
                    $('#WorkIMID').val(jsonstring[0].ID);
                    if ($("#Assignment").val() == "True") {
                        WorkItemChange();
                    }
                    // $('#WorkIMID').val(jsonstring[0].ID);
                }

                $('#ddlWorkItem').change(function () {
                    var id = $('#ddlWorkItem').val();
                    $('#WorkIMID').val(id);
                    if ($("#Assignment").val() == "True") {
                        WorkItemChange();
                    }
                })
                $('#DisableDiv').html("");

            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlWorkItem').append($("<option>").val("").html("- Select -"));
        $('#WorkIMID').val("");
    }
    $('#DisableDiv').html("");
}

function GetAgentActivityDetails() {
    $('#tbl_Data').html("");
    $.ajax({
        type: "POST",
        url: $('#AppName').val() + "/WAT/GetAgentActivityDetails",
        data: '{StartDate:"",EndDate: "",LoginMID: "' + $("#LoginMID").val() + '"}',
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
                $("#hidData").val(data);
                var jsonstring = JSON.parse(data)

                $('#tbl_Data tr td').parents('tr').remove();
                if (jsonstring.length > 0) {
                    $("#dvSave").show();
                    jsonstring = data.replace('[{', '{ "DataSet":[{') + "}";
                    $('#hdnData').val(jsonstring);
                }
                else {
                    $("#dvSave").hide();
                    $('#hdnData').val('');
                    LoadError("No record found.");
                    $('#DisableDiv').html("");
                }
                BindData();
            }
            else {
                $("#dvSave").hide();
                LoadError("Problem performing operation, please try again.");
                $('#DisableDiv').html("");
            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
    return false;
}

function BindData() {
    $('#tbl_Data').html("");
    var data1 = $("#hidData").val();
    var test = false;
    var jsonstring
    if (data1 == "[]") {
        jsonstring = '{"DataSet":[{}]}'
        $("#hidData").val('{"DataSet":[]');
    }
    else {
        test = true;
        jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
    }
    var data = $.parseJSON(jsonstring);
    if (jsonstring.indexOf("DataSet") >= 0) {
        $('#tbl_Data tr:not(:first)').remove();
        var datapaging = data.DataSet.splice(0, data.DataSet.length);
        $("#tbl_Data").append("<tbody><tr><th>Employee name</th>" +
            "<th>Work Group</th>" +
            "<th>Work Division</th>" +
            "<th>Work Item</th>" +
            "<th>Work Carried Forward</th>" +
            "<th>Work Received</th>" +
            "<th>Work Completed</th>" +
            "</tr></tbody>");
        if (test) {
            for (var i = 0; i < datapaging.length; i++) {
                $("#tbl_Data").append('<tr><td>' + datapaging[i].EmployeeName +
                    '</td><td>' + datapaging[i].WorkGroupName +
                    '</td><td>' + datapaging[i].WorkDivisionName +
                    '</td><td>' + datapaging[i].WorkItemName +
                    '</td><td>' + datapaging[i].CarryForwardWork +
                    '</td><td>' + datapaging[i].TodayWork +
                    '</td><td>' + datapaging[i].CompletedWork +
                    '</td></tr>');
            }
        }
    }
    else {
        $('#tbl_Data').html('');
    }
    $('#DisableDiv').html("");
}

function ValidateandInsertTasks() {
    if ($('#WorkGMID').val() == "") {
        LoadError("Please select Work Group");
        $("#ddlWorkGroup").focus();
        return false;
    }
    if ($('#WorkDMID').val() == "") {
        LoadError("Please select Work Division");
        $("#ddlWorkDivision").focus();
        return false;
    }
    if ($('#WorkIMID').val() == "") {
        LoadError("Please select Work Item");
        $("#ddlWorkItem").focus();
        return false;
    }
    if ($("#WorkReceived").val() == "") {
        LoadError("Please enter Work Received");
        $("#WorkReceived").focus();
        return false;
    }
    if (parseInt($("#WorkReceived").val()) == 0) {
        LoadError("Work Received cannot be 0");
        $("#WorkReceived").focus();
        return false;
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: $('#AppName').val() + "/WAT/InsertAgentALGTasks",
        data: '{LoginMID:"' + $("#LoginMID").val() + '",GlobalUserID:"' + $("#GlobalUserID").val() + '",WorkGMID:"' + $("#WorkGMID").val() +
            '",WorkDMID:"' + $("#WorkDMID").val() + '",WorkIMID:"' + $("#WorkIMID").val() + '",WorkReceived:"' + $("#WorkReceived").val() + '",Host:"' + $("#Host").val() + '"}',
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            if (data != "") {
                //  var jsonstring = JSON.parse(data)
                $('#WorkGMID').val("");
                $("#WorkDMID").val("");
                $("#WorkIMID").val("");
                $("#WorkReceived").val("")
                LoadWorkGroups();
                GetAgentActivityDetails();
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
    return false;
}

function ValidateHistoricDataSearch() {
    if (trim($('#StartDate').val()) == "") {
        LoadError('Please enter From Date.');
        $('#StartDate').focus();
        return false;
    }
    if (trim($('#EndDate').val()) == "") {
        LoadError("Please enter To Date.");
        $('#EndDate').focus();
        return false;
    }
    if (!ComparewithCurrentDate(trim($('#StartDate').val()))) {
        LoadError("Start date can't be greater than current date.");
        $('#StartDate').focus();
        return false;
    }
    if (!CompareDate(trim($('#StartDate').val()), trim($('#EndDate').val()))) {
        LoadError("Start date can't be greater than end date.");
        $('#StartDate').focus();
        return false;
    }
    GetAgentHistoricDataDetails();
}

function GetAgentHistoricDataDetails() {
    $('#tbl_Data').html("");
    $.ajax({
        type: "POST",
        url: $('#AppName').val() + "/WAT/GetAgentActivityDetails",
        data: '{StartDate:"' + $("#StartDate").val() + '",EndDate: "' + $("#EndDate").val() + '",LoginMID: "' + $("#LoginMID").val() + '"}',
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
                $("#hidData").val(data);
                var jsonstring = JSON.parse(data);
                $('#tbl_Data tr td').parents('tr').remove();
                if (jsonstring.length > 0) {
                    $("#dvSave").show();
                    var jsonstringval = data.replace('[{', '{ "DataSet":[{') + "}";
                    $('#hdnData').val(jsonstringval);
                }
                else {
                    $("#dvSave").hide();
                    $('#hdnData').val('');
                    LoadError("No record found.");
                    $('#DisableDiv').html("");
                }
                BindHistoricData();
            }
            else {
                $("#dvSave").hide();
                LoadError("Problem performing operation, please try again.");
                $('#DisableDiv').html("");
            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
    return false;
}

function BindHistoricData() {
    $('#tbl_Data').html("");
    var data1 = $("#hidData").val();
    var test = false;
    var jsonstring
    if (data1 == "[]") {
        jsonstring = '{"DataSet":[{}]}'
        $("#hidData").val('{"DataSet":[]');
    }
    else {
        test = true;
        jsonstring = data1.replace('[{', '{ "DataSet":[{') + "}";
    }
    var data = $.parseJSON(jsonstring);
    if (jsonstring.indexOf("DataSet") >= 0) {
        $('#tbl_Data tr:not(:first)').remove();
        var datapaging = data.DataSet.splice(0, data.DataSet.length);
        $("#tbl_Data").append("<tbody><tr><th>Activity Date</th>" +
            "<th>Employee name</th>" +
            "<th>Work Group</th>" +
            "<th>Work Division</th>" +
            "<th>Work Item</th>" +
            "<th>Work Carried Forward</th>" +
            "<th>Work Received</th>" +
            "<th>Work Completed</th>" +
            "</tr></tbody>");
        if (test) {
            for (var i = 0; i < datapaging.length; i++) {
                $("#tbl_Data").append('<tr><td>' + datapaging[i].ActivityDate +
                    '</td><td>' + datapaging[i].EmployeeName +
                    '</td><td>' + datapaging[i].WorkGroupName +
                    '</td><td>' + datapaging[i].WorkDivisionName +
                    '</td><td>' + datapaging[i].WorkItemName +
                    '</td><td>' + datapaging[i].CarryForwardWork +
                    '</td><td>' + datapaging[i].TodayWork +
                    '</td><td>' + datapaging[i].CompletedWork +
                    '</td></tr>');
            }
        }
    }
    else {
        $('#tbl_Data').html('');
    }
    $('#DisableDiv').html("");
}

function ClearControl(DropDown, hiddenfields) {
    $(DropDown).empty();
    $(DropDown).multiselect("uncheckAll");
    $(DropDown).multiselect("refresh");
    $(hiddenfields).val('');
}

function ComparewithCurrentDate(date) {
    var dt1 = parseInt(date.substring(0, 2), 10);
    var mon1 = parseInt(date.substring(3, 5), 10);
    var yr1 = parseInt(date.substring(6, 10), 10);
    var StartDate = new Date(yr1, (mon1 - 1), dt1);

    var CurDate = $("#hidDate").val();
    var dt3 = parseInt(CurDate.substring(0, 2), 10);
    var mon3 = parseInt(CurDate.substring(3, 5), 10);
    var yr3 = parseInt(CurDate.substring(6, 10), 10);
    var Today = new Date(yr3, (mon3 - 1), dt3);

    if (StartDate > Today) {
        return false;
    }
    return true;
}

function CompareDate(startdate, enddate) {
    var dt1 = parseInt(startdate.substring(0, 2), 10);
    var mon1 = parseInt(startdate.substring(3, 5), 10);
    var yr1 = parseInt(startdate.substring(6, 10), 10);
    var StartDate = new Date(yr1, (mon1 - 1), dt1);

    var dt3 = parseInt(enddate.substring(0, 2), 10);
    var mon3 = parseInt(enddate.substring(3, 5), 10);
    var yr3 = parseInt(enddate.substring(6, 10), 10);
    var EndDate = new Date(yr3, (mon3 - 1), dt3);

    if (StartDate > EndDate) {
        return false;
    }
    return true;
}

function newWorkItemsDetails() {
    ShowProgress();
    $('#ddlWorkDivision').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID:' + $("#GlobalUserID").val() + '" }'

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: $('#AppName').val() + '/WAT/FetchWorkGroups',
        data: parameter,
        dataType: "json",
        success: function (data) {

            var jsonstring = JSON.parse(data)
            $('#ddlWorkDivision').empty();
            $('#ddlWorkDivision').append($("<option>").val("").html("WorkDivision"));
            for (var i = 0; i < jsonstring.length; i++) {
                $('#ddlWorkDivision').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
            }
            $('#ddlWorkDivision').val($('#WorkGMID').val());
            $('#ddlWorkDivision').change(function () {
                var id = $('#ddlWorkDivision').val();
                $('#WorkDMID').val(id);
                LoadWorkItems(id)
            })
            $('#DisableDiv').html("");
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });

}

function WorkItemsDetails() {

    // newWorkItemsDetails();
    // LoadWorkDivision(1);
    $('#ddlWorkGrpItem').empty();
    $('#ddlWorkDivision').append($("<option>").val("").html("WorkDivision"));
    $('#ddlWorkItem').append($("<option>").val("").html("WorkItem"));
    ShowProgress();
    // var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID:' + $("#GlobalUserID").val() + '" }'
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID:"' + $("#GlobalUserID").val() + '" }'
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        //url: $('#AppName').val() + '/WAT/WorkItemsDetails_ALG',
        url: $('#AppName').val() + '/WAT/FetchWorkGroupnew',
        data: parameter,
        dataType: "json",
        success: function (data) {


            var jsonstring = JSON.parse(data)
            $('#ddlWorkGrpItem').append($("<option>").val("").html("Work Group"));
            $(jsonstring).each(function (index) {
                $('#ddlWorkGrpItem').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });
            if (jsonstring.length == 1) {
                $('#WorkGMID').val(jsonstring[0].ID)
                LoadWorkDivision(jsonstring[0].ID);
            }
            if ($('#WorkGMID').val() != "XB8sqbXYA9s=")
                LoadWorkDivision($('#WorkGMID').val());
            LoadWorkDivision($('#WorkGMID').val());
            $('#ddlWorkGrpItem').val($('#WorkGMID').val());
            $('#ddlWorkGrpItem').change(function () {

                var id = $('#ddlWorkGrpItem').val();
                $('#WorkGMID').val(id);
                LoadWorkDivision(id)
                //WorkItemChange();
            })
            $('#DisableDiv').html("");
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
}



function WorkItemsDetails_EasyJet() {

    // newWorkItemsDetails();
    // LoadWorkDivision(1);
    $('#ddlWorkGrpItem').empty();
    $('#ddlWorkDivision').append($("<option>").val("").html("WorkDivision"));
    $('#ddlWorkItem').append($("<option>").val("").html("WorkItem"));
    ShowProgress();
    // var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID:' + $("#GlobalUserID").val() + '" }'
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",GlobalUserID:"' + $("#GlobalUserID").val() + '" }'
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        //url: $('#AppName').val() + '/WAT/WorkItemsDetails_ALG',
        url: $('#AppName').val() + '/WAT/FetchWorkGroupnew_EasyJet',
        data: parameter,
        dataType: "json",
        success: function (data) {


            var jsonstring = JSON.parse(data)
            $('#ddlWorkGrpItem').append($("<option>").val("").html("Work Group"));
            $(jsonstring).each(function (index) {
                $('#ddlWorkGrpItem').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });
            if (jsonstring.length == 1) {
                $('#WorkGMID').val(jsonstring[0].ID)
                LoadWorkDivision(jsonstring[0].ID);
            }
            if ($('#WorkGMID').val() != "XB8sqbXYA9s=")
                LoadWorkDivision($('#WorkGMID').val());
            LoadWorkDivision($('#WorkGMID').val());
            $('#ddlWorkGrpItem').val($('#WorkGMID').val());
            $('#ddlWorkGrpItem').change(function () {

                var id = $('#ddlWorkGrpItem').val();
                $('#WorkGMID').val(id);
                LoadWorkDivision(id)
                //WorkItemChange();
            })
            $('#DisableDiv').html("");
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
}






function LoadWorkDivision(WorkGMID) {
    ShowProgress();

    $('#ddlWorkDivision').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkGMID: "' + WorkGMID + '" }'
    if (WorkGMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/FetchWorkDivisionsNew',
            data: parameter,
            dataType: "json",
            success: function (data) {

                var jsonstring = JSON.parse(data)
                $('#ddlWorkDivision').empty();
                $('#ddlWorkDivision').append($("<option>").val("").html("WorkDivision"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkDivision').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                if (jsonstring.length == 1) {
                    $('#WorkDMID').val(jsonstring[0].ID)
                    LoadWorkItems(jsonstring[0].ID, WorkGMID);
                }
                $('#ddlWorkDivision').val($('#WorkDMID').val());
                if ($('#WorkDMID').val() != "XB8sqbXYA9s=")
                    LoadWorkItems($('#WorkDMID').val(), WorkGMID);
                $('#ddlWorkDivision').change(function () {
                    var id = $('#ddlWorkDivision').val();
                    $('#WorkDMID').val(id);
                    LoadWorkItems(id, WorkGMID)
                })
                $('#DisableDiv').html("");
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlCallOutcome').append($("<option>").val("").html("Outcome"));
        $('#OutcomeMID').val("");
    }
}




function WorkItemChange() {
    if ($('#WorkIMID').val() != "") {
        var TodayTask = $("#WorkIMID").val().split('#~#');
        $('#divTask').show();
        $('#divTask').html(TodayTask[1]);
    }
    else {
        $('#divTask').hide();
    }

}

function ValidateWorkStart(Btn) {

    var ddlWorkItem = document.getElementById("ddlWorkItem");
    var wrkgroupMID = $("#ddlWorkGrpItem").val();

    $("#hidSelectWorkGroupMID").val(wrkgroupMID);
    if (ddlWorkItem.selectedIndex <= 0) {
        LoadError("Please select Work Item.");
        ddlWorkItem.focus();
        return false;
    }
    $("#ActionSMID").val(2);
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }
}

function ChangeRefStatus() {
    if ($("#RefNoStatus").is(':checked') == true) {
        $("#RefNo").val("");
        $("#RefNo").prop('disabled', false);
    }
    else {
        $("#RefNo").val("");
        $("#RefNo").prop('disabled', true);
    }
}

function ValidateCompleteWork(Btn) {

    if ($("#WorkDMID").val() == "28") {
        if (trim($("#StartDateTime").val()) == '') {
            LoadError("Please enter Start Date.");
            $("#StartDateTime").focus();
            return false;
        }

        if (trim($("#Hour").val()) == '') {
            LoadError("Please enter hour.");
            //$("#StartDateTime").focus();
            return false;
        }

        if (trim($("#minute").val()) == '') {
            LoadError("Please enter minute.");
            //$("#StartDateTime").focus();
            return false;
        }

        var dd = $("#StartDateTime").val().split('/')[0];
        var MM = $("#StartDateTime").val().split('/')[1];
        var yyyy = $("#StartDateTime").val().split('/')[2];
        var HH = $("#Hour").val();
        var mm = $("#minute").val();
        var newDate = new Date(yyyy, MM - 1, dd, HH, mm, 00);
        var SysDate = new Date();
        if (newDate > SysDate) {
            LoadError("Start Datetime cannot be greater than Current Datetime.");
            return false;
        }
    }
    if ($("#ddlOutcome").val() == "") {
        LoadError("Please select Outcome.");
        $("#ddlOutcome").focus();
        return false;
    }
    if (!validateDynamicControl())
        return false;
    //    SubQueue = $("#ddlSubQueueID option:selected").val()
    //    $("#SubQueueID").val(SubQueue);
    //    $("#OutcomeMID").val($("#ddlOutcome").val());

    $("#ActionSMID").val(2);
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }

}
function CancelWork(Btn) {
    $("#ActionSMID").val(2);
    $("#Type").val("Cancel");
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }
}

function ValidateCallComplete(Btn) {
    if ($("#ddlWorkGroup").val() == "") {
        LoadError("Please select Work Group.");
        $("#ddlWorkGroup").focus();
        $("#ddlWorkGroup").focus();
        return false;
    }
    if ($("#ddlWorkDivision").val() == "") {
        LoadError("Please select Work Division.");
        $("#ddlWorkDivision").focus();
        return false;
    }
    if ($("#ddlCallOutcome").val() == "") {
        LoadError("Please select Call Outcome.");
        $("#ddlCallOutcome").focus();
        return false;
    }

    //    if (isNaN($("#Ticket").val())) {
    //        LoadError("Please Enter Numaric Value Only.");
    //        $("#ddlCallOutcome").focus();
    //        return false;
    //    } 
    $("#OutcomeMID").val($("#ddlCallOutcome").val());

    $("#ActionSMID").val(22);
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }
}

function DownloadAgentActivityDetails() {
    window.open($('#hidAppName').val() + '/WAT/DownloadAgentActivityDetails?StartDate=&EndDate=&LoginMID=' + $("#LoginMID").val());
}

function DownloadAgentHistoricActivityDetails() {
    window.open($('#hidAppName').val() + '/WAT/DownloadAgentActivityDetails?StartDate=' + $("#StartDate").val() + '&EndDate=' + $("#EndDate").val() + '&LoginMID=' + $("#LoginMID").val());
}

function logout(Btn) {
    $("#ActionSMID").val(16);
    if ($(Btn).hasClass("disabled")) {
        return false;
    }
    else {
        $(Btn).addClass("disabled");
        return true;
    }
}
function GetDynamicName(ID) {
    var bewID = ID.id;
    var res = bewID.substring(3, bewID.length);
    var DLTExt = $('#' + ID.id + ' option:selected').text();
    var newtsxt = $('#' + ID.id).val();
    //ltext = DLTExt + '~' + newtsxt;
    $('#hid' + res).val(newtsxt);
    $('#txt' + res).val(DLTExt);

}
//Added by Amit
function GetDynamicRadioName(rowId, values, colname) {
    $("#LoadDynamicControlForComActivity_" + rowId + "__MiscValue").val(values);
    // ShowHidePCC(rowId, colname);
}
function GetDynamicNameDependent(ID, Dependent, ParentDocDID) {

    var bewID = ID.id;
    var res = bewID.replace('ddl', '');
    //    var res = bewID.substring(3, 5);
    var DLTExt = $('#' + ID.id + ' option:selected').text();
    var newtsxt = $('#' + ID.id).val();
    //var ltext = DLTExt + '~' + newtsxt;
    $('#hid' + res).val(newtsxt);
    $('#txt' + res).val(DLTExt);
    GetDynamicDDLData(newtsxt, "ddl" + ParentDocDID);
}

function GetDynamicDDLData(ParentDocMID, DropDown) {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: $('#AppName').val() + '/WAT/BindDynamicDependentDropdown',
        data: '{ ParentDocMID: "' + ParentDocMID + '"}',
        dataType: "json",
        success: function (data) {

            var jsonstring = JSON.parse(data)
            $('#' + DropDown).empty();
            $('#' + DropDown).append($("<option>").val("").html("- Select -"));
            for (var i = 0; i < jsonstring.length; i++) {
                $('#' + DropDown).append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
            }

        },
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $('#td_Message').html('Problem performing operation, please try again.')
            }
            $('#DisableDiv').html("");

        }
    });
}

function KeyPressSpecialCharValidate(id, key) {

    if ((key.keyCode == 13) || (key.charCode == 32) || (key.keyCode == 8) || (key.keyCode == 46) || (key.keyCode == 37) || (key.keyCode == 39) || (key.keyCode == 40) || (key.charCode == 33) || (key.charCode == 34) || (key.charCode == 35) || (key.charCode == 58) || (key.charCode == 63) || (key.charCode == 59)) {
        $("#" + id).val('');

        return true;
    }
    else if ((key.charCode < 97 || key.charCode > 122) && (key.charCode < 65 || key.charCode > 90) && (key.charCode <= 38 || key.charCode > 57)) {

        return false;
    }
    else {

        return true;
    }
}
function Isnum(el) {
    var id = el.id;
    var value = $("#" + id).val();
    if (check(value) == false) {
        LoadError("");
    }
    else {
        $("#" + id).val("");
        LoadError('Your Enter illegal characters.');
        return true;

    }
    if ($.isNumeric(value) && (value % 1 === 0)) {
        LoadError("");

    }
    else {
        $("#" + id).val("");
        LoadError("Please enter numeric value");
    }
}

function CountChangeDynamicControls(obj, Llb_Id, limit, event) {

    var _count = 0;
    var Lbl_count = document.getElementById(Llb_Id);
    var Key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (Key == 17) {
        return false;
    }
    else {
        if (trim(obj.value).length > 0) {
            if (trim(obj.value).length > limit) {
                obj.value = obj.value.substring(0, limit);
                Lbl_count.innerHTML = "Limit Exceeded.";
                Lbl_count.style.color = "#a12830";
            }
            else {
                _count = limit - trim(obj.value).length;
                Lbl_count.innerHTML = (_count > 0 ? _count + " char remaining." : "");
                Lbl_count.style.color = "#000000";
            }
            return true;
        }
    }
}
function isEmail(el) {

    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var id = el.id;
    var value = $("#" + id).val();
    if (check(value) == false) {
        LoadError("");
    }
    else {
        $("#" + id).val("");
        LoadError('Your Enter illegal characters.');
        return true;

    }
    if (filter.test(value)) {
        LoadError("");
    }
    else {
        $("#" + id).val("");
        LoadError("Please enter Valid Email");
    }
}
var specialChars = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`-="
var check = function (string) {
    for (var i = 0; i < specialChars.length; i++) {
        if (string.indexOf(specialChars[i]) > -1) {
            return true
        }
    }
    return false;
}

function ValidateRegular(el) {
    var id = el.id;
    var value = $("#" + id).val();
    if (check(value) == false) {
        LoadError("");
    }
    else {
        $("#" + id).val("");
        LoadError('Your Enter illegal characters.');
        return true;

    }
}
function BindMultiselect(URl, Parameter, DropDown, HiddenField, hidtxt) {

    $('#' + DropDown).empty();
    ShowProgress();
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: URl,
        data: Parameter,
        dataType: "json",
        success: function (data) {

            var jsonstring = JSON.parse(data)
            $('#' + DropDown).empty();
            for (var i = 0; i < jsonstring.length; i++) {
                $('#' + DropDown).append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
            }
            LoadMultiSelect('#' + DropDown, "#" + HiddenField);
            // $('#DisableDiv').html("");
            $('#' + DropDown).multiselect({
                close: function () {
                    var values = new Array();
                    var txts = new Array();
                    $('#' + DropDown).multiselect("getChecked").each(function (index, item) {
                        values.push($(item).val());



                        txts.push($(item).attr('title'));

                    });
                    $("input[id*=" + HiddenField + "]").val(values.join(","));
                    $("input[id*=" + hidtxt + "]").val(txts.join(","));

                }
            });

        },
        error: function (xhr) {
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                $('#td_Message').html('Problem performing operation, please try again.')
            }
            $('#DisableDiv').html("");

        }
    });
}
function LoadMultiSelect(dropdown, hidden) {

    $(dropdown).multiselect("uncheckAll");

    if ($(dropdown).children('option').length == 0) {
        $(hidden).val("");
    }
    var listAssigned = $(dropdown + "option:selected").length;

    if (listAssigned == 0 || listAssigned == undefined) {

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

    if ($(dropdown + " option:selected").length == 0) {
        $(hidden).val("");
    }
}
function Minlen(len, el, evt) {
    var id = el.id;
    var regex = new RegExp("^[a-zA-Z0-9-]+$");
    var key = String.fromCharCode(!event.charCode ? evt.which : evt.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
    $("#" + id).attr('maxlength', len);
}
function IsAlfaNumeric(el, evt) {
    var id = el.id;
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? evt.which : evt.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}
function ShowHidePCC() {
    //var radioValue = $("input[name='rdo" + colname+"']:checked").val()
    var radioValue = $("input[name='rdoCol5']:checked").val()
    if (radioValue == "Delayed") {
        $("#lblLi_Col6,#lblLi_Col7").css('display', 'none');
        $("input[name='LoadDynamicControlForComActivity[5].MiscValue']").val("");
        $("#LoadDynamicControlForComActivity_6__MiscValue").val("");
        $("#LoadDynamicControlForComActivity_4__MiscValue").val("Delayed")

    }
    else if (radioValue == "Closed") {
        $("#lblLi_Col6,#lblLi_Col7").css('display', '');
        $("#LoadDynamicControlForComActivity_6__MiscValue").val($('input[name=rdoCol7]:checked').val());
        $("#LoadDynamicControlForComActivity_4__MiscValue").val("Closed")
    }
}


////function LoadWorkItems(WorkDMID) {
////    ShowProgress();
////    LoadError("");
////    $('#ddlWorkItem').empty();
////    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkDMID: "' + WorkDMID + '" }'
////    if (WorkDMID != "") {
////        $.ajax({
////            type: "POST",
////            contentType: "application/json; charset=utf-8",
////            url: $('#AppName').val() + '/WAT/FetchWorkItems',
////            data: parameter,
////            dataType: "json",
////            success: function (data) {
////                var jsonstring = JSON.parse(data)
////                $('#ddlWorkItem').empty();
////                $('#ddlWorkItem').append($("<option>").val("").html("- Select -"));
////                for (var i = 0; i < jsonstring.length; i++) {
////                    $('#ddlWorkItem').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
////                }
////                $('#ddlWorkItem').val($('#WorkIMID').val());
////                $('#ddlWorkItem').change(function () {
////                    var id = $('#ddlWorkItem').val();
////                    $('#WorkIMID').val(id);
////                })
////                $('#DisableDiv').html("");
////            },
////            error: function (xhr) {
////                $('#DisableDiv').html("");
////                if (xhr.status === 401) {
////                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
////                    return;
////                }
////                else {
////                    LoadError("Problem performing operation, please try again.");
////                }
////            }
////        });
////    }
////    else {
////        $('#ddlWorkItem').append($("<option>").val("").html("- Select -"));
////        $('#WorkIMID').val("");
////    }
////    $('#DisableDiv').html("");
////}


function LoadWorkDivisionsDataEntry(WorkGMID) {
    ShowProgress();

    LoadError("");
    $('#ddlWorkDivisionDataEntry').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",AccessType: "' + $("#AccessType").val() + '",WorkGMID: "' + WorkGMID + '" }'
    if (WorkGMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/FetchWorkDivisions',
            data: parameter,
            dataType: "json",
            success: function (data) {
                $('#ddlWorkDivisionDataEntry').empty();
                var jsonstring = JSON.parse(data)
                $('#ddlWorkDivisionDataEntry').append($("<option>").val("").html("- Select -"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkDivisionDataEntry').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                $('#ddlWorkDivisionDataEntry').val($('#WorkDMID').val());
                $('#ddlWorkDivisionDataEntry').change(function () {
                    var id = $('#ddlWorkDivisionDataEntry').val();
                    $('#WorkDMID').val(id);
                    LoadWorkItemsDataEntry($('#WorkDMID').val(), $('#WorkGMID').val());
                })
                LoadWorkItemsDataEntry($('#WorkDMID').val(), $('#WorkGMID').val());
                $('#DisableDiv').html("");
            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlWorkDivision').append($("<option>").val("").html("- Select -"));
        $('#WorkDMID').val("");
        LoadWorkItemsDataEntry("");
    }
    $('#DisableDiv').html("");
}

function LoadWorkItemsDataEntry(WorkDMID, WorkGMID) {
    ShowProgress();
    LoadError("");

    $('#ddlWorkItemDataEntry').empty();
    var parameter = '{ LoginMID: "' + $("#LoginMID").val() + '",WorkDMID: "' + WorkDMID + '",WorkGMID: "' + WorkGMID + '"}';
    if (WorkDMID != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $('#AppName').val() + '/WAT/WorkItemsALGDetailsDataEntry',
            data: parameter,
            dataType: "json",
            success: function (data) {
                debugger;
                var jsonstring = JSON.parse(data)
                $('#ddlWorkItemDataEntry').empty();
                $('#ddlWorkItemDataEntry').append($("<option>").val("").html("WorkItem"));
                for (var i = 0; i < jsonstring.length; i++) {
                    $('#ddlWorkItemDataEntry').append($("<option>").val(jsonstring[i].ID).html(jsonstring[i].Name));
                }
                $('#ddlWorkItemDataEntry').val($('#WorkIMID').val());

                if (jsonstring.length == 1) {
                    $('#ddlWorkItemDataEntry').val(jsonstring[0].ID);
                    $('#WorkIMID').val(jsonstring[0].ID);
                    //if ($("#Assignment").val() == "True") {
                    //    WorkItemChange();
                    //}
                    // $('#WorkIMID').val(jsonstring[0].ID);
                }

                $('#ddlWorkItemDataEntry').change(function () {
                    var id = $('#ddlWorkItemDataEntry').val();
                    $('#WorkIMID').val(id);
                    //if ($("#Assignment").val() == "True") {
                    //    WorkItemChange();
                    //}
                })
                $('#DisableDiv').html("");

            },
            error: function (xhr) {
                $('#DisableDiv').html("");
                if (xhr.status === 401) {
                    window.location.href = $('#hidAppName').val() + "/WAT/Close";
                    return;
                }
                else {
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
    }
    else {
        $('#ddlWorkItemDataEntry').append($("<option>").val("").html("- Select -"));
        $('#WorkIMID').val("");
    }
    $('#DisableDiv').html("");
}

function GetFIFODetail() {

    $.ajax({
        type: "POST",
        url: $('#AppName').val() + "/WAT/FetchCaseFromDataMasterFIFO",
        data: '{WorkGroupMID:"' + $("#WorkGMID").val() + '",LoginMID: "' + $("#LoginMID").val() + '"}',
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
                $("#hidData").val(data);
                var jsonstring = JSON.parse(data);
                var MainData = jsonstring.MainData;
                var RowData = jsonstring.RowData;
                var SectorsData = jsonstring.SectorsData;
                var str = "";
                var param = '';
                for (var i = 0; i < MainData.length; i++) {
                    //var DataValue = MainData[i].DataMDBColumnName
                    if (i == (MainData.length - 1)) {
                        param += RowData[0][MainData[i].DataColumn] + ''
                    }
                    else {
                        param += RowData[0][MainData[i].DataColumn] + ','
                    }
                    str += "<li>" + MainData[i].LabelNameOnATScreen + " : " + RowData[0][MainData[i].DataColumn] + " </li>"
                }

                if (SectorsData[0].SectorsVisibility == "1") {
                    // str += "<li>Sectors : " + SectorsData[0].SectorsVisibility + " </li>"
                    str += "<li>Sectors : " + '<a class="hint--top" data-hint="Show All Sectors" id="titlefav" href="#"><button type="button" id = "btnAdd" class="add_icon icon-plus" onclick = "return ShowAddAuditTemplate();" ></button ></a >' + "</li>"
                    // $("#titlefav").show();
                }


                $("#hidParameterList").val(param);
                $("#dvActiveWork").append(str);

            }
            else {
                $("#dvSave").hide();
                LoadError("Problem performing operation, please try again.");
                $('#DisableDiv').html("");
            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/Close";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
    return false;

}
function ShowAddAuditTemplate() {
    // $("#showUpdateRole").animate({ width: 'toggle' });
    var parameter = $("#hidParameterList").val();
    var str = parameter;
    var array = str.split(',');
    $.ajax({
        type: "POST",
        url: $('#AppName').val() + "/WAT/FetchCaseSectorFromFormatedData",
        data: '{Date:"' + array[0] + '",PNR:"' + array[1] + '",UMID:"89",LoginMID: "' + $("#LoginMID").val() + '"}',
        // data: '{Date:"26 Feb 2021",PNR:"K1VN753",UMID:"89",LoginMID: "' + $("#LoginMID").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            ShowProgress();
        },
        complete: function () {
            $('#DisableDiv').html("");
        },
        success: function (data) {
            //debugger;
            if (data != "") {
                $("#hidData").val(data);
                var jsonstring = JSON.parse(data);
                var MainData = jsonstring.MainData;
                if (MainData.length > 0) {
                    //  debugger;
                    $("#showUpdateRole").animate({ width: 'toggle' });
                    BindContentData(MainData, "TableSectors");
                }
                else {
                    $('#TableSectors').html("");
                    LoadError("No sectors found.");
                }

            }
            else {
                $("#dvSave").hide();
                LoadError("Problem performing operation, please try again.");
                $('#DisableDiv').html("");
            }
        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/WAT/ActivityTracker_Easyjet";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
    return false;

}

function ClosePopUP() {
    $("#showUpdateRole").animate({ height: 'toggle' });
    $("#td_Message").html("")
    return ('false');
}


function BindContentData(jsonstring, Table) {
    //  debugger;
    $('#' + Table).html("");
    //var temp = "";
    if (jsonstring.length != 0) {
        var header = Object.keys(jsonstring[0]);
        var tbldetail = '<thead> <tr>'; // +

        header.forEach(function (text, index) {
            if (text.toLowerCase().indexOf("id") == -1 && text.toLowerCase().indexOf("querystring") == -1) {
                tbldetail = tbldetail + '<th>' + text + '</th>';
            }
        });
        $('#' + Table).append(tbldetail);
        for (var i = 0; i < jsonstring.length; i++) {
            var col = '';
            //var ReplyCol = '';
            var Item = "";
            var newRowContent = '<tr>';
            for
                (var j = 0; j < header.length; j++) {
                if (jsonstring[i][header[j]] == null || jsonstring[i][header[j]] == undefined) {
                    Item = "";
                }
                else {
                    Item = jsonstring[i][header[j]];
                }

                newRowContent = newRowContent + '<td>' + Item + '</td>'

            }
            newRowContent = newRowContent + col + '</tr>';
            $('#' + Table).append(newRowContent);
        } //-------------------outer for loops ends

    }
    $('#DisableDiv').html("");
}








