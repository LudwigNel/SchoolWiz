$(function () {
    $('#countryTable').DataTable({
        "processing": true,
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true
    });

    $('#usersTable').DataTable({
        "processing": true,
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true, 
        "sScrollX": "100%",
        "sScrollXInner": "110%",
    });

    $('#citiesTable').DataTable({
        "processing": true,
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "sScrollX": "100%",
        "sScrollXInner": "110%",
    });
});

var siteJs = {
    getCountryProvincesCityCreate(url) {

        var ddlSource = "#cityCreateCountryList";
        $.getJSON(url,
            { countryId: $(ddlSource).val() },
            function(data) {
                var items = '';
                $("#cityCreateProvinceList").empty();
                items += "<option value='0'>-- Select a Province --</option>";
                $.each(data,
                    function(i, province) {
                        items += "<option value='" + province.value + "'>" + province.text + "</option>";
                    });
                $("#cityCreateProvinceList").html(items);
                $("#cityCreateProvinceList").selectpicker("refresh");
            });
    },

    getCountryProvincesCityEdit(url, selectedValue) {

        var ddlSource = "#cityEditCountryList";
        $.getJSON(url,
            { countryId: $(ddlSource).val() },
            function (data) {
                var items = '';
                items += "<option value='0'>-- Select a Province --</option>";
                $("#cityEditProvinceList").empty();
                $.each(data,
                    function (i, province) {
                        items += "<option value='" + province.value + "'>" + province.text + "</option>";
                    });
                $("#cityEditProvinceList").html(items);
                $("#cityEditProvinceList").selectpicker('val', selectedValue);
                $("#cityEditProvinceList").selectpicker("refresh");
            });
    }
}