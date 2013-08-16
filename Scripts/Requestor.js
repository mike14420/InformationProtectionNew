


$(document).ready(function () {




    $('#showEmployeesDropDwn').hide();

    $('input:radio[name="RadBtnSelfOrEmployee"]').change(
        function () {


            // checks that the clicked radio button is the one of value 'Yes'
            // the value of the element is the one that's checked (as noted by @shef in comments)
            if ($(this).val() == 'Other') {
                $('#showEmployeesDropDwn').show();

            }
            else {
                var hiddenName = $('#FullNameHiddenId').val();
                GetUser(hiddenName);
                $('#showEmployeesDropDwn').hide();

            }
        });


    $('#AllUsersID').change(function () {


    });

    //$('input:radio[name="AllUsers"]').change(
    //    function () {


    //        // checks that the clicked radio button is the one of value 'Yes'
    //        // the value of the element is the one that's checked (as noted by @shef in comments)
    //        if ($(this).val() == 'Other') {
    //            $('#showEmployeesDropDwn').show();

    //        }
    //        else {

    //            $('#showEmployeesDropDwn').hide();
    //        }
    //    });


    $('#AllUsersID').change(function () {
        var myId = $("#AllUsersID").val();
        var myTxt = $("#AllUsersID option:selected").text();
        //var myEmpId = $("#AllUsersID option:selected").val();
        GetUser(myTxt, myId);

    });

    function GetUser(myTxt, myId) {
        if (myTxt && myId) {
            var myUrl = "../Requestor/GetData";
            // take the data and post it via json
            $.ajax({
                url: myUrl,
                type: "POST",
                datatype: "json",
                contentType: "application/json; charset=utf-8;",
                data: JSON.stringify({ name: myTxt, EgmId: myId }),
                success: function (result) {
                    var dn = result.DisplayName;
                    var f = result.FirstName;
                    var l = result.LastName;
                    var m = result.MiddleName;
                    var d = result.DepartmentID;
                    var e = result.Email;
                    var empId = result.Emp_id;
                    var p = result.PhoneNumber;
                    var j = result.JobTitle;
                    var deptN = result.DepartmentName;
                    var fn = result.FullName;

                    $('#FullName').val(dn);

                    $('#FnameDispId').val(f);
                    $('#FnameId').val(f);

                    $('#EmpID').val(empId);
                    $('#EmpID1').val(empId);

                    $('#LnameDispId').val(l);
                    $('#Lname').val(l);

                    $('#Mname').val(m);
                    $('#MnameDispId').val(m);

                    $('#Email').val(e);
                    $('#EmailDispId').val(e);

                    $('#JobTitle').val(j);
                    $('#JobTitleDisp').val(j);

                    $('#DeptNameDispId').val(d);
                    $('#DeptName').val(d);


                    $('#DeptIdDispId').val(deptN);
                    $('#DeptId').val(deptN);

                    $('#FullNameDispId').val(fn);
                    $('#FullName').val(fn);
                },
                error: function (data) {
                    alert("ERROR");
                }

            });
        }

    }


});