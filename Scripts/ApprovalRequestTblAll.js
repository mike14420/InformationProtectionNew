//Not Submitted request
$(document).ready(function () {
    var EmpId = $("#EmpId").val();
    var RequestorId = $("#RequestorId").val();
    $('#allRequestTbl').jtable({
        title: 'Information Protection Request (All Request)',
        actions: {
            listAction: 'UsersView/GetRequestsAll?EmpId=' + EmpId
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
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
        }
    });

    //Load person list from server
    $('#allRequestTbl').jtable('load');

});
