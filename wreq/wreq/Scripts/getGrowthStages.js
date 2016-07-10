$(document).ready(function () {
    $('#ddlCulture').change(function () {
        $.ajax({
            url: '/Crops/GetGrowthStages',
            dataType: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            cache: false,
            data: { cultureId: $('#ddlCulture').val() },
            success: function (data) {
                $('#txtLengthIni').val(data.Lini);
                $('#txtLengthDev').val(data.Ldev);
                $('#txtLengthMid').val(data.Lmid);
                $('#txtLengthLate').val(data.Llate);

            },
            error: function (xhr) {
            }
        });
    });
});