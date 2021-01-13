
function openNav() {
    document.getElementById("mySidebar").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    document.getElementById("display").style.display = "block";
}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById("display").style.display = "none";
}
function openNavType() {
    document.getElementById("side-drawer").style.width = "400px";


}
showInPopup = (url, title) => {

    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            document.getElementById("side-drawer").style.width = "auto";
            $('#side-drawer .card-body').html(res);
            $('#side-drawer .card-title').html(title);
            $.ajax({
                url: '/MarchentProfileDetail/GetBankingType',
                type: 'GET',
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    $("#Type").empty();
                    $.each(data, function (i, obj) {
                        var s = '<option value="' + obj.id + '">' + obj.bankingMethodName + '</option>';
                        console.log(s);
                        $("#Type").append(s);
                        $("#Type").change(function () {
                            if ($(this).val() == "c85a689b-e845-492a-8a00-c360ac19b63e") {
                                $('#hideDiv').show();
                                $('#Route').attr('required', '');
                                $('#Route').attr('data-error', 'This field is required.');
                                $('#Branch').attr('required', '');
                                $('#Branch').attr('data-error', 'This field is required.');
                            } else {
                                $('#hideDiv').hide();
                                $('#Route').removeAttr('required');
                                $('#Route').removeAttr('data-error');
                                $('#Branch').removeAttr('required');
                                $('#Branch').removeAttr('data-error');
                            }
                        });
                        $("#Type").trigger("change");
                    });

                },
                error: function (res) {
                    console.log(res);
                }
            });

            console.log(res, "check");
        }

    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#side-drawer .card-body').html('');
                    $('#side-drawer .card-title').html('');
                    document.getElementById("side-drawer").style.width = "0";
                }
                else
                    $('#side-drawer .card-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
function closeNavType() {
    document.getElementById("side-drawer").style.width = "0";
    document.getElementById("tablebody").style.marginLeft = "0";
    $('#side-drawer .card-body').html('');
    $('#side-drawer .card-title').html('');

}
//delete
Delete = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
         
            if (res.isValid) {
                $('#view-all').html(res.html)
                toastr.success("Delete Success");
            } else {
                toastr.warning("Delete wrang");
            }
        },
        error: function (res) {
            console.log(res);
        }
    });

    console.log(res, "check");
}



    //Add Marchet Charge
    function selectAll() {
        $("#AreaList tr").each(function (row, tr) {
            $(this).closest('tr').find('.AreaId1').prop('checked', true);
        });
    }
    function unSelectAll() {
        $("#AreaList tr").each(function (row, tr) {
            var isSelected = $(this).closest('tr').find('.AreaId1').is(":checked", true);
            if (isSelected) {
                $(this).closest('tr').find('.AreaId1').prop('checked', false);
            }

        });
    }
    //Add MArchcent Charge
    function addButton() {
        var arrItem = new Array();

        $("#AreaList tr").each(function (row, tr) {
            var isSelected = $(this).closest('tr').find('.AreaId1').is(":checked", true);
            if (isSelected) {
                arrItem.push({
                    "Id": $(this).closest('tr').find('#AreaId').val(),
                    "MarchentId": $(this).closest('tr').find('#marchentId').val(),
                    "Area": $(this).closest('tr').find('#areaName').val(),
                    "BaseChargeAmount": $(this).closest('tr').find('#base').val(),
                    "IncreaseChargePerKg": $(this).closest('tr').find('#inc').val(),
                });
            }

        })
        if (arrItem.length != 0) {
            console.log("piku", arrItem);
            $.ajax({
                url: "/MarchentCharge/AddCharge1",
                type: 'POST',
                data: { "postArrItem": arrItem },
                success: function (response) {

                    toastr.success("Successfully Charge Add");
                    document.getElementById("side-drawer").style.width = "0";
                }
            })
            console.log("prianka", JSON.stringify(arrItem));

        }
    }
    //Edit Marchent
    function editButton() {
        var arrItem = new Array();

        $("#AreaList tr").each(function (row, tr) {
            var isSelected = $(this).closest('tr').find('.AreaId1').is(":checked", true);
            if (isSelected) {
                arrItem.push({
                    "Id": $(this).closest('tr').find('#AreaId').val(),
                    "MarchentId": $(this).closest('tr').find('#marchentId').val(),
                    "Area": $(this).closest('tr').find('#areaName').val(),
                    "BaseChargeAmount": $(this).closest('tr').find('#base').val(),
                    "IncreaseChargePerKg": $(this).closest('tr').find('#inc').val(),
                });
            }

        })
        if (arrItem.length != 0) {
            console.log("piku", arrItem);
            $.ajax({
                url: "/MarchentCharge/PostEditMArchentCharge",
                type: 'POST',
                data: { "postArrItem": arrItem },
                success: function (response) {

                    toastr.success("Successfully Charge Update");
                    document.getElementById("side-drawer").style.width = "0";
                }
            })
            console.log("prianka", JSON.stringify(arrItem));

        }
    }
    //Add Marchent

    function bankList() {
        var d = $("#Type option:selected").val();

        console.log(d);
        $.ajax({
            url: '/MarchentProfileDetail/GetOrganizationName/?id=' + d,
            type: 'GET',
            dataType: 'JSON',
            success: function (data) {
                console.log(data);
                $("#Organize").empty();
                $("#Branch").empty();
                $.each(data, function (i, obj) {
                    var s = '<option value="' + obj.id + '">' + obj.organizationName + '</option>';
                    //console.log(s);
                    $("#Organize").append(s);
                });
            },
            error: function (res) {
                console.log(res);
            }
        });
    }
    function branchList() {
        $("#Organize").click(function () {
            var d = $("#Organize option:selected").val();
            console.log(d);
            $.ajax({
                url: '/MarchentProfileDetail/GetBranch/?id=' + d,
                type: 'GET',
                dataType: 'JSON',
                success: function (data) {
                    console.log(data, "prianka");
                    $("#Branch").empty();
                    //$("#rout").empty();
                    $.each(data, function (i, obj) {
                        console.log(obj, "mondal");
                        var s = '<option value="' + obj.id + '">' + obj.branchName + '</option>';
                        console.log(s, "hdfjhs");
                        $("#Branch").append(s);
                    });
                },
                error: function (res) {
                    console.log(res);
                }
            });
        });
    }
    var loadImg = function loadImg(event) {

        var output = document.getElementById('img');
        output.src = URL.createObjectURL(event.target.files[0]);
        output.onload = function () {
            URL.revokeObjectURL(output.src) // free memory
        }
    };
    var loadLogo = function loadLogo(event) {
        var output = document.getElementById('logo');
        output.src = URL.createObjectURL(event.target.files[0]);
        output.onload = function () {
            URL.revokeObjectURL(output.src) // free memory
        }
    };

    Approve = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                //var approve = '@ViewBag.approve';
                //if (approve != null) {
                //    toastr.success(approve);
                //}
                toastr.success("Approve Success");
            },
            error: function (res) {
                console.log(res);
            }
        });

        console.log(res, "check");
    }

