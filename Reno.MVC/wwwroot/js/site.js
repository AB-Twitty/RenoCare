$(".select2").select2();
$('.i-checks').iCheck({
    checkboxClass: 'icheckbox_square-green',
    radioClass: 'iradio_square-green'
});
$('.datepicker').datepicker({
    // rtl: App.isRTL(),
    orientation: "left",
    format: 'dd-M-yyyy',
    startDate: new Date(1900, 1 - 1, 1),
    endDate: '+1y',
    autoclose: true
});

$(".mask_phone").inputmask("mask", { "mask": "999-999999" }, { "placeholder": "" });
$('#ConfirmNationalID').bind("cut copy paste", function (e) {
    e.preventDefault(); return false;
});

$('#PatienNationalID').bind("cut copy paste", function (e) {
    e.preventDefault(); return false;
});
$(".touchspinDes").TouchSpin({
    min: 0,
    max: 200,
    step: 0.1,
    decimals: 2,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnumMonth").TouchSpin({
    min: 0,
    max: 11,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnum4").TouchSpin({
    min: 0,
    max: 10000,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnum").TouchSpin({
    min: 0,
    max: 200,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnum100").TouchSpin({
    min: 0,
    max: 100,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnumFrom1").TouchSpin({
    min: 1,
    max: 100,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnumFrom1to200").TouchSpin({
    min: 1,
    max: 100,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinnum800").TouchSpin({
    min: 0,
    max: 800,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
var age = parseInt($('#Age').val() / 12);
$(".touchspinYear").TouchSpin({
    min: 0,
    max: age,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});
$(".touchspinYearWheelChair").TouchSpin({
    min: 2,
    max: age,
    step: 1,
    decimals: 0,
    boostat: 5,
    maxboostedstep: 10,
    buttondown_class: 'btn btn-white',
    buttonup_class: 'btn btn-white'
});

setTimeout(function () {
    showHideSpinner(false);
}, 2000);

function showHideSpinner(state) {
    if (state === true) {
        $(".laodingModal").show();
        $(".loadingPopup").show();
    }
    else if (state === false) {
        $(".laodingModal").hide();
        $(".loadingPopup").hide();
    }
}
function validatAge(month, year, input) {
    var monthval = $("#" + month).val();
    var yearval = $("#" + year).val();
    var newage = ((parseInt(yearval) || 0) * 12) + (parseInt(monthval) || 0);
    var realAge = parseInt($('#Age').val());
    if (newage >= realAge) {
        var newval = $(input).val() - 1;
        $(input).val('');
    }
    if (parseInt(monthval) === 0 && parseInt(yearval) === 0) {
        $(input).val('');
        swal({
            title: "Invalid Age!",
            text: "Year and Month cannot be equal 0 ",
            type: "error"
        });

    }
}
$(document).ready(function () {
    $('#NewPatient').on('shown.bs.modal', function (e) {
        $('#PatienNationalID').removeAttr('disabled')
        $('#ConfirmNationalID').removeAttr('disabled')
    })

    $("#NewPatient #CreatePatient").on('click', function () {
        debugger;
        var Isvaild = true;
        $("#NationalIDMessage").text("");
        $("#ConfirmNationalIDMessage").text("");
        $("#ConfirmNationalIDMessage").text("");
        $("#NationalIDMessage").css("display", "none");
        $("#ConfirmNationalIDMessage").css("display", "none");
        $("#ErrorMessage").css("display", "none");

        var _NationalID = $("#PatienNationalID").val();
        var ConfirmNationalID = $("#ConfirmNationalID").val();
        if (_NationalID === null || _NationalID === "") {
            $("#NationalIDMessage").append(" National ID Required");
            $("#NationalIDMessage").css("display", "block");
            Isvaild = false;
        }
        if (ConfirmNationalID === null || ConfirmNationalID === "") {
            $("#ConfirmNationalIDMessage").append(" Confirm National ID Required");
            $("#ConfirmNationalIDMessage").css("display", "block");
            Isvaild = false;
        }
        if (_NationalID !== ConfirmNationalID) {
            $("#ErrorMessage").text(" National ID And Confirm National ID Not Match");
            $("#ErrorMessage").css("display", "block");
            Isvaild = false;
        }
        if (Isvaild) {
            showHideSpinner(true);
            $.ajax({
                url: "/Patients/CheckExsitNationalId/?NationalID=" + _NationalID,
                type: "GET",
                success: function (data) {
                    if (data === false) {
                        $.ajax({
                            url: "/Patients/CheckNationalId/?NationalID=" + _NationalID,
                            type: "GET",
                            success: function (data) {
                                if (data === true) {
                                    window.location = "/Patients/Create/" + _NationalID;
                                }
                                else {
                                    $("#ErrorMessage").text(" National ID Is Not Valid");
                                    $("#ErrorMessage").css("display", "block");
                                    showHideSpinner(false);
                                }
                            }
                        });
                    }
                    else {
                        $.ajax({
                            url: "/Patients/GetPatientByNational/?NationalID=" + _NationalID,
                            type: "GET",
                            success: function (data) {
                                if (data.sameDoctor === true && data.sameHosptail === true) {
                                    console.log(data);
                                    _NationalID = '';
                                    $("#PatienNationalID").val('');
                                    $("#ConfirmNationalID").val('');
                                    ConfirmNationalID = '';
                                    window.location = "/" + data.diseaseName + "/Index/" + data.patientId;
                                }
                                else if (data.sameDoctor === false && data.sameHosptail === true) {
                                    swal({
                                        title: "Patient is already registered",
                                        text: "Please note that this  patient is already registered in the hospital, do you want to continue to patient report ?",
                                        type: "warning",
                                        showCancelButton: true,
                                        confirmButtonClass: "btn-primary",
                                        confirmButtonText: "Yes, Continue",
                                        cancelButtonText: "No, Cancel",
                                        closeOnConfirm: true,
                                        closeOnCancel: true
                                    },
                                        function (isConfirm) {
                                            if (isConfirm) {
                                                _NationalID = '';
                                                $("#PatienNationalID").val('');
                                                $("#ConfirmNationalID").val('');
                                                ConfirmNationalID = '';
                                                window.location = "/" + data.diseaseName + "/Index/" + data.patientId;
                                            } else {
                                                $('#NewPatient').modal('hide');
                                                showHideSpinner(false);
                                            }
                                        });
                                }
                                else if (data.sameDoctor === false && data.sameHosptail === false) {
                                    swal({
                                        title: "Patient is already registered!",
                                        text: "This patient is registered in hospital '" + data.hosptailName + "' with 'Dr." + data.doctorName + "', therefore permission is required from patient and doctor to transfer patient’s record to the hospital.Please contact the registry Manager.",
                                        type: "error",
                                        showCancelButton: true,
                                        confirmButtonText: "Send Transfer Request",
                                    }, function (isConfirm) {
                                        if (isConfirm) {
                                            $("#TransferPatientPopUp").show();
                                            //SendTransferRequest(event)
                                        } else {

                                        }
                                    });
                                    $('#NewPatient').modal('hide');
                                    showHideSpinner(false);
                                }
                            }
                        });
                    }
                }
            });
        }
    });
    $('#NewPatient').on('hidden.bs.modal', function () {
        _NationalID = '';
        $("#NationalID").val('');
        $("#ConfirmNationalID").val('');
        ConfirmNationalID = '';
    });
});


function SendTransferRequest(e) {
    debugger;
    showHideSpinner(true);
    var _NationalID = $("#PatienNationalID").val();
    var _reason = $("#transfer_reason").val();
    $.ajax({
        url: "/TransferRequest/NewTransferRequest/?NationalID=" + _NationalID + "&reason=" + _reason,
        type: "Post",
        success: function (data) {
            showHideSpinner(false);
            if (data === true) {
                alert("Request has been sent");
                $("#TransferPatientPopUp").hide();
            }
            else {
                //$("#ErrorMessage").text(" National ID Is Not Valid");
                //$("#ErrorMessage").css("display", "block");
                //showHideSpinner(false);
            }
        }
    });
}

$("#closepopup").on('click', function () {
    $("#TransferPatientPopUp").hide();
});

//$(".tab1").on('click', function () {
//    $('.nav-tabs a[href="#tab-1"]').tab('show');
//});
//$(".tab2").on('click', function () {
//    $('.nav-tabs a[href="#tab-2"]').tab('show');
//});
//$(".tab3").on('click', function () {
//    $('.nav-tabs a[href="#tab-3"]').tab('show');
//});
//$(".tab4").on('click', function () {
//    $('.nav-tabs a[href="#tab-4"]').tab('show');
//});
//$(".tab5").on('click', function () {
//    $('.nav-tabs a[href="#tab-5"]').tab('show');
//});
//$(".tab6").on('click', function () {
//    $('.nav-tabs a[href="#tab-6"]').tab('show');
//});
//$(".tab7").on('click', function () {
//    $('.nav-tabs a[href="#tab-7"]').tab('show');
//});
//$(".tab8").on('click', function () {
//    $('.nav-tabs a[href="#tab-8"]').tab('show');
//});
//$(".tab9").on('click', function () {
//    $('.nav-tabs a[href="#tab-9"]').tab('show');
//});
//$(".tab10").on('click', function () {
//    $('.nav-tabs a[href="#tab-10"]').tab('show');
//});
//$(".tab11").on('click', function () {
//	$('.nav-tabs a[href="#tab-11"]').tab('show');
//});
//$(".tab12").on('click', function () {
//	$('.nav-tabs a[href="#tab-12"]').tab('show');
//});
//$(".tabClinicalDiagnosis").on('click', function () {
//	$('.nav-tabs a[href="#tab-ClinicalDiagnosis"]').tab('show');
//});
//$(".tabGeneral").on('click', function () {
//	$('.nav-tabs a[href="#tab-General"]').tab('show');
//});
//$(".tabGeneticTest").on('click', function () {
//	$('.nav-tabs a[href="#tab-GeneticTest"]').tab('show');
//});
//$(".tabCORTICOSTEROIDS").on('click', function () {
//	$('.nav-tabs a[href="#tab-CORTICOSTEROIDS"]').tab('show');
//});
//$(".tabOther").on('click', function () {
//	$('.nav-tabs a[href="#tab-Other"]').tab('show');
//});
//$(".tabHospitalization").on('click', function () {
//	$('.nav-tabs a[href="#tab-Hospitalization"]').tab('show');
//});
//$(".tabComorbidities").on('click', function () {
//	$('.nav-tabs a[href="#tab-Comorbidities"]').tab('show');
//});
//$(".tabTRANSLARNA").on('click', function () {
//	$('.nav-tabs a[href="#tab-TRANSLARNA"]').tab('show');
//});
//$(".tabEXONDYS").on('click', function () {
//	$('.nav-tabs a[href="#tab-EXONDYS"]').tab('show');
//});
//$(".tabCARDIACMEDICATION").on('click', function () {
//	$('.nav-tabs a[href="#tab-CARDIACMEDICATION"]').tab('show');
//});
//$(".tabVITAMIND").on('click', function () {
//	$('.nav-tabs a[href="#tab-VITAMIND"]').tab('show');
//});
//$(".tabOTHERTREATMENT").on('click', function () {
//	$('.nav-tabs a[href="#tab-OTHERTREATMENT"]').tab('show');
//});
//$(".tabAssistive").on('click', function () {
//	$('.nav-tabs a[href="#tab-Assistive"]').tab('show');
//});
//$(".tabAmbulatory").on('click', function () {
//	$('.nav-tabs a[href="#tab-Ambulatory"]').tab('show');
//});
//$(".tabNiceSplints").on('click', function () {
//	$('.nav-tabs a[href="#tab-NiceSplints"]').tab('show');
//});
//$('.tabAllopathicDrugs').on('click', function () {
//	$('.nav-tabs a[href="#tab-AllopathicDrugs"]').tab('show');
//});
//$('.tabDiseaseModifyingTherapy').on('click', function () {
//	$('.nav-tabs a[href="#tab-DiseaseModifyingTherapy"]').tab('show');
//});
//This patient is registered in hospital, therefore permission is required from patient and doctor to transfer patient’s record to the hospital.Please contact the registry manger
$("#SubmitCountry").on('click', function (e) {
    if ($('#Name').val().trim() === "" || $('#Name').val() === null || $('#Code').val().trim() === "" || $('#Code').val() === null) {
        e.preventDefault();
        swal({
            title: "Please Fill Data!",
            text: "Fields cannot be only white space.",
            type: "error"
        });
    }
});
$("#CitiesSubmit").on('click', function (e) {
    if ($('#Name').val().trim() === "" || $('#Name').val() === null) {
        e.preventDefault();
        swal({
            title: "Please Fill Data!",
            text: "Fields cannot be only white space.",
            type: "error"
        });
    }
});

