﻿let objRoomDatable;
$(document).ready(function () {
    loadRoomDataTable();
    
    $('.btn-add').click(function () {
        LoadModal('/Room/Create', '#modal-add-content');
        InputBoxFocus('#RoomName', '#modal-add');

    });

    $('.btn-save').click(function () {        
        SaveRoom('/Room/Create', '#form-add');
    });

    $('#tbl_Rooms').on('click', '.select-edit-btn', function () {       
        let rowData = GetRowData(objRoomDatable, $(this));              
        LoadModal('/Room/Update', '#modal-edit-content', rowData);
        InputBoxFocus('#RoomName', '#modal-edit');
    });

    $('.btn-edit').click(function () {
        SaveRoom('/Room/Update', '#form-edit');
    });

    $('#tbl_Rooms').on('click', '.select-delete-btn', function () {
        let rowData = GetRowData(objRoomDatable, $(this));
        $('#RoomId').val(rowData.roomId);
        $('#modal-delete').modal('show');
    });

    $('.btn-delete').click(function () {
        DeleteRoom();
    });


});

function loadRoomDataTable() {
    objRoomDatable = $('#tbl_Rooms').DataTable({
        ajax: {
            url: '/Room/GetAll'
        },
        columns: [
            { data: 'roomId', visible: false },
            { data: 'roomName', width: '100px' },
            {

                data: 'description',
                width: '400px',
                render: function (data, type, row) {
                    return '<span title="' + data + '" >' + (data.length > 80 ? data.substr(0, 80 - 3) + '....' : data) + '</span>';
                }


            },
            { data: 'roomPrice', className: 'text-center', width: '100px' },
            { data: 'maxOccupancy', className: 'text-center', width: '100px' },
            { data: 'imageUrl', visible: false },
            {
                data: 'roomId',
                width: '100px',
                render: function (data, type, row) {
                    return '<button class="btn btn-primary btn-sm select-edit-btn w-100">Edit</button>';
                }
            },
            {
                data: 'roomId',
                width: '100px',
                render: function (data, type, row) {
                    return '<button class="btn btn-danger btn-sm select-delete-btn w-100">Delete</button>';
                }
            }
        ],
        fixedColumns: true,
        scrollY: true
    });
}


function SaveRoom(url, formSelector) {

    let data = new FormData($(formSelector)[0]);

    let is_true = false;

    is_true = IsFieldValid(formSelector);

    if (!is_true) {
        return;
    }

    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        contentType: false, // Ensure proper content type for file upload
        processData: false, // Prevent jQuery from automatically processing the data
        success: function (response) {

            if (response.success) {
                ReloadDataTable(objRoomDatable);
                HideModal(formSelector.replace('form', 'modal'));
                ShowToaster('success', 'Room', response.message);
            } else {
                ShowToaster('error', 'Room', response.message);
            }

        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'Room', error);
        }
    });

}

function DeleteRoom() {


    let token = $('input[name="__RequestVerificationToken"]').val();
    let roomId = $('#RoomId').val();

    let data = {
        RoomId: roomId,
        __RequestVerificationToken: token
    };

    $.ajax({
        type: 'POST',
        url: '/Room/Delete',
        data: data,
        success: function (response) {
            ReloadDataTable(objRoomDatable);
            HideModal('#modal-delete');
            ShowToaster('success', 'Room', response.message);
        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'Room', error);
        }
    });
}