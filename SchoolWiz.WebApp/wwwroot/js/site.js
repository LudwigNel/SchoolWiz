var controls = {
    leftArrow: '<i class="@(Settings.Theme.IconPrefix) fa-angle-left" style="font-size: 1.25rem"></i>',
    rightArrow: '<i class="@(Settings.Theme.IconPrefix) fa-angle-right" style="font-size: 1.25rem"></i>'
}

var siteJs = {
    showDeleteView: function(url, dialog, target, id) {
        url = url + id;

        $.get(url,
            function (response) {
                $(target).html(response);
                $(dialog).modal('show');
            });
    },

    initializeDataTable: function(table) {
        $(table).dataTable({
            responsive: true
        });
    }, 

    initializeDatePicker: function(datePicker) {
        $(datePicker).datepicker({
            todayBtn: "linked",
            clearBtn: true,
            todayHighlight: true,
            templates: controls
        });
    },

    getCountryProvincesCityCreate(url) {

        var ddlSource = "#cityCreateCountryList";
        $.getJSON(url,
            { countryId: $(ddlSource).val() },
            function (data) {
                var items = '';
                $("#cityCreateProvinceList").empty();
                items += "<option value='0'>-- Select a Province --</option>";
                $.each(data,
                    function (i, province) {
                        items += "<option value='" + province.value + "'>" + province.text + "</option>";
                    });
                $("#cityCreateProvinceList").html(items);
            });
    },

    getCountryProvincesCityEdit(url, selectedValue) {
        var ddlSource = "#cityEditCountryList";
        $.getJSON(url,
            { countryId: $(ddlSource).val() },
            function (data) {
                $.each(data,
                    function (i, province) {
                        var selected = false;
                        if (province.value === selectedValue) {
                            selected = true;
                        }
                        var option = new Option(province.text, province.value, false, selected);
                        $("#cityEditProvinceList").append(option);
                    });

                $("#cityEditProvinceList").val(selectedValue).trigger('change');

                var val = $("#cityEditProvinceList").val();
                console.log('Selected province id: ' + val);
            });
    }, 

    getCityDetails(url, selectedValue, provinceElement, countryElement) {
        $.getJSON(url,
            { cityId: $(selectedValue).val() },
            function (data) {
                $(provinceElement).val(data.province);
                $(countryElement).val(data.country);
            }, function(error) {
                toastr.error('Error', 'Something went wrong while getting the city details.');
            });
    }, 

    studentRegistrationStudentIdentityChanged() {
        var identityNumber = $('#studentRegistrationStudentIdentityNumber').val();
        if (identityNumber !== null && typeof identityNumber !== 'undefined' && identityNumber.length > 5) {
            var day = identityNumber.substring(4, 6);
            var month = identityNumber.substring(2, 4);
            var year = identityNumber.substring(0, 2);

            var dateOfBirth = new Date(year, month, day, 0, 0, 0, 0);
            $('#studentRegistrationStudentDateOfBirth').val(dateOfBirth.toLocaleDateString());
        }
    }, 

    copyAddress(unitNumberFrom, complexNameFrom, streetAddressFrom, suburbFrom, cityFrom, postalCodeFrom,
        unitNumberTo, complexNameTo, streetAddressTo, suburbTo, cityTo, postalCodeTo) {
        const unitNumber = $(unitNumberFrom).val();
        $(unitNumberTo).val(unitNumber);
        const complexName = $(complexNameFrom).val();
        $(complexNameTo).val(complexName);
        const streetAddress = $(streetAddressFrom).val();
        $(streetAddressTo).val(streetAddress);
        const suburb = $(suburbFrom).val();
        $(suburbTo).val(suburb);
        const city = $(cityFrom).val();
        $(cityTo).val(city).trigger('change');;
        const postalCode = $(postalCodeFrom).val();
        $(postalCodeTo).val(postalCode);
    }, 

    completeInvoiceRun(period) {
        $.post('Invoice/CompleteInvoiceRun', { period: period });
    }
}