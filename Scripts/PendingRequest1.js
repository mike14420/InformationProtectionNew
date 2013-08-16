
    $(document).ready(function () {
        var EmpId = $("#EmpId").val();
        var RequestorId = $("#RequestorId").val();
        //Prepare jtable plugin
        $('#PendingRequestTable').jtable({
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
                FirstSupName: {
                    title: '1st'
                },
                FirstSupStatus: {
                    title: 'St',
                    sorting: false
                },
                SecondSupName: {
                    title: '2nd'
                },
                SecondSupStatus: {
                    title: 'St',
                    sorting: false
                },
                VpHrName: {
                    title: 'Vphr'
                },
                VpHrStatus: {
                    title: 'St',
                    sorting: false
                },
                RhCfoName: {
                    title: 'RhCfo'
                },
                RhCfoStatus: {
                    title: 'St',
                    sorting: false
                },
                IpdName: {
                    title: 'IPD'
                },
                IpdStatus: {
                    title: 'St',
                    sorting: false
                },
                CioName: {
                    title: 'CIO'
                },
                CioStatus: {
                    title: 'St',
                    sorting: false
                }
            }
        });

        //Load person list from server
        $('#PendingRequestTable').jtable('load');

    });

