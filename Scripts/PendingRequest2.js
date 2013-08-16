
    $(document).ready(function () {
        var EmpId = $("#EmpId").val();
        var RequestorId = $("#RequestorId").val();
        //Prepare jtable plugin
        $('#PendingRequestTable2').jtable({
            title: 'All Pending Request',
            sorting: true,
            pageSize: 20,
            paging: true,
            defaultSorting: 'Name ASC',
            selecting: false,
            multiselect: false,
            selectingCheckboxes: false,
            actions: {
                listAction: 'GetByRequestsState?state=pending'
            },
            fields: {
                Id: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                RemindersLink: {
                    title: ''
                },
                SubmitDate: {
                    title: 'Submited',
                    type: 'date',
                    displayFormat: 'mm/dd/yy'
                },
                RequestorsName: {
                    title: 'Name'
                },
                RequestType: {
                    title: 'type'
                },
                PendingApproverLevel: {
                    title: 'Pending On',
                    sort: 'false'
                },
                PendingApproverName: {
                    title: 'Approver',
                    sorting: false
                }
            }
        });

        //Load person list from server
        $('#PendingRequestTable2').jtable('load');

    });
