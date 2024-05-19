let objBookingTable;

$(document).ready(function () {
    let status = 'pending';

    $('.btn-group-toggle a').click(function () {

        if (!$(this).hasClass('bg-success')) {

            $(this).toggleClass('bg-primary bg-success');

            $('.btn-group-toggle a').not(this).removeClass('bg-success').addClass('bg-primary');
        }
    });

    $('#pending, #approved, #checkin, #checkout, #cancelled').click(function (e) {
        e.preventDefault();
        status = $(this).attr("id").toLowerCase();

        LoadBookings(status);
    });

    LoadBookings(status);

    $('#tbl_Bookings').on('click', '.select-view-btn', function (e) {
        e.preventDefault();
        var rowData = GetRowData(objBookingTable, $(this));
        LoadModal('/Booking/BookingDetails', '#modal-booking-content', rowData);
    });

    $(document).on('click', '.btn-checkin', function (e) {
        e.preventDefault();
        CheckIn();
    });

    $(document).on('click', '.btn-checkout', function (e) {
        e.preventDefault();
        CheckOut();
    });

    $(document).on('click', '.btn-cancel', function (e) {
        e.preventDefault();
        CancelBooking();
    });

    $(document).on('click', '.btn-payment', function (e) {
        e.preventDefault();
        ProceedPayment();
    });

});

function LoadBookings(status) {

    $('#tbl_Bookings').DataTable().clear().destroy();
    objBookingTable = $('#tbl_Bookings').DataTable({
        ajax: {
            type: 'GET',
            url: '/Booking/GetAll',
            data: { status: status },
        },
        columns: [
            { data: 'bookingId', visible: false },
            { data: 'userName', width: '5%' },
            { data: 'userEmail', width: '5%' },
            { data: 'bookingStatus', width: '5%' },
            { data: 'checkinDate', width: '5%' },
            { data: 'checkoutDate', width: '5%' },
            { data: 'noOfStay', width: '5%' },
            {
                data: 'totalCost',
                width: '5%',
                render: function (data, type, row) {
                    return '$' + parseFloat(data).toFixed(2);
                }
            },
            {
                data: 'bookingId',
                width: '5%',
                'render': function (data, type, row) {
                    return '<button class="btn btn-primary btn-sm select-view-btn w-100">View Details</button>';
                }
            },
        ],
        fixedColumns: true,
        scrollY: true

    });

    PopulateModalFooter(status);
}

function PopulateModalFooter(status) {
    let role = $('#user_role').val().toLowerCase();

    let first_content = `<div class="modal-footer"> `;
    let end_content = `</div>`;

    let footerContent = "";

    if (status == 'pending' || status == 'approved') {
        footerContent =
            `<div class="col-auto">
                <button type="button" class="btn btn-danger btn-cancel" style="width:120px;">Cancel Booking</button>
            </div>`;
    }

    footerContent += `<div class="col"></div>`;

    footerContent += `<div class="col-auto">
                            <button  type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="width:100px;">Close</button>
                        </div>`;

    if (status == 'pending' && role == 'admin') {
        footerContent += `<div class="col-auto">
                                <button type="button" class="btn btn-primary btn-payment" style="width:100px;">Payment</button>
                         </div>`;
    }

    if (status == 'approved' && role == 'admin') {
        footerContent += `<div class="col-auto">
                                <button type="button" class="btn btn-primary btn-checkin" style="width:100px;">Check-In</button>
                          </div>`;

    }

    if (status == 'checkin' && role == 'admin') {
        footerContent += `<div class="col-auto">
                            <button type="button" class="btn btn-primary btn-checkout" style="width:100px;">Check-Out</button>
                        </div>`;
    }

    footerContent = first_content + footerContent + end_content;

    $('#footer-content').html('');
    $('#footer-content').empty();
    $('#footer-content').html(footerContent);

}

function CheckIn() {
    debugger

    let token = $('input[name="__RequestVerificationToken"]').val(); 

    let data = {
        BookingId: $('#BookingId').val(),
        RoomNo: $('#RoomNo').val(),
        __RequestVerificationToken: token
    };

    
  
    $.ajax({
        type: 'POST',
        url: '/Booking/CheckIn',
        data: data,
        success: function (response) {

            if (response.success) {              
                ReloadDataTable(objBookingTable);
                HideModal('#modal-booking');
                ShowToaster('success', 'CHECKIN', response.message);
            } else {
                ShowToaster('error', 'CHECKIN', response.message);
            }

        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'CHECKIN', error);
        }
    });   
   
}

function CheckOut() {

    let token = $('input[name="__RequestVerificationToken"]').val(); 
    let bookingId = $('#BookingId').val()    

    let data = {
        BookingId: bookingId,
        __RequestVerificationToken : token
    };

    $.ajax({
        type: 'POST',
        url: '/Booking/CheckOut',
        data: data,
        success: function (response) {
            if (response.success) {              
                ReloadDataTable(objBookingTable);               
                HideModal('#modal-booking');
                ShowToaster('success','CHECKOUT', response.message);
            } else {
                ShowToaster('error','CHECKOUT', response.message);
            }
        },
        error: function (xhr, status, error) {          
            ShowToaster('error', 'CHECKOUT', error);
        }
    });

    $(document).on('hidden.bs.modal', modalContentSelector.replace('-content', ''), function () {
        $(modalContentSelector).html('');
    });
}

function ProceedPayment() {    
    

    let token = $('input[name="__RequestVerificationToken"]').val(); 
    let bookingId = $('#BookingId').val();    

    let data = {
        BookingId: bookingId,
        __RequestVerificationToken : token
    };

    $.ajax({
        url: '/Booking/ShowPayment',
        method: 'POST',
        data: data,
        success: function (response) {
            
            if (response.success) {
                window.location.href = response.redirectUrl;
            }
            else {               
                ShowToaster('error', 'PAYMENT', response.message);
            }
        },
        error: function (xhr, status, error) {      
            ShowToaster('error', 'PAYMENT', error);
        }
    });
    
}


function CancelBooking() {

    let token = $('input[name="__RequestVerificationToken"]').val(); 
    let bookingId = $('#BookingId').val();    

    let data = {
        BookingId: bookingId,
        __RequestVerificationToken : token
    };

    $.ajax({
        type: 'POST',
        url: '/Booking/CancelBooking',
        data: data,

        success: function (response) {
            if (response.success) {           
                ReloadDataTable(objBookingTable);               
                HideModal('#modal-booking');
                ShowToaster('success', 'CANCEL BOOKING', response.message);
            } else {              
                ShowToaster('error', 'CANCEL BOOKING', response.message);
            }
        },
        error: function (xhr, status, error) {           
            ShowToaster('error', 'CANCEL BOOKING', error);
        }
    });

    $(document).on('hidden.bs.modal', modalContentSelector.replace('-content', ''), function () {
        $(modalContentSelector).html('');
    });
}


function ConfirmBooking(RoomId) {

    let serializedData = $('#checking_info').serialize();


    $.ajax({
        url: '/Booking/ConfirmBooking',
        method: 'POST',
        data: { Id: RoomId, jsonData: serializedData },
        success: function (response) {
            if (response.success) {

                let response_data = JSON.parse(response.booking);
                $('#modal-booking-' + RoomId).modal('hide');
                ShowPayment(response_data.BookingId);
            }
            else {
                ShowToaster('success', 'CONFIRM BOOKING', respons.message);
            }
        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'Something went wrong : ' + error);
        }
    });
}

function ShowPayment(bookingId) {


    let data = {
        BookingId: bookingId
    };

    $.ajax({
        url: '/Booking/ShowPayment',
        method: 'POST',
        data: data,
        success: function (response) {

            if (response.success) {

                window.location.href = response.redirectUrl;
            }
            else {
                ShowToaster('error', 'SHOW PAYMENT', response.message);
            }
        },
        error: function (xhr, status, error) {
            ShowToaster('error', 'SHOW PAYMENT', error);
        }
    });

}



function GetBooking(RoomId) {

    let serializedData = $('#checking_info').serialize();

    let data = {
        Id: RoomId,
        jsonData: serializedData
    };

    $.ajax({
        url: '/Booking/CreateBooking',
        method: 'GET',
        data: data,
        success: function (response) {

            if (response.success) {
                let modalContent = $('#modal-booking-content-' + RoomId);
                modalContent.empty();
                modalContent.html(response.htmlContent);
                $('#modal-booking-' + RoomId).modal('show');
            }
            else {
                ShowToaster('error', 'CHECK ROOM AVAILABITLIY', response.message);
            }

        },
        error: function (xhr, status, error) {
            ShowToaster('warning', 'BOOKING', error + ', Please login first to procced booking. Thank you.');
        }
    });
}



