//Not Submitted request
    $(document).ready(function () {
        var EmpId = $("#EmpId").val();
        var RequestorId = $("#RequestorId").val();
        $('#IpRequestTable').jtable({
            title: 'Information Protection Request (not submited)',
            sorting: false,
            selecting: true,
            multiselect: true,
            selectingCheckboxes: true,
            actions: {
                listAction: 'UsersView/GetRequestsNotSubmitted?EmpId=' + EmpId
            },
            fields: {
                Id: {
                    key: true,
                    create: false,
                    edit: false,
                    list: true,
                    title: 'Id'
                },
                RequestType: {
                    title: 'Request Type'
                },
                DeviceId: {
                    title: 'Device Id'
                },
                ApprovedStatus: {
                    title: 'Status'
                },
                SubmitDate: {
                    title: 'Submited',
                    type: 'date'
                },
                RequestDetailsLink: {
                    title: 'Details'
                }
            },
            //Register to selectionChanged event to hanlde events
            selectionChanged: function () {
                //Get all selected rows
                var $selectedRows = $('#IpRequestTable').jtable('selectedRows');

                $('#OurApprovalListId').val('');
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    var index = 0;
                    var selectList = '<select id="ApproveTheese" name="ApproveTheese">'
                    var selectList1 = 'Our Approvalls:'
                    $selectedRows.each(function () {
                        var record = $(this).data('record');
                        var x = String(index);
                        selectList1 += "Index=" + x + " RecordId=" + record.Id + ",";
                        index++;
                    });
                    $('#OurApprovalList').val(selectList1);
                } else {
                    //No rows selected
                    $('#SelectedRowList').append('No row selected! Select rows to see here...');
                }
            }
        });

        //Load person list from server
        $('#IpRequestTable').jtable('load');

    });
