﻿//Not Submitted request
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
            RequestDetailsLink: {
                title: ''
            },
            RequestType: {
                title: 'Request Type'
            },
            Identifier: {
                title: '(id,devId)',
                list: false
            },
            ApprovedStatus: {
                title: 'Status'
            },
            SubmitDate: {
                title: 'Submitted',
                type: 'date',
                displayFormat: 'mm/dd/yy'
            }
        }
    });

    //Load person list from server
    $('#allRequestTbl').jtable('load');

});
