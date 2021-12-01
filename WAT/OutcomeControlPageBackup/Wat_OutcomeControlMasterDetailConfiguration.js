

function ShowAddOutcomeControlConfiguration() {
    $("#showUpdateRole").animate({ width: 'toggle' });
    $("#AddEdit").val("A");
}
function BindSingleSelectClient(option, option1, Option2) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddlclient').empty();
    $('#ddlclientSearch').empty();
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
            $('#ddlclientSearch').append($("<option selected>").val("").html("- Select -"));
            $(jsonstring).each(function (index) {
                $('#ddlclient').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
                $('#ddlclientSearch').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
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

            $('#ddlclientSearch').change(function () {
                BindOutcomeControlSearch('DynamicOutcomeControlMaster', $('#ddlclientSearch').val());
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
            })
            // $('#ddloutcomecontrol').change(function () {
            //   $('#DOCMID').val($('#ddloutcomecontrol').val());
            // })

        }
    });
}

function BindOutcomeControlSearch(option, option1) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddloutcomecontrolSearch').empty();

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
            $('#ddloutcomecontrolSearch').append($("<option>").val("").html("- Select -"));

            $(jsonstring).each(function (index) {
                $('#ddloutcomecontrolSearch').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));

            });

            if (jsonstring.length == 1) {
                $('#ddloutcomecontrolSearch').val(jsonstring[0].ID);

            }

        }
    });
}


function BindSingleSelect(option, option1) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddlUploadName').empty();
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
            $('#ddlUploadName').append($("<option>").val("").html("- Select -"));

            $(jsonstring).each(function (index) {
                $('#ddlUploadName').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));
            });

            if (jsonstring.length == 1) {
                $('#ddlUploadName').val(jsonstring[0].ID);
                $('#hdnUMID').val(jsonstring[0].ID);

            }

            $('#ddlUploadName').val($('#UMID').val());
            $('#ddlUploadName').change(function () {
                var id = $('#ddlUploadName').val();
                $('#hdnUMID').val(id);
                $('#UMID').val(id);
                BindSingleSelectColumnName('BindColumnHeaderName', $('#hdnUMID').val());
                BinDispositionColumnName($('#hdnUMID').val());
            })

        }
    });
}


function BindSingleSelectColumnName(option, option1) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddlFormatedTableColumnName').empty();
    $.ajax({
        type: "POST",
        async: false,
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
            $('#ddlFormatedTableColumnName').append($("<option>").val("").html("- Select -"));
            $(jsonstring).each(function (index) {
                $('#ddlFormatedTableColumnName').append($("<option>").val(jsonstring[index].Name).html(jsonstring[index].Name));
            });

            if (jsonstring.length == 1) {
                $('#ddlFormatedTableColumnName').val(jsonstring[0].Name);
                $('#hdnHeaderCMMID').val(jsonstring[0].Name);

            }

            //$('#ddlFormatedTableColumnName').val($('#FormattedTableColumnName').val());

            // $("#ddlFormatedTableColumnName option:selected").text($('#FormattedTableColumnName').val());
            //$("#ddlFormatedTableColumnName").val($('#FormattedTableColumnName').val());
            $('#ddlFormatedTableColumnName').change(function () {
                var TableColumnName = $("#ddlFormatedTableColumnName :selected").val();
                $('#FormattedTableColumnName').val(TableColumnName);

            })

        }
    });
}

function BinDispositionColumnName(UMID) {
    $('#ddlDispositionColumnName').empty();
    ShowProgress();
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: $('#hidAppName').val() + '/WAT/FetchDisPositionColumnName',
        data: '{UMID:"' + UMID + '"}',
        dataType: "json",
        success: function (data) {
            var jsonstring = JSON.parse(data)
            for (var i = 0; i < jsonstring.length; i++) {
                $('#ddlDispositionColumnName').append($("<option>").val(jsonstring[i].Name).html(jsonstring[i].Name));
            }
            LoadMultiSelect('#ddlDispositionColumnName', '#DispositionColumnName');
            $('#DisableDiv').html("");
            $('#ddlDispositionColumnName').multiselect({
                beforeopen: function () {
                    $('#ddlDispositionColumnName').multiselect('refresh');
                },
                close: function () {
                    var values = new Array();
                    $('#ddlDispositionColumnName').multiselect("getChecked").each(function (index, item) {
                        values.push($(item).val());
                    });
                    //  var data = checkMultiselectValues(values.join(","), $('#AuditParameterType').val())
                    $("input[id*=DispositionColumnName]").val(values.join(","));
                }
            });

        },
        error: function (xhr) {
            $('#DisableDiv').html("");
            if (xhr.status === 401) {
                window.location.href = $('#hidAppName').val() + "/Login";
                return;
            }
            else {
                LoadError("Problem performing operation, please try again.");
            }
        }
    });
}

function BindControl(option, option1, Option2) {
    var URl = $('#hidAppName').val() + '/WAT/BindDropDown';
    $('#ddlControlType').empty();
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
            var type = $("#AddEdit").val();
            if (type == "E") {
                $('#ddlControlType').append($("<option>").val("0").html("TextBox"));
                $(jsonstring).each(function (index) {
                    $('#ddlControlType').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));

                });
            }
            else {
                $('#ddlControlType').append($("<option>").val("").html("TextBox"));
                $(jsonstring).each(function (index) {
                    $('#ddlControlType').append($("<option>").val(jsonstring[index].ID).html(jsonstring[index].Name));

                });
            }


            if (jsonstring.length == 1) {
                $('#ddlControlType').val(jsonstring[0].ID);
            }
            $('#ddlControlType').val($('#hdnCntrolMID').val());

            $('#ddlControlType').change(function () {
                var ControlID = $('#ddlControlType').val();
                $('#ControlID').val(ControlID);

            })

        }
    });
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

function showLableName() {
    //debugger;
    val = $('#IsVisibleLableOnATScreen').val();
    if (val == "1") {
        $('#isvisible').show();
    }
    else {
        $('#isvisible').hide();
    }
}

function showDispositionColumnName() {
    //debugger;
    val = $('#IsVisibleDataOnDisposition').val();

    if (val == "Yes") {
        $('#isvisibleDispositionColumnName').show();
        $('#ControlTypeid').show();
    }
    else {
        $('#isvisibleDispositionColumnName').hide();
        $('#ControlTypeid').hide();
    }
}


function SaveData() {
    //   alert("ok");
    if ($('#ddlclient').val() == "XB8sqbXYA9s=" || $('#ddlclient').val() == "" || $('#ddlclient').val() == null) {
        $('#td_MessageEditSave').html('Please select Client.');
        $('#ddlclient').focus();
        return false;
    }

    if ($('#ddloutcomecontrol').val() == "XB8sqbXYA9s=" || $('#ddloutcomecontrol').val() == "" || $('#ddloutcomecontrol').val() == null) {
        $('#td_MessageEditSave').html('Please select WorkGroup Name.');
        $('#ddlworkGroupName').focus();
        return false;
    }


    if ($('#DBColumnName').val() == "") {
        $('#td_MessageEditSave').html('Select DB Column Name.');
        $('#DBColumnName').focus();
        return false;
    }

    if ($('#LabelName').val() == "") {
        $('#td_MessageEditSave').html('Select Label Name.');
        $('#LabelName').focus();
        return false;
    }

    var validation = $('#ValidationType').val();
    if (validation == "" || validation == null) {
        $('#ValidationType').val('0');
    }
    //alert(validation);



    return true;
}




function ShowError() {
    var showHideErrorBox = document.getElementById('showHideErrorBox');
    if ($("#td_Message").text() == '' || showHideErrorBox.style.display == 'none') {
        HideShow2('showHideErrorBox');
        return false;
    }
}

function LoadError(errormessage) {
    if (errormessage != null && errormessage != "") {
        $("#td_Message").html(errormessage);
        ShowError();
    }
}


function LoadGridData() {
    //debugger;
    var clientMID = $('#ddlclientSearch').val()
    var DOCMID = $('#ddloutcomecontrolSearch').val()
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

    else {
        $('#tbl_Data').html("");
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
                    var jsonstring = JSON.parse(data)

                    $('#tbl_Data tr td').parents('tr').remove();
                    if (jsonstring.length > 0) {
                        jsonstring = data.replace('[{', '{ "DataSet":[{') + "}";
                        $('#hdnData').val(jsonstring);
                    }
                    else {
                        $('#hdnData').val('');
                        $("#td_Message").html("No record found.");
                        $('#DisableDiv').html("");
                    }
                    SpreadValue(1);
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
                    LoadError("Problem performing operation, please try again.");
                }
            }
        });
        return false;
    }
}



function SpreadValue(Value) {
    ShowProgress();
    FilterData();
}

function FilterData() {
    //  debugger;
    $('#tbl_Data').html("");
    var data = parseJSONSafely($("#hdnData").val());
    if ($("#hdnData").val().indexOf("DataSet") >= 0) {
        $('#tbl_Data tr:not(:first)').remove();
        $('#hdnFilteredRecordCount').val(data.DataSet.length);
        $(".Pager").Pagging_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: parseInt('1'),
            PageSize: parseInt('10'),
            RecordCount: parseInt(data.DataSet.length.toString())
        });
        var datapaging = data.DataSet.splice(0, 10);
        $("#tbl_Data").append("<tbody><tr><th>ClientName</th><th>Wat Control Master Name</th><th>Label Name</th><th>Control Name</th><th>Is Mandatory</th><th>Validation Type</th><th>Max Len</th><th>Min Len</th><th>CreatedBy</th><th>Edit/Delete</th><th>Add All Dropdown Value</th></tbody>");
        for (var i = 0; i < datapaging.length; i++) {

            
            if (datapaging[i].ControlID == "1") {
                var edit = '<a href="#" class="hint--top" data-hint="Add Dropdown Value"><button type="button" id="btn' + i + '" onclick="return AddDropdownData( ' + "'" + datapaging[i].DOCDID + "'" + ', ' + "'" + datapaging[i].ClientIDEnc + "'" + ', ' + "'" + datapaging[i].DOCMIDEnc + "'" + ', ' + "'" + datapaging[i].DBColumnName + "'" + ', ' + "'" + datapaging[i].LabelName + "'" + ',' + "'" + datapaging[i].ControlID + "'" + ',' + "'" + datapaging[i].IsMandatory + "'" + ',' + "'" + datapaging[i].ValidationType + "'" + ',' + "'" + datapaging[i].Maxlen + "'" + ',' + "'" + datapaging[i].Minlen + "'" + ',' + "'" + datapaging[i].OrderID + "'" + ');" value="Add Dropdown Value" class="delete_user icon-pencil" ></button></a>';
            }
            else {
                edit = '';
            }



            $("#tbl_Data").append('<tr><td>' + datapaging[i].ClientName +
                '</td><td>' + datapaging[i].WatControlMasterName +
                '</td><td>' + datapaging[i].LabelName +
                '</td><td>' + datapaging[i].ControlName +
                '</td><td>' + datapaging[i].IsMandatory +
                '</td><td>' + datapaging[i].ValidationType +
                '</td><td>' + datapaging[i].Maxlen +
                '</td><td>' + datapaging[i].Minlen +
        
                '</td><td>' + datapaging[i].CreatedBy +
                '</td><td align="center">' +
                '<a href="#" class="hint--top" data-hint="Edit"><button type="button" id="btn' + i + '" onclick="return EditData( ' + "'" + datapaging[i].DOCDID + "'" + ', ' + "'" + datapaging[i].ClientIDEnc + "'" + ', ' + "'" + datapaging[i].DOCMIDEnc + "'" + ', ' + "'" + datapaging[i].DBColumnName + "'" + ', ' + "'" + datapaging[i].LabelName + "'" + ',' + "'" + datapaging[i].ControlID + "'" + ',' + "'" + datapaging[i].IsMandatory + "'" + ',' + "'" + datapaging[i].ValidationType + "'" + ',' + "'" + datapaging[i].Maxlen + "'" + ',' + "'" + datapaging[i].Minlen + "'" + ',' + "'" + datapaging[i].OrderID + "'" + ');" value="Edit" class="delete_user icon-pencil" ></button></a>' +
                '</td><td>' + edit +
                '</td></tr>');

        }
    }
    else {
        $('#tbl_Data').html('');
        $("#hdnFilteredData").val('');
        $('#divPage').html('');
    }
    $('#DisableDiv').html("");
}


function BindGridPagging(Pagging) {
    // debugger;
    var Page = Pagging - 1;
    var minindex = Page * 10
    // var maxindex = minindex + 10
    var data = parseJSONSafely($("#hdnData").val());
    var datapaging = data.DataSet.splice(minindex, 10);
    $('#tbl_Data tr:not(:first)').remove();
    for (var i = 0; i < datapaging.length; i++) {
        $("#tbl_Data").append('<tr><td>' + datapaging[i].ClientName +
            '</td><td>' + datapaging[i].WatControlMasterName +
            
            '</td><td>' + datapaging[i].LabelName +
            '</td><td>' + datapaging[i].ControlName +
            '</td><td>' + datapaging[i].IsMandatory +
            '</td><td>' + datapaging[i].ValidationType +
            '</td><td>' + datapaging[i].Maxlen +
            '</td><td>' + datapaging[i].Minlen +
            
            '</td><td>' + datapaging[i].CreatedBy +
            '</td><td align="center">' +
            '<a href="#" class="hint--top" data-hint="Edit"><button type="button" id="btn' + i + '" onclick="return EditData( ' + "'" + datapaging[i].DOCDID + "'" + ', ' + "'" + datapaging[i].ClientIDEnc + "'" + ', ' + "'" + datapaging[i].DOCMIDEnc + "'" + ', ' + "'" + datapaging[i].DBColumnName + "'" + ', ' + "'" + datapaging[i].LabelName + "'" + ',' + "'" + datapaging[i].ControlID + "'" + ',' + "'" + datapaging[i].IsMandatory + "'" + ',' + "'" + datapaging[i].ValidationType + "'" + ',' + "'" + datapaging[i].Maxlen + "'" + ',' + "'" + datapaging[i].Minlen + "'" + ',' + "'" + datapaging[i].OrderID + "'" + ');" value="Edit" class="delete_user icon-pencil" ></button></a>' +
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
    //debugger;
    BindGridPagging(parseInt($(this).attr('page')));
});





function EditData(DOCDID, ClientID, DOCMID, DBColumnName, LabelName, ControlID, IsMandatory, ValidationType, Maxlen, Minlen, OrderID) {
    //debugger;

    $("#showUpdateRole").animate({ width: 'toggle' });
    $("#DOCDID").val(DOCDID);
    $("#AddEdit").val("E");
    $("#ClientMID").val(ClientID);
    $("#hdnClientMID").val(ClientID);
    // $("#ddlclient").val(ClientID);
    //BindSingleSelect('BindUploadName', ClientID);ddloutcomecontrol
    BindSingleSelectClient("Client", "", "");
    $("#DOCMID").val(DOCMID);
    BindOutcomeControlMaster('DynamicOutcomeControlMaster', $('#ClientMID').val());
    $("#ddloutcomecontrol option:selected").val($("#DOCMID").val());
    $("#DBColumnName").val(DBColumnName);
    $("#LabelName").val(LabelName);
    $("#ControlID").val(ControlID);
    BindControl("ControlBind", "", "");
    $("#hdnCntrolMID").val(ControlID);
    $("#ControlID").val(ControlID);
    if (IsMandatory == "Yes") {
        IsMandatory = "1"
        $("#IsManadatory").val(IsMandatory);
    }
    else {
        IsMandatory = "0"
        $("#IsManadatory").val(IsMandatory);
    }

    if (ValidationType == "Mobile Number") {
        $("#ValidationType").val("1");
    }
    else if (ValidationType == "Email") {
        $("#ValidationType").val("2");
    }
    else {
        $("#ValidationType").val("0");
    }

    $("#ValidationType").val(ValidationType);
    $("#Maxlength").val(Maxlen);
    $("#Minlen").val(Minlen);
    $("#OrderID").val(OrderID);
    return false;
}

function AddDrodpwData () {
    $("#showUpdateRole").animate({ width: 'toggle' });
    $("#AddEdit").val("A");
}


function SaveDropdownData() {
    //   alert("ok");
    if ($('#ddlclient').val() == "XB8sqbXYA9s=" || $('#ddlclient').val() == "" || $('#ddlclient').val() == null) {
        $('#td_MessageEditSave').html('Please select Client.');
        $('#ddlclient').focus();
        return false;
    }

    if ($('#ddloutcomecontrol').val() == "XB8sqbXYA9s=" || $('#ddloutcomecontrol').val() == "" || $('#ddloutcomecontrol').val() == null) {
        $('#td_MessageEditSave').html('Please select WorkGroup Name.');
        $('#ddlworkGroupName').focus();
        return false;
    }


    if ($('#DBColumnName').val() == "") {
        $('#td_MessageEditSave').html('Select DB Column Name.');
        $('#DBColumnName').focus();
        return false;
    }

    if ($('#LabelName').val() == "") {
        $('#td_MessageEditSave').html('Select Label Name.');
        $('#LabelName').focus();
        return false;
    }

    var validation = $('#ValidationType').val();
    if (validation == "" || validation == null) {
        $('#ValidationType').val('0');
    }
    //alert(validation);



    return true;
}









